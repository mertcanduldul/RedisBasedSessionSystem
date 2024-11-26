using Microsoft.AspNetCore.Mvc;
using RedisBasedAuthSystem.Services;
using RedisBasedSessionSystem.DTOs;

namespace RedisBasedSessionSystem.Controller;

[ApiController]
[Route("api/[controller]/[action]")]
public class MainController : ControllerBase
{
    private readonly MenuService _menuService;
    private readonly SessionService _sessionService;

    public MainController(MenuService menuService, SessionService sessionService)
    {
        _menuService = menuService;
        _sessionService = sessionService;
    }

    [HttpPost("set-menu")]
    public async Task<IActionResult> SetMenu([FromBody] SetMenuRequestDto request)
    {
        await _menuService.SaveMenuAsync(request.Role, request.MenuItems);
        return Ok(new { Message = "Menü başarıyla kaydedildi." });
    }

    [HttpPost("get-menu")]
    public async Task<IActionResult> GetMenu([FromBody] GetMenuRequestDto request)
    {
        var menu = await _menuService.GetMenuAsync(request.Role);
        return menu != null ? Ok(menu) : NotFound(new { Message = "Menü bulunamadı." });
    }

    [HttpPost("create-session")]
    public async Task<IActionResult> CreateSession([FromBody] CreateSessionRequestDto request)
    {
        await _sessionService.CreateSessionAsync(request.SessionKey, request.SessionData);
        return Ok(new { Message = "Oturum başarıyla oluşturuldu." });
    }

    [HttpPost("get-session")]
    public async Task<IActionResult> GetSession([FromBody] GetSessionRequestDto request)
    {
        var session = await _sessionService.GetSessionAsync(request.SessionKey);
        return session != null ? Ok(session) : NotFound(new { Message = "Oturum bulunamadı." });
    }
}
