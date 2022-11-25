using MediatR;
using DAL;
using ApplicationServices.Domain.WordGroupActions.Queries;
using AutoMapper;

namespace ApplicationServices.Domain.WordGroupActions.Handlers;

public class CheckIfUserIsAuthorizedHandler : IRequestHandler<CheckIfUserIsAuthorizedQuery, bool>
{
    private readonly WordRepository _wordRepository;
    private readonly IMapper _mapper;
    public CheckIfUserIsAuthorizedHandler(WordRepository wordRepository, IMapper mapper)
    {
        _wordRepository = wordRepository;
        _mapper = mapper;
    }
    public async Task<bool> Handle(CheckIfUserIsAuthorizedQuery request, CancellationToken cancellationToken)
    {
        var dalWord = _mapper.Map<DAL.Models.Word>(request);
        bool wordAlreadyExists = await _wordRepository.CheckIfWordExists(dalWord);
        return wordAlreadyExists;
    }
}