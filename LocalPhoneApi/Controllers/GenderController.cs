using LocalPhoneDomain.Models;
using LocalPhoneDomain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LocalPhoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = Policies.DEFAULT_ADMIN_POLICY_NAME)]
    public class GenderController : Controller
    {
        private readonly IGenderService _service;
        private readonly ICountryService _countryService;

        public GenderController(IGenderService genderService, ICountryService countryService)
        {
            _service = genderService ??
                throw new ArgumentNullException(nameof(genderService));
            _countryService = countryService;
        }

        [HttpGet]
        public IActionResult GetAllItems(bool includeCountries = false,
            string orderBy = null, string orderByDescending = null,
            int pageIndex = 1, int pageSize = 0)
        {
            var orderByAndIsDescending = CommonControllerOperations.GetOrderByThatWillBeUsedAndIfIsDescending(orderBy, orderByDescending);

            var items = _service.GetAllItems(includeCountries, orderByAndIsDescending.Item1, orderByAndIsDescending.Item2, pageIndex, pageSize);

            CommonControllerOperations.AddPaginationInfosToHttpResponseHeaders(Response, _service.PaginationInfos);

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenderByIdAsync(int id, bool includeCountries = false)
        {
            var genderFound = await _service.GetGenderByIdAsync(id, includeCountries);

            if (genderFound == null)
            {
                return NotFound();
            }

            return Ok(genderFound);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenderAsync(int id, GenderModel genderModel)
        {
            if (id != genderModel.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _service.IsThereAnActiveGenderWithTheIdAsync(id))
            {
                return NotFound();
            }

            if (!await _countryService.IsThereAnActiveCountryWithTheIdAsync(genderModel.IdCountry.Value))
            {
                ModelState.AddModelError("errors", $"Could not find a country with id = {genderModel.IdCountry.Value}");
                return BadRequest(ModelState);
            }

            var genderUpdated = await _service.UpdateGenderAsync(id, genderModel);

            if (genderUpdated != null)
            {
                return Ok(genderUpdated);
            }

            return UnprocessableEntity();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenderAsync(GenderModel genderModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _countryService.IsThereAnActiveCountryWithTheIdAsync(genderModel.IdCountry.Value))
            {
                ModelState.AddModelError("errors", $"Could not find a country with id = {genderModel.IdCountry.Value}");
                return BadRequest(ModelState);
            }

            var genderCreated = await _service.CreateGenderAsync(genderModel);

            if (genderCreated != null)
            {
                return CreatedAtAction("CreateGenderAsync", genderCreated);
            }

            return UnprocessableEntity();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenderByIdAsync(int id)
        {
            bool deleted = await _service.DeleteGenderByIdAsync(id);

            if (deleted)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpGet("byCountry/{idCountry}")]
        public async Task<IActionResult> GetGenderByCountryAsync(int idCountry)
        {
            if (!await _countryService.IsThereAnActiveCountryWithTheIdAsync(idCountry)) {
                return NotFound();
            }

            return Ok(_service.GetGendersByCountry(idCountry));
        }
    }
}
