using E_Tickets.Models.Cart;
using E_Tickets.Models.ModelsDB;
using E_Tickets.Models.RepositoryService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Tickets.Models.Operations
{
    public class OrderOperations
    {
        private readonly UnitOfWork _unitOfWork = new();
        private readonly IRepository<Order> _order;
        private readonly IRepository<OrderItem> _orderItem;


        public OrderOperations()
        {
            _order = _unitOfWork.GetRepositoryInstance<Order>();
            _orderItem = _unitOfWork.GetRepositoryInstance<OrderItem>();
        }

        public List<Order> GetOrdersByUserId(string userId)
        {
            var orders =  _order.GetAllEntitiesIQueryable()
                                .Include(n => n.OrderItems)
                                .ThenInclude(n => n.Movie)
                                .Where(n => n.UserId == userId)
                                .ToList();

            return orders;
        }

        public void StoreOrder(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };

            _order.Add(order);

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id,
                    Price = item.Movie.Price
                };

                _orderItem.Add(orderItem);
            }
        }

    }
}
