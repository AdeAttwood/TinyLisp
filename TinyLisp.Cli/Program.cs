using TinyLisp.Cli;

var command = args.ElementAtOrDefault(0);
if (command == null)
{
    throw new Exception("No command given");
}

switch (command)
{
    case "run":
        new CommandRun().Run(args.ElementAtOrDefault(1) ?? throw new Exception("Run command requires a entry point"));
        break;
    case "test":
        new CommandTest().Run(args.ElementAtOrDefault(1) ?? throw new Exception("Test command requires a directory"));
        break;
    default:
        throw new Exception($"Invalid command '{command}'");
}