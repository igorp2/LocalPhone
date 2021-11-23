using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace LocalPhoneAdmin.Data
{
    public interface IAdminRepository<TModel> : IAdminRepository<TModel, int> where TModel : class
    {

    }

    public interface IAdminRepository<TModel, TId> where TModel : class where TId : IEquatable<TId>
    {
        protected List<Expression<Func<TModel, bool>>> Filters { get; }
        protected List<(Expression<Func<TModel, object>>, Expression<Func<object, object>>)> Includes { get; }

        void ClearFilters()
        {
            Filters.Clear();
        }

        void ClearIncludes()
        {
            Includes.Clear();
        }

        void AddFilterWhenToRetrievingItemsFromDatabase(Expression<Func<TModel, bool>> filterExpression,
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

        Task<bool> AddNewItemAsync(TModel item);

        Task<bool> DeleteTheItemWithTheIdAsync(TId Id);

        Task<bool> DeleteLogicallyTheItemWithTheIdAsync(TId Id);

        Task<TModel> UpdateTheItemWithTheIdAsync(TId Id, TModel newItem);

        Task<bool> UpdateThenDeleteLogicallyTheItemWithTheIdAsync(TId Id, TModel newItem);

        Task<TModel> GetItemByIdAsync(TId Id);

        Task<bool> ExistsTheItemWithTheIdAsync(TId Id);

        IEnumerable<TModel> GetAllItems();
    }
}
