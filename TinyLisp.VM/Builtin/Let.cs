using TinyLisp.VM.Primitives;

namespace TinyLisp.VM.Builtin;

public class Let : BaseValue
{
    public override string Display()
    {
        return "let";
    }

    public override BaseValue EvaluateWithValue(VM vm, BaseValue value)
    {
        var listValue = value as ListValue;
        if (listValue == null)
        {
            throw new LispException("let has been called with a value that is not a list");
        }

        var bindings = listValue.GetValueAt(1) as ListValue;
        if (bindings == null)
        {
            throw new LispException("let bindings must be a list");
        }

        vm.PushScope();

        for (var i = 0; i < bindings.ItemCount(); i++)
        {
            var binding = bindings.GetValueAt(i) as ListValue;
            if (binding == null)
            {
                throw new LispException($"let binding arguments must be a list found {bindings.GetValueAt(i)}");
            }

            var left = binding.GetValueAt(0);
            var right = binding.GetValueAt(1);

            if (left.GetType() != typeof(SymbolValue))
            {
                throw new LispException("let binding left hand side must be a symbol");
            }

            vm.Define(left.Display(), vm.Evaluate(right));
        }

        BaseValue returnValue = new NullValue();
        for (var i = 2; i < listValue.ItemCount(); i++)
        {
            returnValue = vm.Evaluate(listValue.GetValueAt(i));
        }

        vm.PopScope();

        return returnValue;
    }
}