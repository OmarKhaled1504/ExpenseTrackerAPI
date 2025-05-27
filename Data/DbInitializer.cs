using System;
using ExpenseTrackerAPI.Entities;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTrackerAPI.Data;

public static class DbInitializer

{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = new[] { "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    public static async Task SeedAdminUserAsync(UserManager<User> userManager)
    {
        // Add a default admin user if needed
        string adminEmail = "admin@example.com";
        string adminUserName = "admin";
        string adminPassword = "Admin123!"; // Use strong password in production

        var user = await userManager.FindByNameAsync(adminUserName);
        if (user == null)
        {
            user = new User
            {
                UserName = adminUserName,
                Email = adminEmail,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }else
            {
                // Ensure the user is in the Admin role
                if (!await userManager.IsInRoleAsync(user, "Admin"))
                    await userManager.AddToRoleAsync(user, "Admin");
            }
    }
}
