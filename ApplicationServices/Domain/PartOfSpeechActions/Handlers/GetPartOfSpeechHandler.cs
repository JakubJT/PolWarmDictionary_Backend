using MediatR;
using ApplicationServices.Domain;
using DAL;
using ApplicationServices.Domain.Models;
using ApplicationServices.Domain.PartOfSpeechActions.Queries;

namespace ApplicationServices.Domain.PartOfSpeechActions.Handlers;

public class GetPartOfSpeechHandler : IRequestHandler<GetPartOfSpeechQuery, PartOfSpeech>
{
    private readonly PartOfSpeechRepository _partOfSpeechRepository;
    public GetPartOfSpeechHandler(PartOfSpeechRepository partOfSpeechRepository)
    {
        _partOfSpeechRepository = partOfSpeechRepository;
    }
    public async Task<PartOfSpeech> Handle(GetPartOfSpeechQuery request, CancellationToken cancellationToken)
    {
        var partOfSpeech = await _partOfSpeechRepository.GetItemByName(request.Name);
        return new PartOfSpeech()
        {
            PartOfSpeechId = partOfSpeech.PartOfSpeechId,
            Name = partOfSpeech.Name
        };
    }
}