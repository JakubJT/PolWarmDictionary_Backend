using MediatR;
using DAL;
using ApplicationServices.Domain.Models;
using ApplicationServices.Domain.WordActions.Queries;
using AutoMapper;

namespace ApplicationServices.Domain.WordActions.Handlers;

public class GetWordsHandler : IRequestHandler<GetWordsQuery, Words>
{
    private readonly WordRepository _wordRepository;
    private readonly IMapper _mapper;

    public GetWordsHandler(WordRepository wordRepository, IMapper mapper)
    {
        _wordRepository = wordRepository;
        _mapper = mapper;
    }

    public async Task<Words> Handle(GetWordsQuery request, CancellationToken cancellationToken)
    {
        var (words, numbeOfPages) = await _wordRepository.GetWords(request.AscendingOrder, request.SortBy, request.PageNumber, request.WordsPerPage);
        var domainWords = _mapper.Map<List<ApplicationServices.Domain.Models.Word>>(words);
        return new Words() { WordList = domainWords, NumbeOfPages = numbeOfPages };
    }
}