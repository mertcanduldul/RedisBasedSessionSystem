using Microsoft.AspNetCore.Mvc;
using RedisBasedAuthSystem.Services;
using RedisBasedSessionSystem.DTOs;

namespace RedisBasedAuthSystem.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserSessionController : ControllerBase
{
    private readonly UserSessionService _userSessionService;

    public UserSessionController(UserSessionService userSessionService)
    {
        _userSessionService = userSessionService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSession(CreateUserSessionRequest request)
    {
        await _userSessionService.SetSessionAsync(request);
        return Ok("Session created");
    }

    [HttpGet("role")]
    public async Task<IActionResult> GetRole(string sessionId)
    {
        var role = await _userSessionService.GetRoleAsync(sessionId);
        if (role == null) return Unauthorized();

        return Ok($"Role: {role}");
    }
}
