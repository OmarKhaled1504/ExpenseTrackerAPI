using System;
using ExpenseTrackerAPI.Dtos.UsersDtos;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Services;

public interface IAuthService
{
    public Task<(TokenDto? Token, IEnumerable<string>? Errors)> RegisterAsync(RegisterDto dto);
    public Task<(TokenDto? Token, IEnumerable<string>? Errors)> LoginAsync(LoginDto dto);
}
