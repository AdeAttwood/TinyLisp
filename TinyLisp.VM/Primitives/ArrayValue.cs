namespace TinyLisp.VM.Primitives;

public class ArrayValue : BaseValue
{
    public List<BaseValue> Values { get; set; } = new List<BaseValue>();

    public override string Display()
    {
        return "todo i am a array";
    }
}