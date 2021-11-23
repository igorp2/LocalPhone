using LocalPhoneDomain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using LocalPhoneDomain.Services;
using System;

namespace LocalPhoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = Policies.DEFAULT_ADMIN_POLICY_NAME)]
    public class AvailableNumberController : ControllerBase
    {
        private readonly IAvailableNumberService _service;
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;

        public AvailableNumberController(IAvailableNumberService availableNumberService,
            ICityService cityService, ICountryService countryService)
        {
            _service = availableNumberService ??
                throw new ArgumentNullException(nameof(availableNumberService));
            _cityService = cityService;
            _countryService = countryService;
        }

        [HttpGet]
        public IActionResult GetAllItems(
            string orderBy = null, string orderByDescending = null,
            int pageIndex = 1, int pageSize = 0,
            bool includeDetails = false)
        {
            var orderByAndIsDescending = CommonControllerOperations.GetOrderByThatWillBeUsedAndIfIsDescending(orderBy, orderByDescending);

            var items = _service.GetAllItems(orderByAndIsDescending.Item1, orderByAndIsDescending.Item2, pageIndex, pageSize, includeDetails);

            CommonControllerOperations.AddPaginationInfosToHttpResponseHeaders(Response, _service.PaginationInfos);

            return Ok(items);
        }


        [HttpGet("{idCountry}/{idCity?}")]
        public async Task<IActionResult> GetAvailableNumberByRegionAsync(int idCountry, int idCity)
        {
            var available = _service.GetAvailableNumberByRegionAsync(idCountry, idCity);

            if (!await _cityService.IsThereAnActiveCityWithThisIdAsync(idCity))
            {
                ModelState.AddModelError("errors", $"There isn't a city with id = {idCity}");
            }

            if (!await _countryService.IsThereAnActiveCountryWithTheIdAsync(idCountry))
            {
                ModelState.AddModelError("errors", $"There isn't a country with id = {idCountry}");
            }

            if (!available.Any())
            {
                return NotFound();
            }

            return Ok(available);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAvailableNumberAsync(AvailableNumberModel availableNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await CheckForeignKeysAndAddErrorsToModelStateIfNecessaryAsync(availableNumber);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemCreated = await _service.CreateAvailableNumberAsync(availableNumber);

            if (itemCreated != null)
            {
                return CreatedAtAction("PostAvailableNumberModel", itemCreated);
            }

            return UnprocessableEntity(availableNumber);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAvailableNumberAsync(int id, AvailableNumberModel newAvailableNumberModel)
        {
            if (id != newAvailableNumberModel.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            await CheckForeignKeysAndAddErrorsToModelStateIfNecessaryAsync(newAvailableNumberModel);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var numberUpdated = await _service.UpdateAvailableNumberAsync(id, newAvailableNumberModel);

            if (numberUpdated != null)
            {
                return Ok(numberUpdated);
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAvailableNumberByIdAsync(int id)
        {
            bool deleted = await _service.DeleteAvailableNumberByIdAsync(id);

            if (deleted)
            {
                return Ok();
            }

            return NotFound();
        }

        private async Task CheckForeignKeysAndAddErrorsToModelStateIfNecessaryAsync(AvailableNumberModel availableNumber)
        {
            if (!await _cityService.IsThereAnActiveCityWithThisIdAsync(availableNumber.idCity))
            {
                ModelState.AddModelError("errors", $"Could not find a city with id = {availableNumber.idCity}");
            }

            if (!await _countryService.IsThereAnActiveCountryWithTheIdAsync(availableNumber.idCountry))
            {
                ModelState.AddModelError("errors", $"Could not find a country with id = {availableNumber.idCountry}");
            }
        }
    }
}
