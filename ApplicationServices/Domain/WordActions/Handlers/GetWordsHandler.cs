using MediatR;
using ApplicationServices;
using DAL;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApplicationServices.Domain.Models;
using ApplicationServices.Domain.WordActions.Queries;

namespace ApplicationServices.Domain.WordActions.Handlers;

public class GetWordsHandler : IRequestHandler<GetWordsQuery, List<Word>>
{
    private readonly IRepository<DAL.Models.Word> _wordRepository;
    public GetWordsHandler(IRepository<DAL.Models.Word> wordRepository)
    {
        _wordRepository = wordRepository;
    }
    public Task<List<Word>> Handle(GetWordsQuery request, CancellationToken cancellationToken)
    {
        var words = _wordRepository.GetAllItems().Include(w => w.PartOfSpeech);
        var domainWords = words.Select(x => new Domain.Models.Word()
        {
            InPolish = x.InPolish,
            InWarmian = x.InWarmian,
            PartOfSpeech = x.PartOfSpeech.Name
        }).ToList();

        return Task.FromResult(domainWords);
    }
}