using MediatR;
using DAL;
using ApplicationServices.Domain.Models;
using ApplicationServices.Domain.WordGroupActions.Queries;
using AutoMapper;

namespace ApplicationServices.Domain.WordGroupActions.Handlers;

public class GetAllWordGroupsHandler : IRequestHandler<GetAllWordGroupsQuery, List<WordGroup>>
{
    private readonly WordGroupRepository _wordGroupRepository;
    private readonly IMapper _mapper;

    public GetAllWordGroupsHandler(WordGroupRepository wordGroupRepository, IMapper mapper)
    {
        _wordGroupRepository = wordGroupRepository;
        _mapper = mapper;
    }

    public async Task<List<WordGroup>> Handle(GetAllWordGroupsQuery request, CancellationToken cancellationToken)
    {
        var wordGroups = await _wordGroupRepository.GetAllWordGroups(request.UserIdentifier);
        var domainWordGroups = _mapper.Map<List<ApplicationServices.Domain.Models.WordGroup>>(wordGroups);
        return domainWordGroups;
    }
}