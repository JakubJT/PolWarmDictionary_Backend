using MediatR;
using ApplicationServices;
using DAL;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApplicationServices.Domain.Models;
using ApplicationServices.Domain.WordActions.Queries;
using AutoMapper;

namespace ApplicationServices.Domain.WordActions.Handlers;

public class GetWordsHandler : IRequestHandler<GetWordsQuery, List<Word>>
{
    private readonly WordRepository _wordRepository;
    private readonly IMapper _mapper;
    public GetWordsHandler(WordRepository wordRepository, IMapper mapper)
    {
        _wordRepository = wordRepository;
        _mapper = mapper;

    }
    public Task<List<Word>> Handle(GetWordsQuery request, CancellationToken cancellationToken)
    {
        var words = _wordRepository.GetAllItems().Include(w => w.PartOfSpeech);
        var domainWords = _mapper.Map<List<ApplicationServices.Domain.Models.Word>>(words);
        return Task.FromResult(domainWords);
    }
}