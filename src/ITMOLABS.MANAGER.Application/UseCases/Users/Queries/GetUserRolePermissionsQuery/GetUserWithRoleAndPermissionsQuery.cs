using Identity.Application.Commons.Bases;
using Identity.Application.Dtos.Users;
using MediatR;

namespace Identity.Application.UseCases.Users.Queries.UserRolePermissionsQuery;

public class GetUserWithRoleAndPermissionsQuery : IRequest<BaseResponse<UserWithRoleAndPermissionsDto>>
{
    public int UserId { get; set; }
}
