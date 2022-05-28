using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using white_cloud.data.EF;
using white_cloud.identity;

namespace white_cloud.web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IdentitySeeder _identitySeeder;
        private readonly WCDbContext _wcDbContext;

        public AdminController(ILogger<AdminController> logger, IdentitySeeder identitySeeder, WCDbContext wCDbContext)
        {
            _logger = logger;
            _identitySeeder = identitySeeder;
            _wcDbContext = wCDbContext;
        }

        [HttpGet("updateDatabase")]
        public IActionResult UpdateDatabase()
        {
            if (Request.Host.Host != "localhost")
            {
                return Forbid();
            }

            try
            {
                _wcDbContext.Database.Migrate();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("seedIdentity")]
        public async Task<IActionResult> SeedIdentity()
        {
            if (Request.Host.Host != "localhost")
            {
                return Forbid();
            }
            await _identitySeeder.Seed();
            return Ok();
        }
    }
}
