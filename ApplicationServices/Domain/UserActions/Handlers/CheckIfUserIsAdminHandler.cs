using MediatR;
using DAL;
using ApplicationServices.Domain.UserActions.Queries;
using AutoMapper;

namespace ApplicationServices.Domain.UserActions.Handlers;

public class CheckIfUserIsAdminHandler : IRequestHandler<CheckIfUserIsAdminQuery, bool>
{
    private readonly UserRepository _userRepository;

    public CheckIfUserIsAdminHandler(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<bool> Handle(CheckIfUserIsAdminQuery request, CancellationToken cancellationToken)
    {
        bool isUserAdmin = await _userRepository.CheckIfUserIsAdmin(request.UserADId!);
        return isUserAdmin;
    }
}