using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LocalPhoneDomain.Models;
using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.AvailableNumber.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IAvailableNumberService _service;

        public DeleteModel(IAvailableNumberService service)
        {
            _service = service;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _service.DeleteAvailableNumberByIdAsync(id.Value);
            
            return RedirectToPage("./Index");
        }
    }
}
