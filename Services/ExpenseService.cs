using System;
using ExpenseTrackerAPI.Dtos.ExpensesDtos;
using ExpenseTrackerAPI.Entities;
using ExpenseTrackerAPI.Mapping;
using ExpenseTrackerAPI.Repositories;

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

}
