using MediatR;

namespace ApplicationServices.Domain.WordGroupActions.Commands;

public class DeleteWordGroupCommand : IRequest
{
    public int WordGroupId { get; set; }
}