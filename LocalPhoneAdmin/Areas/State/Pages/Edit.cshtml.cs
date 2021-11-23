using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LocalPhoneDomain.Models;
using System.Security.Claims;
using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.State.Pages
{
    public class EditModel : PageWithSelectList<CountryModel>
    {
        private readonly IStateService _service;
        private readonly ICountryService _countryService;

        public EditModel(IStateService service, ICountryService countryService)
        {
            _service = service;
            _countryService = countryService;
        }

        [BindProperty]
        public StateModel StateModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PopulateDropDownList(_countryService.GetAllItems().ToList(), "Id", "Name");

            StateModel = await _service.GetStateByIdAsync(id.Value);

            if (StateModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            StateModel.LastModificationDate = DateTime.Now;
            StateModel.UserThatMadeTheLastModification = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _service.UpdateStateAsync(StateModel.Id, StateModel);

            return RedirectToPage("./Index");
        }
    }
}
