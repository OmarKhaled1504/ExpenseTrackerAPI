using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Dtos.ExpensesDtos;

public record class ExpenseUpdateDto(
[Required] [StringLength(50)] string Name,
string? Description,
[Range(0.1, 100000)]decimal Amount,
[Required]int CategoryId
);
