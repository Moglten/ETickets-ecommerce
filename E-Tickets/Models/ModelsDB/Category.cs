using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace E_Tickets.Models.ModelsDB
{
    public partial class Category
    {
        public Category()
        {
            MoviesCategories = new HashSet<MoviesCategory>();
        }

        public int CategoryId { get; set; }
        [Required]
        public string Categ { get; set; }

        public virtual ICollection<MoviesCategory> MoviesCategories { get; set; }
    }
}
