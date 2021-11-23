using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalPhoneDomain.Models
{
    public class RegistrationInformationModel
    {
        public const string PATTERN_TO_VALIDATE_A_SIMPLE_PHONE_NUMBER = @"^\d+$";

        [Required]
        [MinLength(8)]
        [Display(Name = "Phone number")]
        [RegularExpression(PATTERN_TO_VALIDATE_A_SIMPLE_PHONE_NUMBER, ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int? IdCountry { get; set; }

        [Required]
        [Display(Name = "Operational System")]
        [Column(TypeName = "varchar(100)")]
        public string OperationalSystem { get; set; }

        [Required]
        [Display(Name = "Published app version")]
        [Column(TypeName = "int")]
        public int? PublishedAppVersion { get; set; }
    }
}
