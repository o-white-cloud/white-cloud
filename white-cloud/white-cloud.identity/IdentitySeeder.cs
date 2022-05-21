using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using white_cloud.identity.Entities;

namespace white_cloud.identity
{
    public class IdentitySeeder
    {
        private readonly ILogger<IdentitySeeder> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentitySeeder(ILogger<IdentitySeeder> logger, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            await CreateRole(Roles.Therapist);
            await CreateRole(Roles.Admin);

           var admin = await _userManager.CreateAsync(new User()
            {
                Email = "alex.zero.a@gmail.com",
                EmailConfirmed = true,
            }, "Toby_389929");
            _userManager.AddToRoleAsync(admin, Roles.Admin);
        }

        private async Task CreateRole(string roleName)
        {
            var role = _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(Roles.Therapist));
                if(!result.Succeeded)
                {
                    _logger.LogError("Could not create role {role}: {error}", roleName, string.Join(Environment.NewLine, result.Errors));
                }
            }
        }
    }
}
