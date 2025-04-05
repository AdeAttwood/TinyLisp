using TinyLisp.VM.Primitives;

namespace TinyLisp.VM.Builtin;

public class Defun : BaseValue
{
    public override string Display()
    {
        return "defun";
    }

    public override BaseValue EvaluateWithValue(VM vm, BaseValue value)
    {
        var listValue = value as ListValue;
        if (listValue == null)
        {
            throw new LispException("defun has been called with a value that is not a list", value.Location);
        }

        var functionName = listValue.GetValueAt<SymbolValue>(1);
        var function = this.CreateUserFunction(functionName.Value);

        var parameters = listValue.GetValueAt<ListValue>(2);
        for (var i = 0; i < parameters.ItemCount(); i++)
        {
            var parameter = parameters.GetValueAt<SymbolValue>(i);
            function.Parameters.Add(parameter);
        }


        for (var i = 3; i < listValue.ItemCount(); i++)
        {
            function.Body.Add(listValue.GetValueAt(i));
        }

        vm.Define(function.Name, function);

        return new NullValue();
    }

    protected virtual UserFunctionValue CreateUserFunction(string name)
    {
        return new UserFunctionValue { Name = name };
    }
}
