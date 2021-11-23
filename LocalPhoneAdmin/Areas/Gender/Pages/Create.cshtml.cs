using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LocalPhoneDomain.Models;
using System.Security.Claims;
using LocalPhoneDomain;
using LocalPhoneDomain.Services;
using System.Linq;

namespace LocalPhoneAdmin.Areas.Gender.Pages
{
    public class CreateModel : PageWithSelectList<CountryModel>
    {
        private readonly IGenderService _service;
        private readonly ICountryService _countryService;

        public CreateModel(IGenderService service, ICountryService countryService)
        {
            _service = service;
            _countryService = countryService;
        }

        public IActionResult OnGetAsync()
        {
            PopulateDropDownList(_countryService.GetAllItems().ToList(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public GenderModel GenderModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            GenderModel.CreationDate = DateTime.Now;
            GenderModel.CreatorUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            GenderModel.Status = ModelStatuses.ACTIVE;

            await _service.CreateGenderAsync(GenderModel);

            return RedirectToPage("./Index");
        }
    }
}
