using LocalPhoneDomain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LocalPhoneDomain.Services;
using LocalPhoneDomain;
using Microsoft.AspNetCore.Http;

namespace LocalPhoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;
        private readonly ICountryService _countryService;
        private readonly IVoxboneService _voxboneService;

        public CustomerController(ICustomerService service, ICountryService countryService,
            IVoxboneService voxboneService)
        {
            _service = service;
            _countryService = countryService;
            _voxboneService = voxboneService;
        }

        [HttpGet]
        public IActionResult GetAllItems(bool includeCountries = false,
            bool includeNumbers = false, bool includeAddress = false,
            string orderBy = null, string orderByDescending = null,
            int pageIndex = 1, int pageSize = 0)
        {
            var orderByAndIsDescending = CommonControllerOperations.GetOrderByThatWillBeUsedAndIfIsDescending(orderBy, orderByDescending);

            var items = _service.GetAllItems(includeCountries,
                includeNumbers, includeAddress,
                orderByAndIsDescending.Item1, orderByAndIsDescending.Item2, pageIndex, pageSize);

            CommonControllerOperations.AddPaginationInfosToHttpResponseHeaders(Response, _service.PaginationInfos);

            return Ok(items);
        }

        [HttpGet("{phoneNumber}")]
        public async Task<IActionResult> GetCustomerByIdAsync(string phoneNumber,
            bool includeCountries = false, bool includeNumbers = false,
            bool includeAddress = false)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return BadRequest();
            }

            var customerFound = await _service.GetCustomerByIdAsync(phoneNumber, includeCountries,
                includeNumbers, includeAddress);

            if (customerFound == null)
            {
                return NotFound();
            }

            return Ok(customerFound);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterNewCustomerAsync(RegistrationInformationModel registrationInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _countryService.IsThereAnActiveCountryWithTheIdAsync(registrationInformation.IdCountry.Value))
            {
                ModelState.AddModelError("errors", $"Could not find a country with id = {registrationInformation.IdCountry.Value}");
                return BadRequest(ModelState);
            }

            var customerCreated = await _service.CreateCustomerAsync(registrationInformation);

            if (customerCreated != null)
            {
                var stringContent = await _service.GetStringContentWithVerificationCodeAsync(customerCreated);
                var smsRelatedInfos = await _voxboneService.MakeRequestToVoxboneApiAsync(stringContent, customerCreated.PhoneNumber);

                if (smsRelatedInfos != null) {
                    return Ok(new { customerCreated.PhoneNumber, customerCreated.VerificationCode, smsRelatedInfos });
                }

                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { Errors = $"Could not send SMS to number {customerCreated.PhoneNumber}" });
            }
            else
            {
                ModelState.AddModelError("errors", $"The phone number {registrationInformation.PhoneNumber} " +
                    $"is already registered in application");
                return BadRequest(ModelState);
            }
        }

        [HttpGet("notifyAgain/{phoneNumber}")]
        public async Task<IActionResult> SendNotificationWithVerificationCodeAgainAsync(string phoneNumber)
        {
            CustomerModel customerFound = await _service.GetCustomerByIdAsync(phoneNumber);

            if (customerFound == null)
            {
                return NotFound();
            }
            else if (customerFound.Status != CustomerStatuses.PENDING)
            {
                ModelState.AddModelError("errors", $"The customer that has the phone number {phoneNumber} does not" +
                    $" have a PENDING state, so he can not be notified with verification code");
                return BadRequest(ModelState);
            }

            var stringContent = await _service.GetStringContentWithVerificationCodeAsync(customerFound);
            var smsRelatedInfos = await _voxboneService.MakeRequestToVoxboneApiAsync(stringContent, customerFound.PhoneNumber);

            if (smsRelatedInfos != null) {
                return Ok(new { customerFound.PhoneNumber, customerFound.VerificationCode, smsRelatedInfos });
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new { Errors = $"Could not send SMS to number {phoneNumber}" } );
        }

        [HttpPut("{phoneNumber}")]
        public async Task<IActionResult> UpdateCustomerAsync(string phoneNumber, CustomerModel newCustomerModel)
        {
            if (phoneNumber != newCustomerModel.PhoneNumber || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var customerFound = await _service.GetCustomerByIdAsync(phoneNumber);

            if (customerFound == null)
            {
                return NotFound();
            }

            var customerUpdated = await _service.UpdateCustomerAsync(phoneNumber, newCustomerModel);
                
            if (customerUpdated != null)
            {
                return Ok(customerUpdated);
            }

            return UnprocessableEntity();
        }

        [HttpDelete("{phoneNumber}")]
        public async Task<IActionResult> DeleteCustomerByIdAsync(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                return BadRequest();
            }

            if (await _service.DeleteCustomerLogicallyByIdAsync(phoneNumber))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpGet("byCountry/{idCountry}")]
        public async Task<IActionResult> GetCustomersByCountryAsync(int idCountry, bool includeNumbers = false,
            bool includeAddresses = false)
        {
            if (!await _countryService.IsThereAnActiveCountryWithTheIdAsync(idCountry)) {
                return NotFound();
            }

            return Ok(_service.GetCustomersByCountry(idCountry));
        }
    }
}
