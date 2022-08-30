using MediatR;
using ApplicationServices.Domain.Models;

namespace ApplicationServices.Domain.WordActions.Commands;

public class EditWordCommand : IRequest
{
    public Word Word { get; set; }
    public int PartOfSpeechId { get; set; }
}