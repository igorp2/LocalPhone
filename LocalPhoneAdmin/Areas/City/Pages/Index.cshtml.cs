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

namespace LocalPhoneAdmin.Areas.City.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICityService _cityService;
        private readonly IConfiguration Configuration;
        public PaginatedList<CityModel> CityPaginatedList { get; set; }
        public string PhonecodeSort { get; set; }
        public string DescriptionSort { get; set; }
        public string StateDescriptionSort { get; set; }
        public string StateAbbreviationSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IndexModel(IConfiguration configuration, ICityService cityService)
        {
            Configuration = configuration;
            _cityService = cityService;
        }

        public void OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;

            PhonecodeSort = String.IsNullOrEmpty(sortOrder) ? "phonecode_desc" : "";
            DescriptionSort = sortOrder == "description" ? "description_desc" : "description";
            StateDescriptionSort = sortOrder == "state" ? "state_desc" : "state";
            StateAbbreviationSort = sortOrder == "abbreviation" ? "abbreviation_desc" : "abbreviation";

            IEnumerable<CityModel> cities = _cityService.GetAllItems(includeStates: true).Select(city => new CityModel()
                {
                    Id = city.Id,
                    IdState = city.IdState,
                    Phonecode = city.Phonecode,
                    Description = city.Description,
                    State = city.State
                });

            if (String.IsNullOrEmpty(searchString))
            {
                CurrentFilter = currentFilter;
            }
            else
            {
                CurrentFilter = searchString;
                cities = cities.Where(city => city.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            cities = sortOrder switch
            {
                "phonecode_desc" => cities.OrderByDescending(s => s.Phonecode),
                "description" => cities.OrderBy(s => s.Description),
                "description_desc" => cities.OrderByDescending(s => s.Description),
                "state" => cities.OrderBy(s => s.State.Name),
                "state_desc" => cities.OrderByDescending(s => s.State.Name),
                "abbreviation" => cities.OrderBy(s => s.State.Abbreviation),
                "abbreviation_desc" => cities.OrderByDescending(s => s.State.Abbreviation),
                _ => cities.OrderBy(s => s.Phonecode),
            };

            var pageSize = Configuration.GetValue("PageSize", 4);

            CityPaginatedList = new PaginatedList<CityModel>(cities.ToList(), pageIndex ?? 1, pageSize);

            //TODO: Importante nao apagar
            //CityModel = await _repository.City
            //    .Include(c => c.Country)
            //    .AsNoTracking()
            //    .ToListAsync();
        }
    }
}
