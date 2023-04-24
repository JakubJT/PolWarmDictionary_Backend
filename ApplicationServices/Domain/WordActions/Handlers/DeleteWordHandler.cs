using MediatR;
using DAL;
using ApplicationServices.Domain.WordActions.Commands;

namespace ApplicationServices.Domain.WordActions.Handlers;

public class DeleteWordHandler : IRequestHandler<DeleteWordCommand>
{
    private readonly WordRepository _wordRepository;
    public DeleteWordHandler(WordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }
    public async Task Handle(DeleteWordCommand request, CancellationToken cancellationToken)
    {
        await _wordRepository.DeleteWord(request.WordId);
    }
}