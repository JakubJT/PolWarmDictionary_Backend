using MediatR;
using ApplicationServices.Domain;
using DAL;
using ApplicationServices.Domain.Models;
using ApplicationServices.Domain.PartOfSpeechActions.Queries;
using AutoMapper;

namespace ApplicationServices.Domain.PartOfSpeechActions.Handlers;

public class GetPartOfSpeechHandler : IRequestHandler<GetPartOfSpeechQuery, PartOfSpeech>
{
    private readonly PartOfSpeechRepository _partOfSpeechRepository;
    private readonly IMapper _mapper;
    public GetPartOfSpeechHandler(PartOfSpeechRepository partOfSpeechRepository, IMapper mapper)
    {
        _partOfSpeechRepository = partOfSpeechRepository;
        _mapper = mapper;
    }
    public async Task<PartOfSpeech> Handle(GetPartOfSpeechQuery request, CancellationToken cancellationToken)
    {
        var partOfSpeech = await _partOfSpeechRepository.GetItemByName(request.Name);
        return _mapper.Map<ApplicationServices.Domain.Models.PartOfSpeech>(partOfSpeech);
    }
}