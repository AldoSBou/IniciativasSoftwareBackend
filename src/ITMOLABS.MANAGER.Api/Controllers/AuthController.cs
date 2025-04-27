using Identity.Application.UseCases.Users.Commands.LoginRefreshTokenCommand;
using Identity.Application.UseCases.Users.Commands.RevokeRefreshTokenCommand;
using Identity.Application.UseCases.Users.Queries.LoginQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginQuery request)
    {
        var response = await _sender.Send(request);
        return Ok(response);
    }

    [HttpPost("LoginRefreshToken")]
    public async Task<IActionResult> LoginRefreshToken([FromBody] LoginRefreshTokenCommand request)
    {
        var response = await _sender.Send(request);
        return Ok(response);
    }

    [HttpDelete("RevokeRefreshToken/{userId:int}")]
    public async Task<IActionResult> RevokeRefreshToken(int userId)
    {
        var response = await _sender.Send(new RevokeRefreshTokenCommand { UserId = userId });
        return Ok(response);
    }
}
