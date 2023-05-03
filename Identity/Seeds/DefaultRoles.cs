using Application.Enum;
using Identity.Model;
using Microsoft.AspNetCore.Identity;

namespace Identity.Seeds
{
    /// <DefaultRoles>
    /// In this class we create the necessary roles in the DB.
    /// </DefaultRoles>
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            //Filled with created or mapped roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
        }
    }
}
