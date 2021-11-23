using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LocalPhoneDomain.Models;
using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.State.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IStateService _service;

        public DetailsModel(IStateService service)
        {
            _service = service;
        }

        public StateModel StateModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
                       
            StateModel = await _service.GetStateByIdAsync(id.Value, true);

            if (StateModel == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
