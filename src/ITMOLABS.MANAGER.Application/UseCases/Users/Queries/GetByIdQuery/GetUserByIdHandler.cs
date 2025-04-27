using AutoMapper;
using Identity.Application.Commons.Bases;
using Identity.Application.Dtos.Users;
using Identity.Application.Interfaces.Services;
using MediatR;

namespace Identity.Application.UseCases.Users.Queries.GetByIdQuery;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, BaseResponse<UserByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<UserByIdResponseDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<UserByIdResponseDto>();

        try
        {
            var user = await _unitOfWork.User.GetByIdAsync(request.UserId);

            if (user is null)
            {
                response.IsSuccess = false;
                response.Message = "No se encontraron registros.";
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<UserByIdResponseDto>(user);
            response.Message = "Consulta exitosa";
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
