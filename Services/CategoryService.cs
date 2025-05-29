using System;
using ExpenseTrackerAPI.Dtos.CategoriesDtos;
using ExpenseTrackerAPI.Entities;
using ExpenseTrackerAPI.Mapping;
using ExpenseTrackerAPI.Repositories;

namespace ExpenseTrackerAPI.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CategoryService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<CategoryDto?> CreateCategoryAsync(CategoryCreateDto dto)
    {
        var newCategory = dto.ToEntity();
        var existingCategory = await _unitOfWork.Categories.GetCategoryByNameAsync(newCategory.Name);
        if (existingCategory != null)
        {
            return null;
        }
        var created = await _unitOfWork.Categories.CreateCategoryAsync(newCategory);
        await _unitOfWork.CompleteAsync();
        return created.ToDto();

    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync(int pageNumber, int pageSize)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("User not authenticated.");
        var response = await _unitOfWork.Categories.GetCategoriesAsync(pageNumber, pageSize);

        var result = response.Select(cat => cat.ToDto(cat.Expenses.Where(exp => exp.UserId == userId)));

        return result;

    }

    public async Task<CategoryDto?> GetCategoryAsync(int id)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("User not authenticated.");
        var response = await _unitOfWork.Categories.GetCategoryAsync(id);
        if (response is null)
            return null;
        var result = response.ToDto(response.Expenses.Where(exp => exp.UserId == userId));
        return result;
    }

    public async Task<int> GetTotalCategoriesCountAsync()
    {
        return await _unitOfWork.Categories.GetTotalCategoriesCountAsync();
    }

    public async Task<CategoryDto?> UpdateCategoryAsync(int id, CategoryUpdateDto dto)
    {
        var category = await _unitOfWork.Categories.GetCategoryAsync(id);
        if (category is null)
            return null;
        category.Name = dto.Name;
        await _unitOfWork.CompleteAsync();
        return category.ToDto();
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var existingCategory = await _unitOfWork.Categories.GetCategoryAsync(id);
        if (existingCategory is null)
            return false;
        if (existingCategory.Name == "Unspecified")
            throw new InvalidOperationException("Cannot delete the 'Unspecified' category.");
        var unspecified = await _unitOfWork.Categories.GetCategoryByNameAsync("Unspecified");
        await _unitOfWork.Expenses.ReassignCategoryAsync(id, unspecified!.Id);
        _unitOfWork.Categories.DeleteCategoryAsync(existingCategory);
        await _unitOfWork.CompleteAsync();
        return true;
    }
}
