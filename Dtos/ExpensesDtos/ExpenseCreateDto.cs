namespace ExpenseTrackerAPI.Dtos.ExpensesDtos;

public record class ExpenseCreateDto(
string Name,
string? Description,
decimal Amount,
int CategoryId
);
