namespace TinyLisp.VM.Primitives;

public class NullValue : BaseValue
{
    public override string Display()
    {
        return "null";
    }
}
