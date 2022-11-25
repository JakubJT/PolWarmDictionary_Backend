using MediatR;
using ApplicationServices.Domain.Models;

namespace ApplicationServices.Domain.WordGroupActions.Commands;

public class CreateWordGroupCommand : IRequest
{
    public WordGroup? WordGroup { get; set; }
}