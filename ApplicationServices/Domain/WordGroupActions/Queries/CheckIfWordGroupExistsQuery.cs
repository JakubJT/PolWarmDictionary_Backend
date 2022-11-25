using MediatR;
using ApplicationServices.Domain.Models;

namespace ApplicationServices.Domain.WordGroupActions.Queries;

public class CheckIfWordGroupExistsQuery : IRequest<bool>
{
    public string? WordGroupName { get; set; }
    public string? UserIdentifier { get; set; }
}