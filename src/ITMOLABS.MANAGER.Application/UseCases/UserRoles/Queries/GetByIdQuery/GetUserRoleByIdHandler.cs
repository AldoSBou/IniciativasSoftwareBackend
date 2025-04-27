using AutoMapper;
using Identity.Application.Commons.Bases;
using Identity.Application.Dtos.UserRole;
using Identity.Application.Interfaces.Services;
using MediatR;

namespace Identity.Application.UseCases.UserRoles.Queries.GetByIdQuery;

public class GetUserRoleByIdHandler : IRequestHandler<GetUserRoleByIdQuery, BaseResponse<UserRoleByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserRoleByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<UserRoleByIdResponseDto>> Handle(GetUserRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<UserRoleByIdResponseDto>();

        try
        {
            var userRole = await _unitOfWork.UserRole.GetByIdAsync(request.UserRoleId);

            if (userRole is null)
            {
                response.IsSuccess = false;
                response.Message = "No se encontraron registros.";
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<UserRoleByIdResponseDto>(userRole);
            response.Message = "Consulta exitosa";
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
