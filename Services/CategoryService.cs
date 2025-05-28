using System;
using ExpenseTrackerAPI.Dtos.CategoriesDtos;
using ExpenseTrackerAPI.Entities;
using ExpenseTrackerAPI.Mapping;
using ExpenseTrackerAPI.Repositories;

namespace ExpenseTrackerAPI.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    private readonly IExpenseRepository _expenseRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CategoryService(ICategoryRepository categoryRepository, IExpenseRepository expenseRepository, IHttpContextAccessor httpContextAccessor)
    {
        _categoryRepository = categoryRepository;
        _httpContextAccessor = httpContextAccessor;
        _expenseRepository = expenseRepository;
    }

    public async Task<CategoryDto?> CreateCategoryAsync(CategoryCreateDto dto)
    {
        var newCategory = dto.ToEntity();
        var existingCategory = await _categoryRepository.GetCategoryByNameAsync(newCategory.Name);
        if (existingCategory != null)
        {
            return null;
        }
        var created = await _categoryRepository.CreateCategoryAsync(newCategory);
        return created.ToDto();

    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync(int pageNumber, int pageSize)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("User not authenticated.");
        var response = await _categoryRepository.GetCategoriesAsync(pageNumber, pageSize);

        var result = response.Select(cat => cat.ToDto(cat.Expenses.Where(exp => exp.UserId == userId)));

        return result;

    }

    public async Task<CategoryDto?> GetCategoryAsync(int id)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("User not authenticated.");
        var response = await _categoryRepository.GetCategoryAsync(id);
        if (response is null)
            return null;
        var result = response.ToDto(response.Expenses.Where(exp => exp.UserId == userId));
        return result;
    }

    public async Task<int> GetTotalCategoriesCountAsync()
    {
        return await _categoryRepository.GetTotalCategoriesCountAsync();
    }

    public async Task<CategoryDto?> UpdateCategoryAsync(int id, CategoryUpdateDto dto)
    {
        var existingCategory = await _categoryRepository.GetCategoryAsync(id);
        if (existingCategory is null)
            return null;


        var updated = await _categoryRepository.UpdateCategoryAsync(existingCategory, dto.Name);
        return updated.ToDto();
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var existingCategory = await _categoryRepository.GetCategoryAsync(id);
        if (existingCategory is null)
            return false;
        if (existingCategory.Name == "Unspecified")
            throw new InvalidOperationException("Cannot delete the 'Unspecified' category.");
        var unspecified = await _categoryRepository.GetCategoryByNameAsync("Unspecified");
        await _expenseRepository.ReassignCategoryAsync(id, unspecified!.Id);
        await _categoryRepository.DeleteCategoryAsync(existingCategory);
        return true;
    }
}
