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

namespace LocalPhoneAdmin.Areas.City
{
    public class DeleteModel : PageModel
    {
        private readonly ICityService _cityService;

        public DeleteModel(ICityService cityService)
        {
            _cityService = cityService;
        }

        [BindProperty]
        public CityModel CityModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CityModel = await _cityService.GetCityByIdAsync(id.Value, true);

            if (CityModel == null)
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

            CityModel cityFound = await _cityService.GetCityByIdAsync(id.Value, true);

            if (cityFound == null)
            {
                return NotFound();
            }

            cityFound.LastModificationDate = DateTime.Now;
            cityFound.UserThatMadeTheLastModification = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _cityService.UpdateCityAsync(id.Value, cityFound);
            await _cityService.DeleteCityByIdAsync(id.Value);

            return RedirectToPage("./Index");
        }
    }
}
