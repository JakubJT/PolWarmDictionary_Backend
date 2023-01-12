using MediatR;
using DAL;
using ApplicationServices.Domain.Models;
using ApplicationServices.Domain.UserActions.Queries;
using AutoMapper;

namespace ApplicationServices.Domain.UserActions.Handlers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<User>>
{
    private readonly UserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetAllUsersHandler(UserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllUsers();
        var domainUsers = _mapper.Map<List<ApplicationServices.Domain.Models.User>>(users);
        return domainUsers;
    }
}