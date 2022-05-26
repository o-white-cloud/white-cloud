using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using white_cloud.entities;
using white_cloud.interfaces.Data;

namespace white_cloud.identity
{
    public class IdentitySeeder
    {
        private readonly ILogger<IdentitySeeder> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITherapistsRepository _therapistsRepository;

        public IdentitySeeder(ILogger<IdentitySeeder> logger, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ITherapistsRepository therapistsRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _therapistsRepository = therapistsRepository;
        }

        public async Task Seed()
        {
            await CreateRole(Roles.Therapist);
            await CreateRole(Roles.Admin);

            await CreateAdminUser();
        }

        private async Task CreateAdminUser()
        {
            var adminUser = new User()
            {
                Email = "office@white-cloud.ro",
                UserName = "office@white-cloud.ro",
                FirstName = "Marina",
                LastName = "Otea-Popa",
                EmailConfirmed = true,
            };
            var adminCreateResult = await _userManager.CreateAsync(adminUser, "Toby_389929");
            if (!adminCreateResult.Succeeded)
            {
                _logger.LogError("Could not create admin user: {errors}", string.Join(Environment.NewLine, adminCreateResult.Errors));
            }
            await _userManager.AddToRolesAsync(adminUser, new string[] { Roles.Admin, Roles.Therapist });
            await _therapistsRepository.AddTherapist(new Therapist()
            {
                UserId = adminUser.Id,
                CopsiNumber = "123"
            });
        }

        private async Task CreateRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded)
                {
                    _logger.LogError("Could not create role {role}: {error}", roleName, string.Join(Environment.NewLine, result.Errors));
                }
            }
        }
    }
}
