using LocalPhoneDomain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalPhoneDomain.Services
{
    public interface IAvailableNumberService : IPaginatedService
    {
        IEnumerable<AvailableNumberModel> GetAllItems(
            string orderByPropertyName = null, bool isOrderByInDescendingOrder = false,
            int pageIndex = 1, int pageSize = 0, bool includeDetails = false);

        IEnumerable<AvailableNumberModel> GetAvailableNumberByRegionAsync(int idCountry, int idCity);

        Task<AvailableNumberModel> CreateAvailableNumberAsync(AvailableNumberModel availableNumber);

        Task<AvailableNumberModel> UpdateAvailableNumberAsync(int id, AvailableNumberModel newAvailableNumberModel);

        Task<bool> DeleteAvailableNumberByIdAsync(int id);

        Task<bool> IsThereAnActiveAvailableNumberWithThisIdAsync(int id);
        
        Task<AvailableNumberModel> GetAvailableNumberByIdAsync(int id);
    }
}
