using TinyLisp.VM.Primitives;

namespace TinyLisp.VM.Builtin;

public static class FlowControl
{
    [LispFunction("if")]
    public static BaseValue If(VM vm, BaseValue condition, BaseValue trueValue, BaseValue falseValue)
    {
        var value = Equals(new BoolValue { Value = true }, vm.Evaluate(condition)) ? trueValue : falseValue;

        return vm.Evaluate(value);
    }

    [LispFunction("=")]
    public static BoolValue IsEqual(VM vm, BaseValue left, BaseValue right)
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
