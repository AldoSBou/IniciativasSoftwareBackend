using Identity.Application.Commons.Bases;
using Identity.Application.Dtos.Permissions;
using MediatR;

namespace Identity.Application.UseCases.Permissions.Queries.GetByIdQuery;

public class GetPermissionsByRoleIdQuery : IRequest<BaseResponse<IEnumerable<PermissionsByRoleResponseDto>>>
{
    public int? RoleId {  get; set; } 
}
