using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TCGShop.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the TCGShopUser class
    public class TCGShopUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        [PersonalData]
        [Column(TypeName = "varchar(255)")]
        public string Adress { get; set; }

        public bool IsAdmin { get; set; }
    }
}
