using MediatR;
using DAL;
using ApplicationServices.Domain.WordActions.Commands;
using ApplicationServices.Domain.Models;

namespace ApplicationServices.Domain.WordActions.Handlers;

public class CreateWordHandler : AsyncRequestHandler<CreateWordCommand>
{
    private readonly WordRepository _wordRepository;
    public CreateWordHandler(WordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }
    protected async override Task Handle(CreateWordCommand request, CancellationToken cancellationToken)
    {
        var dalWord = new DAL.Models.Word()
        {
            WordId = request.Word.Id,
            InPolish = request.Word.InPolish,
            InWarmian = request.Word.InWarmian,
            PartOfSpeechId = request.PartOfSpeechId,
            AuthorId = 1
        };
        await _wordRepository.EditWord(dalWord);
    }
}