using LocalPhoneDomain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalPhoneDomain.Models
{
    public class MessageModel : BaseModel
    {
        [Required]
        [Column(TypeName = "varchar(20)")]
        [ForeignKey("FK_MessageCustomerSending")]
        public string IdCustomerSending { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        [ForeignKey("FK_MessageCustomerReceiving")]
        public string IdCustomerReceiving { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        public string Text { get; set; }

        [Required]
        public MessageType Type { get; set; }

        [Required]
        [Column(TypeName = "DateTime")]
        public DateTime? Date { get; set; }

        public virtual CustomerModel CustomerReceiving { get; set; }
        public virtual CustomerModel CustomerSending { get; set; }


        public override void CloneFromModel(BaseModel modelToClone)
        {
            MessageModel newModel = modelToClone as MessageModel;

            Text = newModel.Text;
            Date = newModel.Date;
            //IdCustomerReceiving = newModel.IdCustomerReceiving;
            //IdCustomerSending = newModel.IdCustomerSending;

            base.CloneBaseModelAttributesExcludingStatusFromModel(modelToClone);
        }
    }
}
