namespace TinyLisp.Cli;

public class CommandRun : CommandBase
{
    public void Run(string entryPoint)
    {
        var fileContent = File.ReadAllText(entryPoint);
        this.BuildVm().Evaluate(fileContent);
    }
}