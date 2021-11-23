using System.ComponentModel.DataAnnotations;

namespace LocalPhoneDomain.Models
{
    public class AvailableNumberModel : BaseModel
    {
        [Display(Name = "Country")]
        public int idCountry { get; set; }

        [Display(Name = "City")]
        public int idCity { get; set; }

        [Display(Name = "Phone Number")]
        public string phoneNumber { get; set; }

        public virtual CountryModel Country { get; set; }

        public virtual CityModel City { get; set; }

        public override void CloneFromModel(BaseModel modelToClone)
        {
            AvailableNumberModel newAvailableNumber = modelToClone as AvailableNumberModel;

            phoneNumber = newAvailableNumber.phoneNumber;
            idCity = newAvailableNumber.idCity;
            idCountry = newAvailableNumber.idCountry;
            Status = newAvailableNumber.Status;

            base.CloneBaseModelAttributesExcludingStatusFromModel(modelToClone);
        }
    }
}
