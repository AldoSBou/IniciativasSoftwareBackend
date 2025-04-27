using Identity.Application.Commons.Bases;
using Identity.Application.Dtos.Roles;
using MediatR;

namespace Identity.Application.UseCases.Roles.Queries.GetAllQuery;

public class GetAllRoleQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<RoleResponseDto>>> { }
