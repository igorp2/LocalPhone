using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LocalPhoneDomain.Models;

using LocalPhoneDomain;
using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.Customer.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomerModel CustomerModel { get; set; }

        public DetailsModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> OnGetAsync(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                return NotFound();
            }

            CustomerModel = await _customerService.GetCustomerByIdAsync(phoneNumber, 
                includeCountries: true, includeAddresses: true, includeNumbers: true);

            if (CustomerModel == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
