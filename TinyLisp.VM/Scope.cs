namespace TinyLisp.VM;

public class Scope
{
    /// <summary>
    /// A map of values in the current scope. This is keyed by the fully
    /// qualified name of the value.
    /// </summary>
    public Dictionary<string, BaseValue> Values { get; set; } = new Dictionary<string, BaseValue>();

    /// <summary>
    /// Tests to see is the current scope has a value
    /// </summary>
    public bool HasValue(string fqn)
    {
        return this.Values.ContainsKey(fqn);
    }
}
