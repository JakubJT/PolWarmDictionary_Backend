using MediatR;
using ApplicationServices.Domain.Models;

namespace ApplicationServices.Domain.UserActions.Queries;

public class GetAllUsersQuery : IRequest<List<User>>
{
}