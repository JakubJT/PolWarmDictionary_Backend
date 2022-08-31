using MediatR;
using DAL;
using ApplicationServices.Domain.WordActions.Commands;
using AutoMapper;

namespace ApplicationServices.Domain.WordActions.Handlers;

public class EditWordHandler : AsyncRequestHandler<EditWordCommand>
{
    private readonly WordRepository _wordRepository;
    private readonly IMapper _mapper;
    public EditWordHandler(WordRepository wordRepository, IMapper mapper)
    {
        _wordRepository = wordRepository;
        _mapper = mapper;
    }

    protected async override Task Handle(EditWordCommand request, CancellationToken cancellationToken)
    {
        var dalWord = _mapper.Map<DAL.Models.Word>(request);
        await _wordRepository.EditWord(dalWord);
    }
}