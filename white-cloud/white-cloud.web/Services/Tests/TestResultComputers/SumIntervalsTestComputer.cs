using white_cloud.entities.Tests;

namespace white_cloud.web.Services.Tests.TestResultComputers;

public class SumIntervalsTestComputer : ITestResultComputer
{
    private readonly ILogger<SumIntervalsTestComputer> _logger;

    public SumIntervalsTestComputer(ILogger<SumIntervalsTestComputer> logger)
    {
        _logger = logger;
    }

    public TestResultStrategy Strategy => TestResultStrategy.SumIntervals;

    public Task<TestSubmissionResult> GetResult(TestModel model, Dictionary<int, string> answers)
    {
        var results = model.Results as TestResultsSumIntervals;
        if(results is null)
        {
            throw new ArgumentException($"Results for test {model.Name} with id {model.Id} is not of type TestResultsSumIntervals");
        }

        var sum = answers.Sum(kvp => int.Parse(kvp.Value));
        var finalResult = Math.Round((double)sum / (double)results.NormalizeValue);
        var interval = results.Intervals.OrderBy(i => i.Max).FirstOrDefault(x => x.Min <= finalResult && finalResult <= x.Max);
        if (interval is null)
        {
            return Task.FromResult(new TestSubmissionResult {  Description = "Could not find proper interval", Title = "Error"});
        }
        return Task.FromResult(new TestSubmissionResult { Description = interval.Details, Title = interval.Name });
    }
}
