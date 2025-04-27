using Identity.Application.Commons.Bases;
using MediatR;

namespace Identity.Application.UseCases.Users.Commands.DeleteCommand;

public class DeleteUserCommand : IRequest<BaseResponse<bool>>
{
    public int UserId { get; set; }
}
