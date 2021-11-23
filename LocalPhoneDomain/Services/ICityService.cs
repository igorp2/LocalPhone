using LocalPhoneDomain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalPhoneDomain.Services
{
    public interface ICityService : IPaginatedService
    {
        IEnumerable<CityModel> GetAllItems(bool includeStates = false, bool includeCountries = false,
            string orderByPropertyName = null, bool isOrderByInDescendingOrder = false,
            int pageIndex = 1, int pageSize = 0);

        Task<CityModel> GetCityByIdAsync(int id, bool includeStates = false, bool includeCountries = false);

        Task<CityModel> UpdateCityAsync(int id, CityModel newCityModel);

        Task<CityModel> CreateCityAsync(CityModel newCityModel);

        Task<bool> DeleteCityByIdAsync(int id);

        IEnumerable<CityModel> GetCitiesByState(int idState);

        Task<bool> IsThereAnActiveCityWithThisIdAsync(int id);
    }
}
