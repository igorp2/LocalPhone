using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System;
using LocalPhoneDomain.Areas.Identity.Data;
using LocalPhoneAdmin.Data;

namespace LocalPhoneAdmin.Areas.Users.Pages
{
    public class IndexModel : PageModel
    {
        private readonly LocalPhoneAdminContext _context;                
        private readonly IConfiguration Configuration;
        public PaginatedList<UserModel> UserModelList { get; set; }
        public string FirstnameSort { get; set; }
        public string LastNameSort { get; set; }
        public string EmailSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IndexModel(LocalPhoneAdminContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public void OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;

            FirstnameSort = String.IsNullOrEmpty(sortOrder) ? "Firstname_desc" : "";
            LastNameSort = sortOrder == "LastName" ? "LastName_desc" : "LastName";
            EmailSort = sortOrder == "Email" ? "Email_desc" : "Email";

            var usersWithRoles = (from user in _context.Users
                                  select new
                                  {
                                      FirstName = user.Firstname,
                                      LastName = user.LastName,
                                      Email = user.Email,
                                     
                                  }).ToList().Select(p => new UserModel()

                                  {
                                      Firstname = p.FirstName,
                                      LastName = p.LastName,
                                      Email = p.Email,
                                     
                                  });

            if (String.IsNullOrEmpty(searchString))
            {
                CurrentFilter = currentFilter;
            }
            else
            {
                CurrentFilter = searchString;
                usersWithRoles = usersWithRoles.Where(UserModel => UserModel.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase));  
            }

            usersWithRoles = sortOrder switch
            {
 
                "Firstname_desc" => usersWithRoles.OrderByDescending(s => s.Firstname),

                "LastName" => usersWithRoles.OrderBy(s => s.LastName),
                "LastName_desc" => usersWithRoles.OrderByDescending(s => s.LastName),

                "Email" => usersWithRoles.OrderBy(s => s.Email),
                "Email_desc" => usersWithRoles.OrderByDescending(s => s.Email), 

                _ => usersWithRoles.OrderBy(s => s.Firstname),
            };

            var pageSize = Configuration.GetValue("PageSize", 4);
            UserModelList = new PaginatedList<UserModel>(usersWithRoles.ToList(), pageIndex ?? 1, pageSize);
        }
    }
}