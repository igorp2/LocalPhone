using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LocalPhoneApi.Data
{
    public interface INonUpdatableItemsRepository<TModel> : INonUpdatableItemsRepository<TModel, int>
        where TModel : class
    {
    }

    public interface INonUpdatableItemsRepository<TModel, TId> : IRecoverableItemsRepository<TModel> 
        where TModel : class where TId : IEquatable<TId>
    {
        Task<TModel> AddNewItemAsync(TModel item);

        Task<bool> DeleteTheItemWithTheIdAsync(TId id);

        Task<TModel> GetItemByIdAsync(TId id);

        Task<bool> ExistsTheItemWithTheIdAsync(TId id);
    }
}
