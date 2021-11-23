using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LocalPhoneDomain.Models;
using Microsoft.AspNetCore.Authorization;
using LocalPhoneDomain.Services;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Collections.Immutable;

namespace LocalPhoneApi
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = Policies.DEFAULT_ADMIN_POLICY_NAME)]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _service;

        public CountryController(ICountryService countryService)
        {
            _service = countryService;
        }

        [HttpGet]
        public IActionResult GetAllItems(bool includeStates = false, bool includeCities = false, 
            string orderBy = null, string orderByDescending = null,
            int pageIndex = 1, int pageSize = 0)
        {
            var orderByAndIsDescending = CommonControllerOperations.GetOrderByThatWillBeUsedAndIfIsDescending(orderBy, orderByDescending);

            var items = _service.GetAllItems(includeStates, includeCities, 
                orderByAndIsDescending.Item1, orderByAndIsDescending.Item2,
                pageIndex, pageSize);

            CommonControllerOperations.AddPaginationInfosToHttpResponseHeaders(Response, _service.PaginationInfos);

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryByIdAsync(int id, bool includeStates = false,
            bool includeCities = false)
        {
            var countryFound = await _service.GetCountryByIdAsync(id, includeStates, includeCities);

            if (countryFound == null)
            {
                return NotFound();
            }

            return Ok(countryFound);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountryAsync(int id, CountryModel countryModel)
        {
            if (id != countryModel.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _service.IsThereAnActiveCountryWithTheIdAsync(id))
            {
                return NotFound();
            }

            var countryUpdated = await _service.UpdateCountryAsync(id, countryModel);

            if (countryUpdated != null)
            {
                return Ok(countryUpdated);
            }

            return UnprocessableEntity();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountryAsync(CountryModel countryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var itemCreated = await _service.CreateCountryAsync(countryModel);

            if (itemCreated != null)
            {
                return CreatedAtAction("CreateCountryAsync", itemCreated);
            }

            return UnprocessableEntity();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountryByIdAsync(int id)
        {
            var deleted = await _service.DeleteCountryByIdAsync(id);

            if (deleted)
            {
                return Ok();
            }

            return NotFound();
        }

    }
}
