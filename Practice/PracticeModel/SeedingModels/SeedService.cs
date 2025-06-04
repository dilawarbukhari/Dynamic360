using Microsoft.AspNetCore.Identity;

using PracticeModel.DataModel;

namespace PracticeModel.SeedingModels
{

    public static class SeedService { 
    public static async Task SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { SeedModel.RoleSeed.Admin, SeedModel.RoleSeed.User };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {

                await roleManager.CreateAsync(new IdentityRole(role));

            }

        }



            var verify = await userManager.FindByEmailAsync(SeedModel.AdminCredentials.Email);
            if (verify == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Ali",
                    LastName="Naqvi",
                    UserName = SeedModel.AdminCredentials.Username,
                    Email = SeedModel.AdminCredentials.Email,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user, SeedModel.AdminCredentials.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, SeedModel.RoleSeed.Admin);
                }
            }
        }
    }

}
