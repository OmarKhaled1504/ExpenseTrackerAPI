using System;
using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Entities;
using ExpenseTrackerAPI.Repositories;

namespace ExpenseTrackerAPI.Services;

public class UnitOfWork(ExpenseContext context,
IUserRepository users,
IExpenseRepository expenses,
ICategoryRepository categories) : IUnitOfWork
{
    private readonly ExpenseContext _context = context;
    public IUserRepository Users { get; } = users;

    public IExpenseRepository Expenses { get; } = expenses;

    public ICategoryRepository Categories { get; } = categories;

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
