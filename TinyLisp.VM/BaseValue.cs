namespace TinyLisp.VM;

public abstract class BaseValue
{
    public Location? Location { get; set; }

    public abstract string Display();

    public virtual BaseValue Evaluate(VM vm)
    {
        throw new LispException($"Value `{this.GetType()}` is not evaluable", this.Location);
    }

    public virtual BaseValue EvaluateWithValue(VM vm, BaseValue value)
    {
        throw new LispException($"Value `{this.GetType()}` is not evaluable with a value `{value.GetType()}`", this.Location);
    }
}
