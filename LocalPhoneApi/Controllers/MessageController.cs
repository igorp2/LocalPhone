using LocalPhoneDomain.Enums;
using LocalPhoneDomain.Models;
using LocalPhoneDomain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LocalPhoneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = Policies.DEFAULT_ADMIN_POLICY_NAME)]
    public class MessageController : Controller
    {
        private readonly IMessageService _service;
        private readonly ICustomerService _customerService;
        private readonly IVoxboneService _voxboneService;
        private readonly INumberService _numberService;

        public MessageController(IMessageService service, ICustomerService customerService,
            IVoxboneService voxboneService, INumberService numberService)
        {
            _service = service;
            _customerService = customerService;
            _voxboneService = voxboneService;
            _numberService = numberService;
        }

        [HttpGet]
        public IActionResult GetAllItems(bool includeCustomerSending = false,
            bool includeCustomerReceiving = false,
            string orderBy = null, string orderByDescending = null,
            int pageIndex = 1, int pageSize = 0)
        {
            var orderByAndIsDescending = CommonControllerOperations.GetOrderByThatWillBeUsedAndIfIsDescending(orderBy, orderByDescending);

            var items = _service.GetAllItems(includeCustomerSending, includeCustomerReceiving, orderByAndIsDescending.Item1, orderByAndIsDescending.Item2, pageIndex, pageSize);

            CommonControllerOperations.AddPaginationInfosToHttpResponseHeaders(Response, _service.PaginationInfos);

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessageByIdAsync(int id, bool includeCustomerSending = false, 
            bool includeCustomerReceiving = false)
        {
            MessageModel messageFound = await _service.GetMessageByIdAsync(id, includeCustomerSending,
                includeCustomerReceiving);

            if (messageFound == null)
            {
                return NotFound();
            }

            return Ok(messageFound);
        }

        [HttpGet("SendedBy/{idSendedBy}/RecievedBy/{idRecievedBy}")]
        public IActionResult GetChatMessage(string idSendedBy, string idRecievedBy)
        {
            var chat = _service.GetChatMessage(idSendedBy, idRecievedBy);

            if (!chat.Any())
                return NotFound();

            return Ok(chat);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessageAsync(int id, MessageModel newMessageModel)
        {
            if (id != newMessageModel.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _service.IsThereAnActiveMessageWithTheIdAsync(id))
            {
                return NotFound();
            }

            await CheckForeignKeysAndAddErrorsToModelStateIfNecessaryAsync(newMessageModel);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var messageUpdated = await _service.UpdateMessageAsync(id, newMessageModel);

            if (messageUpdated == null)
            {
                return UnprocessableEntity();
            }

            return Ok(messageUpdated);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessageAsync(MessageModel newMessageModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await CheckForeignKeysAndAddErrorsToModelStateIfNecessaryAsync(newMessageModel);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var messageCreated = await _service.CreateMessageAsync(newMessageModel);

            if (messageCreated != null)
            {
                if (messageCreated.Type == MessageType.SMS)
                {
                    var requestContent = _service.GetStringContent(messageCreated.Text);

                    await _voxboneService.MakeRequestToVoxboneApiAsync(requestContent, newMessageModel.IdCustomerReceiving);
                }

                return CreatedAtAction("CreateMessageAsync", messageCreated);
            }

            return UnprocessableEntity();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessageByIdAsync(int id)
        {
            bool deleted = await _service.DeleteMessageLogicallyByIdAsync(id);

            if (deleted)
            {
                return Ok();
            }

            return NotFound();
        }

        private async Task CheckForeignKeysAndAddErrorsToModelStateIfNecessaryAsync(MessageModel message)
        {
            if (await _customerService.GetCustomerByIdAsync(message.IdCustomerSending) == null)
            {
                ModelState.AddModelError("errors", $"Could not find a customer with id = {message.IdCustomerSending}");
            }

            if (await _customerService.GetCustomerByIdAsync(message.IdCustomerReceiving) == null)
            {
                ModelState.AddModelError("errors", $"Could not find a customer with id = {message.IdCustomerReceiving}");
            }
        }

        private string GetIdCustomer(string phoneNumber)
        {
            var customer = _numberService.GetCustomerByNumber(phoneNumber);

            return customer.First().IdCustomer;
        }
    }
}
