using Microsoft.AspNetCore.Mvc;
using white_cloud.web.Data;
using white_cloud.web.Models;

namespace white_cloud.web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestsController : ControllerBase
    {
        private readonly ILogger<TestsController> _logger;
        private readonly ITestsRepository _testsRepository;

        public TestsController(ILogger<TestsController> logger, ITestsRepository testsRepository)
        {
            _logger = logger;
            _testsRepository = testsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<TestModel>> Get()
        {
            return await _testsRepository.GetTests();
        }
    }
}