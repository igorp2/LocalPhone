using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocalPhoneDomain.Models;
using System;
using System.Security.Claims;

using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.City
{
    public class EditModel : PageWithSelectList<StateModel>
    {
        private readonly ICityService _cityService;
        private readonly IStateService _stateService;

        [BindProperty]
        public CityModel CityModel { get; set; }

        public EditModel(ICityService cityService, IStateService stateService)
        {
            _cityService = cityService;
            _stateService = stateService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PopulateDropDownList(_stateService.GetAllItems().ToList(), "Id", "Name");

            CityModel = await _cityService.GetCityByIdAsync(id.Value);

            if (CityModel == null)
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

            CityModel.LastModificationDate = DateTime.Now;
            CityModel.UserThatMadeTheLastModification = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _cityService.UpdateCityAsync(CityModel.Id, CityModel);

            return RedirectToPage("./Index");
        }
    }
}
