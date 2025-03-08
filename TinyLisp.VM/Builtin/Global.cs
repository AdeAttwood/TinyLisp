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

    [LispFunction("=")]
    public static BoolValue Equals(VM vm, BaseValue left, BaseValue right)
    {
        return new BoolValue { Value = vm.Evaluate(left).Equals(vm.Evaluate(right)) };
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