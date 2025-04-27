using Identity.Application.Commons.Bases;
using Identity.Application.Interfaces.Services;
using MediatR;

namespace Identity.Application.UseCases.Users.Commands.RevokeRefreshTokenCommand;

internal sealed class RevokeRefreshTokenHandler(IUnitOfWork unitOfWork) : IRequestHandler<RevokeRefreshTokenCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<BaseResponse<bool>> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            await _unitOfWork.RefreshToken.RevokeRefreshTokenAsync(request.UserId);
            response.IsSuccess = true;
            response.Message = "Revocar el token de actualización con éxito.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
