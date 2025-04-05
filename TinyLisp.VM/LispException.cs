namespace TinyLisp.VM;

public class LispException : Exception
{
    public Location? Location { get; set; }

    public LispException(string message) : base(message) { }

    public LispException(string message, Location? location) : base(message)
    {
        this.Location = location;
    }

    public string MsBuild()
    {
        if (Location != null)
        {
            return $"{Location?.File ?? "/dev/null"}({Location?.Line ?? 0},{Location?.Column ?? 0}): {Message}";
        }
        else
        {
            return this.Message;
        }
    }

    public string FullMessage(string fileContent)
    {
        var output = this.MsBuild();
        if (this.Location != null)
        {
            output += "\n\n";
            output += this.Location.Annotate(fileContent, Message);
        }

        return output;
    }
}
