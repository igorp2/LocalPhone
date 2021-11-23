using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LocalPhoneAdmin.Data;
using LocalPhoneDomain.Models;
using System.Security.Claims;
using LocalPhoneDomain;
using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.State.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IStateService _service;

        public DeleteModel(IStateService service)
        {
            _service = service;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StateModel = await _service.GetStateByIdAsync(id.Value);

            if (StateModel != null)
            {
                StateModel.LastModificationDate = DateTime.Now;
                StateModel.UserThatMadeTheLastModification = User.FindFirstValue(ClaimTypes.NameIdentifier);

                await _service.DeleteStateByIdAsync(id.Value);
            }

            return RedirectToPage("./Index");
        }
    }
}
