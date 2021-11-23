using LocalPhoneApi.Data;
using LocalPhoneDomain;
using LocalPhoneDomain.Models;
using LocalPhoneDomain.Services;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LocalPhoneApi.Services
{
    public class CityService : BaseService<CityModel>, ICityService
    {
        private readonly IApiRepository<CityModel> _repository;

        private static readonly ImmutableDictionary<string, Expression<Func<CityModel, object>>> _sortOptions =
            new Dictionary<string, Expression<Func<CityModel, object>>>
            {
                {"date", state => state.CreationDate },
                {"phonecode", city => city.Phonecode },
            }.ToImmutableDictionary();

        protected override ImmutableDictionary<string, Expression<Func<CityModel, object>>> PropertyNamesAndTheirOrderByExpressions { get => _sortOptions; }

        private PaginationInformations paginationInformations;
        PaginationInformations IPaginatedService.PaginationInfos => paginationInformations;

        public CityService(IApiRepository<CityModel> repository)
        {
            _repository = repository;
        }

        public IEnumerable<CityModel> GetAllItems(bool includeStates = false, bool includeCountries = false,
            string orderByPropertyName = null, bool isOrderByInDescendingOrder = false,
            int pageIndex = 1, int pageSize = 0)
        {
            var orderByExpression = GetOrderByExpressionForPropertyName(orderByPropertyName) ?? (city => city.Description);

            _repository.AddFilterWhenRetrievingItemsFromDatabase(city => city.Status == ModelStatuses.ACTIVE);
            _repository.AddOrderByWhenRetrievingItemsFromDatabase(orderByExpression, isOrderByInDescendingOrder);

            if (includeStates)
            {
                if (includeCountries)
                {
                    _repository.AddIncludeWhenRetrievingItemsFromDatabase(city => city.State, state => (state as StateModel).Country);
                }
                else
                {
                    _repository.AddIncludeWhenRetrievingItemsFromDatabase(city => city.State);
                }
            }

            var items = _repository.GetAllItems(pageIndex, pageSize);

            paginationInformations = _repository.PaginationInformations;

            return items;
        }

        public async Task<CityModel> GetCityByIdAsync(int id, bool includeStates = false, bool includeCountries = false)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(city => city.Status == ModelStatuses.ACTIVE);

            if (includeStates)
            {
                if (includeCountries)
                {
                    _repository.AddIncludeWhenRetrievingItemsFromDatabase(city => city.State, state => (state as StateModel).Country);
                }
                else
                {
                    _repository.AddIncludeWhenRetrievingItemsFromDatabase(city => city.State);
                }
            }

            var cityFound = await _repository.GetItemByIdAsync(id);

            return cityFound;
        }

        public async Task<CityModel> UpdateCityAsync(int id, CityModel newCityModel)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(city => city.Status == ModelStatuses.ACTIVE);

            var cityUpdated = await _repository.UpdateTheItemWithTheIdAsync(id, newCityModel);

            return cityUpdated;
        }

        public async Task<CityModel> CreateCityAsync(CityModel newCityModel)
        {
            return await _repository.AddNewItemAsync(newCityModel);
        }

        public async Task<bool> DeleteCityByIdAsync(int id)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(city => city.Status == ModelStatuses.ACTIVE);

            var deleted = await _repository.DeleteLogicallyTheItemWithTheIdAsync(id);

            return deleted;
        }

        public IEnumerable<CityModel> GetCitiesByState(int idState)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(city => city.Status == ModelStatuses.ACTIVE);
            _repository.AddFilterWhenRetrievingItemsFromDatabase(city => city.IdState == idState, false);

            var cities = _repository.GetAllItems();

            return cities;
        }

        public async Task<bool> IsThereAnActiveCityWithThisIdAsync(int id)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(city => city.Status == ModelStatuses.ACTIVE);

            return await _repository.ExistsTheItemWithTheIdAsync(id);
        }
    }
}
