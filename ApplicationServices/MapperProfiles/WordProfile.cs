using AutoMapper;

namespace ApplicationServices.MapperProfiles;

public class WordProfile : Profile
{
    public WordProfile()
    {
        CreateMap<DAL.Models.Word, ApplicationServices.Domain.Models.Word>()
            .ForMember(dest => dest.PartOfSpeech, opt => opt.MapFrom(src => src.PartOfSpeech!.Name))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.WordId));

        CreateMap<ApplicationServices.Domain.WordActions.Commands.CreateWordCommand, DAL.Models.Word>()
            .ForMember(dest => dest.InPolish, opt => opt.MapFrom(src => src.Word!.InPolish))
            .ForMember(dest => dest.InWarmian, opt => opt.MapFrom(src => src.Word!.InWarmian))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => 1))
            .ForMember(dest => dest.PartOfSpeechId, opt => opt.MapFrom(src => src.Word!.PartOfSpeechId));

        CreateMap<ApplicationServices.Domain.WordActions.Commands.EditWordCommand, DAL.Models.Word>()
            .ForMember(dest => dest.InPolish, opt => opt.MapFrom(src => src.Word!.InPolish))
            .ForMember(dest => dest.InWarmian, opt => opt.MapFrom(src => src.Word!.InWarmian))
            .ForMember(dest => dest.WordId, opt => opt.MapFrom(src => src.Word!.Id))
            .ForMember(dest => dest.PartOfSpeechId, opt => opt.MapFrom(src => src.Word!.PartOfSpeechId))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => 1));

        CreateMap<ApplicationServices.Domain.WordActions.Queries.CheckIfWordExistsQuery, DAL.Models.Word>()
            .ForMember(dest => dest.InPolish, opt => opt.MapFrom(src => src.Word!.InPolish))
            .ForMember(dest => dest.InWarmian, opt => opt.MapFrom(src => src.Word!.InWarmian))
            .ForMember(dest => dest.PartOfSpeechId, opt => opt.MapFrom(src => src.Word!.PartOfSpeechId));
    }
}