using Identity.Application.Commons.Bases;
using MediatR;

namespace Identity.Application.UseCases.Users.Queries.LoginQuery;

public class LoginQuery : IRequest<BaseResponse<string>>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
