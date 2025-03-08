namespace TinyLisp.VM.Primitives;

public class SymbolValue : BaseValue
{
    public required string Value { get; set; }

    public override string Display()
    {
        return this.Value;
    }
}