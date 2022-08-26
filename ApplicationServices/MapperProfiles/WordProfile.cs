using AutoMapper;

namespace ApplicationServices.MapperProfiles;

public class WordProfile : Profile
{
    public WordProfile()
    {
        this.CreateMap<DAL.Models.Word, ApplicationServices.Domain.Models.Word>();
    }
}