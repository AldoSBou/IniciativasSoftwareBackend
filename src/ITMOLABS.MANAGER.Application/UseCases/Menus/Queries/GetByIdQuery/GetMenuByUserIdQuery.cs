using Identity.Application.Commons.Bases;
using Identity.Application.Dtos.Menus;
using MediatR;

namespace Identity.Application.UseCases.Menus.Queries.GetByIdQuery;

public class GetMenuByUserIdQuery : IRequest<BaseResponse<IEnumerable<MenuResponseDto>>>
{
    public int UserId { get; set; }
}
