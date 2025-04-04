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
            throw new LispException(description != null ? description.Value : "Assertion failed");
        }

        return new NullValue();
    }

    [LispFunction("assert-eq")]
    public static NullValue AssertEqual(VM vm, BaseValue left, BaseValue right)
    {
        if (!Equals(vm, left, right).Value)
        {
            throw new LispException($"Failed asserting that {left.Display()} is equal to {right.Display()}");
        }

        return new NullValue();
    }

    [LispFunction("get")]
    public static BaseValue Get(ArrayValue value, NumberValue index)
    {
        return value.Values.ElementAtOrDefault(index.Value) ?? new NullValue();
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

    [LispFunction("if")]
    public static BaseValue If(VM vm, BaseValue condition, BaseValue trueValue, BaseValue falseValue)
    {
        var value = Equals(new BoolValue { Value = true }, vm.Evaluate(condition)) ? trueValue : falseValue;

        return vm.Evaluate(value);
    }

    [LispFunction("=")]
    public static BoolValue Equals(VM vm, BaseValue left, BaseValue right)
    {
        return new BoolValue { Value = vm.Evaluate(left).Equals(vm.Evaluate(right)) };
    }

    [LispFunction("!=")]
    public static BoolValue NotEquals(VM vm, BaseValue left, BaseValue right)
    {
        return new BoolValue { Value = !vm.Evaluate(left).Equals(vm.Evaluate(right)) };
    }

    [LispFunction(">")]
    public static BoolValue GraterThan(VM vm, NumberValue left, NumberValue right)
    {
        return new BoolValue { Value = left.Value > right.Value };
    }

    [LispFunction(">=")]
    public static BoolValue GraterThanOrEqualTo(VM vm, NumberValue left, NumberValue right)
    {
        return new BoolValue { Value = left.Value >= right.Value };
    }

    [LispFunction("<")]
    public static BoolValue LessThan(VM vm, NumberValue left, NumberValue right)
    {
        return new BoolValue { Value = left.Value < right.Value };
    }

    [LispFunction("<=")]
    public static BoolValue LessThanOrEqualTo(VM vm, NumberValue left, NumberValue right)
    {
        return new BoolValue { Value = left.Value <= right.Value };
    }

    [LispFunction("+")]
    public static NumberValue Add(NumberValue left, NumberValue right)
    {
        return new NumberValue { Value = left.Value + right.Value };
    }

    [LispFunction("-")]
    public static NumberValue Minus(NumberValue left, NumberValue right)
    {
        return new NumberValue { Value = left.Value - right.Value };
    }

    [LispFunction("*")]
    public static NumberValue Times(NumberValue left, NumberValue right)
    {
        return new NumberValue { Value = left.Value * right.Value };
    }

    [LispFunction("/")]
    public static NumberValue Divide(NumberValue left, NumberValue right)
    {
        return new NumberValue { Value = left.Value / right.Value };
    }
}