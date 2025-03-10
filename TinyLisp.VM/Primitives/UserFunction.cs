namespace TinyLisp.VM.Primitives;

public class UserFunctionValue : BaseValue
{
    public required string Name { get; set; }

    public List<SymbolValue> Parameters = new List<SymbolValue>();

    public List<BaseValue> Body = new List<BaseValue>();

    public override string Display()
    {
        return this.Name;
    }

    public override BaseValue EvaluateWithValue(VM vm, BaseValue value)
    {
        var listValue = value as ListValue;
        if (listValue == null)
        {
            throw new LispException($"function '{this.Name}' has been called with a value that is not a list");
        }

        if (this.Parameters.Count != listValue.ItemCount() - 1)
        {
            throw new Exception($"Function '{this.Name}' expects {this.Parameters.Count} arguments, but {listValue.ItemCount() - 1} were provided.");
        }

        vm.PushScope();

        for (var i = 0; i < this.Parameters.Count; i++)
        {
            vm.Define(this.Parameters[i].Value, vm.Evaluate(listValue.GetValueAt(i + 1)));
        }

        BaseValue returnValue = new NullValue();
        foreach (var item in this.Body)
        {
            returnValue = vm.Evaluate(item);
        }

        vm.PopScope();

        return returnValue;
    }
}