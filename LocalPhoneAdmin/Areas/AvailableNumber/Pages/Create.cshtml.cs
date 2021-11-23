using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LocalPhoneDomain.Models;
using System.Security.Claims;
using LocalPhoneDomain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LocalPhoneDomain.Services;
using System.Linq;

namespace LocalPhoneAdmin.Areas.AvailableNumber.Pages
{
    public class CreateModel : PageWithSelectList<CountryModel>
    {
        private readonly IAvailableNumberService _service;
        private readonly ICountryService _countryService;

        public CreateModel(IAvailableNumberService service,
            ICountryService countryService)
        {
            _service = service;
            _countryService = countryService;
        }

        public IActionResult OnGet()
        {
            PopulateDropDownList(_countryService.GetAllItems().ToList(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public AvailableNumberModel AvailableNumberModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            AvailableNumberModel.CreationDate = DateTime.Now;
            AvailableNumberModel.CreatorUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AvailableNumberModel.Status = ModelStatuses.ACTIVE;

            await _service.CreateAvailableNumberAsync(AvailableNumberModel);

            return RedirectToPage("./Index");
        }
    }
}
