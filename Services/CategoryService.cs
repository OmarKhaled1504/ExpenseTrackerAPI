using System;
using ExpenseTrackerAPI.Dtos.CategoriesDtos;
using ExpenseTrackerAPI.Mapping;
using ExpenseTrackerAPI.Repositories;

namespace ExpenseTrackerAPI.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CategoryService(ICategoryRepository categoryRepository, IHttpContextAccessor httpContextAccessor)
    {
        _categoryRepository = categoryRepository;
        _httpContextAccessor = httpContextAccessor;
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
}
