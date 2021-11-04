using System;
using System.Collections.Generic;

#nullable disable

namespace E_Tickets.Models.ModelsDB
{
    public partial class ShoppingCartItem
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int Amount { get; set; }
        public string UserId { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual AspNetUser User { get; set; }
    }
}
