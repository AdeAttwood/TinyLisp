using TinyLisp.VM.Primitives;

namespace TinyLisp.VM.Builtin;

public static class Global
{
    [LispFunction("print")]
    public static NullValue Print(BaseValue item)
    {
        Console.WriteLine(item.Display());

        return new NullValue();
    }

    [LispFunction("assert")]
    public static NullValue Assert(BoolValue item, StringValue? description)
    {
        if (!item.Value)
        {
            throw new LispException(description != null ? description.Value : "Assertion failed", item.Location);
        }

        return new NullValue();
    }

    [LispFunction("assert-eq")]
    public static NullValue AssertEqual(VM vm, BaseValue left, BaseValue right)
    {
        if (!FlowControl.IsEqual(vm, left, right).Value)
        {
            throw new LispException($"Failed asserting that {left.Display()} is equal to {right.Display()}", left.Location);
        }

        return new NullValue();
    }

    [LispFunction("defvar")]
    public static NullValue DefVar(VM vm, SymbolValue variable, BaseValue value)
    {
        vm.Define(variable.Value, vm.Evaluate(value));
        return new NullValue();
    }

    [LispFunction("let")]
    public static BaseValue Let(VM vm, ListValue listValue)
    {
        var bindings = listValue.GetValueAt<ListValue>(1);

        vm.PushScope();

        for (var i = 0; i < bindings.ItemCount(); i++)
        {
            var binding = bindings.GetValueAt<ListValue>(i);
            var left = binding.GetValueAt<SymbolValue>(0);
            var right = binding.GetValueAt(1);

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
