using LocalPhoneDomain.Models;
using LocalPhoneDomain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LocalPhoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = Policies.DEFAULT_ADMIN_POLICY_NAME)]
    public class NumberController : ControllerBase
    {
        private readonly INumberService _service;
        private readonly ICustomerService _customerService;

        public NumberController(INumberService numberService, ICustomerService customerService)
        {
            _service = numberService ??
                throw new ArgumentNullException(nameof(numberService));
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult GetAllItems(
            string orderBy = null, string orderByDescending = null,
            int pageIndex = 1, int pageSize = 0)
        {
            var orderByAndIsDescending = CommonControllerOperations.GetOrderByThatWillBeUsedAndIfIsDescending(orderBy, orderByDescending);

            var items = _service.GetAllItems(
                orderByAndIsDescending.Item1, orderByAndIsDescending.Item2, pageIndex, pageSize);

            CommonControllerOperations.AddPaginationInfosToHttpResponseHeaders(Response, _service.PaginationInfos);

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNumberByIdAsync(int id)
        {
            var number = await _service.GetNumberByIdAsync(id);

            if (number == null)
            {
                return NotFound();
            }

            return Ok(number);
        }

        [HttpGet("byCustomer/{idCustomer}")]
        public async Task<IActionResult> GetNumberByCustomerAsync(string idCustomer)
        {
            if (!await _customerService.IsThereACustomerThatIsNotInactiveWithTheIdAsync(idCustomer))
            {
                ModelState.AddModelError("errors", $"Could not find a customer with id = {idCustomer}");
                return BadRequest(ModelState);
            }

            var numbers = _service.GetNumberByCustomer(idCustomer);

            if (!numbers.Any())
            {
                return NotFound();
            }

            return Ok(numbers);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNumberAsync(int id, NumberModel newNumberModel)
        {
            if (id != newNumberModel.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _customerService.IsThereACustomerThatIsNotInactiveWithTheIdAsync(newNumberModel.IdCustomer))
            {
                ModelState.AddModelError("errors", $"Could not find a customer with id = {newNumberModel.IdCustomer}");
                return BadRequest(ModelState);
            }

            var numberUpdated = await _service.UpdateNumberAsync(id, newNumberModel);

            if (numberUpdated != null)
            {
                return Ok(numberUpdated);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAvailableNumberAsync(NumberModel number)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _customerService.IsThereACustomerThatIsNotInactiveWithTheIdAsync(number.IdCustomer))
            {
                ModelState.AddModelError("errors", $"Could not find a customer with id = {number.IdCustomer}");
                return BadRequest(ModelState);
            }

            var numberCreated = await _service.CreateNumberAsync(number);

            if (numberCreated != null)
            {
                return CreatedAtAction("PostAvailableNumberModel", numberCreated);
            }

            return UnprocessableEntity(number);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNumberByIdAsync(int id)
        {
            bool deleted = await _service.DeleteNumberByIdAsync(id);

            if (deleted)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
