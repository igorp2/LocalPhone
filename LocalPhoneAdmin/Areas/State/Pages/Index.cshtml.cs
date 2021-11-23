using Microsoft.AspNetCore.Mvc.RazorPages;
using LocalPhoneDomain.Models;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System;
using LocalPhoneDomain.Services;

namespace LocalPhoneAdmin.Areas.State.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IStateService _service;
        
        public PaginatedList<StateModel> StateModel { get; set; }

        private readonly IConfiguration Configuration;

        public string NameSort { get; set; }
        public string AbbreviationSort { get; set; }
        public string CountrySort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IndexModel(IStateService service, IConfiguration configuration)
        {
            _service = service;
            Configuration = configuration;
        }

        public void OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;

            NameSort = String.IsNullOrEmpty(sortOrder) ? "state_desc" : "";
            AbbreviationSort = sortOrder == "abbreviation" ? "abbreviation_desc" : "abbreviation";
            CountrySort = sortOrder == "country" ? "country_desc" : "country";

            var states = _service.GetAllItems(includeCountries: true).Select(state => new StateModel()
                {
                    Id = state.Id,
                    Abbreviation = state.Abbreviation,
                    Name = state.Name,
                    IdCountry = state.IdCountry,
                    Country = state.Country
                });

            if (String.IsNullOrEmpty(searchString))
            {
                CurrentFilter = currentFilter;
            }
            else
            {
                CurrentFilter = searchString;
                states = states.Where(state => state.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            states = sortOrder switch
            {
                "state_desc" => states.OrderByDescending(s => s.Name),

                "abbreviation" => states.OrderBy(s => s.Abbreviation),
                "abbreviation_desc" => states.OrderByDescending(s => s.Abbreviation),

                "country" => states.OrderBy(s => s.Country.Name),
                "country_desc" => states.OrderByDescending(s => s.Country.Name),

                _=> states.OrderBy(s => s.Name),
            };

            var pageSize = Configuration.GetValue("PageSize", 4);
            StateModel = new PaginatedList<StateModel>(states.ToList(), pageIndex ?? 1, pageSize);
        }
    }
}
