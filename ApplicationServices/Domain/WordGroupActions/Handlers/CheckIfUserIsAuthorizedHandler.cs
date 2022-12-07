using MediatR;
using DAL;
using ApplicationServices.Domain.WordGroupActions.Queries;
using AutoMapper;

namespace ApplicationServices.Domain.WordGroupActions.Handlers;

public class CheckIfUserIsAuthorizedHandler : IRequestHandler<CheckIfUserIsAuthorizedQuery, bool>
{
    private readonly WordGroupRepository _wordGroupRepository;
    private readonly IMapper _mapper;
    public CheckIfUserIsAuthorizedHandler(WordGroupRepository wordGroupRepository, IMapper mapper)
    {
        _wordGroupRepository = wordGroupRepository;
        _mapper = mapper;
    }
    public async Task<bool> Handle(CheckIfUserIsAuthorizedQuery request, CancellationToken cancellationToken)
    {
        bool userIsAuthorized = await _wordGroupRepository.CheckIfUserIsAuthorized(request.UserADId!, request.WordGroupId);
        return userIsAuthorized;
    }
}