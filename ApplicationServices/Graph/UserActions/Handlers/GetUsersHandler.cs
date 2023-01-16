using MediatR;
using DAL;
using ApplicationServices.Domain.Models;
using ApplicationServices.Graph.UserActions.Queries;
using AutoMapper;

namespace ApplicationServices.Graph.UserActions.Handlers;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<User>>
{
    private readonly GraphClient _graphClient;
    private readonly IMapper _mapper;

    public GetUsersHandler(GraphClient graphClient, IMapper mapper)
    {
        _graphClient = graphClient;
        _mapper = mapper;
    }

    public async Task<List<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = request.UsersFromDB;
        var graphClient = _graphClient.GetClient();
        foreach (var user in users!)
        {
            var userAD = await graphClient.Users[$"{user.ADId}"]
                .Request()
                .Select("displayName,mail")
                .GetAsync();

            user.Email = userAD.Mail;
            user.Name = userAD.DisplayName;
        }
        return users;
    }
}