using MediatR;
using ApplicationServices.Domain.Models;

namespace ApplicationServices.Domain.WordActions.Commands;

public class DeleteWordCommand : IRequest
{
    public int WordId { get; set; }
}