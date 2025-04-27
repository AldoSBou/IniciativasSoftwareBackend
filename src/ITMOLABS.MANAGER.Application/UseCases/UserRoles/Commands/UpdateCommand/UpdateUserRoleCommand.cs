using Identity.Application.Commons.Bases;
using MediatR;

namespace Identity.Application.UseCases.UserRoles.Commands.UpdateCommand;

public class UpdateUserRoleCommand : IRequest<BaseResponse<bool>>
{
    public int UserRoleId {  get; init; }
    public int UserId { get; init; }
    public int RoleId { get; init; }
    public string? State { get; init; }
}
