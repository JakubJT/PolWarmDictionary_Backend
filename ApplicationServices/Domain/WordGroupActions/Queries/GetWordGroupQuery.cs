using MediatR;
using ApplicationServices.Domain.Models;

namespace ApplicationServices.Domain.WordGroupActions.Queries;

public class GetWordGroupQuery : IRequest<WordGroup>
{
    public int WordGroupId { get; set; }
}