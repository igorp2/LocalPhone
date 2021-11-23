using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LocalPhoneDomain.Models;
//using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using LocalPhoneDomain;
using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.Customer.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly ICountryService _countryService;
        private readonly IVoxboneService _voxboneService;
        private readonly IGenderService _genderService;

        public SelectList CountrySelectList { get; set; }
        public SelectList GenderSelectList { get; set; }

        [BindProperty]
        public CustomerModel CustomerModel { get; set; }

        public CreateModel(ICustomerService customerService, ICountryService countryService,
            IGenderService genderService, IVoxboneService voxboneService)
        {
            _customerService = customerService;
            _countryService = countryService;
            _voxboneService = voxboneService;
            _genderService = genderService;
        }

        public IActionResult OnGet()
        {
            CountrySelectList = new SelectList(_countryService.GetAllItems().ToList(), "Id", "Name");
            GenderSelectList = new SelectList(_genderService.GetAllItems().ToList(), "Id", "Gender");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var customerCreated = await _customerService.CreateCustomerAsync(CustomerModel);

            if (customerCreated == null)
            {
                ModelState.AddModelError("errors", $"The phone number {CustomerModel.PhoneNumber} already exists in application");
                return Page();
            }
            else
            {
                var stringContent = await _customerService.GetStringContentWithVerificationCodeAsync(CustomerModel);

                await _voxboneService.MakeRequestToVoxboneApiAsync(stringContent, CustomerModel.PhoneNumber);
            }

            return RedirectToPage("./Index");
        }
    }
}
