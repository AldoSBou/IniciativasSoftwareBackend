using Identity.Application.Commons.Bases;
using Identity.Application.Interfaces.Authentication;
using Identity.Application.Interfaces.Services;
using Identity.Domain.Entities;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace Identity.Application.UseCases.Users.Queries.LoginQuery;

public class LoginHandler : IRequestHandler<LoginQuery, BaseResponse<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<BaseResponse<string>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<string>();

        try
        {
            var user = await _unitOfWork.User.UserByEmailAsync(request.Email);

            if (user is null)
            {
                response.IsSuccess = false;
                response.Message = "El usuario no existe en la base de datos.";
                return response;
            }

            if (!BC.Verify(request.Password, user.Password))
            {
                response.IsSuccess = false;
                response.Message = "La contraseña es incorrecta.";
                return response;
            }

            response.IsSuccess = true;
            response.AccessToken = _jwtTokenGenerator.GenerateToken(user);

            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = _jwtTokenGenerator.GenerateRefreshToken(),
                ExpiresOnUtc = DateTime.UtcNow.AddDays(7)
            };

            _unitOfWork.RefreshToken.CreateToken(refreshToken);
            await _unitOfWork.SaveChangesAsync();
            response.RefreshToken = refreshToken.Token;
            response.Message = "Token generado correctamente";
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
