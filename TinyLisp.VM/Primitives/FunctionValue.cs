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
            var argumentValue = vm.Evaluate(listValue.GetValueAt(i + 1));
            var param = requiredParams[i];

            if (!param.ParameterType.IsInstanceOfType(argumentValue))
            {
                throw new Exception($"Argument type mismatch for parameter '{param.Name}' in function '{this.Name}' expected '{param.ParameterType}' got '{argumentValue}'");
            }

            convertedArgs.Add(argumentValue);
        }

        return (BaseValue?)this.Callable.Invoke(null, convertedArgs.ToArray()) ?? new NullValue();
    }
}