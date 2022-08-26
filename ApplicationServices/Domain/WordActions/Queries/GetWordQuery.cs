using MediatR;
using ApplicationServices.Domain.Models;

namespace ApplicationServices.Domain.WordActions.Queries;

public class GetWordQuery : IRequest<Word>
{
    public string Word { get; set; }
    public bool TranslateFromPolish { get; set; }
}