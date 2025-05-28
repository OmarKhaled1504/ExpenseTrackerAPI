namespace ExpenseTrackerAPI.Dtos.ExpensesDtos;

public record class ExpenseDto(
    int Id,
    string Name,
    string? Description,
    string Category,
    decimal Amount,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
