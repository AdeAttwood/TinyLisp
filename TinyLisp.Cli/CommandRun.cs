using TinyLisp.VM;

namespace TinyLisp.Cli;

public class CommandRun : CommandBase
{
    public void Run(string entryPoint)
    {
        var fileContent = File.ReadAllText(entryPoint);
        try
        {
            this.BuildVm().Evaluate(fileContent, entryPoint);
        }
        catch (LispException e)
        {
            Console.WriteLine(e.FullMessage(fileContent));
        }
    }
}