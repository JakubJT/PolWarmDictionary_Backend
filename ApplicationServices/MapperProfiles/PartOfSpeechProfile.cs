using AutoMapper;

namespace ApplicationServices.MapperProfiles;

public class PartOfSpeechProfile : Profile
{
    public PartOfSpeechProfile()
    {
        CreateMap<DAL.Models.PartOfSpeech, ApplicationServices.Domain.Models.PartOfSpeech>();
    }
}