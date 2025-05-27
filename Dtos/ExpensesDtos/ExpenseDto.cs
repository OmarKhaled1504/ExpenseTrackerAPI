namespace ExpenseTrackerAPI.Dtos.ExpensesDtos;

public record class ExpenseDto(
    string Name,
    string Description,
    string Category,
    decimal Amount,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
