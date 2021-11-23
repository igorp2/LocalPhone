using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalPhoneDomain.Models
{
    public class CustomerModel
    {
        [Key]
        [Required]
        [MinLength(8)]
        [Display(Name = "Phone Number")]
        [Column(TypeName = "varchar(15)")]
        [RegularExpression(RegistrationInformationModel.PATTERN_TO_VALIDATE_A_SIMPLE_PHONE_NUMBER,
            ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Operational System")]
        [Column(TypeName = "varchar(100)")]
        public string OperationalSystem { get; set; }

        [Display(Name = "Verification code")]
        [Column(TypeName = "int")]
        public int? VerificationCode { get; set; }

        [Display(Name = "Validation Code Date")]
        [Column(TypeName = "DateTime")]
        public DateTime? ValidationCodeDate { get; set; }

        [Display(Name = "Verification Code Date")]
        [Column(TypeName = "DateTime")]
        public DateTime? VerificationCodeDate { get; set; }

        [Required]
        [Display(Name = "Published app version")]
        [Column(TypeName = "int")]
        public int? PublishedAppVersion { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Password { get; set; }

        [Column(TypeName = "varchar(35)")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Date of birth")]
        [Column(TypeName = "DateTime")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "First name")]
        [Column(TypeName = "varchar(20)")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Column(TypeName = "varchar(25)")]
        public string LastName { get; set; }

        public string Avatar { get; set; }

        public virtual GenderModel Gender { get; set; }

        [Column(TypeName = "int")]
        [Display(Name = "Gender")]
        [ForeignKey("FK_CostumerGender")]
        public int? IdGender { get; set; }

        public CustomerStatuses Status { get; set; }

        public virtual CountryModel Country { get; set; }

        [Required]
        [Display(Name = "Country")]
        [Column(TypeName = "int")]
        [ForeignKey("FK_CostumerCountry")]
        public int? IdCountry { get; set; }

        [Column(TypeName = "datetime")]
        public virtual DateTime? CreationDate { get; set; }

        [Column(TypeName = "varchar(450)")]
        public virtual string CreatorUser { get; set; }

        [Column(TypeName = "datetime")]
        public virtual DateTime? LastModificationDate { get; set; }

        [Column(TypeName = "varchar(450)")]
        public virtual string UserThatMadeTheLastModification { get; set; }

        public virtual IEnumerable<AddressModel> Addresses { get; set; }
        public virtual IEnumerable<ContactModel> Contacts { get; set; }
        public virtual IEnumerable<MessageModel> MessageIdCustomerReceiving { get; set; }
        public virtual IEnumerable<MessageModel> MessageIdCustomerSending { get; set; }
        public virtual IEnumerable<NumberModel> Numbers { get; set; }
        public virtual IEnumerable<PaymentModel> Payments { get; set; }

        public static CustomerModel CreateFromRegistrationInformation(RegistrationInformationModel registrationInformation)
        {
            CustomerModel newCustomer = new CustomerModel();
            newCustomer.CloneFromRegistrationInformation(registrationInformation);
            return newCustomer;
        }

        public void CloneFromRegistrationInformation(RegistrationInformationModel registrationInformation)
        {
            PhoneNumber = registrationInformation.PhoneNumber;
            IdCountry = registrationInformation.IdCountry;
            OperationalSystem = registrationInformation.OperationalSystem;
            PublishedAppVersion = registrationInformation.PublishedAppVersion;
            CreationDate = DateTime.Now;
        }

        public void CloneFromAnotherCustomer(CustomerModel customer)
        {
            OperationalSystem = customer.OperationalSystem ?? OperationalSystem;
            PublishedAppVersion = customer.PublishedAppVersion ?? PublishedAppVersion;
            Password = customer.Password ?? Password;
            Email = customer.Email ?? Email;
            DateOfBirth = customer.DateOfBirth ?? DateOfBirth;
            FirstName = customer.FirstName ?? FirstName;
            LastName = customer.LastName ?? LastName;
            IdGender = customer.IdGender ?? IdGender;
            Status = customer.Status;
            LastModificationDate = customer.LastModificationDate ?? LastModificationDate ?? DateTime.Now;
            UserThatMadeTheLastModification = customer.UserThatMadeTheLastModification ?? UserThatMadeTheLastModification;
            Avatar = customer.Avatar ?? Avatar;
            VerificationCode = customer.VerificationCode ?? VerificationCode;
            VerificationCodeDate = customer.VerificationCodeDate ?? VerificationCodeDate;
            ValidationCodeDate = customer.ValidationCodeDate ?? ValidationCodeDate;
        }
    }
}

        //[Display(Name = "Status")]
        //[Column(TypeName = "int")]
        //[ForeignKey("FK_CostumerCountry")]
        //public int idStatus { get; set; }

        //[phoneNumber] [varchar] (20) NOT NULL,
        //[idCountry] [int] NULL,
        //[operationalSystem] [varchar] (100) NULL,
        //[verificationCode] [int] NULL,
        //[validationCodeDate] [datetime] NULL,
        //[verificationCodeDate] [datetime] NULL,
        //[idStatus] [int] NULL,

        //[Required]
        //[StringLength(100, ErrorMessage = "Name length can't be more than 100.")]
        //[Display(Name = "Password")]
        //[Column(TypeName = "varchar(100)")]
        //public string Password { get; set; }

        //[Required]
        //[StringLength(200, ErrorMessage = "Name length can't be more than 200.")]
        //[Display(Name = "Email")]
        //[Column(TypeName = "varchar(200)")]
        //public string Email { get; set; }

        //[Display(Name = "Date Of Birth")]
        //[Column(TypeName = "datetime")]
        //public DateTime DateOfBirth { get; set; }

        //[StringLength(100, ErrorMessage = "Name length can't be more than 100.")]
        //[Display(Name = "First Name")]
        //[Column(TypeName = "varchar(100)")]
        //public string FirstName { get; set; }

        //[StringLength(100, ErrorMessage = "Name length can't be more than 100.")]
        //[Display(Name = "Last Name")]
        //[Column(TypeName = "varchar(100)")]
        //public string LastName { get; set; }

        //[StringLength(100, ErrorMessage = "Name length can't be more than 100.")]
        //[Display(Name = "Gender")]
        //[Column(TypeName = "varchar(100)")]
        //public string Gender { get; set; }

        //[Display(Name = "Avatar")]
        //[Column(TypeName = "varbinary(max)")]
        //public virtual byte[] Avatar { get; set; }

        //[StringLength(200, ErrorMessage = "Name length can't be more than 200.")]
        //[Display(Name = "Street")]
        //[Column(TypeName = "varchar(200)")]
        //public string Street { get; set; }

        //[Display(Name = "City")]
        //[Column(TypeName = "int")]
        //[ForeignKey("FK_CostumerCity")]
        //public int IdCity { get; set; }

        //public virtual CityModel City { get; set; }

        //[Display(Name = "State")]
        //[Column(TypeName = "int")]
        //[ForeignKey("FK_CostumerState")]
        //public int IdState { get; set; }

        //public virtual StateModel State { get; set; }

        //[StringLength(30, ErrorMessage = "Name length can't be more than 30.")]
        //[Display(Name = "Zip")]
        //[Column(TypeName = "varchar(30)")]
        //public string Zip { get; set; }



        //[StringLength(16, ErrorMessage = "Name length can't be more than 16.")]
        //[Display(Name = "Card Number")]
        //[Column(TypeName = "varchar(16)")]
        //public string CardNumber { get; set; }

        //[Display(Name = "Expiration Month")]
        //[Column(TypeName = "int")]
        //public int ExpirationMonth { get; set; }

        //[Display(Name = "Expiration Year")]
        //[Column(TypeName = "int")]
        //public int ExpirationYear { get; set; }

        //[Display(Name = "CVC")]
        //[Column(TypeName = "int")]
        //public int CVC { get; set; }

