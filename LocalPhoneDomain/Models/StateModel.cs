using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalPhoneDomain.Models
{
    public class StateModel : BaseModel
    {
        [Required]
        [StringLength(250, ErrorMessage = "Name length can't be more than 250.")]
        [Column(TypeName = "varchar(250)")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
        [Display(Name = "Abbreviation")]
        [Column(TypeName = "varchar(50)")]
        public string Abbreviation { get; set; }

        [Required]
        [Display(Name = "Country")]
        [Column(TypeName = "int")]
        [ForeignKey("FK_StateCountry")]
        public int? IdCountry { get; set; }

        public virtual CountryModel Country { get; set; }

        public virtual IEnumerable<CityModel> Cities { get; set; }

        public override void CloneFromModel(BaseModel modelToClone)
        {
            StateModel newState = modelToClone as StateModel;

            Name = newState.Name;
            Abbreviation = newState.Abbreviation;
            IdCountry = newState.IdCountry;

            base.CloneBaseModelAttributesExcludingStatusFromModel(modelToClone);
        }
    }
}

