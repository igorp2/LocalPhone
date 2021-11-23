using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LocalPhoneAdmin.Data;
using LocalPhoneDomain.Areas.Identity.Data;

namespace LocalPhoneAdmin.Areas.Users.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly LocalPhoneAdminContext _context;

        public DeleteModel(LocalPhoneAdminContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserModel UserModel { get; set; }

        public async Task<IActionResult> OnGetAsync(string email)
        {
            if (email == null)
            {
                return NotFound();
            }

            UserModel = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Email == email);

            if (UserModel == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string email)
        {

            UserModel = await _context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Email == email);

            _context.Users.Remove(UserModel);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");

        }
    }
}
