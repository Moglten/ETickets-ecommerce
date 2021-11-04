using System;
using System.Collections.Generic;

#nullable disable

namespace E_Tickets.Models.ModelsDB
{
    public partial class MoviesCategory
    {
        public int MovieId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
