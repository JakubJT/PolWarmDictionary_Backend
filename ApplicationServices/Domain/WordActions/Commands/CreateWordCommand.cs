using MediatR;
using ApplicationServices.Domain.Models;

namespace ApplicationServices.Domain.WordActions.Commands;

public class CreateWordCommand : IRequest
{
    public Word? Word { get; set; }
}