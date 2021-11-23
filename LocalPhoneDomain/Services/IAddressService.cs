using LocalPhoneDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalPhoneDomain.Services
{
    public interface IAddressService : IPaginatedService
    {
        IEnumerable<AddressModel> GetAllItems(bool includeCities = false,
            bool includeStates = false, bool includeCountries = false,
            bool includeCustomers = false,
            string orderByPropertyName = null, bool isOrderByInDescendingOrder = false,
            int pageIndex = 1, int pageSize = 0);

        Task<AddressModel> GetAddressByIdAsync(int id, bool includeCities = false,
            bool includeStates = false, bool includeCountries = false,
            bool includeCustomers = false);

        Task<AddressModel> UpdateAddressAsync(int id, AddressModel newAddressModel);

        Task<AddressModel> CreateAddressAsync(AddressModel newAddressModel);

        Task<bool> DeleteAddressByIdAsync(int id);

        Task<bool> IsThereAnActiveAddressWithThisIdAsync(int id);
    }
}
