using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LocalPhoneDomain.Models;
using Microsoft.Extensions.Configuration;
using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.Gender.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IGenderService _service;
        private readonly IConfiguration Configuration;
        public PaginatedList<GenderModel> GenderModel { get; set; }

        public string GenderSort { get; set; }
        public string AbbreviationSort { get; set; }
        public string DescriptionSort { get; set; }
        public string CountrySort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IndexModel(IGenderService service, IConfiguration configuration)
        {
            _service = service;
            Configuration = configuration;
        }
        public void OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;

            GenderSort = String.IsNullOrEmpty(sortOrder) ? "gender_desc" : "";
            AbbreviationSort = sortOrder == "abbreviation" ? "abbreviation_desc" : "abbreviation";
            DescriptionSort = sortOrder == "description" ? "description_desc" : "description";
            CountrySort = sortOrder == "country" ? "country_desc" : "country";        

            var genders = _service.GetAllItems(true).Select(gender => new GenderModel()
                {
                    Id = gender.Id,
                    Gender = gender.Gender,
                    Abbreviation = gender.Abbreviation,
                    Description = gender.Description,
                    Country = gender.Country,
                    IdCountry = gender.IdCountry
                });

            if (String.IsNullOrEmpty(searchString))
            {
                CurrentFilter = currentFilter;
            }
            else
            {
                CurrentFilter = searchString;
                genders = genders.Where(gender => gender.Gender.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            genders = sortOrder switch
            {
                "gender_desc" => genders.OrderByDescending(s => s.Gender),

                "abbreviation" => genders.OrderBy(s => s.Abbreviation),
                "abbreviation_desc" => genders.OrderByDescending(s => s.Abbreviation),

                "description" => genders.OrderBy(s => s.Description),
                "description_desc" => genders.OrderByDescending(s => s.Description),

                "country" => genders.OrderBy(s => s.Country.Name),
                "country_desc" => genders.OrderByDescending(s => s.Country.Name),

                _ => genders.OrderBy(s => s.Gender),
            };

            var pageSize = Configuration.GetValue("PageSize", 4);

            GenderModel = new PaginatedList<GenderModel>(genders.ToList(), pageIndex ?? 1, pageSize);
        }
    }
}
