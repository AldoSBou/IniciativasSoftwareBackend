using Identity.Application.UseCases.UserRoles.Commands.CreateCommand;
using Identity.Application.UseCases.UserRoles.Commands.DeleteCommand;
using Identity.Application.UseCases.UserRoles.Commands.UpdateCommand;
using Identity.Application.UseCases.UserRoles.Queries.GetAllQuery;
using Identity.Application.UseCases.UserRoles.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserRoleController : ControllerBase
{
    private readonly ISender _sender;

    public UserRoleController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> UserRoleList([FromQuery] GetAllUserRoleQuery query)
    {
        var response = await _sender.Send(query);
        return Ok(response);
    }

    [HttpGet("{userRoleId:int}")]
    public async Task<IActionResult> UserRoleById(int userRoleId)
    {
        var response = await _sender.Send(new GetUserRoleByIdQuery { UserRoleId = userRoleId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> UserRoleCreate([FromBody] CreateUserRoleCommand command)
    {
        var response = await _sender.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> UserRoleUpdate([FromBody] UpdateUserRoleCommand command)
    {
        var response = await _sender.Send(command);
        return Ok(response);
    }

    [HttpPut("Delete/{userRoleId:int}")]
    public async Task<IActionResult> UserRoleDelete(int userRoleId)
    {
        var response = await _sender.Send(new DeleteUserRoleCommand() { UserRoleId = userRoleId });
        return Ok(response);
    }
}
