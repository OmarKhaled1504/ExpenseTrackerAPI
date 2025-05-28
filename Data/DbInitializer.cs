using System;
using ExpenseTrackerAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        }
        else
        {
            // Ensure the user is in the Admin role
            if (!await userManager.IsInRoleAsync(user, "Admin"))
                await userManager.AddToRoleAsync(user, "Admin");
        }
    }

    public static async Task SeedUnspecifiedCategoryAsync(ExpenseContext context)
    {
        // Make sure the DB is created (should be already)
        await context.Database.EnsureCreatedAsync();

        // Look for the "Unspecified" category
        var existing = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Unspecified");
        if (existing == null)
        {
            var category = new Category { Name = "Unspecified" };
            context.Categories.Add(category);
            await context.SaveChangesAsync();
        }
    }
}
