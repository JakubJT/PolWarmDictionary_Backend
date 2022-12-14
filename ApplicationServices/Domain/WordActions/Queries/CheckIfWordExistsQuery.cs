using MediatR;
using ApplicationServices.Domain.Models;

namespace ApplicationServices.Domain.WordActions.Queries;

public class CheckIfWordExistsQuery : IRequest<bool>
{
    public Word? Word { get; set; }
}