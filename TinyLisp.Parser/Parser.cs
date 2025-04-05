using Antlr4.Runtime;

namespace TinyLisp.Parser;

public class Parser
{
    public static TinyLispParser.ListContext[] Parse(string code)
    {
        var lexer = new TinyLispLexer(new AntlrInputStream(code));
        var tokens = new CommonTokenStream(lexer);
        var parser = new TinyLispParser(tokens);

        var file = parser.file();
        return file.list();
    }
}
