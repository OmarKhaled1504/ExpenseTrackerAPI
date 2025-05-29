using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Dtos.UsersDtos;

public record class LoginDto(
    [Required]string Username,
    [Required]string Password
);
