using System;
using ExpenseTrackerAPI.Dtos.ExpensesDtos;
using ExpenseTrackerAPI.Entities;

namespace ExpenseTrackerAPI.Repositories;

public interface IExpenseRepository
{
    public Task CreateExpenseAsync(Expense expense);
    public void DeleteExpenseAsync(Expense expense);
    public Task<Expense?> GetExpenseAsync(int id);
    public Task ReassignCategoryAsync(int from, int to);
    public Task<int> GetTotalExpensesCountByIdAndFilterAsync(string userId, DateTime fromDate, DateTime toDate);
    public Task<IEnumerable<Expense>> GetExpensesAsync(string userId, DateTime fromDate, DateTime toDate, int pageNumber, int PageSize);
}
