using Identity.Application.Commons.Bases;
using Identity.Application.Interfaces.Services;
using MediatR;

namespace Identity.Application.UseCases.UserRoles.Commands.DeleteCommand;

public class DeleteUserRoleHandler : IRequestHandler<DeleteUserRoleCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserRoleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<bool>> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existsUserRole = await _unitOfWork.UserRole.GetByIdAsync(request.UserRoleId);

            if (existsUserRole is null)
            {
                response.IsSuccess = false;
                response.Message = "El rol del usuario no existe en la base de datos.";
                return response;
            }

            await _unitOfWork.UserRole.DeleteAsync(request.UserRoleId);
            await _unitOfWork.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = "Eliminación exitosa.";
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
