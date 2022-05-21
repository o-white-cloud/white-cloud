using Microsoft.AspNetCore.Mvc;
using white_cloud.web.Models.DTOs;
using white_cloud.web.Services.Tests;
using white_cloud.web.Services;
using white_cloud.interfaces.Data;
using white_cloud.entities.Tests;
using Microsoft.AspNetCore.Authorization;

namespace white_cloud.web.controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestsController : ControllerBase
    {
        private readonly ILogger<TestsController> _logger;
        private readonly ITestsRepository _testsRepository;
        private readonly ITestService _testService;
        private readonly IEmailService _emailService;

        public TestsController(ILogger<TestsController> logger, ITestsRepository testsRepository, ITestService testService, IEmailService emailService)
        {
            _logger = logger;
            _testsRepository = testsRepository;
            _testService = testService;
            _emailService = emailService;
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

            var result = await _testService.ComputeTestResults(postedTestAnswers.Email, test, postedTestAnswers.Answers);
            //await _emailService.SendEmail(postedTestAnswers.Email, "Test results", result.Description);
            return Ok(result);
        }

        [HttpGet("submitted")]
        [Authorize]
        public async Task<IActionResult> GetSubmittedTests()
        {
            return await Task.FromResult(Ok(HttpContext.User.Claims.ToList()));
        }
    }
}