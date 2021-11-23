using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalPhoneDomain.Models
{
    public class AddressModel : BaseModel
    {
        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string Type { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string Street { get; set; }

        [Required]
        [Column(TypeName = "int")]
        [ForeignKey("FK_AddressCity")]
        public int? IdCity { get; set; }

        [Required]
        [Column(TypeName = "int")]
        [ForeignKey("FK_AddressState")]
        public int? IdState { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        public string Zip { get; set; }

        [Required]
        [Column(TypeName = "int")]
        [ForeignKey("FK_AddressCountry")]
        public int? IdCountry { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Note { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        [ForeignKey("FK_AddressCustomer")]
        public string IdCustomer { get; set; }

        public virtual CityModel City { get; set; }
        public virtual CountryModel Country { get; set; }
        public virtual CustomerModel Customer { get; set; }
        public virtual StateModel State { get; set; }

        public override void CloneFromModel(BaseModel modelToClone)
        {
            AddressModel newModel = modelToClone as AddressModel;

            Type = newModel.Type;
            Street = newModel.Street;
            Zip = newModel.Zip;
            Note = newModel.Note;
            IdCity = newModel.IdCity;
            IdState = newModel.IdState;
            IdCountry = newModel.IdCountry;
            IdCustomer = newModel.IdCustomer;

            base.CloneBaseModelAttributesExcludingStatusFromModel(modelToClone);
        }
    }
}
