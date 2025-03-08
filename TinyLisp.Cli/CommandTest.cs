namespace TinyLisp.Cli;

public class CommandTest
{
    private VM.VM _vm;

    public CommandTest(VM.VM vm)
    {
        _vm = vm;
    }

    public void Run(string directory)
    {
        Console.WriteLine("directory");
    }
}