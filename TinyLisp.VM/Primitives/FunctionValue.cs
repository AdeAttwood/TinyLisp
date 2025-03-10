using System.Reflection;

namespace TinyLisp.VM.Primitives;

public class FunctionValue : BaseValue
{
    public required string Name { get; set; }

    public required MethodInfo Callable { get; set; }

    public override string Display()
    {
        return this.Name;
    }

    public override BaseValue EvaluateWithValue(VM vm, BaseValue value)
    {
        var listValue = (ListValue)value;
        if (listValue == null)
        {
            throw new LispException($"Function {this.Name} was called with a non list value type {value.GetType()}");
        }

        var requiredParams = this.Callable
            .GetParameters()
            .Where(param => param.ParameterType != typeof(VM))
            .ToArray();

        if (requiredParams.Length != listValue.ItemCount() - 1)
        {
            throw new Exception($"Function '{this.Name}' expects {requiredParams.Length} arguments, but {listValue.ItemCount() - 1} were provided.");
        }

        var convertedArgs = new List<object>();
        if (this.Callable.GetParameters().First()?.ParameterType == typeof(VM))
        {
            convertedArgs.Add(vm);
        }

        for (int i = 0; i < requiredParams.Length; i++)
        {
            var argumentValue = listValue.GetValueAt(i + 1);
            var param = requiredParams[i];

            // If the argument types don't match, try and evaluate it to see if
            // they do after its evaluated. In some cases we can be specific
            // and say we want a `BoolValue` in this case we want the list to
            // be evaluated so it becomes one. In others we can prevent the by
            // using a `BaseValue` this will prevent the list from getting
            // evaluated, this is useful for things like if else when we don't
            // want the else being evaluated if the condition is true.
            if (!param.ParameterType.IsInstanceOfType(argumentValue))
            {
                argumentValue = vm.Evaluate(argumentValue);
            }

            // Now throw an error if the types don't match.
            if (!param.ParameterType.IsInstanceOfType(argumentValue))
            {
                throw new Exception($"Argument type mismatch for parameter '{param.Name}' in function '{this.Name}' expected '{param.ParameterType}' got '{argumentValue}'");
            }

            convertedArgs.Add(argumentValue);
        }

        try
        {
            return (BaseValue?)this.Callable.Invoke(null, convertedArgs.ToArray()) ?? new NullValue();
        }
        catch (TargetInvocationException ex) when (ex.InnerException is not null)
        {
            throw ex.InnerException;
        }
    }
}