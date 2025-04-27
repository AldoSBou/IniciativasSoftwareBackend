using AutoMapper;
using Identity.Application.Commons.Bases;
using Identity.Application.Constants;
using Identity.Application.Dtos.Commons;
using Identity.Application.Interfaces.Services;
using MediatR;

namespace Identity.Application.UseCases.Roles.Queries.GetSelectQuery;

public class GetRoleSelectHandler : IRequestHandler<GetRoleSelectQuery, BaseResponse<IEnumerable<SelectResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetRoleSelectHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<SelectResponseDto>>> Handle(GetRoleSelectQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<SelectResponseDto>>();

        try
        {
            var roles = await _unitOfWork.Role.GetAllAsync();

            if (roles is null)
            {
                response.IsSuccess = false;
                response.Message = GlobalMessages.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<IEnumerable<SelectResponseDto>>(roles);
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
