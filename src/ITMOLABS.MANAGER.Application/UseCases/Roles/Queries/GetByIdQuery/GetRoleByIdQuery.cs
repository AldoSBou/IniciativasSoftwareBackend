using Identity.Application.Commons.Bases;
using Identity.Application.Dtos.Roles;
using MediatR;

namespace Identity.Application.UseCases.Roles.Queries.GetByIdQuery;

public class GetRoleByIdQuery : IRequest<BaseResponse<RoleByIdResponseDto>>
{
    public int RoleId { get; set; }
}
