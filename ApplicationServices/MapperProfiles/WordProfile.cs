using AutoMapper;

namespace ApplicationServices.MapperProfiles;

public class WordProfile : Profile
{
    public WordProfile()
    {
        CreateMap<DAL.Models.Word, ApplicationServices.Domain.Models.Word>()
            .ForMember(dest => dest.PartOfSpeech, opt => opt.MapFrom(src => src.PartOfSpeech.Name))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.WordId));
    }
}