using Microsoft.AspNetCore.Mvc;
using white_cloud.identity;

namespace white_cloud.web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IdentitySeeder _identitySeeder;

        public AdminController(ILogger<AdminController> logger, IdentitySeeder identitySeeder)
        {
            _logger = logger;
            _identitySeeder = identitySeeder;
        }

        [HttpGet("seedIdentity")]
        public async Task<IActionResult> SeedIdentity()
        {
            await _identitySeeder.Seed();
            return Ok();
        }
    }
}
