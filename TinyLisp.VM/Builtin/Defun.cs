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
            throw new LispException("defun has been called with a value that is not a list");
        }

        var functionName = listValue.GetValueAt(1) as SymbolValue;
        if (functionName == null)
        {
            throw new LispException("defun function names must be a symbol");
        }

        var function = new UserFunction { Name = functionName.Value };

        var parameters = listValue.GetValueAt(2) as ListValue;
        if (parameters == null)
        {
            throw new LispException("defun was called with no parameters");
        }

        for (var i = 0; i < parameters.ItemCount(); i++)
        {
            var parameter = parameters.GetValueAt(i) as SymbolValue;
            if (parameter == null)
            {
                throw new LispException("defun all parameters must be symbol");
            }

            function.Parameters.Add(parameter);
        }


        for (var i = 3; i < listValue.ItemCount(); i++)
        {
            function.Body.Add(listValue.GetValueAt(i));
        }

        vm.Define(function.Name, function);

        return new NullValue();
    }
}