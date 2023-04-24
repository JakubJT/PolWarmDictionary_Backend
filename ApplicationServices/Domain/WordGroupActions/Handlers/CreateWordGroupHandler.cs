using MediatR;
using DAL;
using ApplicationServices.Domain.WordGroupActions.Commands;
using AutoMapper;

namespace ApplicationServices.Domain.WordGroupActions.Handlers;

public class CreateWordGroupHandler : IRequestHandler<CreateWordGroupCommand>
{
    private readonly WordGroupRepository _wordGroupRepository;
    private readonly IMapper _mapper;

    public CreateWordGroupHandler(WordGroupRepository wordGroupRepository, IMapper mapper)
    {
        _wordGroupRepository = wordGroupRepository;
        _mapper = mapper;
    }

    public async Task Handle(CreateWordGroupCommand request, CancellationToken cancellationToken)
    {
        var dalWordGroup = _mapper.Map<DAL.Models.WordGroup>(request);
        await _wordGroupRepository.CreateWordGroup(dalWordGroup);
    }
}