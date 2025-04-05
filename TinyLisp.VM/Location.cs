using Antlr4.Runtime;

namespace TinyLisp.VM;

public class Location
{
    public string? File { get; set; }

    public int Line { get; set; }

    public int Column { get; set; }

    public int Length { get; set; }

    public Location(IToken token)
    {
        this.Column = token.Column;
        this.Length = token.StopIndex - token.StartIndex + 1;
        this.Line = token.Line;
    }

    public Location(string? file, IToken token) : this(token)
    {
        this.File = file;
    }

    public string Annotate(string code, string? message = null)
    {
        var context = 2;
        var lines = code.Split("\n");

        var beforeLines = Math.Max(0, Line - context);
        var afterLines = Math.Min(lines.Length, Line + context);

        var output = "";

        for (var i = beforeLines - 1; i < afterLines; i++)
        {
            output += $"{(i + 1).ToString().PadLeft(4)} | {lines[i]}\n";
            if (i == Line - 1)
            {
                output += new string(' ', Column + 7);
                output += new string('^', Length);
                if (message != null) output += " " + message;
                output += "\n";
            }
        }

        return output;
    }
}