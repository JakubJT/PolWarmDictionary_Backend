using MediatR;
using DAL;
using ApplicationServices.Domain.WordGroupActions.Queries;
using AutoMapper;

namespace ApplicationServices.Domain.WordGroupActions.Handlers;

public class CheckIfWordGroupExistsHandler : IRequestHandler<CheckIfWordGroupExistsQuery, bool>
{
    private readonly WordGroupRepository _wordGroupRepository;
    private readonly IMapper _mapper;
    public CheckIfWordGroupExistsHandler(WordGroupRepository wordGroupRepository, IMapper mapper)
    {
        _wordGroupRepository = wordGroupRepository;
        _mapper = mapper;
    }
    public async Task<bool> Handle(CheckIfWordGroupExistsQuery request, CancellationToken cancellationToken)
    {
        bool wordAlreadyExists = await _wordGroupRepository.CheckIfWordGroupExists(request.UserIdentifier!, request.WordGroupName!);
        return wordAlreadyExists;
    }
}