using Application.Enum;
using Application.Exceptions;
using Application.Wrappres;
using Identity.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Seeds
{
    /// <DefaultAdminUser>
    /// In this class the role of the administrator user is created
    /// </DefaultAdminUser>
    public static class DefaultAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed Default Admin User
            var defaultUser = new ApplicationUser
            {
                UserName = "userAdmin",
                Email = "userAdmin@mail.com",
                Name = "VladiZ",
                DocumentNumber = "10924733",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                
            };
            //In here search for the email address if you do not find it, enter or send a default password in this case 123Hola
            if (userManager.Users.All(x=>x.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if(user == null)
                {
                    var detailCreateUser = await userManager.CreateAsync(defaultUser, "123userAdmin.");
                    if (!detailCreateUser.Succeeded)
                        throw new ApiException($"Error creating a user {string.Join(',', detailCreateUser.Errors.Select(f => f.Description).ToList())}");
                    //The default permissions are added to this role as administrator and Basic.
                    await userManager.AddToRoleAsync(defaultUser,Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }
            }
        }
    }
}
