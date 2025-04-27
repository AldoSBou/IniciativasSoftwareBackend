using Identity.Application.Commons.Bases;
using Identity.Application.Interfaces.Authentication;
using Identity.Application.Interfaces.Services;
using MediatR;

namespace Identity.Application.UseCases.Users.Commands.LoginRefreshTokenCommand;

internal sealed class LoginRefreshTokenHandler : IRequestHandler<LoginRefreshTokenCommand, BaseResponse<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginRefreshTokenHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<BaseResponse<string>> Handle(LoginRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<string>();

        try
        {
            var refreshToken = await _unitOfWork.RefreshToken
                .GetRefreshTokenAsync(request.RefreshToken!);

            if (refreshToken is null || refreshToken.ExpiresOnUtc < DateTime.UtcNow)
            {
                response.IsSuccess = false;
                response.Message = "El token de actualización ha expirado.";
                return response;
            }

            string accessToken = _jwtTokenGenerator.GenerateToken(refreshToken.User);
            refreshToken.Token = _jwtTokenGenerator.GenerateRefreshToken();
            refreshToken.ExpiresOnUtc = DateTime.UtcNow.AddDays(7);

            await _unitOfWork.SaveChangesAsync();

            response.IsSuccess = true;
            response.AccessToken = accessToken;
            response.RefreshToken = refreshToken.Token;
            response.Message = "Token de actualización creado exitosamente.";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
