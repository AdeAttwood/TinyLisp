namespace TinyLisp.VM.Primitives;

public class BoolValue : BaseValue
{
    public required bool Value { get; set; }

    public override string Display()
    {
        return this.Value ? "true" : "false";
    }

    public static BoolValue Parse(string value)
    {
        if (value == "true")
        {
            return new BoolValue { Value = true };
        }

        if (value == "false")
        {
            return new BoolValue { Value = false };
        }

        throw new LispException($"Unable to parse '{value}' in to a boolean");
    }

    public override bool Equals(object? obj)
    {
        var other = obj as BoolValue;

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