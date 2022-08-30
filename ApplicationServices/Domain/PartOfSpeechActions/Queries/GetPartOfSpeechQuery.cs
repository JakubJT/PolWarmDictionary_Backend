using MediatR;
using ApplicationServices.Domain.Models;

namespace ApplicationServices.Domain.PartOfSpeechActions.Queries;

public class GetPartOfSpeechQuery : IRequest<PartOfSpeech>
{
    public string Name { get; set; }
}