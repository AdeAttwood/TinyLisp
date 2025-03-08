namespace TinyLisp.Cli;

public class TestSummary
{
    public int PassCount { get; set; } = 0;
    public int FailCount { get; set; } = 0;

    public int TotalCount() => this.FailCount + this.PassCount;

    public List<TestFailure> Failures { get; set; } = new List<TestFailure>();

    public void AddFailure(TestFailure test)
    {
        this.FailCount += 1;
        this.Failures.Add(test);
    }

    public void AddPass()
    {
        this.PassCount += 1;
    }
}