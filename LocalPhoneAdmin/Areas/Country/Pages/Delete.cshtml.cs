using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LocalPhoneDomain.Models;

using System;
using System.Security.Claims;
using LocalPhoneDomain;
using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.Country.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;

        public DeleteModel(ICountryService countryService, ICityService cityService)
        {
            _countryService = countryService;
            _cityService = cityService;
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CountryModel = await _countryService.GetCountryByIdAsync(id.Value);

            if (CountryModel != null)
            {
                //var cities = await _cityRepository.GetAllItemsThatFitTheFilterAsync(item => item.IdState == id);

                //if (cities != null && cities.Any())
                //{
                //    foreach (var item in cities)
                //    {
                //        item.Status = 0;
                //        item.LastModificationDate = DateTime.Now;
                //        item.UserThatMadeTheLastModification = User.FindFirstValue(ClaimTypes.NameIdentifier);

                //        await _cityRepository.UpdateTheItemWithTheIdAsync(item.Id, item);
                //    }
                //}

                CountryModel.LastModificationDate = DateTime.Now;
                CountryModel.UserThatMadeTheLastModification = User.FindFirstValue(ClaimTypes.NameIdentifier);

                await _countryService.UpdateCountryAsync(CountryModel.Id, CountryModel);
                await _countryService.DeleteCountryByIdAsync(CountryModel.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
