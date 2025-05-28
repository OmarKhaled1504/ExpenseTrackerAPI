using System;
using ExpenseTrackerAPI.Dtos.ExpensesDtos;
using ExpenseTrackerAPI.Entities;

namespace ExpenseTrackerAPI.Repositories;

public interface IExpenseRepository
{
    public Task CreateExpenseAsync(Expense expense);
    public Task DeleteExpenseAsync(Expense expense);
    public Task<Expense?> GetExpenseAsync(int id);
    public Task ReassignCategoryAsync(int from, int to);
}
