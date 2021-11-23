using LocalPhoneDomain;
using LocalPhoneDomain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LocalPhoneApi.Data
{
    public class CustomerRepository : RecoverableItemsRepository<CustomerModel>, IApiRepository<CustomerModel, string>
    {
        private readonly ApiDbContext _dbContext;

        public CustomerRepository(ApiDbContext apiDbContext) : base(apiDbContext)
        {
            _dbContext = apiDbContext;
        }

        public async Task<CustomerModel> GetItemByIdAsync(string id)
        {
            if (id == null)
            {
                return null;
            }

            return await GetItemsWithInclusionsFiltersAndOrderBys().AsNoTracking()
                .FirstOrDefaultAsync(m => m.PhoneNumber == id);
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

        public async Task<CustomerModel> AddNewItemAsync(CustomerModel item)
        {
            if (item == null)
            {
                return null;
            }

            CustomerModel customerFound = await _dbContext.Customers
                .FirstOrDefaultAsync(customer => customer.PhoneNumber == item.PhoneNumber);

            if (customerFound != null)
            {
                if (customerFound.Status == CustomerStatuses.INACTIVE)
                {
                    customerFound.CloneFromAnotherCustomer(item);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                item.Status = CustomerStatuses.PENDING;

                await _dbContext.Customers.AddAsync(item);
            }

            if (await SaveDbChangesAsync())
            {
                return item;
            }

            return null;
        }

        public async Task<bool> DeleteTheItemWithTheIdAsync(string id)
        {
            if (id == null)
            {
                return false;
            }

            var itemToRemove = await GetItemsWithInclusionsFiltersAndOrderBys().AsNoTracking()
                .FirstOrDefaultAsync(customer => customer.PhoneNumber == id);

            if (IsNullOrInactive(itemToRemove))
            {
                return false;
            }

            _dbContext.Customers.Remove(itemToRemove);
            return await SaveDbChangesAsync();
        }

        public async Task<bool> DeleteLogicallyTheItemWithTheIdAsync(string id)
        {
            if (id == null)
            {
                return false;
            }

            var itemToRemove = await GetItemsWithInclusionsFiltersAndOrderBys()
                .FirstOrDefaultAsync(customer => customer.PhoneNumber == id);

            if (IsNullOrInactive(itemToRemove))
            {
                return false;
            }

            DeleteACustomerLogically(itemToRemove);

            return await SaveDbChangesAsync();
        }

        public async Task<bool> UpdateThenDeleteLogicallyTheItemWithTheIdAsync(string id, CustomerModel newItem)
        {
            CustomerModel itemToRemove = await GetItemsWithInclusionsFiltersAndOrderBys()
                .FirstOrDefaultAsync(item => item.PhoneNumber == id);

            if (id == null)
            {
                return false;
            }

            if (IsNullOrInactive(itemToRemove))
            {
                return false;
            }

            itemToRemove.CloneFromAnotherCustomer(newItem);
            DeleteACustomerLogically(itemToRemove);

            return await SaveDbChangesAsync();
        }

        public async Task<CustomerModel> UpdateTheItemWithTheIdAsync(string id, CustomerModel newItem)
        {
            if (id == null)
            {
                return null;
            }

            var customerFound = await GetItemsWithInclusionsFiltersAndOrderBys()
                .FirstOrDefaultAsync(m => m.PhoneNumber == id);

            if (IsNullOrInactive(customerFound))
            {
                return null;
            }

            customerFound.CloneFromAnotherCustomer(newItem);

            if (await SaveDbChangesAsync())
            {

                return customerFound;
            }

            return null;
        }

        private static void DeleteACustomerLogically(CustomerModel customer)
        {
            customer.Status = CustomerStatuses.INACTIVE;
            customer.LastModificationDate = DateTime.Now;
        }

        private static bool IsNullOrInactive(CustomerModel customer)
        {
            return customer == null || customer.Status == CustomerStatuses.INACTIVE;
        }

        public async Task<bool> ExistsTheItemWithTheIdAsync(string id)
        {
            return (await GetItemsWithInclusionsFiltersAndOrderBys().AsNoTracking()
                .FirstOrDefaultAsync(m => m.PhoneNumber == id)) != null;
        }
    }
}
