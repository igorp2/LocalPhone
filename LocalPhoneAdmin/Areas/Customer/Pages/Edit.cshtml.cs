using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocalPhoneDomain.Models;
using System;
using System.Security.Claims;

using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.Customer.Pages
{
    public class EditModel : PageWithSelectList<CountryModel>
    {
        private readonly ICustomerService _customerService;
        private readonly ICountryService _countryService;

        [BindProperty]
        public CustomerModel CustomerModel { get; set; }

        public EditModel(ICustomerService customerService, ICountryService countryService)
        {
            _customerService = customerService;
            _countryService = countryService;
        }

        public async Task<IActionResult> OnGetAsync(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                return NotFound();
            }

            PopulateDropDownList(_countryService.GetAllItems().ToList(), "Id", "Name");

            CustomerModel = await _customerService.GetCustomerByIdAsync(phoneNumber, includeCountries: true);

            if (CustomerModel == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            CustomerModel.LastModificationDate = DateTime.Now;
            CustomerModel.UserThatMadeTheLastModification = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _customerService.UpdateCustomerAsync(CustomerModel.PhoneNumber, CustomerModel);

            return RedirectToPage("./Index");
        }
    }
}
