using LocalPhoneDomain.Models;
using LocalPhoneDomain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LocalPhoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = Policies.DEFAULT_ADMIN_POLICY_NAME)]
    public class StateController : ControllerBase
    {
        private readonly IStateService _service;
        private readonly ICountryService _countryService;

        public StateController(IStateService stateService, ICountryService countryService)
        {
            _service = stateService;
            _countryService = countryService;
        }

        [HttpGet]
        public IActionResult GetAllItems(bool includeCountries = false, bool includeCities = false,
            string orderBy = null, string orderByDescending = null,
            int pageIndex = 1, int pageSize = 0)
        {
            var orderByAndIsDescending = CommonControllerOperations.GetOrderByThatWillBeUsedAndIfIsDescending(orderBy, orderByDescending);

            var items = _service.GetAllItems(includeCountries, includeCities, orderByAndIsDescending.Item1, orderByAndIsDescending.Item2, pageIndex, pageSize);

            CommonControllerOperations.AddPaginationInfosToHttpResponseHeaders(Response, _service.PaginationInfos);

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStateByIdAsync(int id, bool includeCountries = false,
            bool includeCities = false)
        {
            var state = await _service.GetStateByIdAsync(id, includeCountries, includeCities);

            if (state == null)
            {
                return NotFound();
            }

            return Ok(state);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStateAsync(int id, StateModel newStateModel)
        {
            if (id != newStateModel.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _service.IsThereAnActiveStateWithTheIdAsync(id))
            {
                return NotFound();
            }

            if (!await _countryService.IsThereAnActiveCountryWithTheIdAsync(newStateModel.IdCountry.Value))
            {
                ModelState.AddModelError("errors", $"Could not find a country with id = {newStateModel.IdCountry.Value}");
                return BadRequest(ModelState);
            }

            var stateUpdated = await _service.UpdateStateAsync(id, newStateModel);

            if (stateUpdated != null)
            {
                return Ok(stateUpdated);
            }

            return UnprocessableEntity();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStateAsync(StateModel newStateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _countryService.IsThereAnActiveCountryWithTheIdAsync(newStateModel.IdCountry.Value))
            {
                ModelState.AddModelError("errors", $"Could not find a country with id = {newStateModel.IdCountry.Value}");
                return BadRequest(ModelState);
            }

            var stateCreated = await _service.CreateStateAsync(newStateModel);

            if (stateCreated != null)
            {
                return CreatedAtAction("CreateStateAsync", stateCreated);
            }

            return UnprocessableEntity();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStateByIdAsync(int id)
        {
            var deleted = await _service.DeleteStateByIdAsync(id);

            if (deleted)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpGet("byCountry/{idCountry}")]
        public async Task<IActionResult> GetStatesByCountryAsync(int idCountry)
        {
            if (!await _countryService.IsThereAnActiveCountryWithTheIdAsync(idCountry)) {
                return NotFound();
            }

            return Ok(_service.GetStatesByCountry(idCountry));
        }
    }
}
