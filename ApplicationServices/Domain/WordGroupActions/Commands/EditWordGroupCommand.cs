using MediatR;
using ApplicationServices.Domain.Models;

namespace ApplicationServices.Domain.WordGroupActions.Commands;

public class EditWordGroupCommand : IRequest
{
    public WordGroup? WordGroup { get; set; }
}