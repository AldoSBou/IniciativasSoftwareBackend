using Identity.Application.Commons.Bases;
using Identity.Application.Dtos.Users;
using MediatR;

namespace Identity.Application.UseCases.Users.Queries.GetByIdQuery;

public class GetUserByIdQuery : IRequest<BaseResponse<UserByIdResponseDto>>
{
    public int UserId { get; set; }
}
