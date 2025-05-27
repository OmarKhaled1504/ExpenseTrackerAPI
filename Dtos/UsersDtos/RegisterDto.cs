namespace ExpenseTrackerAPI.Dtos.UsersDtos;

public record class RegisterDto(
    string Username,
    string Email,
    string Password
);
