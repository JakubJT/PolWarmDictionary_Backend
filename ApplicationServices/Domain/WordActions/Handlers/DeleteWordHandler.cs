using MediatR;
using DAL;
using ApplicationServices.Domain.WordActions.Commands;

namespace ApplicationServices.Domain.WordActions.Handlers;

public class DeleteWordHandler : AsyncRequestHandler<DeleteWordCommand>
{
    private readonly WordRepository _wordRepository;
    public DeleteWordHandler(WordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }
    protected async override Task Handle(DeleteWordCommand request, CancellationToken cancellationToken)
    {
        await _wordRepository.DeleteWord(request.WordId);
    }
}