using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Dtos.CategoriesDtos;

public record class CategoryCreateDto(
    [Required] [StringLength(50)]string Name
);
