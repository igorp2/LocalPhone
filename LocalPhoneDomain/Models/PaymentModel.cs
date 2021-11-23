using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalPhoneDomain.Models
{
    public class PaymentModel : BaseModel
    {
        //[Column(TypeName = "nvarchar(100)")]
        //public string MethodTypes { get; set; }

        [Column(TypeName = "nvarchar(16)")]
        public string CardNumber { get; set; }

        //[Column(TypeName = "int")]
        //public int? ExpirationMonth { get; set; }

        //[Column(TypeName = "int")]
        //public int? ExpirationYear { get; set; }

        //[Column(TypeName = "int")]
        //public int? Cvc { get; set; }

        //[Column(TypeName = "int")]
        //public int? OrderAmount { get; set; }

        //[Column(TypeName = "nvarchar(100)")]
        //public string ClientSecret { get; set; }

        //[Column(TypeName = "nvarchar(100)")]
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public int Cvc { get; set; }
        public int OrderAmount { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string NameOnCard { get; set; }
        public string ZipCode { get; set; }
        //public string ClientSecret { get; set; }
        //public string PaymentIntentId { get; set; }
        //[Column(TypeName = "nvarchar(100)")]
        //public string PaymentIntentId { get; set; }

        [Column(TypeName = "varchar(20)")]
        [ForeignKey("FK_PaymentCustomer")]
        public string IdCustomer { get; set; }

        //public string BoughtNumber { get; set; }

        public virtual CustomerModel Customer { get; set; }
        //public virtual IEnumerable<NumberModel> Numbers { get; set; }

        public override void CloneFromModel(BaseModel modelToClone)
        {
            PaymentModel newModel = modelToClone as PaymentModel;

            //MethodTypes = newModel.MethodTypes;
            CardNumber = newModel.CardNumber;
            ExpirationMonth = newModel.ExpirationMonth;
            ExpirationYear = newModel.ExpirationYear;
            Cvc = newModel.Cvc;
            OrderAmount = newModel.OrderAmount;
            //ClientSecret = newModel.ClientSecret;
            //PaymentIntentId = newModel.PaymentIntentId;
            IdCustomer = newModel.IdCustomer;
            Status = newModel.Status;

            base.CloneBaseModelAttributesExcludingStatusFromModel(modelToClone);
        }
    }
}
