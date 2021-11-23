using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LocalPhoneDomain.Models;
using System.Security.Claims;
using LocalPhoneDomain;
using LocalPhoneDomain.Services;
using System.Linq;

namespace LocalPhoneAdmin.Areas.State
{
    public class CreateModel : PageWithSelectList<CountryModel>
    {
        private readonly IStateService _service;
        private readonly ICountryService _countryService;

        public CreateModel(IStateService service, ICountryService countryService)
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
        public StateModel StateModel { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            StateModel.CreationDate = DateTime.Now;
            StateModel.CreatorUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            StateModel.Status = ModelStatuses.ACTIVE;

            await _service.CreateStateAsync(StateModel);

            return RedirectToPage("./Index");
        }
    }
}
