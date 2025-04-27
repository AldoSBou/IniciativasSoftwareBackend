using AutoMapper;
using Identity.Application.Commons.Bases;
using Identity.Application.Constants;
using Identity.Application.Dtos.Commons;
using Identity.Application.Interfaces.Services;
using MediatR;

namespace Identity.Application.UseCases.Users.Queries.GetSelectQuery;

public class GetUserSelectHandler : IRequestHandler<GetUserSelectQuery, BaseResponse<IEnumerable<SelectResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserSelectHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<SelectResponseDto>>> Handle(GetUserSelectQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<SelectResponseDto>>();

        try
        {
            var users = await _unitOfWork.User.GetAllAsync();

            if (users is null)
            {
                response.IsSuccess = false;
                response.Message = GlobalMessages.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<IEnumerable<SelectResponseDto>>(users);
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
