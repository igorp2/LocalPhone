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

namespace LocalPhoneAdmin.Areas.City
{
    public class CreateModel : PageWithSelectList<StateModel>
    {
        private readonly ICityService _cityService;
        private readonly IStateService _stateService;

        [BindProperty]
        public CityModel CityModel { get; set; }


        public CreateModel(ICityService cityService, IStateService stateService)
        {
            _cityService = cityService;
            _stateService = stateService;
        }

        public IActionResult OnGet()
        {
            PopulateDropDownList(_stateService.GetAllItems().ToList(), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (CityModel.Description == null)
                CityModel.Description = "";

            CityModel.CreationDate = DateTime.Now;
            CityModel.CreatorUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CityModel.Status = ModelStatuses.INACTIVE;

            await _cityService.CreateCityAsync(CityModel);

            return RedirectToPage("./Index");
        }
    }
}
