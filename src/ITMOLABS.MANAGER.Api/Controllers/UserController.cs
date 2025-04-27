using Identity.Application.UseCases.Users.Commands.CreateCommand;
using Identity.Application.UseCases.Users.Commands.DeleteCommand;
using Identity.Application.UseCases.Users.Commands.UpdateCommand;
using Identity.Application.UseCases.Users.Queries.GetAllQuery;
using Identity.Application.UseCases.Users.Queries.GetByIdQuery;
using Identity.Application.UseCases.Users.Queries.GetSelectQuery;
using Identity.Application.UseCases.Users.Queries.UserRolePermissionsQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> UserList([FromQuery] GetAllUserQuery query)
    {
        var response = await _sender.Send(query);
        return Ok(response);
    }

    [HttpGet("Select")]
    public async Task<IActionResult> UserSelect()
    {
        var response = await _sender.Send(new GetUserSelectQuery());
        return Ok(response);
    }

    [HttpGet("{userId:int}")]
    public async Task<IActionResult> UserById(int userId)
    {
        var response = await _sender.Send(new GetUserByIdQuery { UserId = userId });
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpGet("UserWithRoleAndPermissions/{userId:int}")]
    public async Task<IActionResult> UserWithRoleAndPermissions(int userId)
    {
        var response = await _sender.Send(new GetUserWithRoleAndPermissionsQuery { UserId = userId });
        return Ok(response);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> UserCreate([FromBody] CreateUserCommand command)
    {
        var response = await _sender.Send(command);
        return Ok(response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> UserUpdate([FromBody] UpdateUserCommand command)
    {
        var response = await _sender.Send(command);
        return Ok(response);
    }

    [HttpPut("Delete/{userId:int}")]
    public async Task<IActionResult> UserDelete(int userId)
    {
        var response = await _sender.Send(new DeleteUserCommand() { UserId = userId });
        return Ok(response);
    }
}
