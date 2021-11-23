using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalPhoneDomain.Models
{
    public class ContactModel : BaseModel
    {
        [Required]
        [Display(Name = "Customer")]
        [Column(TypeName = "varchar(20)")]
        [ForeignKey("FK_ContactCustomer")]
        public string IdCustomer { get; set; }

        public virtual CustomerModel Customer { get; set; }

        public override void CloneFromModel(BaseModel modelToClone)
        {
            ContactModel newContact = modelToClone as ContactModel;

            IdCustomer = newContact.IdCustomer;

            base.CloneBaseModelAttributesExcludingStatusFromModel(modelToClone);
        }
    }
}
