using AutoMapper;
using Identity.Application.Commons.Bases;
using Identity.Application.Dtos.Menus;
using Identity.Application.Interfaces.Services;
using MediatR;

namespace Identity.Application.UseCases.Menus.Queries.GetByIdQuery;

public class GetMenuByUserIdHandler : IRequestHandler<GetMenuByUserIdQuery, BaseResponse<IEnumerable<MenuResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetMenuByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<MenuResponseDto>>> Handle(GetMenuByUserIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<MenuResponseDto>>();

        try
        {
            var menus = await _unitOfWork.Menu.GetMenuByUserIdAsync(request.UserId);

            if (menus is null)
            {
                response.IsSuccess = false;
                response.Message = "No se encontraron registros.";
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<IEnumerable<MenuResponseDto>>(menus);
            response.Message = "Consulta exitosa.";
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
