using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LocalPhoneDomain.Models;
using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.AvailableNumber.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IAvailableNumberService _service;

        public DetailsModel(IAvailableNumberService service)
        {
            _service = service;
        }

        public AvailableNumberModel AvailableNumberModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AvailableNumberModel = await _service.GetAvailableNumberByIdAsync(id.Value);

            if (AvailableNumberModel == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
