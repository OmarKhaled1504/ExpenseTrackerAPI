using System;
using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Dtos.CategoriesDtos;
using ExpenseTrackerAPI.Entities;
using ExpenseTrackerAPI.Mapping;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ExpenseContext _context;

    public CategoryRepository(ExpenseContext context)
    {
        _context = context;
    }

    public async Task<Category> CreateCategoryAsync(Category newCategory)
    {
        await _context.Categories.AddAsync(newCategory);
        return newCategory;
    }
    public async Task<Category?> GetCategoryByNameAsync(string name)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(cat => cat.Name == name);
        return category is null ? null : category;
    }

    public async Task<List<Category>> GetCategoriesAsync(int pageNumber, int pageSize)
    {
        if (pageSize > 100)
            pageSize = 100;

        var query = _context.Categories.Include(cat => cat.Expenses).AsQueryable();

        return await query
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
    }

    public async Task<Category?> GetCategoryAsync(int id)
    {
        return await _context.Categories.Include(category => category.Expenses).FirstOrDefaultAsync(category => category.Id == id);
    }


    public async Task<int> GetTotalCategoriesCountAsync()
    {
        return await _context.Categories.CountAsync();
    }

    public void DeleteCategoryAsync(Category category)
    {
        _context.Categories.Remove(category);
    }
}
