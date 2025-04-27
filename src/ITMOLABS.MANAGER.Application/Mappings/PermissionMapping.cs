using AutoMapper;
using Identity.Application.Dtos.Permissions;
using Identity.Domain.Entities;

namespace Identity.Application.Mappings;

public class PermissionMapping : Profile
{
    public PermissionMapping()
    {
        CreateMap<Permission, PermissionsResponseDto>()
                .ForMember(x => x.PermissionId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.PermissionName, x => x.MapFrom(y => y.Name))
                .ForMember(x => x.PermissionDescription, x => x.MapFrom(y => y.Description))
                .ReverseMap();
    }
}
