using LocalPhoneDomain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LocalPhoneApi.Data
{
    public class SmsRelatedInformationRepository : RecoverableItemsRepository<SmsRelatedInformationModel>,
        INonUpdatableItemsRepository<SmsRelatedInformationModel>
    {
        private readonly ApiDbContext _dbContext;

        public SmsRelatedInformationRepository(ApiDbContext apiDbContext) : base(apiDbContext)
        {
            _dbContext = apiDbContext;
        }

        public async Task<SmsRelatedInformationModel> GetItemByIdAsync(int id)
        {
            return await GetItemsWithInclusionsFiltersAndOrderBys().AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        private async Task<bool> SaveDbChangesAsync()
        {
            bool isOk;

            try
            {
                isOk = await _dbContext.SaveChangesAsync() > 0;
            }
            catch
            {
                isOk = false;
            }

            return isOk;
        }

        public async Task<SmsRelatedInformationModel> AddNewItemAsync(SmsRelatedInformationModel item)
        {
            if (item == null)
            {
                return null;
            }

            await _dbContext.SmsRelatedInformation.AddAsync(item);

            if (await SaveDbChangesAsync())
            {
                return item;
            }

            return null;
        }

        public async Task<bool> DeleteTheItemWithTheIdAsync(int id)
        {
            var itemToRemove = await GetItemsWithInclusionsFiltersAndOrderBys().AsNoTracking()
                 .FirstOrDefaultAsync(smsRelatedInfos => smsRelatedInfos.Id == id);

            if (itemToRemove == null)
            {
                return false;
            }

            _dbContext.SmsRelatedInformation.Remove(itemToRemove);
            return await SaveDbChangesAsync();
        }

        public async Task<bool> ExistsTheItemWithTheIdAsync(int id)
        {
            return (await GetItemsWithInclusionsFiltersAndOrderBys().AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id)) != null;
        }
    }
}
