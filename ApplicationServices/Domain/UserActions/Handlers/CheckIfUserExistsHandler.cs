using MediatR;
using DAL;
using ApplicationServices.Domain.UserActions.Queries;
using AutoMapper;

namespace ApplicationServices.Domain.UserActions.Handlers;

public class CheckIfUserExistsHandler : IRequestHandler<CheckIfUserExistsQuery, bool>
{
    private readonly UserRepository _userRepository;
    public CheckIfUserExistsHandler(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<bool> Handle(CheckIfUserExistsQuery request, CancellationToken cancellationToken)
    {
        bool userAlreadyExists = await _userRepository.CheckIfUserExists(request.UserADId!);
        return userAlreadyExists;
    }
}