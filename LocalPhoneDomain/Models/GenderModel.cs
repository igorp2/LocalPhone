using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalPhoneDomain.Models
{
    public class GenderModel : BaseModel
    {
        [Required]
        [StringLength(250, ErrorMessage = "Name length can't be more than 250.")]
        [Display(Name = "Gender")]
        [Column(TypeName = "varchar(250)")]
        public string Gender { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
        [Display(Name = "Abbreviation")]
        [Column(TypeName = "varchar(50)")]
        public string Abbreviation { get; set; }

        [StringLength(500, ErrorMessage = "Name length can't be more than 500.")]
        [Display(Name = "Description")]
        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }

        [Display(Name = "Country")]
        [Column(TypeName = "int")]
        [ForeignKey("FK_GenderCountry")]
        public int? IdCountry { get; set; }

        public virtual CountryModel Country { get; set; }

        public override void CloneFromModel(BaseModel modelToClone)
        {
            GenderModel newModel = modelToClone as GenderModel;

            Gender = newModel.Gender;
            Abbreviation = newModel.Abbreviation;
            Description = newModel.Description;
            IdCountry = newModel.IdCountry;

            base.CloneBaseModelAttributesExcludingStatusFromModel(modelToClone);
        }
    }
}
