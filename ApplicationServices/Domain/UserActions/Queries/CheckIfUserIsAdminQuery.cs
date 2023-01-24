using MediatR;

namespace ApplicationServices.Domain.UserActions.Queries;

public class CheckIfUserIsAdminQuery : IRequest<bool>
{
    public string? UserADId { get; set; }
}