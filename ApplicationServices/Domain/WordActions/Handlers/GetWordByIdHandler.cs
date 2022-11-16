using MediatR;
using DAL;
using ApplicationServices.Domain.WordActions.Queries;
using ApplicationServices.Domain.Models;
using AutoMapper;

namespace ApplicationServices.Domain.WordActions.Handlers;

public class GetWordByIdHandler : IRequestHandler<GetWordByIdQuery, (Word Word, int? PartOfSpeechId)>
{
    private readonly WordRepository _wordRepository;
    private readonly IMapper _mapper;
    public GetWordByIdHandler(WordRepository wordRepository, IMapper mapper)
    {
        _wordRepository = wordRepository;
        _mapper = mapper;
    }
    public async Task<(Word Word, int? PartOfSpeechId)> Handle(GetWordByIdQuery request, CancellationToken cancellationToken)
    {
        var word = await _wordRepository.GetWordById(request.WordId);
        if (word == null) return default;
        return (_mapper.Map<ApplicationServices.Domain.Models.Word>(word), word.PartOfSpeechId);
    }
}