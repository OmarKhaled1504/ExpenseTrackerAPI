using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Dtos.UsersDtos;

public record class RegisterDto(
    [Required, StringLength(50, MinimumLength = 3)]string Username,
    [Required, EmailAddress]
string Email,
    [Required, StringLength(100, MinimumLength = 6)]string Password
);
