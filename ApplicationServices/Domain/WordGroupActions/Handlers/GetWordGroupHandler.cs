using MediatR;
using DAL;
using ApplicationServices.Domain.WordGroupActions.Queries;
using ApplicationServices.Domain.Models;
using AutoMapper;

namespace ApplicationServices.Domain.WordGroupActions.Handlers;

public class GetWordGroupHandler : IRequestHandler<GetWordGroupQuery, WordGroup>
{
    private readonly WordGroupRepository _wordGroupRepository;
    private readonly IMapper _mapper;
    public GetWordGroupHandler(WordGroupRepository wordGroupRepository, IMapper mapper)
    {
        _wordGroupRepository = wordGroupRepository;
        _mapper = mapper;
    }
    public async Task<WordGroup> Handle(GetWordGroupQuery request, CancellationToken cancellationToken)
    {
        var word = await _wordGroupRepository.GetWordGroup(request.WordGroupId);
        if (word == null) return default!;
        return _mapper.Map<ApplicationServices.Domain.Models.WordGroup>(word);
    }
}