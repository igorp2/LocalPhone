using LocalPhoneDomain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using LocalPhoneApi.Data;

namespace LocalPhoneAdmin.Data
{
    public class AdminRepository<T> : IAdminRepository<T> where T : BaseModel
    {
        private readonly ApiDbContext _dbContext;
        private readonly DbSet<T> setWhereDoTheOperations;
        private readonly List<Expression<Func<T, bool>>> filters;
        private readonly List<(Expression<Func<T, object>>, Expression<Func<object, object>>)> includes;
        List<Expression<Func<T, bool>>> IAdminRepository<T, int>.Filters { get => filters; }
        List<(Expression<Func<T, object>>, Expression<Func<object, object>>)> IAdminRepository<T, int>.Includes { get => includes; }

        public AdminRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
            setWhereDoTheOperations = dbContext.Set<T>();
            filters = new List<Expression<Func<T, bool>>>();
            includes = new List<(Expression<Func<T, object>>, Expression<Func<object, object>>)>();
        }

        public virtual async Task<bool> AddNewItemAsync(T item)
        {
            await setWhereDoTheOperations.AddAsync(item);
            return await SaveChangesAsync();
        }

        public virtual async Task<bool> DeleteTheItemWithTheIdAsync(int Id)
        {
            T itemToRemove = await GetItemByIdAsync(Id);

            if (itemToRemove == null)
            {
                return false;
            }

            setWhereDoTheOperations.Remove(itemToRemove);
            return await SaveChangesAsync();
        }

        public virtual async Task<bool> DeleteLogicallyTheItemWithTheIdAsync(int Id)
        {
            T itemToRemove = await GetItemsWithInclusionsAndFilters().FirstOrDefaultAsync(item => item.Id == Id);

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

        public virtual async Task<bool> UpdateThenDeleteLogicallyTheItemWithTheIdAsync(int Id, T newItem)
        {
            T itemToRemove = await GetItemsWithInclusionsAndFilters().FirstOrDefaultAsync(item => item.Id == Id);

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

        public virtual async Task<T> UpdateTheItemWithTheIdAsync(int Id, T newItem)
        {
            T itemToUpdate = await GetItemsWithInclusionsAndFilters().FirstOrDefaultAsync(item => item.Id == Id);

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

        public virtual async Task<T> GetItemByIdAsync(int Id)
        {
            return await GetItemsWithInclusionsAndFilters().AsNoTracking().FirstOrDefaultAsync(item => item.Id == Id);
        }

        public virtual IEnumerable<T> GetAllItems()
        {
            return GetItemsWithInclusionsAndFilters().AsNoTracking().AsEnumerable();
        }

        private IQueryable<T> GetItemsWithInclusionsAndFilters()
        {
            var items = setWhereDoTheOperations.AsQueryable();

            if (filters.Any())
            {
                foreach(var filter in filters)
                {
                    items = items.Where(filter);
                }
            }

            if (includes.Any())
            {
                foreach(var include in includes)
                {
                    if (include.Item2 != null)
                        items = items.Include(include.Item1).ThenInclude(include.Item2);
                    else
                        items = items.Include(include.Item1);
                }
            }

            return items;
        }

        public async Task<bool> ExistsTheItemWithTheIdAsync(int Id)
        {
            var items = GetItemsWithInclusionsAndFilters();

            return await items.FirstOrDefaultAsync(item => item.Id == Id) != null;
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
