using MediatR;
using DAL;
using ApplicationServices.Domain.WordActions.Queries;
using ApplicationServices.Domain.Models;
using AutoMapper;

namespace ApplicationServices.Domain.WordActions.Handlers;

public class GetWordHandler : IRequestHandler<GetWordQuery, Word>
{
    private readonly WordRepository _wordRepository;
    private readonly IMapper _mapper;

    public GetWordHandler(WordRepository wordRepository, IMapper mapper)
    {
        _wordRepository = wordRepository;
        _mapper = mapper;
    }

    public async Task<Word> Handle(GetWordQuery request, CancellationToken cancellationToken)
    {
        var word = await _wordRepository.GetWord(request.Word, request.TranslateFromPolish);
        if (word == null) return default(Word);
        return _mapper.Map<ApplicationServices.Domain.Models.Word>(word);
    }
}