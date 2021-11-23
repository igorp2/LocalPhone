using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using LocalPhoneDomain.Models;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System;
using LocalPhoneDomain;
using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.Customer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IConfiguration Configuration;
        public PaginatedList<CustomerModel> CustomerPaginatedList { get; set; }
        public string PhoneNumberSort { get; set; }
        public string CountryNameSort { get; set; }
        public string GenderSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IndexModel(IConfiguration configuration, ICustomerService customerService)
        {
            Configuration = configuration;
            _customerService = customerService;
        }

        public void OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;

            PhoneNumberSort = String.IsNullOrEmpty(sortOrder) ? "phonenumber_desc" : "";
            CountryNameSort = sortOrder == "country" ? "country_desc" : "country";
            GenderSort = sortOrder == "gender" ? "gender_desc" : "gender";

            IEnumerable<CustomerModel> customers = _customerService.GetAllItems(includeCountries: true);
                //.Select(customer => new CustomerModel()
                //{
                //    PhoneNumber = customer.PhoneNumber,
                //    IdCountry = customer.IdCountry,
                //    OperationalSystem = customer.OperationalSystem,
                //    PublishedAppVersion = customer.PublishedAppVersion,
                //    Country = customer.Country
                //});

            if (String.IsNullOrEmpty(searchString))
            {
                CurrentFilter = currentFilter;
            }
            else
            {
                CurrentFilter = searchString;
                customers = customers.Where(customer => customer.PhoneNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            customers = sortOrder switch
            {
                "phonenumber" => customers.OrderBy(s => s.PhoneNumber),
                "phonenumber_desc" => customers.OrderByDescending(s => s.PhoneNumber),
                "country" => customers.OrderBy(s => s.Country.Name),
                "country_desc" => customers.OrderByDescending(s => s.Country.Name),
                "gender" => customers.OrderBy(s => s.Gender.Gender),
                "gender_desc" => customers.OrderByDescending(s => s.Gender.Gender),
                _ => customers.OrderBy(s => s.PhoneNumber),
            };

            var pageSize = Configuration.GetValue("PageSize", 4);

            CustomerPaginatedList = new PaginatedList<CustomerModel>(customers.ToList(), pageIndex ?? 1, pageSize);

            //TODO: Importante nao apagar
            //CustomerModel = await _repository.Customer
            //    .Include(c => c.Country)
            //    .AsNoTracking()
            //    .ToListAsync();
        }
    }
}
