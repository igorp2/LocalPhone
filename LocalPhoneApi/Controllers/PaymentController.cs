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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;
        private readonly IAvailableNumberService _availableNumberService;

        public PaymentController(IPaymentService paymentService,
            IAvailableNumberService availableNumberService)
        {
            _service = paymentService ??
                throw new ArgumentNullException(nameof(paymentService));
            _availableNumberService = availableNumberService;
        }

        [HttpPost]
        public async Task<IActionResult> Payment(PaymentModel payment, string boughtNumber, int idAvailableNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _availableNumberService.IsThereAnActiveAvailableNumberWithThisIdAsync(idAvailableNumber))
            {
                ModelState.AddModelError("errors", $"Could not find an available number with id = {idAvailableNumber}");
                return BadRequest(ModelState);
            }

            var paymentCreated = await _service.CreatePaymentAsync(payment);

            if (paymentCreated != null)
            {
                var newPayment = CreatedAtAction("PostPaymentModel", paymentCreated);

                if (await _service.RegisterBoughtNumberAsync(boughtNumber, idAvailableNumber,
                    ((PaymentModel)newPayment.Value).IdCustomer, ((BaseModel)newPayment.Value).Id))
                {
                    return Ok("Success");
                }
            }

            return Ok("Failed");
        }

        [HttpPut("DisableNumber/{idNumber}")]
        public async Task<IActionResult> DisableNumber(int idNumber)
        {
            var disabled = await _service.DisableNumberAsync(idNumber);

            if (disabled)
                return Ok();

            return NotFound();
        }

        [HttpPut("EnableNumber/{idNumber}")]
        public async Task<IActionResult> EnableNumber(int idNumber)
        {
            var enabled = await _service.EnableNumberAsync(idNumber);

            if (enabled)
                return Ok();

            return NotFound();
        }

        [HttpGet("{idCustomer}")]
        public IActionResult GetPaymentByCustomer(string idCustomer,
            string orderBy = null, string orderByDescending = null,
            int pageIndex = 1, int pageSize = 0)
        {
            var orderByAndIsDescending = CommonControllerOperations.GetOrderByThatWillBeUsedAndIfIsDescending(orderBy, orderByDescending);

            var payments = _service.GetPaymentByCustomer(idCustomer, 
                orderByAndIsDescending.Item1, orderByAndIsDescending.Item2, pageIndex, pageSize);

            if (payments == null)
                return NotFound();

            CommonControllerOperations.AddPaginationInfosToHttpResponseHeaders(Response, _service.PaginationInfos);

            return Ok(payments);
        }
    }
}
