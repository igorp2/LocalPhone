using LocalPhoneApi.Data;
using LocalPhoneDomain;
using LocalPhoneDomain.Models;
using LocalPhoneDomain.Services;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LocalPhoneApi.Services
{
    public class StateService : BaseService<StateModel>, IStateService
    {
        private readonly IApiRepository<StateModel> _repository;

        private static readonly ImmutableDictionary<string, Expression<Func<StateModel, object>>> _sortOptions =
            new Dictionary<string, Expression<Func<StateModel, object>>>
            {
                {"date", state => state.CreationDate },
                {"name", state => state.Name },
                {"abbreviation", state => state.Abbreviation },
            }.ToImmutableDictionary();

        protected override ImmutableDictionary<string, Expression<Func<StateModel, object>>> PropertyNamesAndTheirOrderByExpressions { get => _sortOptions; }

        private PaginationInformations paginationInformations;
        PaginationInformations IPaginatedService.PaginationInfos => paginationInformations;

        public StateService(IApiRepository<StateModel> repository)
        {
            _repository = repository;
        }

        public IEnumerable<StateModel> GetAllItems(bool includeCountries = false,
            bool includeCities = false,
            string orderByPropertyName = null, bool isOrderByInDescendingOrder = false,
            int pageIndex = 1, int pageSize = 0)
        {
            var orderByExpression = GetOrderByExpressionForPropertyName(orderByPropertyName) ?? (state => state.Name);

            _repository.AddFilterWhenRetrievingItemsFromDatabase(state => state.Status == ModelStatuses.ACTIVE);
            _repository.ClearIncludes();
            _repository.AddOrderByWhenRetrievingItemsFromDatabase(orderByExpression, isOrderByInDescendingOrder);

            if (includeCities)
            {
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(state =>
                    state.Cities.Where(city => city.Status == ModelStatuses.ACTIVE), clearPreviousIncludes: false);
            }

            if (includeCountries)
            {
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(state => state.Country,
                    clearPreviousIncludes: false);
            }

            var states = _repository.GetAllItems(pageIndex, pageSize);

            paginationInformations = _repository.PaginationInformations;

            return states;
        }

        public async Task<StateModel> GetStateByIdAsync(int id, bool includeCountries = false,
            bool includeCities = false)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(state => state.Status == ModelStatuses.ACTIVE);
            _repository.ClearIncludes();

            if (includeCities)
            {
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(state =>
                    state.Cities.Where(city => city.Status == ModelStatuses.ACTIVE), clearPreviousIncludes: false);
            }

            if (includeCountries)
            {
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(state => state.Country,
                     clearPreviousIncludes: false);
            }

            var state = await _repository.GetItemByIdAsync(id);

            return state;
        }

        public async Task<StateModel> UpdateStateAsync(int id, StateModel newStateModel)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(state => state.Status == ModelStatuses.ACTIVE);

            var stateUpdated = await _repository.UpdateTheItemWithTheIdAsync(id, newStateModel);

            return stateUpdated;
        }

        public async Task<StateModel> CreateStateAsync(StateModel newStateModel)
        {
            return await _repository.AddNewItemAsync(newStateModel);
        }

        public async Task<bool> DeleteStateByIdAsync(int id)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(state => state.Status == ModelStatuses.ACTIVE);

            var deleted = await _repository.DeleteLogicallyTheItemWithTheIdAsync(id);

            return deleted;
        }

        public IEnumerable<StateModel> GetStatesByCountry(int idCountry)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(state => state.Status == ModelStatuses.ACTIVE);
            _repository.AddFilterWhenRetrievingItemsFromDatabase(state => state.IdCountry == idCountry, false);

            var states = _repository.GetAllItems();

            return states;
        }

        public async Task<bool> IsThereAnActiveStateWithTheIdAsync(int id)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(state => state.Status == ModelStatuses.ACTIVE);

            return await _repository.ExistsTheItemWithTheIdAsync(id);
        }
    }
}
