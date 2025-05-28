using System;
using ExpenseTrackerAPI.Dtos.ExpensesDtos;
using ExpenseTrackerAPI.Entities;

namespace ExpenseTrackerAPI.Mapping;

public static class ExpenseMappingExtension
{
    public static ExpenseDto ToDto(this Expense expense)
    {
        return new ExpenseDto(
            expense.Id,
            expense.Name,
            expense.Description,
            expense.Category!.Name,
            expense.Amount,
            expense.CreatedAt,
            expense.UpdatedAt
        );
    }

    public static Expense ToEntity(this ExpenseCreateDto dto)
    {
        return new Expense
        {
            Name = dto.Name,
            Description = dto.Description,
            Amount = dto.Amount,
            CatId = dto.CategoryId
        };
    }
    public static void Update(this Expense expense, ExpenseUpdateDto dto)
    {
        expense.Name = dto.Name;
        expense.Description = dto.Description;
        expense.Amount = dto.Amount;
        expense.CatId = dto.CategoryId;
    }
}
