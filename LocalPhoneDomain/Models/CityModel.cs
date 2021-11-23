using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalPhoneDomain.Models
{
    public class CityModel : BaseModel
    {
        [Required]
        [Display(Name = "Phone Code")]
        [Column(TypeName = "int")]
        public int? Phonecode { get; set; }

        [Required]
        [StringLength(800, ErrorMessage = "Name length can't be more than 800.")]
        [Display(Name = "Description")]
        [Column(TypeName = "varchar(800)")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "State")]
        [Column(TypeName = "int")]
        [ForeignKey("FK_CityState")]
        public int? IdState { get; set; }
        public StateModel State { get; set; }

        public virtual IEnumerable<AddressModel> Addresses { get; set; }

        public virtual IEnumerable<AvailableNumberModel> AvailableNumbers { get; set; }


        public override void CloneFromModel(BaseModel modelToClone)
        {
            CityModel newCity = modelToClone as CityModel;

            IdState = newCity.IdState;
            Phonecode = newCity.Phonecode;
            Description = newCity.Description;

            base.CloneBaseModelAttributesExcludingStatusFromModel(newCity);
        }
    }
}

//    public class City
//    {
//        [Column(TypeName = "int")]
//        public int Id { get; set; }

//        [Display(Name = "Phone Code")]
//        [Column(TypeName = "int")]
//        public int Phonecode { get; set; }

//        [Required]
//        [StringLength(800, ErrorMessage = "Name length can't be more than 800.")]
//        [Display(Name = "Description")]
//        [Column(TypeName = "varchar(800)")]
//        public string Description { get; set; }

//        [Display(Name = "Country")]
//        [Column(TypeName = "int")]
//        [ForeignKey("FK_CityCountry")]
//        public int IdCountry { get; set; }

//    }
//}

//    using System.ComponentModel.DataAnnotations;
//    using System.ComponentModel.DataAnnotations.Schema;
//namespace LocalPhoneDomain.Models
//{
//    public class CityModel : BaseModel
//    {
//        [Required]
//        [Display(Name = "Phone Code")]
//        [Column(TypeName = "int")]
//        public int Phonecode { get; set; }

//        [Required]
//        [MaxLength(800, ErrorMessage = "Description length can't be more than 800.")]
//        [Display(Name = "Description")]
//        [Column(TypeName = "varchar(800)")]
//        public string Description { get; set; }

//        //[Required]
//        //[Display(Name = "Country")]
//        //[Column(TypeName = "int")]
//        //[ForeignKey("FK_CityCountry")]
//        //public int IdCountry { get; set; }

//        //public virtual CountryModel Country { get; set; }

//        [Required]
//        [Display(Name = "State Id")]
//        [Column(TypeName = "int")]
//        [ForeignKey("FK_CityState")]
//        public int IdState { get; set; }

//        public virtual StateModel State { get; set; }


//        public override void CloneFromModel(BaseModel modelToClone)
//        {
//            CityModel newCity = modelToClone as CityModel;

//            Phonecode = newCity.Phonecode;
//            Description = newCity.Description;

//            base.CloneTheBaseModelAttributesFromModel(newCity);
//        }
//    }

//    public class City : BaseModel
//    {
//        [Display(Name = "Phone Code")]
//        [Column(TypeName = "int")]
//        public int Phonecode { get; set; }

//        [Required]
//        [MaxLength(800, ErrorMessage = "Description length can't be more than 800.")]
//        [Display(Name = "Description")]
//        [Column(TypeName = "varchar(800)")]
//        public string Description { get; set; }

//        //[Display(Name = "Country")]
//        //[Column(TypeName = "int")]
//        //[ForeignKey("FK_CityCountry")]
//        //public int IdCountry { get; set; }

//        [Display(Name = "State Id")]
//        [Column(TypeName = "int")]
//        [ForeignKey("FK_CityState")]
//        public int IdState { get; set; }

//        public override void CloneFromModel(BaseModel modelToClone)
//        {
//            City newCity = modelToClone as City;

//            Phonecode = newCity.Phonecode;
//            Description = newCity.Description;

//            base.CloneTheBaseModelAttributesFromModel(newCity);
//        }
//    }
//}

