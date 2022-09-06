using MediatR;
using ApplicationServices.Domain.Models;

namespace ApplicationServices.Domain.WordActions.Queries;

public class GetWordByIdQuery : IRequest<(Word Word, int? PartOfSpeechId)>
{
    public int WordId { get; set; }
}