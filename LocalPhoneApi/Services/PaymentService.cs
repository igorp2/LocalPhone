using LocalPhoneDomain.Models;
using LocalPhoneDomain.Services;
using System;
using System.Threading.Tasks;
using Stripe;
using LocalPhoneApi.Data;
using LocalPhoneDomain;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;

namespace LocalPhoneApi.Services
{
    public class PaymentService : BaseService<PaymentModel>, IPaymentService
    {
        private readonly IApiRepository<PaymentModel> _repository;
        private readonly IApiRepository<NumberModel> _numberRepository;
        private readonly IApiRepository<AvailableNumberModel> _availableNumberRepository;

        private static readonly ImmutableDictionary<string, Expression<Func<PaymentModel, object>>> _sortOptions =
            new Dictionary<string, Expression<Func<PaymentModel, object>>>
            {
                {"date", payment => payment.CreationDate },
                {"cardpayment", payment => payment.CardNumber },
                {"email", payment => payment.Email },
                {"nameoncard", payment => payment.NameOnCard },
                {"zipcode", payment => payment.NameOnCard },
                {"cvc", payment => payment.Cvc },
            }.ToImmutableDictionary();

        protected override ImmutableDictionary<string, Expression<Func<PaymentModel, object>>> PropertyNamesAndTheirOrderByExpressions { get => _sortOptions; }

        private PaginationInformations paginationInformations;
        PaginationInformations IPaginatedService.PaginationInfos => paginationInformations;

        public PaymentService(IApiRepository<PaymentModel> repository,
                              IApiRepository<NumberModel> numberRepository,
                              IApiRepository<AvailableNumberModel> availableNumberRepository)
        {
            _repository = repository;
            _numberRepository = numberRepository;
            _availableNumberRepository = availableNumberRepository;
        }

        private async Task<dynamic> PayAsync(PaymentModel payment)
        {
            try
            {
                StripeConfiguration.ApiKey = "sk_test_51JhxSILvi0EeUTLCKccdYK1QNCFRiJMIoyOJB0Ppq4z973vviAh9mUBPdUKlEWbJ9HHe62BHyvAalucFO3Acf0Mi00Rwwy2Kw4";

                var optionsToken = new TokenCreateOptions
                {
                    Card = new CreditCardOptions
                    {
                        Number = payment.CardNumber,
                        ExpMonth = payment.ExpirationMonth,
                        ExpYear = payment.ExpirationYear,
                        Cvc = payment.Cvc.ToString()
                    }
                };

                var serviceToken = new TokenService();
                Token stripeToken = await serviceToken.CreateAsync(optionsToken);

                var chargeOptions = new ChargeCreateOptions
                {
                    Amount = payment.OrderAmount,
                    Currency = "USD",
                    Description = "LocalPhone subscription.",
                    Source = stripeToken.Id
                };

                var serviceCharge = new ChargeService();
                Charge charge = await serviceCharge.CreateAsync(chargeOptions);

                if (charge.Paid)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<PaymentModel> CreatePaymentAsync(PaymentModel payment)
        {

            await PayAsync(payment);

            payment.CreationDate = DateTime.Now;

            var newPayment = await _repository.AddNewItemAsync(payment);

            return newPayment;

        }

        public async Task<bool> RegisterBoughtNumberAsync(string boughtNumber, int idAvailableNumber, 
                                                     string idCustomer, int idPayment)
        {
            var newBoughtNumber = new NumberModel
            {
                IdCustomer = idCustomer,
                IdPayment = idPayment,
                PhoneNumber = boughtNumber
            };

            var newNumber = await _numberRepository.AddNewItemAsync(newBoughtNumber);

            var removed = await RemoveAvailableNumberAsync(idAvailableNumber);

            if( newNumber != null && removed)
                return true;

            return false;

        }

        private async Task<bool> RemoveAvailableNumberAsync(int idNumber)
        {
            return await _availableNumberRepository.DeleteLogicallyTheItemWithTheIdAsync(idNumber);
        }

        public async Task<bool> DisableNumberAsync(int idNumber)
        {
            var number = await _numberRepository.GetItemByIdAsync(idNumber);
            if (number == null)
                return false;

            number.Status = ModelStatuses.INACTIVE;

            var payment = await _repository.GetItemByIdAsync(number.IdPayment);
            if (payment == null)
                return false;

            payment.Status = ModelStatuses.INACTIVE;

            await _numberRepository.UpdateTheItemWithTheIdAsync(idNumber, number);
            await _repository.UpdateTheItemWithTheIdAsync(number.IdPayment, payment);

            return true;
        }

        public async Task<bool> EnableNumberAsync(int idNumber)
        {
            var number = await _numberRepository.GetItemByIdAsync(idNumber);
            if (number == null)
                return false;

            number.Status = ModelStatuses.ACTIVE;

            var payment = await _repository.GetItemByIdAsync(number.IdPayment);
            if (payment == null)
                return false;

            payment.Status = ModelStatuses.ACTIVE;

            await PayAsync(payment);
            await _numberRepository.UpdateTheItemWithTheIdAsync(idNumber, number);
            await _repository.UpdateTheItemWithTheIdAsync(number.IdPayment, payment);

            return true;
        }

        public IEnumerable<PaymentModel> GetPaymentByCustomer(string idCustomer,
            string orderByPropertyName = null, bool isOrderByInDescendingOrder = false,
            int pageIndex = 1, int pageSize = 0)
        {
            var orderByExpression = GetOrderByExpressionForPropertyName(orderByPropertyName) ?? (payment => payment.CreationDate);

            _repository.AddFilterWhenRetrievingItemsFromDatabase(payment => payment.IdCustomer == idCustomer);
            _repository.AddOrderByWhenRetrievingItemsFromDatabase(orderByExpression, isOrderByInDescendingOrder);

            var items = _repository.GetAllItems(pageIndex, pageSize);

            paginationInformations = _repository.PaginationInformations;

            return items;
        }
    }
}
