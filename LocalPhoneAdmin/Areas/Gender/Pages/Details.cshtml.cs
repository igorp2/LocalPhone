using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LocalPhoneDomain.Models;
using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.Gender.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IGenderService _service;

        public DetailsModel(IGenderService service)
        {
            _service = service;
        }

        public GenderModel GenderModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //_repository.AddFilterWhenToRetrievingItemsFromDatabase(gender => gender.Status == ModelStatuses.ACTIVE);
            //_repository.AddIncludeWhenRetrievingItemsFromDatabase(gender => gender.Country);

            GenderModel = await _service.GetGenderByIdAsync(id.Value, true);

            if (GenderModel == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
