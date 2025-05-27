using ExpenseTrackerAPI.Dtos.UsersDtos;
using ExpenseTrackerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(typeof(TokenDto), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<TokenDto?>> Register(RegisterDto dto)
    {
        var (token, errors) = await _authService.RegisterAsync(dto);
        return token is null ? BadRequest(errors) : Ok(token);
    }

    [HttpPost]
    [Route("login")]
    [ProducesResponseType(typeof(TokenDto), 200)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<TokenDto?>> Login(LoginDto dto)
    {
        var (token, errors) = await _authService.LoginAsync(dto);
        return token is null ? Unauthorized(errors) : Ok(token);
    }

}
