using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

#nullable disable

namespace E_Tickets.Models.ModelsDB
{
    public partial class Actor
    {
        public Actor()
        {
            ActorsMovies = new HashSet<ActorsMovie>();
}

        public int Id { get; set; }
        [Display(Name = "Profile Picture")]
        [FileExtensions(Extensions = "JPEG,JPG,PNG", ErrorMessage = "Error Wrong Extension")]
        public string ProfilePictureUrl { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        public string FullName { get; set; }
        [Display(Name = "Bio")]
        [Required(ErrorMessage = "Bio is required")]
        [StringLength(400, MinimumLength = 25, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        public string Bio { get; set; }

        public virtual ICollection<ActorsMovie> ActorsMovies { get; set; }
    }
}
