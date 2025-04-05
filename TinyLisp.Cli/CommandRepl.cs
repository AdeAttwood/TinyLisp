namespace TinyLisp.Cli;

public class CommandRepl : CommandBase
{
    public void Run()
    {
        var vm = this.BuildVm();

        while (true)
        {
            Console.Write("TL-USER > ");
            var command = Console.ReadLine() ?? "(exit)";
            if (command == "(exit)")
            {
                return;
            }

            try
            {
                var result = vm.Evaluate(command);
                Console.WriteLine(result.Display());
            }
            catch (Exception c)
            {
                Console.WriteLine(c.Message);
            }
        }
    }
}
