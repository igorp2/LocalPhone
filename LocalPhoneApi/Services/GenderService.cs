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
    public class GenderService : BaseService<GenderModel>, IGenderService
    {
        private readonly IApiRepository<GenderModel> _repository;

        private static readonly ImmutableDictionary<string, Expression<Func<GenderModel, object>>> _sortOptions =
            new Dictionary<string, Expression<Func<GenderModel, object>>>
            {
                {"date", state => state.CreationDate },
                {"gender", gender => gender.Gender },
                {"abbreviation", gender => gender.Abbreviation },
            }.ToImmutableDictionary();

        protected override ImmutableDictionary<string, Expression<Func<GenderModel, object>>> PropertyNamesAndTheirOrderByExpressions { get => _sortOptions; }

        private PaginationInformations paginationInformations;
        PaginationInformations IPaginatedService.PaginationInfos => paginationInformations;

        public GenderService(IApiRepository<GenderModel> repository)
        {
            _repository = repository;
        }

        public IEnumerable<GenderModel> GetAllItems(bool includeCountries = false,
            string orderByPropertyName = null, bool isOrderByInDescendingOrder = false,
            int pageIndex = 1, int pageSize = 0)
        {
            var orderByExpression = GetOrderByExpressionForPropertyName(orderByPropertyName) ?? (gender => gender.Gender);

            _repository.AddFilterWhenRetrievingItemsFromDatabase(gender => gender.Status == ModelStatuses.ACTIVE);
            _repository.AddOrderByWhenRetrievingItemsFromDatabase(orderByExpression, isOrderByInDescendingOrder);

            if (includeCountries)
            {
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(gender => gender.Country);
            }

            var items = _repository.GetAllItems(pageIndex, pageSize);

            paginationInformations = _repository.PaginationInformations;

            return items;
        }

        public async Task<GenderModel> GetGenderByIdAsync(int id, bool includeCountries = false)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(gender => gender.Status == ModelStatuses.ACTIVE);

            if (includeCountries)
            {
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(gender => gender.Country);
            }

            var genderFound = await _repository.GetItemByIdAsync(id);

            return genderFound;
        }

        public async Task<GenderModel> UpdateGenderAsync(int id, GenderModel genderModel)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(gender => gender.Status == ModelStatuses.ACTIVE);

            var genderUpdated = await _repository.UpdateTheItemWithTheIdAsync(id, genderModel);

            return genderUpdated;
        }

        public async Task<GenderModel> CreateGenderAsync(GenderModel genderModel)
        {
            return await _repository.AddNewItemAsync(genderModel);
        }

        public async Task<bool> DeleteGenderByIdAsync(int id)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(gender => gender.Status == ModelStatuses.ACTIVE);

            var deleted = await _repository.DeleteLogicallyTheItemWithTheIdAsync(id);
            
            return deleted;
        }

        public async Task<bool> IsThereAnActiveGenderWithTheIdAsync(int id)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(gender => gender.Status == ModelStatuses.ACTIVE);

            return await _repository.ExistsTheItemWithTheIdAsync(id);
        }

        public IEnumerable<GenderModel> GetGendersByCountry(int idCountry)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(gender => gender.Status == ModelStatuses.ACTIVE);
            _repository.AddFilterWhenRetrievingItemsFromDatabase(gender => gender.IdCountry == idCountry, false);

            return _repository.GetAllItems();
        }
    }
}
