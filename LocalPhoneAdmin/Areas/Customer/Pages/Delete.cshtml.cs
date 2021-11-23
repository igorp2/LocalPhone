using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using LocalPhoneDomain.Models;
using System;
using System.Security.Claims;
using System.Linq;
using LocalPhoneDomain;
using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.Customer.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public DeleteModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [BindProperty]
        public RegistrationInformationModel RegistrationInformationModel { get; set; }

        public CustomerModel CustomerModel { get; set; }

        public async Task<IActionResult> OnGetAsync(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                return NotFound();
            }

            CustomerModel = await _customerService.GetCustomerByIdAsync(phoneNumber, true);

            if (CustomerModel == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                return NotFound();
            }

            CustomerModel customerFound = await _customerService.GetCustomerByIdAsync(phoneNumber, true);

            if (customerFound == null)
            {
                return NotFound();
            }

            customerFound.LastModificationDate = DateTime.Now;
            customerFound.UserThatMadeTheLastModification = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _customerService.UpdateCustomerAsync(phoneNumber, customerFound);
            await _customerService.DeleteCustomerLogicallyByIdAsync(phoneNumber);

            return RedirectToPage("./Index");
        }
    }
}
