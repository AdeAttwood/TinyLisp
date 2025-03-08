namespace TinyLisp.VM;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class LispFunctionAttribute : Attribute
{
    public string Name { get; }

    public LispFunctionAttribute(string name)
    {
        Name = name;
    }
}