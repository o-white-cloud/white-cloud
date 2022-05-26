using white_cloud.entities.Tests;
using white_cloud.entities.Tests.Models;

namespace white_cloud.web.Services.Tests.TestResultComputers;

public class SumIntervalsTestComputer : ITestResultComputer
{
    private readonly ILogger<SumIntervalsTestComputer> _logger;

    public SumIntervalsTestComputer(ILogger<SumIntervalsTestComputer> logger)
    {
        _logger = logger;
    }

    public TestResultStrategy Strategy => TestResultStrategy.SumIntervals;

    public Task<TestSubmissionResultModel> GetResult(TestModel model, TestSubmissionAnswer[] answers)
    {
        var results = model.Results as TestResultsSumIntervalsModel;
        if(results is null)
        {
            throw new ArgumentException($"Results for test {model.Name} with id {model.Id} is not of type TestResultsSumIntervals");
        }

        var sum = answers.Sum(a => a.AnswerValue);
        var finalResult = Math.Round((double)sum / (double)results.NormalizeValue);
        var interval = results.Intervals.OrderBy(i => i.Max).FirstOrDefault(x => x.Min <= finalResult && finalResult <= x.Max);
        if (interval is null)
        {
            return Task.FromResult(new TestSubmissionResultModel {  ResultId = 0 });
        }
        return Task.FromResult(new TestSubmissionResultModel { ResultId = interval.Id });
    }
}
