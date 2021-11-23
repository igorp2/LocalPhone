using LocalPhoneDomain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace LocalPhoneApi.Data
{
    public class ApiRepository<T> : RecoverableItemsRepository<T>, IApiRepository<T> where T : BaseModel
    {
        private readonly ApiDbContext _dbContext;
        private readonly DbSet<T> setWhereDoTheOperations;

        public ApiRepository(ApiDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            setWhereDoTheOperations = dbContext.Set<T>();
        }

        public virtual async Task<T> AddNewItemAsync(T item)
        {
            await setWhereDoTheOperations.AddAsync(item);

            if (await SaveChangesAsync())
            {
                return item;
            }

            return null;
        }

        public virtual async Task<bool> DeleteTheItemWithTheIdAsync(int id)
        {
            T itemToRemove = await GetItemByIdAsync(id);

            if (itemToRemove == null)
            {
                return false;
            }

            setWhereDoTheOperations.Remove(itemToRemove);
            return await SaveChangesAsync();
        }

        public virtual async Task<bool> DeleteLogicallyTheItemWithTheIdAsync(int id)
        {
            T itemToRemove = await GetItemsWithInclusionsFiltersAndOrderBys().FirstOrDefaultAsync(item => item.Id == id);

            if (itemToRemove == null)
            {
                return false;
            }

            itemToRemove.DeleteSelfLogically();

            if (await SaveChangesAsync())
            {
                return true;
            }

            return false;
        }

        public virtual async Task<bool> UpdateThenDeleteLogicallyTheItemWithTheIdAsync(int id, T newItem)
        {
            T itemToRemove = await GetItemsWithInclusionsFiltersAndOrderBys().FirstOrDefaultAsync(item => item.Id == id);

            if (itemToRemove == null)
            {
                return false;
            }

            itemToRemove.CloneFromModel(newItem);
            itemToRemove.DeleteSelfLogically();

            if (await SaveChangesAsync())
            {
                return true;
            }

            return false;
        }

        public virtual async Task<T> UpdateTheItemWithTheIdAsync(int id, T newItem)
        {
            T itemToUpdate = await GetItemsWithInclusionsFiltersAndOrderBys().FirstOrDefaultAsync(item => item.Id == id);

            if (itemToUpdate == null)
            {
                return null;
            }

            itemToUpdate.CloneFromModel(newItem);

            if (await SaveChangesAsync())
            {
                return itemToUpdate;
            }

            return null;
        }

        public virtual async Task<T> GetItemByIdAsync(int id)
        {
            return await GetItemsWithInclusionsFiltersAndOrderBys().AsNoTracking().FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<bool> ExistsTheItemWithTheIdAsync(int id)
        {
            var items = GetItemsWithInclusionsFiltersAndOrderBys();

            return await items.FirstOrDefaultAsync(item => item.Id == id) != null;
        }

        private async Task<bool> SaveChangesAsync()
        {
            bool isOk;

            try
            {
                isOk = await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                isOk = false;
            }

            return isOk;
        }
    }
}
