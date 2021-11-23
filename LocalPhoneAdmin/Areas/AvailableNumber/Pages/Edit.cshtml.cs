using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LocalPhoneDomain.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LocalPhoneDomain.Services;
using System.Linq;

namespace LocalPhoneAdmin.Areas.AvailableNumber.Pages
{
    public class EditModel : PageWithSelectList<CountryModel>
    {
        private readonly IAvailableNumberService _service;
        private readonly ICountryService _countryService;

        public EditModel(IAvailableNumberService service,
            ICountryService countryService)
        {
            _service = service;
            _countryService = countryService;
        }

        [BindProperty]
        public AvailableNumberModel AvailableNumberModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PopulateDropDownList(_countryService.GetAllItems().ToList(), "Id", "Name");

            AvailableNumberModel = await _service.GetAvailableNumberByIdAsync(id.Value);

            if (AvailableNumberModel == null)
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

            AvailableNumberModel.LastModificationDate = DateTime.Now;
            AvailableNumberModel.UserThatMadeTheLastModification = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _service.UpdateAvailableNumberAsync(AvailableNumberModel.Id, AvailableNumberModel);

            return RedirectToPage("./Index");
        }
    }
}
