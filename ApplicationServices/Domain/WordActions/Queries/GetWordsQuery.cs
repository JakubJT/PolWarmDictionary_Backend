using MediatR;
using ApplicationServices.Domain.Models;

namespace ApplicationServices.Domain.WordActions.Queries;

public class GetWordsQuery : IRequest<Words>
{
    public bool AscendingOrder { get; set; }
    public string? SortBy { get; set; }
    public int PageNumber { get; set; }
    public int WordsPerPage { get; set; }
}