using Identity.Application.Commons.Bases;
using MediatR;

namespace Identity.Application.UseCases.Roles.Commands.DeleteCommand;

public class DeleteRoleCommand : IRequest<BaseResponse<bool>>
{
    public int RoleId { get; set; }
}
