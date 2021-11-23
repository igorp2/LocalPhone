using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LocalPhoneDomain.Models;
using Microsoft.AspNetCore.Authorization;
using LocalPhoneDomain.Services;

namespace LocalPhoneApi
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = Policies.DEFAULT_ADMIN_POLICY_NAME)]
    public class CityController : ControllerBase
    {
        private readonly ICityService _service;
        private readonly IStateService _stateService;

        public CityController(ICityService cityService, IStateService stateService)
        {
            _service = cityService;
            _stateService = stateService;
        }

        [HttpGet]
        public IActionResult GetAllItems(bool includeStates = false, bool includeCountries = false,
            string orderBy = null, string orderByDescending = null,
            int pageIndex = 1, int pageSize = 0)
        {
            var orderByAndIsDescending = CommonControllerOperations.GetOrderByThatWillBeUsedAndIfIsDescending(orderBy, orderByDescending);

            var items = _service.GetAllItems(includeStates, includeCountries, 
                orderByAndIsDescending.Item1, orderByAndIsDescending.Item2, pageIndex, pageSize);

            CommonControllerOperations.AddPaginationInfosToHttpResponseHeaders(Response, _service.PaginationInfos);

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityByIdAsync(int id, bool includeStates = false, bool includeCountries = false)
        {
            var cityFound = await _service.GetCityByIdAsync(id, includeStates, includeCountries);

            if (cityFound == null)
            {
                return NotFound();
            }

            return Ok(cityFound);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCityAsync(int id, CityModel newCityModel)
        {
            if (id != newCityModel.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _service.IsThereAnActiveCityWithThisIdAsync(id))
            {
                return NotFound();
            }

            if (!await _stateService.IsThereAnActiveStateWithTheIdAsync(newCityModel.IdState.Value))
            {
                ModelState.AddModelError("errors", $"Could not find a state with id = {newCityModel.IdState.Value}");
                return BadRequest(ModelState);
            }

            var cityUpdated = await _service.UpdateCityAsync(id, newCityModel);

            if (cityUpdated != null)
            {
                return Ok(cityUpdated);
            }

            return UnprocessableEntity();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCityAsync(CityModel newCityModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _stateService.IsThereAnActiveStateWithTheIdAsync(newCityModel.IdState.Value))
            {
                ModelState.AddModelError("errors", $"Could not find a state with id = {newCityModel.IdState.Value}");
                return BadRequest(ModelState);
            }

            var itemCreated = await _service.CreateCityAsync(newCityModel);

            if (itemCreated != null)
            {
                return CreatedAtAction("PostCityModel", itemCreated);
            }

            return UnprocessableEntity();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCityByIdAsync(int id)
        {
            var deleted = await _service.DeleteCityByIdAsync(id);

            if (deleted)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpGet("byState/{idState}")]
        public async Task<IActionResult> GetCitiesByStateAsync(int idState)
        {
            if (!await _stateService.IsThereAnActiveStateWithTheIdAsync(idState))
            {
                return NotFound();
            }

            return Ok(_service.GetCitiesByState(idState));
        }
    }
}
