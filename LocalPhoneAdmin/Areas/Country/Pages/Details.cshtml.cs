using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LocalPhoneDomain.Models;
using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.Country.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ICountryService _countryService;

        public DetailsModel(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public CountryModel CountryModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CountryModel = await _countryService.GetCountryByIdAsync(id.Value);

            if (CountryModel == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
