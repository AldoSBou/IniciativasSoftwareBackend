using Identity.Application.Commons.Bases;
using MediatR;

namespace Identity.Application.UseCases.UserRoles.Commands.CreateCommand;

public class CreateUserRoleCommand : IRequest<BaseResponse<bool>>
{
    public int UserId { get; init; }
    public int RoleId { get; init; }
    public string? State { get; init; }
}
