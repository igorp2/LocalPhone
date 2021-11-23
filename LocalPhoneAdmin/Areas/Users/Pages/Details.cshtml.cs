using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LocalPhoneAdmin.Data;
using LocalPhoneDomain.Areas.Identity.Data;

namespace LocalPhoneAdmin.Areas.Users.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly LocalPhoneAdminContext _context;

        public UserModel UserModel { get; set; }

        public DetailsModel(LocalPhoneAdminContext context)
        {
            _context = context;
        }

       public async Task<IActionResult> OnGetAsync(string email)
        {
            UserModel = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Email == email);

            if (UserModel == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
