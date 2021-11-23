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
    public class NumberService : BaseService<NumberModel>, INumberService
    {
        private readonly IApiRepository<NumberModel> _repository;

        private static readonly ImmutableDictionary<string, Expression<Func<NumberModel, object>>> _sortOptions =
            new Dictionary<string, Expression<Func<NumberModel, object>>>
            {
                {"date", number => number.CreationDate },
                {"phonenumber", number => number.PhoneNumber },
            }.ToImmutableDictionary();

        protected override ImmutableDictionary<string, Expression<Func<NumberModel, object>>> PropertyNamesAndTheirOrderByExpressions { get => _sortOptions; }

        private PaginationInformations paginationInformations;
        PaginationInformations IPaginatedService.PaginationInfos => paginationInformations;

        public NumberService(IApiRepository<NumberModel> repository)
        {
            _repository = repository;
        }

        public IEnumerable<NumberModel> GetAllItems(
            string orderByPropertyName = null, bool isOrderByInDescendingOrder = false,
            int pageIndex = 1, int pageSize = 0)
        {
            var orderByExpression = GetOrderByExpressionForPropertyName(orderByPropertyName) ?? (number => number.PhoneNumber);

            _repository.AddOrderByWhenRetrievingItemsFromDatabase(orderByExpression, isOrderByInDescendingOrder);

            var items = _repository.GetAllItems(pageIndex, pageSize);

            paginationInformations = _repository.PaginationInformations;

            return items;
        }

        public async Task<NumberModel> GetNumberByIdAsync(int id)
        {
            return await _repository.GetItemByIdAsync(id);
        }

        public IEnumerable<NumberModel> GetNumberByCustomer(string idCustomer)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(number => number.IdCustomer == idCustomer);

            return _repository.GetAllItems();
        }

        public async Task<NumberModel> UpdateNumberAsync(int id, NumberModel newNumberModel)
        {
            return await _repository.UpdateTheItemWithTheIdAsync(id, newNumberModel);
        }

        public async Task<NumberModel> CreateNumberAsync(NumberModel number)
        {
            return await _repository.AddNewItemAsync(number);
        }

        public async Task<bool> DeleteNumberByIdAsync(int id)
        {
            return await _repository.DeleteLogicallyTheItemWithTheIdAsync(id);
        }

        public async Task<bool> IsThereAnActiveNumberWithTheIdAsync(int id)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(number => number.Status == ModelStatuses.ACTIVE);

            return await _repository.ExistsTheItemWithTheIdAsync(id);
        }

        public IEnumerable<NumberModel> GetCustomerByNumber(string phoneNumber)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(number => number.PhoneNumber == phoneNumber);
            _repository.AddFilterWhenRetrievingItemsFromDatabase(number => number.Status == ModelStatuses.ACTIVE);

            return _repository.GetAllItems();
        }
    }
}
