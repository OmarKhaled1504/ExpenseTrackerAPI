using System;
using ExpenseTrackerAPI.Dtos.ExpensesDtos;

namespace ExpenseTrackerAPI.Services;

public interface IExpenseService
{
    public Task<ExpenseDto?> CreateExpenseAsync(ExpenseCreateDto dto);
    public Task<bool> DeleteExpenseAsync(int id);
    public Task<ExpenseDto?> GetExpenseAsync(int id);
}
