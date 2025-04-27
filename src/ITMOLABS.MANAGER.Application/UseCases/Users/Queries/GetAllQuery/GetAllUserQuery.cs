using Identity.Application.Commons.Bases;
using Identity.Application.Dtos.Users;
using MediatR;

namespace Identity.Application.UseCases.Users.Queries.GetAllQuery;

public class GetAllUserQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<UserResponseDto>>> { }
