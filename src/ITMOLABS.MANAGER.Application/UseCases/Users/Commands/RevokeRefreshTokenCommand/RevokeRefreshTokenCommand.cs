using Identity.Application.Commons.Bases;
using MediatR;

namespace Identity.Application.UseCases.Users.Commands.RevokeRefreshTokenCommand;

public class RevokeRefreshTokenCommand : IRequest<BaseResponse<bool>>
{
    public int UserId { get; set; }
}
