using MediatR;
using DAL;
using ApplicationServices.Domain.WordActions.Queries;
using ApplicationServices.Domain.Models;
using AutoMapper;

namespace ApplicationServices.Domain.WordActions.Handlers;

public class CheckIfWordExistsHandler : IRequestHandler<CheckIfWordExistsQuery, bool>
{
    private readonly WordRepository _wordRepository;
    private readonly IMapper _mapper;
    public CheckIfWordExistsHandler(WordRepository wordRepository, IMapper mapper)
    {
        _wordRepository = wordRepository;
        _mapper = mapper;
    }
    public async Task<bool> Handle(CheckIfWordExistsQuery request, CancellationToken cancellationToken)
    {
        var dalWord = _mapper.Map<DAL.Models.Word>(request);
        bool wordAlreadyExists = await _wordRepository.CheckIfWordExists(dalWord);
        return wordAlreadyExists;
    }
}