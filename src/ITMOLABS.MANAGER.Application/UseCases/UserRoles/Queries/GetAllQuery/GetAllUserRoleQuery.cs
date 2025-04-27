using Identity.Application.Commons.Bases;
using Identity.Application.Dtos.UserRole;
using MediatR;

namespace Identity.Application.UseCases.UserRoles.Queries.GetAllQuery;

public class GetAllUserRoleQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<UserRoleResponseDto>>> { }
