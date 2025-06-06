using System;
using ExpenseTrackerAPI.Dtos.CategoriesDtos;
using ExpenseTrackerAPI.Entities;

namespace ExpenseTrackerAPI.Services;

public interface ICategoryService
{
    public Task<CategoryDto?> CreateCategoryAsync(CategoryCreateDto dto);
    public Task<bool> DeleteCategoryAsync(int id);
    public Task<IEnumerable<CategoryDto>> GetCategoriesAsync(int pageNumber, int pageSize);
    public Task<CategoryDto?> GetCategoryAsync(int id);
    public Task<int> GetTotalCategoriesCountAsync();
    public Task<CategoryDto?> UpdateCategoryAsync(int id, CategoryUpdateDto dto);
}
