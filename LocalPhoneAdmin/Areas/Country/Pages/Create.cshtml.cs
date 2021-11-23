using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LocalPhoneDomain.Models;
using System.Security.Claims;

using LocalPhoneDomain;
using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.Country.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ICountryService _countryService;

        public CreateModel(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CountryModel CountryModel { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            if (CountryModel.Iso == null)
                CountryModel.Iso = "";

            if (CountryModel.Name == null)
                CountryModel.Name = "";

            if (CountryModel.Iso3 == null)
                CountryModel.Iso3 = "";

            if (CountryModel.Nicename == null)
                CountryModel.Nicename = "";

            if (CountryModel.Numcode == null)
                CountryModel.Numcode = 0;

            CountryModel.CreationDate = DateTime.Now;
            CountryModel.CreatorUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CountryModel.Status = ModelStatuses.ACTIVE;

            await _countryService.CreateCountryAsync(CountryModel);

            return RedirectToPage("./Index");
        }
    }
}
