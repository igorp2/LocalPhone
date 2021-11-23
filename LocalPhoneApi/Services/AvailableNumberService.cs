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
    public class AvailableNumberService : BaseService<AvailableNumberModel>, IAvailableNumberService
    {
        private readonly IApiRepository<AvailableNumberModel> _repository;

        private static readonly ImmutableDictionary<string, Expression<Func<AvailableNumberModel, object>>> _sortOptions =
            new Dictionary<string, Expression<Func<AvailableNumberModel, object>>>
            {
                {"date", state => state.CreationDate },
                {"phonenumber", availableNumber => availableNumber.phoneNumber },
            }.ToImmutableDictionary();

        protected override ImmutableDictionary<string, Expression<Func<AvailableNumberModel, object>>> PropertyNamesAndTheirOrderByExpressions { get => _sortOptions; }

        private PaginationInformations paginationInformations;
        PaginationInformations IPaginatedService.PaginationInfos => paginationInformations;

        public AvailableNumberService(IApiRepository<AvailableNumberModel> repository)
        {
            _repository = repository;
        }

        public IEnumerable<AvailableNumberModel> GetAllItems(
            string orderByPropertyName = null, bool isOrderByInDescendingOrder = false,
            int pageIndex = 1, int pageSize = 0, bool includeDetails = false)
        {
            var orderByExpression = GetOrderByExpressionForPropertyName(orderByPropertyName) ?? (availableNumber => availableNumber.phoneNumber);

            _repository.AddFilterWhenRetrievingItemsFromDatabase(available => available.Status == ModelStatuses.ACTIVE);
            _repository.AddOrderByWhenRetrievingItemsFromDatabase(orderByExpression, isOrderByInDescendingOrder);

            if (includeDetails)
            {
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(available => available.Country);
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(available => available.City, clearPreviousIncludes: false);
            }

            var available = _repository.GetAllItems(pageIndex, pageSize);

            paginationInformations = _repository.PaginationInformations;

            return available;
        }

        public IEnumerable<AvailableNumberModel> GetAvailableNumberByRegionAsync(int idCountry, int idCity)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(available => available.Status == ModelStatuses.ACTIVE);
            _repository.AddFilterWhenRetrievingItemsFromDatabase(available => available.idCountry == idCountry, false);

            if (idCity != 0)
                _repository.AddFilterWhenRetrievingItemsFromDatabase(available => available.idCity == idCity, false);

            var available = _repository.GetAllItems();

            return available;
        }

        public async Task<AvailableNumberModel> CreateAvailableNumberAsync(AvailableNumberModel availableNumber)
        {
            return await _repository.AddNewItemAsync(availableNumber);
        }

        public async Task<AvailableNumberModel> UpdateAvailableNumberAsync(int id, AvailableNumberModel newAvailableNumberModel)
        {
            return await _repository.UpdateTheItemWithTheIdAsync(id, newAvailableNumberModel);
        }

        public async Task<bool> DeleteAvailableNumberByIdAsync(int id)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(number => number.Status == ModelStatuses.ACTIVE);

            return await _repository.DeleteLogicallyTheItemWithTheIdAsync(id);
        }

        public async Task<bool> IsThereAnActiveAvailableNumberWithThisIdAsync(int id)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(availableNumber => availableNumber.Status == ModelStatuses.ACTIVE);

            return await _repository.ExistsTheItemWithTheIdAsync(id);
        }

        public async Task<AvailableNumberModel> GetAvailableNumberByIdAsync(int id)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(availableNumber => availableNumber.Status == ModelStatuses.ACTIVE);

            var availableNumber = await _repository.GetItemByIdAsync(id);

            return availableNumber;
        }
    }
}
