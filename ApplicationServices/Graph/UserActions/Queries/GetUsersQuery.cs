using MediatR;
using ApplicationServices.Domain.Models;

namespace ApplicationServices.Graph.UserActions.Queries;

public class GetUsersQuery : IRequest<List<User>>
{
    public List<User>? UsersFromDB { get; set; }
}