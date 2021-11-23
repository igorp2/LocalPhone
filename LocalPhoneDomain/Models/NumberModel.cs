using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalPhoneDomain.Models
{
    public class NumberModel : BaseModel
    {
        [Required]
        [Column(TypeName = "varchar(20)")]
        [ForeignKey("FK_NumberCustomer")]
        public string IdCustomer { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "int")]
        [ForeignKey("FK_NumberPayment")]
        public int IdPayment { get; set; }

        public virtual CustomerModel Customer { get; set; }
        public virtual PaymentModel Payment { get; set; }

        public override void CloneFromModel(BaseModel modelToClone)
        {
            NumberModel newModel = modelToClone as NumberModel;

            PhoneNumber = newModel.PhoneNumber;
            IdCustomer = newModel.IdCustomer;
            IdPayment = newModel.IdPayment;
            Status = newModel.Status;

            base.CloneBaseModelAttributesExcludingStatusFromModel(modelToClone);
        }
    }
}
