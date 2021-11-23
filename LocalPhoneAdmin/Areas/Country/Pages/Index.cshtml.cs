using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LocalPhoneDomain.Models;
using Microsoft.Extensions.Configuration;

using LocalPhoneDomain;
using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.Country.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICountryService _countryService;
        private readonly IConfiguration Configuration;
        public PaginatedList<CountryModel> CountryPaginatedList { get; set; }

        //public string IsoSort { get; set; }
        public string NameSort { get; set; }
        //public string NicenameSort { get; set; }
        //public string Iso3Sort { get; set; }
        //public string NumcodeSort { get; set; }
        public string PhonecodeSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IndexModel(ICountryService countryService, IConfiguration configuration)
        {
            _countryService = countryService;
            Configuration = configuration;
        }

        public void OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;

            //IsoSort = String.IsNullOrEmpty(sortOrder) ? "iso_desc" : "";
            NameSort = sortOrder == "name" ? "name_desc" : "name";
            //NicenameSort = sortOrder == "nicename" ? "nicename_desc" : "nicename";
            //Iso3Sort = sortOrder == "iso3" ? "iso3_desc" : "iso3";
            //NumcodeSort = sortOrder == "numcode" ? "numcode_desc" : "numcode";
            PhonecodeSort = sortOrder == "phonecode" ? "phonecode_desc" : "phonecode";

            var countries = _countryService.GetAllItems()
                .Select(country => new CountryModel()
                {
                    Id = country.Id,
                    Phonecode = country.Phonecode,
                    Name = country.Name
                });

            if (String.IsNullOrEmpty(searchString))
            {
                CurrentFilter = currentFilter;
            }
            else
            {
                CurrentFilter = searchString;
                countries = countries.Where(country => country.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            countries = sortOrder switch
            {
                //"iso_desc" => countries.OrderByDescending(s => s.Iso),

                "name" => countries.OrderBy(s => s.Name),
                "name_desc" => countries.OrderByDescending(s => s.Name),

                //"nicename" => countries.OrderBy(s => s.Nicename),
                //"nicename_desc" => countries.OrderByDescending(s => s.Nicename),

                //"iso3" => countries.OrderBy(s => s.Iso3),
                //"iso3_desc" => countries.OrderByDescending(s => s.Iso3),

                //"numcode" => countries.OrderBy(s => s.Numcode),
                //"numcode_desc" => countries.OrderByDescending(s => s.Numcode),

                "phonecode" => countries.OrderBy(s => s.Phonecode),
                "phonecode_desc" => countries.OrderByDescending(s => s.Phonecode),

                _ => countries.OrderBy(s => s.Phonecode),
            };

            var pageSize = Configuration.GetValue("PageSize", 4);
            CountryPaginatedList = new PaginatedList<CountryModel>(countries.ToList(), pageIndex ?? 1, pageSize);
        }
    }
}
