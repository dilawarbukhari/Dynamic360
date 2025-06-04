using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PracticeModel.DataModel;
using PracticeModel.DBcontext;


namespace PracticeModel.SeedingModels
{
    public class Permissionseeder
    {

        public static async Task SeedRolePermissionsAsync(RoleManager<IdentityRole> roleManager)
        {
            var rolesWithPermissions = new Dictionary<string, List<string>>
        {
            { "Admin", new List<string> { "ManageUsers", "ManageRoles", "ViewReports" } },
            { "User", new List<string> { "ViewDashboard", "CreateRequest" } }
        };

            foreach (var role in rolesWithPermissions)
            {
                var existingRole = await roleManager.FindByNameAsync(role.Key);
                if (existingRole == null)
                {
                    existingRole = new IdentityRole(role.Key);
                    await roleManager.CreateAsync(existingRole);
                }

                foreach (var permission in role.Value)
                {
                    var existingClaims = await roleManager.GetClaimsAsync(existingRole);
                    if (!existingClaims.Any(c => c.Type == "Permission" && c.Value == permission))
                    {
                        await roleManager.AddClaimAsync(existingRole, new System.Security.Claims.Claim("Permission", permission));
                    }
                }
            }
        }
    }

}

