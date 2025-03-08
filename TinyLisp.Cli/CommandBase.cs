using TinyLisp.VM.Builtin;

namespace TinyLisp.Cli;

public class CommandBase
{
    protected VM.VM BuildVm()
    {
        var vm = new VM.VM();

        vm.Define("defun", new Defun());
        vm.Define("deftest", new DefTest());
        vm.DefineFunctionsFromType(typeof(Global));

        return vm;
    }
}