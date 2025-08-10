using System.Linq;
using API.Models;

namespace API.Extensions
{
    public static class QueryableExtensions
    {
        public static PaginatedList<T> ToPaginatedList<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            return PaginatedList<T>.Create(source, pageNumber, pageSize);
        }
    }
}