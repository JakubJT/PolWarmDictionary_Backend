using MediatR;
using ApplicationServices.Domain.Models;

namespace ApplicationServices.Domain.WordGroupActions.Queries;

public class CheckIfUserIsAuthorizedQuery : IRequest<bool>
{
    public string? UserIdentifier { get; set; }
    public int WordGroupId { get; set; }
}