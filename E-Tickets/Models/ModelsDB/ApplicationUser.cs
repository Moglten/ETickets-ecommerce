using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace E_Tickets.Models.ModelsDB
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [PersonalData]
        public int Age { get; set; }

        [Required]
        [PersonalData]
        [Display(Name = "Username")]   
        public string GivenName { get; set; }

    }
}
