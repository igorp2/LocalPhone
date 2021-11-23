using LocalPhoneDomain.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LocalPhoneDomain.Services
{
    public interface ICustomerService : IPaginatedService
    {
        IEnumerable<CustomerModel> GetAllItems(bool includeCountries = false,
            bool includeNumbers = false, bool includeAddresses = false,
            string orderByPropertyName = null, bool isOrderByInDescendingOrder = false,
            int pageIndex = 1, int pageSize = 0);

        IEnumerable<CustomerModel> GetCustomersByCountry(int idCountry);

        Task<CustomerModel> GetCustomerByIdAsync(string id, bool includeCountries = false,
            bool includeNumbers = false, bool includeAddresses = false);

        Task<CustomerModel> CreateCustomerAsync(RegistrationInformationModel registrationInformation);

        Task<CustomerModel> CreateCustomerAsync(CustomerModel newCustomer);

        Task<CustomerModel> UpdateCustomerAsync(string id, CustomerModel newCustomer);

        Task<bool> DeleteCustomerLogicallyByIdAsync(string id);

        Task<bool> IsThereACustomerThatIsNotInactiveWithTheIdAsync(string id);

        Task<StringContent> GetStringContentWithVerificationCodeAsync(CustomerModel customer);
    }
}
