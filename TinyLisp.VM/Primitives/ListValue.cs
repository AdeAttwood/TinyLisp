namespace TinyLisp.VM.Primitives;

public class ListValue : BaseValue
{
    public required TinyLispParser.ListContext Value { get; set; }

    public override string Display()
    {
        return "todo i am a list";
    }

    public override BaseValue Evaluate(VM vm)
    {
        var symbolValue = this.GetValueAt<SymbolValue>(0);
        var type = vm.GetValue(symbolValue.Value);
        if (type == null)
        {
            throw new LispException($"Symbol `{symbolValue.Value}` is not defined in the current scope");
        }

        return type.EvaluateWithValue(vm, this);
    }

    public int ItemCount()
    {
        return this.Value.listItems().Length;
    }

    public BaseValue GetValueAt(int index)
    {
        var item = this.Value.listItems(index);
        if (item == null)
        {
            return new NullValue();
        }

        var numberValue = item.Number();
        if (numberValue != null)
        {
            return NumberValue.Parse(numberValue.GetText());
        }

        var boolValue = item.BooleanConstant();
        if (boolValue != null)
        {
            return BoolValue.Parse(boolValue.GetText());
        }

        var stringValue = item.DoubleQuoteString();
        if (stringValue != null)
        {
            return new StringValue { Value = stringValue.GetText() };
        }

        var symbolValue = item.ID();
        if (symbolValue != null)
        {
            return new SymbolValue { Value = symbolValue.GetText() };
        }

        var listValue = item.list();
        if (listValue != null)
        {
            return new ListValue { Value = listValue };
        }

        throw new LispException($"Unhandled list item '{item.GetText()}'");
    }

    public T GetValueAt<T>(int index) where T : BaseValue
    {
        var value = this.GetValueAt(index);
        if (typeof(T) != value.GetType())
        {
            throw new LispException($"Invalid type, expected '{typeof(T)}' but found '{value.GetType()}'");
        }

        return (T)value;
    }
}