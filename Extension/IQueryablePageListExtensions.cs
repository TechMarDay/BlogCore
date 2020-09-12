using BlogCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlogCore.Extension
{
    public static class IQueryablePageListExtensions
    {
        public static async Task<Pagination<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            var totalRecords = await source.CountAsync(cancellationToken).ConfigureAwait(false);
            List<T> items;

            if (pageIndex == 0 || pageSize == 0)
            {
                items = await source.ToListAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                items = await source.Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize).ToListAsync(cancellationToken).ConfigureAwait(false);
            }

            var pagination = new Pagination<T>()
            {
                Items = items,
                TotalRecords = totalRecords,
                CurrentPage = pageIndex,
                PageSize = pageSize,
                TotalPages = totalRecords / pageSize + 1
            };

            return pagination;
        }
    }
}
