using LocalPhoneDomain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalPhoneDomain.Services
{
    public interface IPaymentService : IPaginatedService
    {
        Task<PaymentModel> CreatePaymentAsync(PaymentModel payment);

        Task<bool> RegisterBoughtNumberAsync(string boughtNumber, int idNumber, 
                                             string idCustomer, int idPayment);

        Task<bool> DisableNumberAsync(int id);

        Task<bool> EnableNumberAsync(int id);

        IEnumerable<PaymentModel> GetPaymentByCustomer(string idCustomer,
            string orderByPropertyName = null, bool isOrderByInDescendingOrder = false,
            int pageIndex = 1, int pageSize = 0);
    }
}
