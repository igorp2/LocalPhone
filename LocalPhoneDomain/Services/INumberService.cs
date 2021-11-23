using LocalPhoneDomain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalPhoneDomain.Services
{
    public interface INumberService : IPaginatedService
    {
        IEnumerable<NumberModel> GetAllItems(
            string orderByPropertyName = null, bool isOrderByInDescendingOrder = false,
            int pageIndex = 1, int pageSize = 0);

        Task<NumberModel> GetNumberByIdAsync(int id);

        IEnumerable<NumberModel> GetNumberByCustomer(string idCustomer);

        Task<NumberModel> CreateNumberAsync(NumberModel number);

        Task<NumberModel> UpdateNumberAsync(int id, NumberModel newNumberModel);

        Task<bool> DeleteNumberByIdAsync(int id);

        Task<bool> IsThereAnActiveNumberWithTheIdAsync(int id);

        IEnumerable<NumberModel> GetCustomerByNumber(string phoneNumber);
    }
}
