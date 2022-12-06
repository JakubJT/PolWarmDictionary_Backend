using MediatR;

namespace ApplicationServices.Domain.UserActions.Queries;

public class CheckIfUserExistsQuery : IRequest<bool>
{
    public string? UserADId { get; set; }
}