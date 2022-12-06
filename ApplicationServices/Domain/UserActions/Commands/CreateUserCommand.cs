using MediatR;

namespace ApplicationServices.Domain.UserActions.Commands;

public class CreateUserCommand : IRequest
{
    public string? UserADId { get; set; }
}