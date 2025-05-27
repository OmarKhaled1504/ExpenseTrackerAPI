using System;
using ExpenseTrackerAPI.Dtos.ExpensesDtos;
using ExpenseTrackerAPI.Entities;

namespace ExpenseTrackerAPI.Mapping;

public static class ExpenseMappingExtension
{
    public static ExpenseDto ToDto(this Expense expense)
    {
        return new ExpenseDto(
            expense.Name,
            expense.Description,
            expense.Category.Name,
            expense.Amount,
            expense.CreatedAt,
            expense.UpdatedAt
        );
    }
}
