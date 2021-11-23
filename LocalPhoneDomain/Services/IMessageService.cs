using LocalPhoneDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LocalPhoneDomain.Services
{
    public interface IMessageService : IPaginatedService
    {
        IEnumerable<MessageModel> GetAllItems(bool includeCustomerSending = false,
            bool includeCustomerReceiving = false,
            string orderByPropertyName = null, bool isOrderByInDescendingOrder = false,
            int pageIndex = 1, int pageSize = 0);

        Task<MessageModel> GetMessageByIdAsync(int id, bool includeCustomerSending = false,
            bool includeCustomerReceiving = false);

        Task<MessageModel> CreateMessageAsync(MessageModel newMessage);

        Task<MessageModel> UpdateMessageAsync(int id, MessageModel newMessage);

        Task<bool> DeleteMessageLogicallyByIdAsync(int id);

        Task<bool> IsThereAnActiveMessageWithTheIdAsync(int id);

        IEnumerable<MessageModel> GetChatMessage(string idSendedBy, string idRecievedBy,
            bool includeCustomerSending = false, bool includeCustomerReceiving = false);

        StringContent GetStringContent(string text);
    }
}
