using LocalPhoneDomain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LocalPhoneApi.Data
{
    public class RecoverableItemsRepository<TModel> : IRecoverableItemsRepository<TModel> where TModel: class
    {
        private readonly DbSet<TModel> setWhereDoTModelheOperations;
        private readonly List<Expression<Func<TModel, bool>>> filters;
        private readonly List<(Expression<Func<TModel, object>>, Expression<Func<object, object>>)> includes;
        private readonly List<(Expression<Func<TModel, object>>, bool)> orderBys;
        private PaginationInformations paginationInformations;
        List<Expression<Func<TModel, bool>>> IRecoverableItemsRepository<TModel>.Filters => filters;
        List<(Expression<Func<TModel, object>>, Expression<Func<object, object>>)> IRecoverableItemsRepository<TModel>.Includes => includes;
        List<(Expression<Func<TModel, object>>, bool)> IRecoverableItemsRepository<TModel>.OrderBys => orderBys;
        PaginationInformations IRecoverableItemsRepository<TModel>.PaginationInformations { get => paginationInformations; }

        public RecoverableItemsRepository(ApiDbContext dbContext)
        {
            setWhereDoTModelheOperations = dbContext.Set<TModel>();
            filters = new List<Expression<Func<TModel, bool>>>();
            includes = new List<(Expression<Func<TModel, object>>, Expression<Func<object, object>>)>();
            orderBys = new List<(Expression<Func<TModel, object>>, bool)>();
        }

        protected IQueryable<TModel> GetItemsWithInclusionsFiltersAndOrderBys()
        {
            IQueryable<TModel> items = setWhereDoTModelheOperations.AsQueryable();

            items = ApplyFiltersInItems(items);
            items = ApplyOrderBysInItems(items);
            items = ApplyIncludesInItems(items);

            return items;
        }

        private IQueryable<TModel> ApplyFiltersInItems(IQueryable<TModel> items)
        {
            if (filters.Any())
            {
                foreach(var filter in filters)
                {
                    items = items.Where(filter);
                }
            }

            return items;
        }

        private IQueryable<TModel> ApplyIncludesInItems(IQueryable<TModel> items)
        {
            if (includes.Any())
            {
                foreach(var include in includes)
                {
                    if (include.Item2 != null)
                        items = items.Include(include.Item1).ThenInclude(include.Item2).AsSplitQuery();
                    else
                        items = items.Include(include.Item1);
                }
            }

            return items;
        }

        private IQueryable<TModel> ApplyOrderBysInItems(IQueryable<TModel> items)
        {
            if (orderBys.Any())
            {
                foreach(var orderBy in orderBys)
                {
                    if (orderBy.Item2 == false)
                        items = items.OrderBy(orderBy.Item1);
                    else
                        items = items.OrderByDescending(orderBy.Item1);
                }
            }

            return items;
        }

        public IEnumerable<TModel> GetAllItems(int pageIndex = 1, int pageSize = 0)
        {
            var items = GetItemsWithInclusionsFiltersAndOrderBys().AsNoTracking();
            int numberOfPages = (int) Math.Ceiling(items.Count() / (double) pageSize);

            if (pageSize > 0 && pageIndex > 0 && pageIndex < numberOfPages)
            {
                items = items.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }

            paginationInformations = new PaginationInformations(numberOfPages, pageSize, pageIndex);

            return items.AsEnumerable();
        }
    }
}
