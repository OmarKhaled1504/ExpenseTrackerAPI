using System;
using ExpenseTrackerAPI.Dtos.ExpensesDtos;
using ExpenseTrackerAPI.Responses;

namespace ExpenseTrackerAPI.Services;

public interface IExpenseService
{
    public Task<ExpenseDto?> CreateExpenseAsync(ExpenseCreateDto dto);
    public Task<bool> DeleteExpenseAsync(int id);
    public Task<ExpenseDto?> GetExpenseAsync(int id);
    public Task<PagedResponse<ExpenseDto>> GetExpenseAsync(string? filter, DateTime? startDate, DateTime? endDate, int pageNumber, int pageSize);
    public Task<ExpenseDto?> UpdateExpenseAsync(int id, ExpenseUpdateDto dto);
}
