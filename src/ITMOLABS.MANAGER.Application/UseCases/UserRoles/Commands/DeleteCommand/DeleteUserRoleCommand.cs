using Identity.Application.Commons.Bases;
using MediatR;

namespace Identity.Application.UseCases.UserRoles.Commands.DeleteCommand;

public class DeleteUserRoleCommand : IRequest<BaseResponse<bool>>
{
    public int UserRoleId { get; init; }
}
