using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LocalPhoneDomain.Models;
using System.Security.Claims;
using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.Country.Pages
{
    public class EditModel : PageModel
    {
        private readonly ICountryService _countryService;

        public EditModel(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

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

            CountryModel.LastModificationDate = DateTime.Now;
            CountryModel.UserThatMadeTheLastModification = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _countryService.UpdateCountryAsync(CountryModel.Id, CountryModel);

            return RedirectToPage("./Index");
        }
    }
}
