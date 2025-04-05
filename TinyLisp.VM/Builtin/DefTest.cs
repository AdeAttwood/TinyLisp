using TinyLisp.VM.Primitives;

namespace TinyLisp.VM.Builtin;

public class DefTest : Defun
{
    public override string Display()
    {
        return "deftest";
    }

    protected override UserFunctionValue CreateUserFunction(string name)
    {
        return new TestFunctionValue { Name = name };
    }
}
