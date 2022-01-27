using Microsoft.AspNetCore.Mvc;
using white_cloud.web.Data;
using white_cloud.web.Models.Tests;
using white_cloud.web.Models.DTOs;
using white_cloud.web.Services.Tests;

namespace white_cloud.web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestsController : ControllerBase
    {
        private readonly ILogger<TestsController> _logger;
        private readonly ITestsRepository _testsRepository;
        private readonly ITestService _testService;

        public TestsController(ILogger<TestsController> logger, ITestsRepository testsRepository, ITestService testService)
        {
            _logger = logger;
            _testsRepository = testsRepository;
            _testService = testService;
        }

        [HttpGet]
        public async Task<IEnumerable<TestModel>> Get()
        {
            return await _testsRepository.GetTests();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitTestAnswers(PostedTestAnswers postedTestAnswers)
        {
            var test = await _testsRepository.GetTest(postedTestAnswers.TestId, includeResults: true);
            if(test is null)
            {
                return NotFound();
            }

            var result = await _testService.ComputeTestResults(test, postedTestAnswers.Answers);
            return Ok(result);
        }
    }
}