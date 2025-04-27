using Identity.Application.Commons.Bases;
using MediatR;

namespace Identity.Application.UseCases.Users.Commands.LoginRefreshTokenCommand;

public class LoginRefreshTokenCommand : IRequest<BaseResponse<string>>
{
    public string? RefreshToken { get; set; }
}
