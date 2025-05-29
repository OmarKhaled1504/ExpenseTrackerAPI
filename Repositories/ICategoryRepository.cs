using System;
using ExpenseTrackerAPI.Dtos.CategoriesDtos;
using ExpenseTrackerAPI.Entities;

namespace ExpenseTrackerAPI.Repositories;

public interface ICategoryRepository
{
    public Task<Category> CreateCategoryAsync(Category newCategory);
    public void DeleteCategoryAsync(Category existingCategory);
    public Task<List<Category>> GetCategoriesAsync(int pageNumber, int pageSize);
    public Task<Category?> GetCategoryAsync(int id);
    public Task<Category?> GetCategoryByNameAsync(string name);
    public Task<int> GetTotalCategoriesCountAsync();
}
