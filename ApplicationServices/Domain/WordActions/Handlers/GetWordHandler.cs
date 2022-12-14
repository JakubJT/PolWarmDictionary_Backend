using MediatR;
using DAL;
using ApplicationServices.Domain.WordActions.Queries;
using ApplicationServices.Domain.Models;
using AutoMapper;

namespace ApplicationServices.Domain.WordActions.Handlers;

public class GetWordHandler : IRequestHandler<GetWordQuery, List<Word>>
{
    private readonly WordRepository _wordRepository;
    private readonly IMapper _mapper;

    public GetWordHandler(WordRepository wordRepository, IMapper mapper)
    {
        _wordRepository = wordRepository;
        _mapper = mapper;
    }

    public async Task<List<Word>> Handle(GetWordQuery request, CancellationToken cancellationToken)
    {
        var translationOfWord = await _wordRepository.GetWord(request.Word!, request.TranslateFromPolish);
        return _mapper.Map<List<ApplicationServices.Domain.Models.Word>>(translationOfWord);
    }
}