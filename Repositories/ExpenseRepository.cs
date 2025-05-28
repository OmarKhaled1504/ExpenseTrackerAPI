using System;
using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Dtos.ExpensesDtos;
using ExpenseTrackerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly ExpenseContext _context;
    public ExpenseRepository(ExpenseContext context)
    {
        _context = context;
    }

    public async Task CreateExpenseAsync(Expense expense)
    {
        await _context.Expenses.AddAsync(expense);
        await _context.SaveChangesAsync();
    }

    public async Task<Expense?> GetExpenseAsync(int id)
    {
        return await _context.Expenses.Include(exp =>exp.Category).SingleOrDefaultAsync(exp =>exp.Id == id);
    }

    public async Task ReassignCategoryAsync(int from, int to)
    {
        var expenses = await _context.Expenses
        .Where(exp => exp.CatId == from)
        .ToListAsync();

        foreach (var expense in expenses)
        {
            expense.CatId = to;
        }
        await _context.SaveChangesAsync();
    }
}
