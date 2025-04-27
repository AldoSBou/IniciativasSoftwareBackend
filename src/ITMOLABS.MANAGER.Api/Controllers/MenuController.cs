using Identity.Application.UseCases.Menus.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Identity.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class MenuController : ControllerBase
{
    private readonly ISender _sender;
    private readonly HttpContext _httpContext;

    public MenuController(ISender sender, IHttpContextAccessor httpContextAccessor)
    {
        _sender = sender;
        _httpContext = httpContextAccessor.HttpContext!;
    }

    [HttpGet("MenuByUser")]
    public async Task<IActionResult> GetMenuByUserId()
    {
        var userId = _httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var response = await _sender
            .Send(new GetMenuByUserIdQuery() { UserId = int.Parse(userId!) });

        return Ok(response);
    }
}
