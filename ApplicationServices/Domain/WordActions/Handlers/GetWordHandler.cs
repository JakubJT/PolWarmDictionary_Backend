using MediatR;
using DAL;
using ApplicationServices.Domain.WordActions.Queries;
using ApplicationServices.Domain.Models;

namespace ApplicationServices.Domain.WordActions.Handlers;

public class GetWordHandler : IRequestHandler<GetWordQuery, Word>
{
    private readonly WordRepository _wordRepository;
    public GetWordHandler(WordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }
    public async Task<Word> Handle(GetWordQuery request, CancellationToken cancellationToken)
    {
        var word = await _wordRepository.GetWord(request.Word, request.TranslateFromPolish);
        if (word == null) return default(Word);
        return new Domain.Models.Word()
        {
            Id = word.WordId,
            InPolish = word.InPolish,
            InWarmian = word.InWarmian,
            PartOfSpeech = word.PartOfSpeech.Name
        };
    }
}