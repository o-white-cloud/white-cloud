using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using white_cloud.data.EF;
using white_cloud.entities;
using white_cloud.identity;
using white_cloud.interfaces.Data;

namespace white_cloud.web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IdentitySeeder _identitySeeder;
        private readonly WCDbContext _wcDbContext;
        private readonly UserManager<User> _userManager;
        private readonly ITherapistsRepository _therapistsRepository;

        public AdminController(ILogger<AdminController> logger, IdentitySeeder identitySeeder, WCDbContext wCDbContext, UserManager<User> userManager, ITherapistsRepository therapistsRepository)
        {
            _logger = logger;
            _identitySeeder = identitySeeder;
            _wcDbContext = wCDbContext;
            _userManager = userManager;
            _therapistsRepository = therapistsRepository;
        }

        [HttpDelete("user/{id}")]
        public async Task<IActionResult> ListUsers(string id)
        {
            if (Request.Host.Host != "localhost")
            {
                return Forbid();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var client = await _therapistsRepository.GetClient(user.Id);
                if (client != null)
                {
                    await _therapistsRepository.RemoveClient(client);
                }
                await _userManager.DeleteAsync(user);
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet("users")]
        public async Task<IActionResult> ListUsers()
        {
            if (Request.Host.Host != "localhost")
            {
                return Forbid();
            }

            return Ok((await _userManager.Users.ToListAsync()).Select(x => new { x.Id, x.Email }));
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
