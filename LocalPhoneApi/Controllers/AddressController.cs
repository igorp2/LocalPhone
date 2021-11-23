using LocalPhoneApi.Data;
using LocalPhoneDomain.Models;
using LocalPhoneDomain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalPhoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = Policies.DEFAULT_ADMIN_POLICY_NAME)]
    public class AddressController : Controller
    {
        private readonly IAddressService _service;
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;
        private readonly ICustomerService _customerService;
        private readonly IStateService _stateService;

        public AddressController(IAddressService service, IStateService stateService,
            ICustomerService customerService, ICountryService countryService, ICityService cityService)
        {
            _service = service;
            _stateService = stateService;
            _customerService = customerService;
            _countryService = countryService;
            _cityService = cityService;
        }

        [HttpGet]
        public IActionResult GetAllItems(bool includeCities = false,
            bool includeStates = false, bool includeCountries = false,
            bool includeCustomers = false,
            string orderBy = null, string orderByDescending = null,
            int pageIndex = 1, int pageSize = 0)
        {
            var orderByAndIsDescending = CommonControllerOperations.GetOrderByThatWillBeUsedAndIfIsDescending(orderBy, orderByDescending);

            var items = _service.GetAllItems(includeCities, includeStates, includeCountries, includeCustomers,
                orderByAndIsDescending.Item1, orderByAndIsDescending.Item2, pageIndex, pageSize);

            CommonControllerOperations.AddPaginationInfosToHttpResponseHeaders(Response, _service.PaginationInfos);

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressByIdAsync(int id, bool includeCities = false, 
            bool includeStates = false, bool includeCountries = false,
            bool includeCustomers = false)
        {
            AddressModel addressFound = await _service.GetAddressByIdAsync(id, 
                includeCities, includeStates, includeCountries, includeCustomers);

            if (addressFound == null)
            {
                return NotFound();
            }

            return Ok(addressFound);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddressAsync(int id, AddressModel newAddressModel)
        {
            if (id != newAddressModel.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            await CheckForeignKeysAndAddErrorsToModelStateIfNecessaryAsync(newAddressModel);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _service.IsThereAnActiveAddressWithThisIdAsync(id))
            {
                return NotFound();
            }

            var addressUpdated = await _service.UpdateAddressAsync(id, newAddressModel);

            if (addressUpdated != null)
            {
                return Ok(addressUpdated);
            }

            return UnprocessableEntity();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddressAsync(AddressModel newAddressModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await CheckForeignKeysAndAddErrorsToModelStateIfNecessaryAsync(newAddressModel);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addressCreated = await _service.CreateAddressAsync(newAddressModel);

            if (addressCreated != null)
            {
                return CreatedAtAction("PostAddressModel", addressCreated);
            }

            return UnprocessableEntity();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddressByIdAsync(int id)
        {
            bool deleted = await _service.DeleteAddressByIdAsync(id);

            if (deleted)
            {
                return Ok();
            }

            return NotFound();
        }

        private async Task CheckForeignKeysAndAddErrorsToModelStateIfNecessaryAsync(AddressModel address)
        {
            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (await _cityService.GetCityByIdAsync(address.IdCity.Value) == null)
            {
                ModelState.AddModelError("errors", $"Could not find a city with id = {address.IdCity.Value}");
            }

            if (await _countryService.GetCountryByIdAsync(address.IdCountry.Value) == null)
            {
                ModelState.AddModelError("errors", $"Could not find a country with id = {address.IdCountry.Value}");
            }

            if (await _customerService.GetCustomerByIdAsync(address.IdCustomer) == null)
            {
                ModelState.AddModelError("errors", $"Could not find a customer with id = {address.IdCustomer}");
            }

            if (await _stateService.GetStateByIdAsync(address.IdState.Value) == null)
            {
                ModelState.AddModelError("errors", $"Could not find a state with id = {address.IdState.Value}");
            }
        }
    }
}