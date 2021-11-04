using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

#nullable disable

namespace E_Tickets.Models.ModelsDB
{
    public partial class Cinema
    {
        public Cinema()
        {
            Movies = new HashSet<Movie>();
        }
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cinema Logo")]
        [FileExtensions(Extensions = "JPEG,JPG,PNG", ErrorMessage = "Error Wrong Extension")]
        public string Logo { get; set; }

        [Display(Name = "Cinema Name")]
        [Required(ErrorMessage = "Cinema name is required")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Cinema description is required")]
        public string Description { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
