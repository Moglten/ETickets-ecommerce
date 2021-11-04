using System;
using System.Collections.Generic;

#nullable disable

namespace E_Tickets.Models.ModelsDB
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUser User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
