using System;
using System.Collections.Generic;

#nullable disable

namespace E_Tickets.Models.ModelsDB
{
    public partial class ActorsMovie
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }

        public virtual Actor Actor { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
