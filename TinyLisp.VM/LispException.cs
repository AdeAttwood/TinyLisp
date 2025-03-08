namespace TinyLisp.VM;

public class LispException : Exception
{
    public LispException(string message) : base(message) { }
}