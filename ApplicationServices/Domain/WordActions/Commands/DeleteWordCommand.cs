using MediatR;

namespace ApplicationServices.Domain.WordActions.Commands;

public class DeleteWordCommand : IRequest
{
    public int WordId { get; set; }
}