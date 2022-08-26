using MediatR;
using ApplicationServices.Domain.Models;

namespace ApplicationServices.Domain.WordActions.Queries;

public class GetWordsQuery : IRequest<List<Word>>
{

}