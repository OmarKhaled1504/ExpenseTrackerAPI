using System;
using ExpenseTrackerAPI.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace ExpenseTrackerAPI.Data;

public class ExpenseContext : IdentityDbContext<User>
{
    public ExpenseContext(DbContextOptions<ExpenseContext> options) : base(options) { }
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Expense> Expenses => Set<Expense>();
}
