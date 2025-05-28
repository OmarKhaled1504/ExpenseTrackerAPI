namespace ExpenseTrackerAPI.Dtos.ExpensesDtos;

public record class ExpenseUpdateDto(
    string Name,
    string Description,
    decimal Amount,
    int CategoryId
);
