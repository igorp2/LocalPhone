using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace LocalPhoneDomain.Areas.Identity.Data
{
    public class UserModel : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Firstname { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }
        

    }
}