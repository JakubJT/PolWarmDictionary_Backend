using MediatR;
using DAL;
using ApplicationServices.Domain.UserActions.Commands;
using AutoMapper;

namespace ApplicationServices.Domain.UserActions.Handlers;

public class CreateUserHandler : IRequestHandler<CreateUserCommand>
{
    private readonly UserRepository _userRepository;
    private readonly IMapper _mapper;
    public CreateUserHandler(UserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var dalUser = _mapper.Map<DAL.Models.User>(request);
        await _userRepository.CreateUser(dalUser);
    }
}