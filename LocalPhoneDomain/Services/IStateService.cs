using LocalPhoneDomain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalPhoneDomain.Services
{
    public interface IStateService : IPaginatedService
    {
        IEnumerable<StateModel> GetAllItems(bool includeCountries = false,
            bool includeCities = false,
            string orderByPropertyName = null, bool isOrderByInDescendingOrder = false,
            int pageIndex = 1, int pageSize = 0);

        Task<StateModel> GetStateByIdAsync(int id, bool includeCountries = false, bool includeCities = false);

        Task<StateModel> UpdateStateAsync(int id, StateModel newStateModel);

        Task<StateModel> CreateStateAsync(StateModel newStateModel);

        Task<bool> DeleteStateByIdAsync(int id);

        IEnumerable<StateModel> GetStatesByCountry(int idCountry);

        Task<bool> IsThereAnActiveStateWithTheIdAsync(int id);
    }
}
