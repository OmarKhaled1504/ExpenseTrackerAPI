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
    protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);

    // User - Expense: Cascade delete
    builder.Entity<User>()
        .HasMany(u => u.Expenses)
        .WithOne(e => e.User)
        .HasForeignKey(e => e.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // Expense - Category: Do NOT cascade, do NOT restrict, do NOT set null
    builder.Entity<Category>()
        .HasMany(c => c.Expenses)
        .WithOne(e => e.Category)
        .HasForeignKey(e => e.CatId)
        .OnDelete(DeleteBehavior.Restrict); // Or ClientSetNull, but you want to handle yourself

    // Optionally, enforce required relationship:
    builder.Entity<Expense>()
        .Property(e => e.CatId)
        .IsRequired();
}

}
