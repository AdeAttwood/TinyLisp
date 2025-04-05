namespace TinyLisp.VM.Primitives;

public class NumberValue : BaseValue
{
    public required int Value { get; set; }

    public override string Display()
    {
        return this.Value.ToString();
    }

    public static NumberValue Parse(string value)
    {
        return new NumberValue { Value = Int32.Parse(value) };
    }

    public override bool Equals(object? obj)
    {
        var other = obj as NumberValue;

        if (other == null)
        {
            return false;
        }

        return this.Value.Equals(other.Value);
    }

    public override int GetHashCode()
    {
        return this.Value.GetHashCode();
    }
}
