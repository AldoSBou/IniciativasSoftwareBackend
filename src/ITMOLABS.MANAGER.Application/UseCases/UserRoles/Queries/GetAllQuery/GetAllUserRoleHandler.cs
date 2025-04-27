using AutoMapper;
using Identity.Application.Commons.Bases;
using Identity.Application.Dtos.UserRole;
using Identity.Application.Interfaces.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Helper = Identity.Application.Helpers.Helpers;

namespace Identity.Application.UseCases.UserRoles.Queries.GetAllQuery;

public sealed class GetAllUserRoleHandler :
    IRequestHandler<GetAllUserRoleQuery, BaseResponse<IEnumerable<UserRoleResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _orderingQuery;

    public GetAllUserRoleHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery orderingQuery)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _orderingQuery = orderingQuery;
    }

    public async Task<BaseResponse<IEnumerable<UserRoleResponseDto>>> Handle(GetAllUserRoleQuery request,
        CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<UserRoleResponseDto>>();

        try
        {
            var userRoles = _unitOfWork.UserRole.GetAllQueryable()
                    .Include(x => x.User)
                    .Include(x => x.Role)
                    .AsQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        userRoles = userRoles.Where(x => x.User.FirstName.Contains(request.TextFilter));
                        break;
                    case 2:
                        userRoles = userRoles.Where(x => x.Role.Name.Contains(request.TextFilter));
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                var stateFilter = Helper.SplitStateFilter(request.StateFilter);
                userRoles = userRoles.Where(x => stateFilter.Contains(x.State));
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                userRoles = userRoles.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate) &&
                    x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _orderingQuery.Ordering(request, userRoles).ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await userRoles.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<UserRoleResponseDto>>(items);
            response.Message = "Consulta existosa.";
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
