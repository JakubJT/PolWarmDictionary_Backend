using MediatR;
using DAL;
using ApplicationServices.Domain.WordActions.Commands;
using AutoMapper;

namespace ApplicationServices.Domain.WordActions.Handlers;

public class CreateWordHandler : IRequestHandler<CreateWordCommand>
{
    private readonly WordRepository _wordRepository;
    private readonly IMapper _mapper;

    public CreateWordHandler(WordRepository wordRepository, IMapper mapper)
    {
        _wordRepository = wordRepository;
        _mapper = mapper;
    }

    public async Task Handle(CreateWordCommand request, CancellationToken cancellationToken)
    {
        var dalWord = _mapper.Map<DAL.Models.Word>(request);
        await _wordRepository.CreateWord(dalWord);
    }
}