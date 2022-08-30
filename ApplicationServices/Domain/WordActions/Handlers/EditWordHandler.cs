using MediatR;
using DAL;
using ApplicationServices.Domain.WordActions.Commands;
using ApplicationServices.Domain.Models;

namespace ApplicationServices.Domain.WordActions.Handlers;

public class EditWordHandler : AsyncRequestHandler<EditWordCommand>
{
    private readonly WordRepository _wordRepository;
    public EditWordHandler(WordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }
    protected async override Task Handle(EditWordCommand request, CancellationToken cancellationToken)
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