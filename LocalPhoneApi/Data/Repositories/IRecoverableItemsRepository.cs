using LocalPhoneDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LocalPhoneApi.Data
{
    public interface IRecoverableItemsRepository<TModel> where TModel : class
    {
        protected List<Expression<Func<TModel, bool>>> Filters { get; }
        protected List<(Expression<Func<TModel, object>>, Expression<Func<object, object>>)> Includes { get; }
        protected List<(Expression<Func<TModel, object>>, bool)> OrderBys { get; }
        public PaginationInformations PaginationInformations { get; }

        void ClearFilters()
        {
            Filters.Clear();
        }

        void ClearIncludes()
        {
            Includes.Clear();
        }

        void ClearOrderBys()
        {
            OrderBys.Clear();
        }

        void AddFilterWhenRetrievingItemsFromDatabase(Expression<Func<TModel, bool>> filterExpression,
            bool clearPreviousFilters = true)
        {
            if (clearPreviousFilters)
            {
                Filters.Clear();
            }

            Filters.Add(filterExpression);
        }

        void AddIncludeWhenRetrievingItemsFromDatabase(
            Expression<Func<TModel, object>> includeExpression,
            Expression<Func<object, object>> thenIncludeExpression = null,
            bool clearPreviousIncludes = true)
        {
            if (clearPreviousIncludes)
            {
                Includes.Clear();
            }

            Includes.Add((includeExpression, thenIncludeExpression));
        }

        void AddOrderByWhenRetrievingItemsFromDatabase(
            Expression<Func<TModel, object>> orderByExpression,
            bool isOrderByInDescendingOrder = false,
            bool clearPreviousOrderBys = true)
        {
            if (clearPreviousOrderBys)
            {
                OrderBys.Clear();
            }

            OrderBys.Add((orderByExpression, isOrderByInDescendingOrder));
        }

        IEnumerable<TModel> GetAllItems(int pageIndex = 1, int pageSize = 0);
    }
}
