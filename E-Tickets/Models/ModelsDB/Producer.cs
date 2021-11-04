using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace E_Tickets.Models.ModelsDB
{
    public partial class Producer
    {
        public Producer()
        {
            Movies = new HashSet<Movie>();
        }
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture")]
        [FileExtensions(Extensions = "JPEG,JPG,PNG", ErrorMessage = "Error Wrong Extension")]
        public string ProfilePictureUrl { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        public string FullName { get; set; }
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography is required")]
        public string Bio { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
