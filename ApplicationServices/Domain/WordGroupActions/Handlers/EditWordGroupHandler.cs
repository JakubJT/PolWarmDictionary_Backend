using MediatR;
using DAL;
using ApplicationServices.Domain.WordGroupActions.Commands;
using AutoMapper;

namespace ApplicationServices.Domain.WordGroupActions.Handlers;

public class EditWordGroupHandler : AsyncRequestHandler<EditWordGroupCommand>
{
    private readonly WordGroupRepository _wordGroupRepository;
    private readonly IMapper _mapper;
    public EditWordGroupHandler(WordGroupRepository wordGroupRepository, IMapper mapper)
    {
        _wordGroupRepository = wordGroupRepository;
        _mapper = mapper;
    }

    protected async override Task Handle(EditWordGroupCommand request, CancellationToken cancellationToken)
    {
        var dalWordGroup = _mapper.Map<DAL.Models.WordGroup>(request.WordGroup);
        await _wordGroupRepository.EditWordGroup(dalWordGroup);
    }
}