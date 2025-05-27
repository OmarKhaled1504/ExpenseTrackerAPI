using System;
using ExpenseTrackerAPI.Dtos.CategoriesDtos;
using ExpenseTrackerAPI.Entities;

namespace ExpenseTrackerAPI.Mapping;

public static class CategoryMappingExtensions
{
    public static CategoryDto ToDto(this Category category, IEnumerable<Expense> filteredExpenses)
    {
        return new CategoryDto(
            category.Id,
            category.Name,
            filteredExpenses.Select(e => e.ToDto()).ToList()
        );
    }
    public static CategoryDto ToDto(this Category category)
    {
        return new CategoryDto(category.Id, category.Name);
    }
    public static Category ToEntity(this CategoryCreateDto dto)
    {
        return new Category
        {
            Name = dto.Name
        };
    }
    public static void Update(this Category category, CategoryUpdateDto dto)
    {
        category.Name = dto.Name;
    }
}
