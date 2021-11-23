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
    public class AddressService : BaseService<AddressModel>, IAddressService
    {
        private readonly IApiRepository<AddressModel> _repository;
        private PaginationInformations paginationInformations;
        PaginationInformations IPaginatedService.PaginationInfos { get => paginationInformations; }

        private static readonly ImmutableDictionary<string, Expression<Func<AddressModel, object>>> _sortOptions =
            new Dictionary<string, Expression<Func<AddressModel, object>>>
            {
                {"date", state => state.CreationDate },
                {"type", address => address.Type },
                {"street", address => address.Street },
                {"zip", address => address.Zip },
            }.ToImmutableDictionary();

        protected override ImmutableDictionary<string, Expression<Func<AddressModel, object>>> PropertyNamesAndTheirOrderByExpressions { get => _sortOptions; }

        public AddressService(IApiRepository<AddressModel> repository)
        {
            _repository = repository;
        }

        public IEnumerable<AddressModel> GetAllItems(bool includeCities = false, 
            bool includeStates = false, bool includeCountries = false,
            bool includeCustomers = false,
            string orderByPropertyName = null, bool isOrderByInDescendingOrder = false,
            int pageIndex = 1, int pageSize = 0)
        {
            var orderByExpression = GetOrderByExpressionForPropertyName(orderByPropertyName) ?? (address => address.Zip);

            _repository.AddFilterWhenRetrievingItemsFromDatabase(address => address.Status == ModelStatuses.ACTIVE);
            _repository.ClearIncludes();

            _repository.AddOrderByWhenRetrievingItemsFromDatabase(orderByExpression, isOrderByInDescendingOrder);

            if (includeStates)
            {
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(address => address.State,
                    clearPreviousIncludes: false);
            }

            if (includeCountries)
            {
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(address => address.Country,
                    clearPreviousIncludes: false);
            }

            if (includeCities)
            {
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(address => address.City, 
                    clearPreviousIncludes: false);
            }

            if (includeCustomers)
            {
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(address => address.Customer);
            }

            var items = _repository.GetAllItems(pageIndex, pageSize);

            paginationInformations = _repository.PaginationInformations;

            return items;
        }

        public async Task<AddressModel> GetAddressByIdAsync(int id, bool includeCities = false,
            bool includeStates = false, bool includeCountries = false,
            bool includeCustomers = false)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(address => address.Status == ModelStatuses.ACTIVE);
            _repository.ClearIncludes();

            if (includeStates)
            {
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(address => address.State,
                    clearPreviousIncludes: false);
            }

            if (includeCountries)
            {
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(address => address.Country,
                    clearPreviousIncludes: false);
            }

            if (includeCities)
            {
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(address => address.City,
                    clearPreviousIncludes: false);
            }

            if (includeCustomers)
            {
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(address => address.Customer);
            }

            return await _repository.GetItemByIdAsync(id);
        }

        public async Task<AddressModel> UpdateAddressAsync(int id, AddressModel newAddressModel)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(address => address.Status == ModelStatuses.ACTIVE);

            if (!await _repository.ExistsTheItemWithTheIdAsync(id))
            {
                return null;
            }

            return await _repository.UpdateTheItemWithTheIdAsync(id, newAddressModel);
        }

        public async Task<AddressModel> CreateAddressAsync(AddressModel newAddressModel)
        {
            return await _repository.AddNewItemAsync(newAddressModel);
        }

        public async Task<bool> DeleteAddressByIdAsync(int id)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(address => address.Status == ModelStatuses.ACTIVE);

            return await _repository.DeleteLogicallyTheItemWithTheIdAsync(id);
        }

        public async Task<bool> IsThereAnActiveAddressWithThisIdAsync(int id)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(address => address.Status == ModelStatuses.ACTIVE);

            return await _repository.ExistsTheItemWithTheIdAsync(id);
        }
    }
}
