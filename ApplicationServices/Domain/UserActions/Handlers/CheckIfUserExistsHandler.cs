using MediatR;
using DAL;
using ApplicationServices.Domain.UserActions.Queries;
using AutoMapper;

namespace ApplicationServices.Domain.UserActions.Handlers;

public class CheckIfUserExistsHandler : IRequestHandler<CheckIfUserExistsQuery, bool>
{
    private readonly UserRepository _userRepository;
    private readonly IMapper _mapper;
    public CheckIfUserExistsHandler(UserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<bool> Handle(CheckIfUserExistsQuery request, CancellationToken cancellationToken)
    {
        bool userAlreadyExists = await _userRepository.CheckIfUserExists(request.UserADId!);
        return userAlreadyExists;
    }
}