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
    public class CountryService : BaseService<CountryModel>, ICountryService
    {
        private readonly IApiRepository<CountryModel> _repository;

        private static readonly ImmutableDictionary<string, Expression<Func<CountryModel, object>>> _sortOptions =
            new Dictionary<string, Expression<Func<CountryModel, object>>>
            {
                {"date", state => state.CreationDate },
                {"name", country => country.Name },
                {"phonecode", country => country.Phonecode },
                {"iso", country => country.Iso },
                {"iso3", country => country.Iso3 },
            }.ToImmutableDictionary();

        protected override ImmutableDictionary<string, Expression<Func<CountryModel, object>>> PropertyNamesAndTheirOrderByExpressions { get => _sortOptions; }

        private PaginationInformations paginationInformations;
        PaginationInformations IPaginatedService.PaginationInfos => paginationInformations;

        public CountryService(IApiRepository<CountryModel> repository)
        {
            _repository = repository;
        }

        public IEnumerable<CountryModel> GetAllItems(bool includeStates = false, bool includeCities = false,
            string orderByPropertyName = null, bool isOrderByInDescendingOrder = false,
            int pageIndex = 1, int pageSize = 0)
        {
            var orderByExpression = GetOrderByExpressionForPropertyName(orderByPropertyName) ?? (country => country.Name);

            _repository.AddFilterWhenRetrievingItemsFromDatabase(country => country.Status == ModelStatuses.ACTIVE);
            _repository.AddOrderByWhenRetrievingItemsFromDatabase(orderByExpression, isOrderByInDescendingOrder);

            if (includeStates)
            {
                if (includeCities)
                    IncludeStatesAndCitiesToRepositoryQuery();
                else
                    IncludeStatesToRepositoryQuery();
            }

            var items = _repository.GetAllItems(pageIndex, pageSize);

            paginationInformations = _repository.PaginationInformations;

            return items;
        }

        private void IncludeStatesAndCitiesToRepositoryQuery()
        {
            _repository.AddIncludeWhenRetrievingItemsFromDatabase(country =>
                country.States.Where(state => state.Status == ModelStatuses.ACTIVE),
                    state => (state as StateModel).Cities.Where(city => city.Status == ModelStatuses.ACTIVE));
        }

        private void IncludeStatesToRepositoryQuery()
        {
            _repository.AddIncludeWhenRetrievingItemsFromDatabase(country =>
                country.States.Where(state => state.Status == ModelStatuses.ACTIVE));
        }

        public async Task<CountryModel> GetCountryByIdAsync(int id, bool includeStates = false,
            bool includeCities = false)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(country => country.Status == ModelStatuses.ACTIVE);

            if (includeStates)
            {
                if (includeCities)
                    IncludeStatesAndCitiesToRepositoryQuery();
                else
                    IncludeStatesToRepositoryQuery();
            }

            var countryFound = await _repository.GetItemByIdAsync(id);

            return countryFound;
        }

        public async Task<CountryModel> UpdateCountryAsync(int id, CountryModel countryModel)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(country => country.Status == ModelStatuses.ACTIVE);

            var countryUpdated = await _repository.UpdateTheItemWithTheIdAsync(id, countryModel);

            return countryUpdated;
        }

        public async Task<CountryModel> CreateCountryAsync(CountryModel countryModel)
        {
            return await _repository.AddNewItemAsync(countryModel);
        }

        public async Task<bool> DeleteCountryByIdAsync(int id)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(country => country.Status == ModelStatuses.ACTIVE);

            var deleted = await _repository.DeleteLogicallyTheItemWithTheIdAsync(id);

            return deleted;
        }

        public async Task<bool> IsThereAnActiveCountryWithTheIdAsync(int id)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(country => country.Status == ModelStatuses.ACTIVE);

            return await _repository.ExistsTheItemWithTheIdAsync(id);
        }
    }
}
