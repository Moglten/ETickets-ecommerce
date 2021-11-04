using System;
using System.Collections.Generic;

#nullable disable

namespace E_Tickets.Models.ModelsDB
{
    public partial class OrderItem
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public int MovieId { get; set; }
        public string OrderId { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Order Order { get; set; }
    }
}
