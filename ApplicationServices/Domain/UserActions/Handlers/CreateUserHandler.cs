using MediatR;
using DAL;
using ApplicationServices.Domain.UserActions.Commands;
using AutoMapper;

namespace ApplicationServices.Domain.UserActions.Handlers;

public class CreateUserHandler : AsyncRequestHandler<CreateUserCommand>
{
    private readonly UserRepository _userRepository;
    private readonly IMapper _mapper;
    public CreateUserHandler(UserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    protected async override Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var dalUser = _mapper.Map<DAL.Models.User>(new Domain.Models.User() { UserADId = request.UserADId });
        await _userRepository.CreateUser(dalUser);
    }
}