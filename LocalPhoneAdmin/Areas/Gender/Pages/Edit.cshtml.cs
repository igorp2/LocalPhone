using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocalPhoneAdmin.Data;
using LocalPhoneDomain.Models;
using System.Security.Claims;
using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.Gender.Pages
{
    public class EditModel : PageWithSelectList<CountryModel>
    {
        private readonly IGenderService _service;
        private readonly ICountryService _countryService;

        public EditModel(IGenderService service, ICountryService countryService)
        {
            _service = service;
            _countryService = countryService;
        }

        [BindProperty]
        public GenderModel GenderModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PopulateDropDownList(_countryService.GetAllItems().ToList(), "Id", "Name");

            GenderModel = await _service.GetGenderByIdAsync(id.Value);

            if (GenderModel == null)
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

            GenderModel.LastModificationDate = DateTime.Now;
            GenderModel.UserThatMadeTheLastModification = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _service.UpdateGenderAsync(GenderModel.Id, GenderModel);

            return RedirectToPage("./Index");
        }
    }
}
