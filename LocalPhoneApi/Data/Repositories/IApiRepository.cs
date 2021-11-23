using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query;

namespace LocalPhoneApi.Data
{ 
    public interface IApiRepository<TModel> : IApiRepository<TModel, int> where TModel : class
    {

    }

    public interface IApiRepository<TModel, TId> : IRecoverableItemsRepository<TModel> 
        where TModel : class where TId : IEquatable<TId>
    {
        Task<TModel> AddNewItemAsync(TModel item);

        Task<bool> DeleteTheItemWithTheIdAsync(TId id);

        Task<bool> DeleteLogicallyTheItemWithTheIdAsync(TId id);

        Task<TModel> UpdateTheItemWithTheIdAsync(TId id, TModel newItem);

        Task<bool> UpdateThenDeleteLogicallyTheItemWithTheIdAsync(TId id, TModel newItem);

        Task<TModel> GetItemByIdAsync(TId id);

        Task<bool> ExistsTheItemWithTheIdAsync(TId id);
    }
}
