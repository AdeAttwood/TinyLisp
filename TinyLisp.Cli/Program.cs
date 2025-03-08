using TinyLisp.Cli;
using TinyLisp.VM;
using TinyLisp.VM.Builtin;

var vm = new VM();

vm.Define("defun", new Defun());
vm.DefineFunctionsFromType(typeof(Global));

var command = args.ElementAtOrDefault(0);
if (command == null)
{
    throw new Exception("No command given");
}

switch (command)
{
    case "run":
        new CommandRun(vm).Run(args.ElementAtOrDefault(1) ?? throw new Exception("Run command requires a entry point"));
        break;
    case "test":
        new CommandTest(vm).Run(args.ElementAtOrDefault(1) ?? throw new Exception("Test command requires a directory"));
        break;
    default:
        throw new Exception($"Invalid command '{command}'");
}