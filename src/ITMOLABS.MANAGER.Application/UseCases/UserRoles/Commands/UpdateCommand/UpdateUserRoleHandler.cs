using AutoMapper;
using Identity.Application.Commons.Bases;
using Identity.Application.Interfaces.Services;
using Identity.Domain.Entities;
using MediatR;

namespace Identity.Application.UseCases.UserRoles.Commands.UpdateCommand;

public class UpdateUserRoleHandler : IRequestHandler<UpdateUserRoleCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUserRoleHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var userRole = _mapper.Map<UserRole>(request);
            userRole.Id = request.UserRoleId;
            _unitOfWork.UserRole.UpdateAsync(userRole);
            await _unitOfWork.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = "Actualización exitosa";
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
