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
    public class CustomerService : BaseService<CustomerModel>, ICustomerService
    {
        private readonly IApiRepository<CustomerModel, string> _repository;
        private readonly ICountryService _countryService;

        private readonly IConfiguration _configuration;

        private readonly string voxbonePhoneNumber;

        private static readonly ImmutableDictionary<string, Expression<Func<CustomerModel, object>>> _sortOptions =
            new Dictionary<string, Expression<Func<CustomerModel, object>>>
            {
                {"date", state => state.CreationDate },
                {"phonenumber", customer => customer.PhoneNumber },
                {"operationalsystem", customer => customer.OperationalSystem },
                {"email", customer => customer.Email },
                {"firstname", customer => customer.FirstName },
                {"lastname", customer => customer.LastName },
            }.ToImmutableDictionary();

        protected override ImmutableDictionary<string, Expression<Func<CustomerModel, object>>> PropertyNamesAndTheirOrderByExpressions { get => _sortOptions; }

        private PaginationInformations paginationInformations;
        PaginationInformations IPaginatedService.PaginationInfos => paginationInformations;

        public CustomerService(IApiRepository<CustomerModel, string> repository,
            IConfiguration configuration, ICountryService countryService)
        {
            _repository = repository;
            _configuration = configuration;

            voxbonePhoneNumber = _configuration["Voxbone:E164PhoneNumber"];
            _countryService = countryService;
        }

        public async Task<CustomerModel> CreateCustomerAsync(CustomerModel newCustomer)
        {
            var phoneNumberWithCountryPhonecode = await GetPhoneNumberWithCountryPhonecodeInsertedAsync(
                newCustomer.PhoneNumber, newCustomer.IdCountry.Value);

            if (phoneNumberWithCountryPhonecode == null)
            {
                return null;
            }

            if (await IsThereACustomerThatIsNotInactiveWithTheIdAsync(phoneNumberWithCountryPhonecode))
            {
                return null;
            }

            newCustomer.PhoneNumber = phoneNumberWithCountryPhonecode;
            newCustomer.Status = CustomerStatuses.PENDING;
            newCustomer.CreationDate = DateTime.Now;

            AddVerificationCodeAndRelatedInformationToCostumerModel(newCustomer);

            return await _repository.AddNewItemAsync(newCustomer);
        }

        public async Task<CustomerModel> CreateCustomerAsync(RegistrationInformationModel registrationInformation)
        {
            registrationInformation.PhoneNumber = await GetPhoneNumberWithCountryPhonecodeInsertedAsync(
                registrationInformation.PhoneNumber, registrationInformation.IdCountry.Value);

            if (registrationInformation.PhoneNumber == null)
            {
                return null;
            }

            var customerToAdd = await _repository.GetItemByIdAsync(registrationInformation.PhoneNumber);

            if (customerToAdd == null)
            {
                customerToAdd = CustomerModel.CreateFromRegistrationInformation(registrationInformation);
            }
            else if (customerToAdd.Status == CustomerStatuses.INACTIVE)
            {
                customerToAdd.CloneFromRegistrationInformation(registrationInformation);
            }

            customerToAdd.Status = CustomerStatuses.PENDING;
            customerToAdd.CreationDate = DateTime.Now;

            AddVerificationCodeAndRelatedInformationToCostumerModel(customerToAdd);

            return await _repository.AddNewItemAsync(customerToAdd);
        }

        private async Task<string> GetPhoneNumberWithCountryPhonecodeInsertedAsync(string phoneNumber, int idCountry)
        {
            var countryFound = await _countryService.GetCountryByIdAsync(idCountry);

            if (countryFound == null)
            {
                return null;
            }

            return phoneNumber.Insert(0, countryFound.Phonecode.ToString());
        }

        private void AddVerificationCodeAndRelatedInformationToCostumerModel(CustomerModel customerModel,
            bool useCurrentVerificationCodeToGenerateANewOne = false)
        {
            if (useCurrentVerificationCodeToGenerateANewOne)
            {
                customerModel.VerificationCode = GenerateVerificationCodeForPhoneNumber(customerModel.VerificationCode.ToString());
            }
            else
            {
                customerModel.VerificationCode = GenerateVerificationCodeForPhoneNumber(customerModel.PhoneNumber);
            }

            customerModel.VerificationCodeDate = DateTime.Now;
            customerModel.ValidationCodeDate = DateTime.Now.AddMinutes(_configuration.GetValue("TimeInMinutesForTheVerificationCodeToExpire", 2));
            customerModel.LastModificationDate = DateTime.Now;
        }

        private static int GenerateVerificationCodeForPhoneNumber(string phoneNumber)
        {
            int lastFourNumbersOfThePhoneNumber = Int32.Parse(phoneNumber.Substring(phoneNumber.Length - 4, 4));
            Random random = new Random(lastFourNumbersOfThePhoneNumber);
            int verificationCode = random.Next(1000, 9999);

            Console.WriteLine($"verification code: {verificationCode}");
            Console.WriteLine($"last four phone numbers: {lastFourNumbersOfThePhoneNumber}");

            return verificationCode;
        }

        public async Task<bool> DeleteCustomerLogicallyByIdAsync(string id)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(customer => customer.Status != CustomerStatuses.INACTIVE);

            CustomerModel customerFound = await _repository.GetItemByIdAsync(id);

            if (customerFound == null)
            {
                return false;
            }

            return await _repository.DeleteLogicallyTheItemWithTheIdAsync(id);
        }

        public async Task<bool> IsThereACustomerThatIsNotInactiveWithTheIdAsync(string id)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(customer => customer.Status != CustomerStatuses.INACTIVE);

            if (await _repository.GetItemByIdAsync(id) == null)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<CustomerModel> GetAllItems(bool includeCountries = false,
            bool includeNumbers = false, bool includeAddresses = false,
            string orderByPropertyName = null, bool isOrderByInDescendingOrder = false,
            int pageIndex = 1, int pageSize = 0)
        {
            var orderByExpression = GetOrderByExpressionForPropertyName(orderByPropertyName) ?? (customer => customer.PhoneNumber);

            _repository.AddIncludeWhenRetrievingItemsFromDatabase(customer => customer.Gender);
            _repository.AddFilterWhenRetrievingItemsFromDatabase(customer => customer.Status != CustomerStatuses.INACTIVE);
            _repository.AddOrderByWhenRetrievingItemsFromDatabase(orderByExpression, isOrderByInDescendingOrder);

            if (includeCountries)
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(customer => customer.Country,
                    clearPreviousIncludes: false);

            if (includeAddresses)
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(customer => customer.Addresses,
                    clearPreviousIncludes: false);

            if (includeNumbers)
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(customer => customer.Numbers,
                    clearPreviousIncludes: false);

            var items = _repository.GetAllItems(pageIndex, pageSize);

            paginationInformations = _repository.PaginationInformations;

            return items;
        }

        public async Task<CustomerModel> GetCustomerByIdAsync(string id, bool includeCountries = false, 
            bool includeNumbers = false, bool includeAddresses = false)
        {
            _repository.AddIncludeWhenRetrievingItemsFromDatabase(customer => customer.Gender);
            _repository.AddFilterWhenRetrievingItemsFromDatabase(customer => customer.Status != CustomerStatuses.INACTIVE);

            if (includeCountries)
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(customer => customer.Country,
                    clearPreviousIncludes: false);

            if (includeAddresses)
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(customer => customer.Addresses,
                    clearPreviousIncludes: false);

            if (includeNumbers)
                _repository.AddIncludeWhenRetrievingItemsFromDatabase(customer => customer.Numbers,
                    clearPreviousIncludes: false);

            CustomerModel customerFound = await _repository.GetItemByIdAsync(id);

            if (customerFound == null)
            {
                return null;
            }

            return customerFound;
        }

        public async Task<CustomerModel> UpdateCustomerAsync(string id, CustomerModel newCustomer)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(customer => customer.Status != CustomerStatuses.INACTIVE);

            CustomerModel customerFound = await _repository.GetItemByIdAsync(id);

            if (customerFound == null)
            {
                return null;
            }

            newCustomer.Status = customerFound.Status;
            newCustomer.VerificationCode = null;
            newCustomer.ValidationCodeDate = null;
            newCustomer.VerificationCodeDate = null;
            newCustomer.LastModificationDate = DateTime.Now;

            return await _repository.UpdateTheItemWithTheIdAsync(id, newCustomer);
        }
        public IEnumerable<CustomerModel> GetCustomersByCountry(int idCountry)
        {
            _repository.AddFilterWhenRetrievingItemsFromDatabase(customer => customer.IdCountry == idCountry);
            _repository.AddFilterWhenRetrievingItemsFromDatabase(customer => customer.Status != CustomerStatuses.INACTIVE, clearPreviousFilters: false);

            return _repository.GetAllItems();
        }

        public async Task<StringContent> GetStringContentWithVerificationCodeAsync(CustomerModel customer)
        {
            if (customer.ValidationCodeDate != null && customer.ValidationCodeDate < DateTime.Now)
            {
                AddVerificationCodeAndRelatedInformationToCostumerModel(customer, true);
                await _repository.UpdateTheItemWithTheIdAsync(customer.PhoneNumber, customer);
            }

            var requestContent = new StringContent(
                $"{{\"from\":\"+{voxbonePhoneNumber}\", \"msg\":\"Your LocalPhone verification code is {customer.VerificationCode}. Please do not share this information.\"}}",
                Encoding.UTF8, "application/json");

            return requestContent;
        }

    }
}
