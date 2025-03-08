using System.Reflection;

using TinyLisp.VM.Primitives;

namespace TinyLisp.VM;

public class VM
{
    private List<Scope> _scopes = new List<Scope> { new Scope() };

    public BaseValue Evaluate(string code)
    {
        var lists = Parser.Parser.Parse(code);

        BaseValue returnValue = new NullValue();
        foreach (var list in lists)
        {
            returnValue = this.Evaluate(list);
        }

        return returnValue;
    }

    public BaseValue Evaluate(TinyLispParser.ListContext listContext)
    {
        var list = new ListValue { Value = listContext };
        return list.Evaluate(this);
    }

    public BaseValue Evaluate(BaseValue value)
    {
        return value switch
        {
            ListValue => value.Evaluate(this),
            SymbolValue v => this.GetValue(v.Value) ?? throw new LispException($"Value {v.Value} is not defined in the current scope"),
            _ => value,
        };
    }

    public void Define(string fqn, BaseValue value)
    {
        _scopes.Last().Values[fqn] = value;
    }

    public void DefineFunctionsFromType(Type type)
    {
        var methods = type.GetMethods(BindingFlags.Static | BindingFlags.Public)
            .Where(m => m.GetCustomAttribute<LispFunctionAttribute>() != null);

        foreach (var method in methods)
        {
            var attribute = method.GetCustomAttribute<LispFunctionAttribute>();
            if (attribute != null)
            {
                this.Define(attribute.Name, new FunctionValue
                {
                    Name = attribute.Name,
                    Callable = method,
                });
            }
        }
    }

    public BaseValue? GetValue(string fqn)
    {
        for (int i = _scopes.Count - 1; i >= 0; i--)
        {
            var scope = _scopes[i];
            if (scope.HasValue(fqn))
            {
                return scope.Values[fqn];
            }
        }

        return null;
    }

    public void PushScope()
    {
        _scopes.Add(new Scope());
    }

    public void PopScope()
    {
        _scopes.RemoveAt(_scopes.Count - 1);
    }

    public List<TestFunctionValue> GetTestFunctions()
    {
        return _scopes.First()
            .Values
            .Values
            .Where(v => v.GetType() == typeof(TestFunctionValue))
            .Select(v => (TestFunctionValue)v)
            .ToList();
    }
}