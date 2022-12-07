using AutoMapper;

namespace ApplicationServices.MapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationServices.Domain.UserActions.Commands.CreateUserCommand, DAL.Models.User>()
            .ForMember(dest => dest.UserADId, opt => opt.MapFrom(src => src.UserADId));
    }
}