using System;
using ExpenseTrackerAPI.Entities;

namespace ExpenseTrackerAPI.Repositories;

public interface ICategoryRepository
{
    public Task<List<Category>> GetCategoriesAsync(int pageNumber, int pageSize);
    public Task<int> GetTotalCategoriesCountAsync();
}
