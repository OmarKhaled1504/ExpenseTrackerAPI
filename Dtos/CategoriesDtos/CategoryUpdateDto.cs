using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Dtos.CategoriesDtos;

public record class CategoryUpdateDto([Required] [StringLength(50)]string Name);
