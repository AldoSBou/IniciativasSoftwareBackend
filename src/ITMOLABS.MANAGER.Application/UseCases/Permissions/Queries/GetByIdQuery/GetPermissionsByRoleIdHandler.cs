using AutoMapper;
using Identity.Application.Commons.Bases;
using Identity.Application.Constants;
using Identity.Application.Dtos.Permissions;
using Identity.Application.Interfaces.Services;
using MediatR;

namespace Identity.Application.UseCases.Permissions.Queries.GetByIdQuery;

public class GetPermissionsByRoleIdHandler : IRequestHandler<GetPermissionsByRoleIdQuery, BaseResponse<IEnumerable<PermissionsByRoleResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPermissionsByRoleIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<PermissionsByRoleResponseDto>>> Handle(GetPermissionsByRoleIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<PermissionsByRoleResponseDto>>();
        var permissionsResult = new List<PermissionsByRoleResponseDto>();

        try
        {
            var menus = await _unitOfWork.Menu.GetMenuPermissionByRoleIdAsync(request.RoleId);

            if (menus is null)
            {
                response.IsSuccess = false;
                response.Message = GlobalMessages.MESSAGE_QUERY_EMPTY;
                return response;
            }

            foreach (var menu in menus)
            {
                var permissions = await _unitOfWork.Permission.GetPermissionsByMenuId(menu.Id);

                var dto = new PermissionsByRoleResponseDto
                {
                    MenuId = menu.Id,
                    Menu = menu.Name!,
                    Icon = menu.Icon!,
                    Permissions = _mapper.Map<IEnumerable<PermissionsResponseDto>>(permissions)
                };

                if (request.RoleId.HasValue)
                {
                    var rolePermissions = await _unitOfWork.Permission.GetRolePermissionsByMenuId(request.RoleId.Value, menu.Id);
                    foreach (var permission in dto.Permissions)
                    {
                        permission.Selected = rolePermissions.Any(rp => rp.Id == permission.PermissionId);
                    }
                }

                permissionsResult.Add(dto);
            }

            response.IsSuccess = true;
            response.Data = permissionsResult;
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
