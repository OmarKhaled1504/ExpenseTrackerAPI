using System;
using ExpenseTrackerAPI.Data;
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
