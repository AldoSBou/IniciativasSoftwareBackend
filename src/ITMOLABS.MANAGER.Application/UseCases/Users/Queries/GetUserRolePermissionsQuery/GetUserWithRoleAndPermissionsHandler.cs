using Identity.Application.Commons.Bases;
using Identity.Application.Constants;
using Identity.Application.Dtos.Users;
using Identity.Application.Interfaces.Services;
using MediatR;

namespace Identity.Application.UseCases.Users.Queries.UserRolePermissionsQuery;

public class GetUserWithRoleAndPermissionsHandler : IRequestHandler<GetUserWithRoleAndPermissionsQuery, BaseResponse<UserWithRoleAndPermissionsDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserWithRoleAndPermissionsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<BaseResponse<UserWithRoleAndPermissionsDto>> Handle(GetUserWithRoleAndPermissionsQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<UserWithRoleAndPermissionsDto>();

        try
        {
            var user = await _unitOfWork.User.GetUserWithRoleAndPermissionsAsync(request.UserId);

            if (user is null)
            {
                response.IsSuccess = false;
                response.Message = GlobalMessages.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = user;
            response.Message = GlobalMessages.MESSAGE_QUERY;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
