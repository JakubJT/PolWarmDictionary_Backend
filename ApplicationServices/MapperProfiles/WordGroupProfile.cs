using AutoMapper;

namespace ApplicationServices.MapperProfiles;

public class WordGroupProfile : Profile
{
    public WordGroupProfile()
    {
        CreateMap<DAL.Models.WordGroup, ApplicationServices.Domain.Models.WordGroup>();

        CreateMap<ApplicationServices.Domain.WordGroupActions.Commands.CreateWordGroupCommand, DAL.Models.WordGroup>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.WordGroup!.Name))
            .ForMember(dest => dest.UserADId, opt => opt.MapFrom(src => src.UserADId))
            .ForMember(dest => dest.Words, opt => opt.MapFrom(src => src.WordGroup!.Words));

        CreateMap<ApplicationServices.Domain.Models.WordGroup, DAL.Models.WordGroup>();
    }
}