using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LocalPhoneDomain.Models;
using Microsoft.Extensions.Configuration;
using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.AvailableNumber.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IAvailableNumberService _service;
        private readonly IConfiguration Configuration;
        public PaginatedList<AvailableNumberModel> AvailableNumberModel { get; set; }

        public string CountrySort { get; set; }
        public string CitySort { get; set; }
        public string PhoneNumberSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IndexModel(IAvailableNumberService service, IConfiguration configuration)
        {
            _service = service;
            Configuration = configuration;
        }
        public void OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;

            CountrySort = sortOrder == "country" ? "country_desc" : "country";
            CitySort = sortOrder == "city" ? "city_desc" : "city";
            PhoneNumberSort = sortOrder == "phoneNumber" ? "phoneNumber_desc" : "phoneNumber";

            var numbers = _service.GetAllItems(null, false, 1, 0, true).Select(number => new AvailableNumberModel()
                {
                    Id = number.Id,
                    idCountry = number.idCountry,
                    Country = number.Country,
                    idCity = number.idCity,
                    City = number.City,
                    phoneNumber = number.phoneNumber
                });

            if (String.IsNullOrEmpty(searchString))
            {
                CurrentFilter = currentFilter;
            }
            else
            {
                CurrentFilter = searchString;
                numbers = numbers.Where(number => number.Country.Nicename.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            numbers = sortOrder switch
            {
                "phoneNumber" => numbers.OrderBy(s => s.phoneNumber),
                "phoneNumber_desc" => numbers.OrderByDescending(s => s.phoneNumber),

                "country" => numbers.OrderBy(s => s.idCountry),
                "country_desc" => numbers.OrderByDescending(s => s.idCountry),

                "city" => numbers.OrderBy(s => s.idCity),
                "city_desc" => numbers.OrderByDescending(s => s.idCity),

                _ => numbers.OrderBy(s => s.idCountry),
            };

            var pageSize = Configuration.GetValue("PageSize", 4);

            AvailableNumberModel = new PaginatedList<AvailableNumberModel>(numbers.ToList(), pageIndex ?? 1, pageSize);
        }
    }
}
