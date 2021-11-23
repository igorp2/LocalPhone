using LocalPhoneDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalPhoneDomain.Services
{
    public interface IGenderService : IPaginatedService
    {
        IEnumerable<GenderModel> GetAllItems(bool includeCountries = false,
            string orderByPropertyName = null, bool isOrderByInDescendingOrder = false,
            int pageIndex = 1, int pageSize = 0);
        
        Task<GenderModel> GetGenderByIdAsync(int id, bool includeCountries = false);

        IEnumerable<GenderModel> GetGendersByCountry(int idCountry);
        
        Task<GenderModel> UpdateGenderAsync(int id, GenderModel genderModel);

        Task<GenderModel> CreateGenderAsync(GenderModel genderModel);

        Task<bool> DeleteGenderByIdAsync(int id);

        Task<bool> IsThereAnActiveGenderWithTheIdAsync(int id);
    }
}
