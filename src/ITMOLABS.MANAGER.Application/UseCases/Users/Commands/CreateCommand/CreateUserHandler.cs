using AutoMapper;
using Identity.Application.Commons.Bases;
using Identity.Application.Interfaces.Services;
using Identity.Domain.Entities;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace Identity.Application.UseCases.Users.Commands.CreateCommand;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var user = _mapper.Map<User>(request);
            user.Password = BC.HashPassword(user.Password);
            await _unitOfWork.User.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = "Registro exitoso";
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
