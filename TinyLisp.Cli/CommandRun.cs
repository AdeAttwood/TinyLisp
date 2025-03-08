namespace TinyLisp.Cli;

public class CommandRun
{
    private VM.VM _vm;

    public CommandRun(VM.VM vm)
    {
        _vm = vm;
    }

    public void Run(string entryPoint)
    {
        var fileContent = File.ReadAllText(entryPoint);
        _vm.Evaluate(fileContent);
    }
}