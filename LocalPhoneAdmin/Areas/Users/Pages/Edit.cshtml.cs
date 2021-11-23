using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LocalPhoneDomain.Areas.Identity.Data;
using LocalPhoneAdmin.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace LocalPhoneAdmin.Areas.Users.Pages
{
    public class EditModel : PageModel
    {
        private readonly LocalPhoneAdminContext _context;

        public EditModel(LocalPhoneAdminContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserModel UserModel { get; set; }       
        
        public IActionResult OnGetAsync(string email)
        {
            if (email == null)
            {
                return NotFound();
            }

            UserModel = _context.Users
                .Where(m => m.Email == email)
                .SingleOrDefault();
            
            if (UserModel == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string email)
        {
            UserModel newUserModel = _context.Users
                .Where(m => m.Email == email)
                .SingleOrDefault();

            if (newUserModel == null)
            {
                return NotFound();
            }

            newUserModel.Firstname = UserModel.Firstname;
            newUserModel.LastName = UserModel.LastName;
                
            _context.Users.Update(newUserModel);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
            
        }
    }
}
