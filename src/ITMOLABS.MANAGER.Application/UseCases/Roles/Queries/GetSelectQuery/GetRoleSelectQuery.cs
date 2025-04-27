using Identity.Application.Commons.Bases;
using Identity.Application.Dtos.Commons;
using MediatR;

namespace Identity.Application.UseCases.Roles.Queries.GetSelectQuery;

public class GetRoleSelectQuery : IRequest<BaseResponse<IEnumerable<SelectResponseDto>>>
{
}
