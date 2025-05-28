using System;
using System.Data;
using ExpenseTrackerAPI.Dtos.ExpensesDtos;
using ExpenseTrackerAPI.Entities;
using ExpenseTrackerAPI.Mapping;
using ExpenseTrackerAPI.Repositories;
using ExpenseTrackerAPI.Responses;

namespace ExpenseTrackerAPI.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ICategoryRepository _categoryRepository;

    private readonly IHttpContextAccessor _httpContextAccessor;
    public ExpenseService(IExpenseRepository expenseRepository, ICategoryRepository categoryRepository, IHttpContextAccessor httpContextAccessor)
    {
        _expenseRepository = expenseRepository;
        _httpContextAccessor = httpContextAccessor;
        _categoryRepository = categoryRepository;
    }

    public async Task<ExpenseDto?> CreateExpenseAsync(ExpenseCreateDto dto)
    {
        var existingCategory = await _categoryRepository.GetCategoryAsync(dto.CategoryId);
        if (existingCategory is null)
        {
            return null;
        }
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var expense = dto.ToEntity();
        expense.UserId = userId!;
        expense.CreatedAt = DateTime.UtcNow;

        await _expenseRepository.CreateExpenseAsync(expense);
        return expense.ToDto();
    }

    public async Task<bool> DeleteExpenseAsync(int id)
    {
        var expense = await _expenseRepository.GetExpenseAsync(id);
        if (expense is null)
        {
            return false;
        }
        if (!CheckOwnership(expense))
        {
            throw new UnauthorizedAccessException("Forbidden.");
        }
        await _expenseRepository.DeleteExpenseAsync(expense);
        return true;
    }

    private bool CheckOwnership(Expense expense)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        return expense.UserId == userId;
    }

    public async Task<ExpenseDto?> GetExpenseAsync(int id)
    {
        var expense = await _expenseRepository.GetExpenseAsync(id);
        if (expense is null)
        {
            return null;
        }
        if (!CheckOwnership(expense))
        {
            throw new UnauthorizedAccessException("Forbidden.");
        }
        return expense.ToDto();
    }

    public async Task<ExpenseDto?> UpdateExpenseAsync(int id, ExpenseUpdateDto dto)
    {   var existingCategory = await _categoryRepository.GetCategoryAsync(dto.CategoryId);
        if (existingCategory is null)
            throw new Exception("Category");
        var expense = await _expenseRepository.GetExpenseAsync(id);
        if (expense is null)
        {
            return null;
        }
        if (!CheckOwnership(expense))
            throw new UnauthorizedAccessException("Forbidden.");
        expense.Update(dto);
        expense.UpdatedAt = DateTime.UtcNow;
        await _expenseRepository.UpdateExpenseAsync(expense);
        return expense.ToDto();
    }

    public async Task<PagedResponse<ExpenseDto>> GetExpenseAsync(string? filter, DateTime? startDate, DateTime? endDate, int pageNumber, int pageSize)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        DateTime fromDate, toDate;
        toDate = DateTime.UtcNow;

        if (!string.IsNullOrWhiteSpace(filter))
        {
            filter = filter.ToLowerInvariant();
            if (filter == "pastweek")
            {
                fromDate = toDate.AddDays(-7);
            }
            else if (filter == "pastmonth")
            {
                fromDate = toDate.AddMonths(-1);
            }
            else if (filter == "last3months")
            {
                fromDate = toDate.AddMonths(-3);
            }
            else if (filter == "custom")
            {
                if (startDate is null || endDate is null)
                    throw new ArgumentException("Custom filter require startDate and endDate to not be null.");
                fromDate = startDate.Value;
                toDate = endDate.Value;
            }
            else
                throw new ArgumentException("invalid filter value");

        }
        else
        {
            fromDate = DateTime.MinValue;
        }

        var expenses = await _expenseRepository.GetExpensesAsync(userId!, fromDate, toDate, pageNumber, pageSize);

        return new PagedResponse<ExpenseDto>
        {
            Data = expenses.Select(exp => exp.ToDto()),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = await _expenseRepository.GetTotalExpensesCountByIdAndFilterAsync(userId!,fromDate,toDate)
        };
    }
}
