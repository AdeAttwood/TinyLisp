using TinyLisp.VM.Primitives;

namespace TinyLisp.Cli;

public class CommandTest : CommandBase
{
    private TestSummary Summary { get; set; } = new TestSummary();

    public void Run(string directory)
    {
        var fileEntries = Directory.GetFiles(directory);
        foreach (string fileName in fileEntries)
        {
            if (fileName.EndsWith(".test.lisp"))
            {
                this.TestFile(fileName);
            }
        }

        foreach (var failure in this.Summary.Failures)
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(" FAIL ");
            Console.ResetColor();
            Console.WriteLine($" {failure.TestName}");
            Console.WriteLine();

            Console.WriteLine(failure.Error);
        }

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine($"Passes:   {this.Summary.PassCount}");
        Console.WriteLine($"Failures: {this.Summary.FailCount}");

        var exitCode = this.Summary.FailCount > 0 ? 1 : 0;
        Environment.Exit(exitCode);
    }

    private void TestFile(string fileName)
    {
        var vm = this.BuildVm();

        var fileContent = File.ReadAllText(fileName);
        vm.Evaluate(fileContent, fileName);

        foreach (var test in vm.GetTestFunctions())
        {
            vm.PushScope();
            var result = this.TestFunction(vm, test, fileContent);
            vm.PopScope();

            if (result != null)
            {
                this.Summary.AddFailure(result);
                Console.Write("x");
            }
            else
            {
                this.Summary.AddPass();
                Console.Write(".");
            }
        }
    }

    private TestFailure? TestFunction(VM.VM vm, TestFunctionValue function, string fileContent)
    {
        foreach (var item in function.Body)
        {
            try
            {
                vm.Evaluate(item);
            }
            catch (VM.LispException e)
            {
                return new TestFailure
                {
                    TestName = function.Name,
                    Error = e.FullMessage(fileContent),
                };
            }
        }

        return null;
    }
}