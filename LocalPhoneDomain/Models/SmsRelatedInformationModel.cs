using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalPhoneDomain.Models
{
    public class SmsRelatedInformationModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Column(TypeName = "char(68)")]
        public string TransactionId { get; set; }

        [ForeignKey("FK_SmsCustomer")]
        [Column(TypeName = "varchar(20)")]
        public string IdCustomer { get; set; }

        public virtual CustomerModel Customer { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreationDate { get; set; }
    }
}
