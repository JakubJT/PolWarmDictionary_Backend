using MediatR;
using DAL;
using ApplicationServices.Domain.WordGroupActions.Commands;

namespace ApplicationServices.Domain.WordGroupActions.Handlers;

public class DeleteWordGroupHandler : IRequestHandler<DeleteWordGroupCommand>
{
    private readonly WordGroupRepository _wordGroupRepository;
    public DeleteWordGroupHandler(WordGroupRepository wordGroupRepository)
    {
        _wordGroupRepository = wordGroupRepository;
    }
    public async Task Handle(DeleteWordGroupCommand request, CancellationToken cancellationToken)
    {
        await _wordGroupRepository.DeleteWordGroup(request.WordGroupId);
    }
}