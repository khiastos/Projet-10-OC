using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController(UserManager<IdentityUser> users, IJwtTokenService tokens) : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager = users;
    private readonly IJwtTokenService _jwtTokenService = tokens;

    public record RegisterDto(string Email, string Password);
    public record LoginDto(string Email, string Password);

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = new IdentityUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            EmailConfirmed = true
        };
        var res = await _userManager.CreateAsync(user, dto.Password);
        if (!res.Succeeded) return BadRequest(res.Errors.Select(e => e.Description));

        await _userManager.AddToRoleAsync(user, "User");

        return Ok(new { user.Id, user.Email });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user is null) return Unauthorized("Identifiants invalides.");

        var ok = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!ok) return Unauthorized("Identifiants invalides.");

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtTokenService.Create(user, roles);
        return Ok(new { token });
    }
}
