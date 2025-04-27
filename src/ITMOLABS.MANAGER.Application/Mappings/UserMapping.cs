using AutoMapper;
using Identity.Application.Dtos.Commons;
using Identity.Application.Dtos.Users;
using Identity.Application.UseCases.Users.Commands.CreateCommand;
using Identity.Application.UseCases.Users.Commands.UpdateCommand;
using Identity.Domain.Entities;

namespace Identity.Application.Mappings;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserResponseDto>()
            .ForMember(x => x.UserId, y => y
                .MapFrom(x => x.Id))
            .ForMember(x => x.StateDescription, y => y
                .MapFrom(x => x.State == "1" ? "Enabled" : "Disabled"))
            .ReverseMap();

        CreateMap<User, UserByIdResponseDto>()
            .ForMember(x => x.UserId, y => y
                .MapFrom(x => x.Id))
            .ReverseMap();

        CreateMap<CreateUserCommand, User>();
        CreateMap<UpdateUserCommand, User>();

        CreateMap<User, SelectResponseDto>()
            .ForMember(x => x.Code, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Description, x => x.MapFrom(y => y.FirstName + " " + y.LastName))
            .ReverseMap();
    }
}
