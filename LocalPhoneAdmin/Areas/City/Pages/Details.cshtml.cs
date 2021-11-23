using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LocalPhoneDomain.Models;
using LocalPhoneDomain;
using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.City
{
    public class DetailsModel : PageModel
    {
        private readonly ICityService _cityService;

        public CityModel CityModel { get; set; }

        public DetailsModel(ICityService cityService)
        {
            _cityService = cityService;
        }

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
    }
}
