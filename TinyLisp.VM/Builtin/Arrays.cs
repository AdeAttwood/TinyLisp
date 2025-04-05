using TinyLisp.VM.Primitives;

namespace TinyLisp.VM.Builtin;

public static class Arrays
{
    [LispFunction("get")]
    public static BaseValue Get(ArrayValue value, NumberValue index)
    {
        return value.Values.ElementAtOrDefault(index.Value) ?? new NullValue();
    }

    [LispFunction("map")]
    public static ArrayValue Map(VM vm, ArrayValue value, UserFunctionValue predicate)
    {
        var newArray = new ArrayValue();
        foreach (var item in value.Values)
        {
            newArray.Values.Add(predicate.EvaluateWithValue(vm, new List<BaseValue> { item }));
        }

        return newArray;
    }

    [LispFunction("count")]
    public static NumberValue Count(ArrayValue value)
    {
        return new NumberValue { Value = value.Values.Count };
    }
}
