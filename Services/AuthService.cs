using System;
using ExpenseTrackerAPI.Dtos.UsersDtos;
using ExpenseTrackerAPI.Entities;
using ExpenseTrackerAPI.Repositories;

namespace ExpenseTrackerAPI.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    public AuthService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<(TokenDto? Token, IEnumerable<string>? Errors)> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetUserByUserNameAsync(dto.Username);
        if (user is null)
            {
                return (null, new[] { "Invalid username or password." });
            }
        var authenticated = await _userRepository.CheckPasswordAsync(user, dto.Password);
        if (!authenticated)
        {
            return (null, new[] { "Invalid username or password." });
        }
        return (new TokenDto(_tokenService.GenerateToken(user)), null);
    }

    public async Task<(TokenDto? Token, IEnumerable<string>? Errors)> RegisterAsync(RegisterDto dto)
    {
        try
        {
            var (user, errors) = await _userRepository.CreateAsync(new User
            {
                UserName = dto.Username,
                Email = dto.Email
            }, dto.Password);
            if (user is null)
            return (null, errors);
            var token = _tokenService.GenerateToken(user);
            return (new TokenDto(token), null);
        }
        catch (ArgumentException ex)
        {
            return (null, new[] { ex.Message });
        }

    }
}
