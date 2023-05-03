using Application.Enum;
using Identity.Model;
using Microsoft.AspNetCore.Identity;

namespace Identity.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed Default Admin User
            var defaultUser = new ApplicationUser
            {
                UserName = "userBasic",
                Email = "userBasic@mail.com",
                Name = "Sofia",
                DocumentNumber = "6656",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            //In here search for the email address if you do not find it, enter or send a default password in this case 123Hola
            if (userManager.Users.All(x => x.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Hola.");
                    //The default permissions are added to this role as administrator and Basic.
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }
            }
        }
    }
}
