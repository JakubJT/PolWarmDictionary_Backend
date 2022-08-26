using MediatR;
using ApplicationServices.Domain;
using DAL;
using DAL.Models;
using System.Linq;

namespace ApplicationServices.Domain.PartOfSpeechActions.Handlers;

public class GetPartOfSpeechHandler : IRequestHandler<GetPartOfSpeechRequest, GetPartOfSpeechResponse>
{
    private readonly IRepository<PartOfSpeech> _partOfSpeechRepository;
    public GetPartOfSpeechHandler(IRepository<PartOfSpeech> partOfSpeechRepository)
    {
        _partOfSpeechRepository = partOfSpeechRepository;
    }
    public Task<GetPartOfSpeechResponse> Handle(GetPartOfSpeechRequest request, CancellationToken cancellationToken)
    {
        var partOfSpeech = _partOfSpeechRepository.GetItem(33);
        var domainPartOfSpeech = new ApplicationServices.Domain.Models.PartOfSpeech()
        {
        };

        var response = new GetPartOfSpeechResponse()
        {
            Data = domainPartOfSpeech
        };

        return Task.FromResult(response);
    }
}