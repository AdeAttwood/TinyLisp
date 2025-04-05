using TinyLisp.Cli;

var command = args.ElementAtOrDefault(0) ?? "repl";
switch (command)
{
    case "run":
        new CommandRun().Run(args.ElementAtOrDefault(1) ?? throw new Exception("Run command requires a entry point"));
        break;
    case "test":
        new CommandTest().Run(args.ElementAtOrDefault(1) ?? throw new Exception("Test command requires a directory"));
        break;
    case "repl":
        new CommandRepl().Run();
        break;
    default:
        throw new Exception($"Invalid command '{command}'");
}
