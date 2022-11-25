using MediatR;
using ApplicationServices.Domain.Models;

namespace ApplicationServices.Domain.WordGroupActions.Queries;

public class GetAllWordGroupsQuery : IRequest<List<WordGroup>>
{
    public string? UserIdentifier { get; set; }
}