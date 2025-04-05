namespace TinyLisp.VM.Primitives;

public class StringValue : BaseValue
{
    public required string Value { get; set; }

    public override string Display()
    {
        return $"\"{this.Value}\"";
    }

    public override bool Equals(object? obj)
    {
        var other = obj as StringValue;

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
