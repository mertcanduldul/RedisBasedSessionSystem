using Microsoft.AspNetCore.Mvc;
using RedisBasedAuthSystem.Services;

namespace RedisBasedAuthSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly MenuService _menuService;

    public MenuController(MenuService menuService)
    {
        _menuService = menuService;
    }

    [HttpPost("set")]
    public async Task<IActionResult> SetMenu(string role, List<object> menuItems)
    {
        await _menuService.SetMenuAsync(role, menuItems);
        return Ok("Menu set");
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetMenu(string role)
    {
        var menu = await _menuService.GetMenuAsync(role);
        return Ok(menu);
    }
}
