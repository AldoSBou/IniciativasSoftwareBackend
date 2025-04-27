using Identity.Application.Commons.Bases;
using Identity.Application.Dtos.Commons;
using MediatR;

namespace Identity.Application.UseCases.Users.Queries.GetSelectQuery;

public class GetUserSelectQuery : IRequest<BaseResponse<IEnumerable<SelectResponseDto>>>
{
}
