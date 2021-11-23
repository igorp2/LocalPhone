using LocalPhoneDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LocalPhoneDomain.Services
{
    public interface ICountryService : IPaginatedService
    {
        IEnumerable<CountryModel> GetAllItems(bool includeStates = false, bool includeCities = false,
            string orderByPropertyName = null, bool isOrderByInDescendingOrder = false,
            int pageIndex = 1, int pageSize = 0);

        Task<CountryModel> GetCountryByIdAsync(int id, bool includeStates = false, bool includeCities = false);

        Task<CountryModel> UpdateCountryAsync(int id, CountryModel countryModel);

        Task<CountryModel> CreateCountryAsync(CountryModel countryModel);

        Task<bool> DeleteCountryByIdAsync(int id);

        Task<bool> IsThereAnActiveCountryWithTheIdAsync(int id);
    }
}
