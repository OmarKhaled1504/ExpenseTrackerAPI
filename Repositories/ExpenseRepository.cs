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
    }

    public void DeleteExpenseAsync(Expense expense)
    {
        _context.Expenses.Remove(expense);
    }

    public async Task<Expense?> GetExpenseAsync(int id)
    {
        return await _context.Expenses.Include(exp => exp.Category).SingleOrDefaultAsync(exp => exp.Id == id);
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
    }

    public async Task<int> GetTotalExpensesCountByIdAndFilterAsync(string userId, DateTime fromDate, DateTime toDate)
    {
        return await _context.Expenses.CountAsync(exp => exp.UserId == userId && exp.CreatedAt >= fromDate && exp.CreatedAt <= toDate);
    }

    public async Task<IEnumerable<Expense>> GetExpensesAsync(string userId, DateTime fromDate, DateTime toDate, int pageNumber, int pageSize)
    {
        return await _context.Expenses
        .Include(exp => exp.Category)
        .Where(exp => exp.UserId == userId && exp.CreatedAt >= fromDate && exp.CreatedAt <= toDate)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .OrderByDescending(exp => exp.CreatedAt)
        .ToArrayAsync();
    }


}
