using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LocalPhoneAdmin.Data;
using LocalPhoneDomain.Models;
using System.Security.Claims;
using LocalPhoneDomain;
using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.Gender.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IGenderService _service;

        public DeleteModel(IGenderService service)
        {
            _service = service;
        }

        [BindProperty]
        public GenderModel GenderModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GenderModel = await _service.GetGenderByIdAsync(id.Value, true);

            if (GenderModel == null)
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
            
            await _service.DeleteGenderByIdAsync(id.Value);
            
            return RedirectToPage("./Index");
        }
    }
}
