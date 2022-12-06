using MediatR;
using ApplicationServices.Domain.Models;

namespace ApplicationServices.Domain.WordGroupActions.Queries;

public class CheckIfUserIsAuthorizedQuery : IRequest<bool>
{
    public string? UserADId { get; set; }
    public int WordGroupId { get; set; }
}