using LocalPhoneApi.Data;
using LocalPhoneDomain;
using LocalPhoneDomain.Models;
using LocalPhoneDomain.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LocalPhoneApi.Services
{
    public class MessageService : BaseService<MessageModel>, IMessageService
    {
        private readonly IApiRepository<MessageModel> _repository;
        private readonly IConfiguration _configuration;

        private readonly string voxbonePhoneNumber;

        private static readonly ImmutableDictionary<string, Expression<Func<MessageModel, object>>> _sortOptions =
            new Dictionary<string, Expression<Func<MessageModel, object>>>
            {
                {"date", message => message.CreationDate },
            }.ToImmutableDictionary();

        protected override ImmutableDictionary<string, Expression<Func<MessageModel, object>>> PropertyNamesAndTheirOrderByExpressions { get => _sortOptions; }

        private PaginationInformations paginationInformations;
        PaginationInformations IPaginatedService.PaginationInfos => paginationInformations;

        public MessageService(IApiRepository<MessageModel> repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;

            voxbonePhoneNumber = _configuration["Voxbone:E164PhoneNumber"];
        }

        public async Task<MessageModel> CreateMessageAsync(MessageModel newMessage)
        {
            newMessage.CreationDate ??= DateTime.Now;
            newMessage.Status = ModelStatuses.ACTIVE;
            newMessage.LastModificationDate ??= DateTime.Now;
                        
            return await _repository.AddNewItemAsync(newMessage);
        }

        public async Task<bool> DeleteMessageLogicallyByIdAsync(int id)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(message => message.Status == ModelStatuses.ACTIVE);

            return await _repository.DeleteLogicallyTheItemWithTheIdAsync(id);
        }

        public async Task<bool> IsThereAnActiveMessageWithTheIdAsync(int id)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(message => message.Status == ModelStatuses.ACTIVE);

            return await _repository.ExistsTheItemWithTheIdAsync(id);
        }

        public IEnumerable<MessageModel> GetAllItems(bool includeCustomerSending = false,
            bool includeCustomerReceiving = false,
            string orderByPropertyName = null, bool isOrderByInDescendingOrder = false,
            int pageIndex = 1, int pageSize = 0)
        {
            var orderByExpression = GetOrderByExpressionForPropertyName(orderByPropertyName) ?? (message => message.CreationDate);

            _repository.AddFilterWhenRetrievingItemsFromDatabase(message => message.Status == ModelStatuses.ACTIVE);
            _repository.ClearIncludes();
            _repository.AddOrderByWhenRetrievingItemsFromDatabase(orderByExpression, isOrderByInDescendingOrder);

            if (includeCustomerSending)
            {
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(message => message.CustomerSending,
                    clearPreviousIncludes: false);
            }

            if (includeCustomerReceiving)
            {
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(message => message.CustomerReceiving,
                    clearPreviousIncludes: false);
            }

            var items = _repository.GetAllItems(pageIndex, pageSize);

            paginationInformations = _repository.PaginationInformations;

            return items;
        }

        public async Task<MessageModel> GetMessageByIdAsync(int id, bool includeCustomerSending = false,
            bool includeCustomerReceiving = false)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(message => message.Status == ModelStatuses.ACTIVE);
            _repository.ClearIncludes();

            if (includeCustomerSending)
            {
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(message => message.CustomerSending,
                    clearPreviousIncludes: false);
            }

            if (includeCustomerReceiving)
            {
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(message => message.CustomerReceiving,
                    clearPreviousIncludes: false);
            }

            return await _repository.GetItemByIdAsync(id);
        }

        public async Task<MessageModel> UpdateMessageAsync(int id, MessageModel newMessage)
        {
            newMessage.CreationDate ??= DateTime.Now;
            newMessage.Status = ModelStatuses.ACTIVE;
            newMessage.LastModificationDate ??= DateTime.Now;

            _repository.AddFilterWhenRetrievingItemsFromDatabase(message => message.Status == ModelStatuses.ACTIVE);

            return await _repository.UpdateTheItemWithTheIdAsync(id, newMessage);
        }

        public IEnumerable<MessageModel> GetChatMessage(string idSendedBy, string idRecievedBy, 
            bool includeCustomerSending = false, bool includeCustomerReceiving = false)
        {

            //TODO: adaptar o código posteriormente com a chamada do order by que o Natan está desesnvolvendo
            _repository.AddFilterWhenRetrievingItemsFromDatabase(message => message.Status == ModelStatuses.ACTIVE);
            _repository.AddFilterWhenRetrievingItemsFromDatabase(message => message.IdCustomerSending == idSendedBy, false);
            _repository.AddFilterWhenRetrievingItemsFromDatabase(message => message.IdCustomerReceiving == idRecievedBy, false);
            _repository.ClearIncludes();

            if (includeCustomerSending)
            {
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(message => message.CustomerSending,
                    clearPreviousIncludes: false);
            }

            if (includeCustomerReceiving)
            {
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(message => message.CustomerReceiving,
                    clearPreviousIncludes: false);
            }

            return _repository.GetAllItems();
        }

        public StringContent GetStringContent(string text)
        {
            var requestContent = new StringContent(
                    $"{{\"from\":\"+{voxbonePhoneNumber}\", \"msg\":\"{text}\"}}",
                    Encoding.UTF8, "application/json");

            return requestContent;
        }
    }
}
