using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace LocalPhoneAdmin.Areas
{
    public class PaginatedList<T> : List<T>
    {
        public int CurrentPageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int pageIndex, int pageSize)
        {
            CurrentPageIndex = pageIndex;
            TotalPages = (int) Math.Ceiling(items.Count / (double) pageSize);

            var paginatedItems = items.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            this.AddRange(paginatedItems);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (CurrentPageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (CurrentPageIndex < TotalPages);
            }
        }

        //public static async Task<PaginatedList<T>> CreateAsync(
        //    IQueryable<T> source, int pageIndex, int pageSize)
        //{
        //    var items = await source.Skip(
        //        (pageIndex - 1) * pageSize)
        //        .Take(pageSize).ToListAsync();
        //    return new PaginatedList<T>(items, pageIndex, pageSize);
        //}
    }
}
