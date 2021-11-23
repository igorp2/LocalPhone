using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalPhoneDomain.Models
{
    public class CountryModel : BaseModel
    {
        [MaxLength(2, ErrorMessage = "Iso length can't be more than 2.")]
        [Column(TypeName = "char(2)")]
        public string Iso { get; set; }

        [Required]
        [MaxLength(80, ErrorMessage = "Name length can't be more than 80.")]
        [Display(Name = "Name")]
        [Column(TypeName = "varchar(80)")]
        public string Name { get; set; }

        [Required]
        [MaxLength(80, ErrorMessage = "Nice name length can't be more than 80.")]
        [Display(Name = "Nice Name")]
        [Column(TypeName = "varchar(80)")]
        public string Nicename { get; set; }

        [MaxLength(3, ErrorMessage = "Iso 3 length can't be more than 3.")]
        [Display(Name = "Iso 3")]
        [Column(TypeName = "char(3)")]
        public string Iso3 { get; set; }

        [Display(Name = "Num Code")]
        [Column(TypeName = "smallint")]
        public int? Numcode { get; set; }

        [Display(Name = "Phone Code")]
        [Column(TypeName = "int")]
        public int Phonecode { get; set; }

        public virtual IEnumerable<CustomerModel> Customers { get; set; }
        public virtual IEnumerable<GenderModel> Genders { get; set; }
        public virtual IEnumerable<StateModel> States { get; set; }
        public virtual IEnumerable<AvailableNumberModel> AvailableNumbers { get; set; }


        public override void CloneFromModel(BaseModel modelToClone)
        {
            CountryModel newCountry = modelToClone as CountryModel;

            Name = newCountry.Name;
            Nicename = newCountry.Nicename;
            Iso = newCountry.Iso;
            Iso3 = newCountry.Iso3;
            Numcode = newCountry.Numcode;
            Phonecode = newCountry.Phonecode;

            base.CloneBaseModelAttributesExcludingStatusFromModel(modelToClone);
        }
    }
}