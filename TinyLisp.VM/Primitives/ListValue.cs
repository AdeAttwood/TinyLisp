namespace TinyLisp.VM.Primitives;

public class ListValue : BaseValue
{
    public required TinyLispParser.ListContext Value { get; set; }

    public required string? File { get; set; }

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
            throw new LispException($"Symbol `{symbolValue.Value}` is not defined in the current scope", symbolValue.Location);
        }

        return type.EvaluateWithValue(vm, this);
    }

    public int ItemCount()
    {
        return this.Value.listItems().Length;
    }

    public BaseValue GetValueAt(int index)
    {
        return this.GetValueFromListItem(this.Value.listItems(index));
    }

    public T GetValueAt<T>(int index) where T : BaseValue
    {
        var value = this.GetValueAt(index);
        if (typeof(T) != value.GetType())
        {
            throw new LispException($"Invalid type, expected '{typeof(T)}' but found '{value.GetType()}'", value.Location);
        }

        return (T)value;
    }

    private BaseValue GetValueFromListItem(TinyLispParser.ListItemsContext item)
    {
        if (item == null)
        {
            return new NullValue();
        }

        var numberValue = item.Number();
        if (numberValue != null)
        {
            var number = NumberValue.Parse(numberValue.GetText());
            number.Location = new Location(this.File, numberValue.Symbol);
            return number;
        }

        var boolValue = item.BooleanConstant();
        if (boolValue != null)
        {
            var boolean = BoolValue.Parse(boolValue.GetText());
            boolean.Location = new Location(this.File, boolValue.Symbol);
            return boolean;
        }

        var stringValue = item.DoubleQuoteString();
        if (stringValue != null)
        {
            return new StringValue
            {
                Value = stringValue.GetText(),
                Location = new Location(this.File, stringValue.Symbol)
            };
        }

        var symbolValue = item.ID();
        if (symbolValue != null)
        {
            return new SymbolValue
            {
                Value = symbolValue.GetText(),
                Location = new Location(this.File, symbolValue.Symbol)
            };
        }

        var listValue = item.list();
        if (listValue != null)
        {
            return new ListValue
            {
                Value = listValue,
                File = this.File,
                Location = new Location(this.File, listValue.LeftPeren().Symbol)
            };
        }

        var quotedList = item.quotedList();
        if (quotedList != null)
        {
            var array = new ArrayValue
            {
                Location = new Location(this.File, quotedList.Quote().Symbol)
            };

            foreach (var listItem in quotedList.listItems())
            {
                array.Values.Add(this.GetValueFromListItem(listItem));
            }

            return array;
        }

        var location = new Location(this.File, item.Start);
        throw new LispException($"Unhandled list item '{item.GetText()}'", location);
    }
}
