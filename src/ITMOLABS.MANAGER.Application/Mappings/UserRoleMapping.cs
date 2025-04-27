using AutoMapper;
using Identity.Application.Dtos.UserRole;
using Identity.Application.UseCases.UserRoles.Commands.CreateCommand;
using Identity.Application.UseCases.UserRoles.Commands.UpdateCommand;
using Identity.Domain.Entities;

namespace Identity.Application.Mappings;

public class UserRoleMapping : Profile
{
    public UserRoleMapping()
    {
        CreateMap<UserRole, UserRoleResponseDto>()
            .ForMember(x => x.UserRoleId, y => y.MapFrom(x => x.Id))
            .ForMember(x => x.User, y => y.MapFrom(x => x.User.FirstName + " " + x.User.LastName))
            .ForMember(x => x.Role, y => y.MapFrom(x => x.Role.Name))
            .ForMember(x => x.StateDescription, y => y.MapFrom(x => x.State == "1" ? "Enabled" : "Disabled"))
            .ReverseMap();

        CreateMap<UserRole, UserRoleByIdResponseDto>()
            .ForMember(x => x.UserRoleId, y => y.MapFrom(x => x.Id))
            .ReverseMap();

        CreateMap<CreateUserRoleCommand, UserRole>();
        CreateMap<UpdateUserRoleCommand, UserRole>();
    }
}
