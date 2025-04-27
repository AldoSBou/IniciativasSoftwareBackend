using Identity.Application.Commons.Bases;
using Identity.Application.Dtos.UserRole;
using MediatR;

namespace Identity.Application.UseCases.UserRoles.Queries.GetByIdQuery;

public class GetUserRoleByIdQuery : IRequest<BaseResponse<UserRoleByIdResponseDto>>
{
    public int UserRoleId { get; init; }
}
