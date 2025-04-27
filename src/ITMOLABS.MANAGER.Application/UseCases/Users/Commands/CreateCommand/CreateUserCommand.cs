using Identity.Application.Commons.Bases;
using MediatR;

namespace Identity.Application.UseCases.Users.Commands.CreateCommand;

public class CreateUserCommand : IRequest<BaseResponse<bool>>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? State { get; set; }
}
