using E_Tickets.Models.ModelsDB;
using E_Tickets.Models.RepositoryService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;


namespace E_Tickets.Models.Cart
{
    public class ShoppingCart
    {

        //Attriputes
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }


        //operations
        private readonly UnitOfWork _unitOfWork = new();
        private readonly IHttpContextAccessor _accessor;
        private readonly IRepository<ShoppingCartItem> _shopingCartItems;


        public ShoppingCart(IHttpContextAccessor accessor)
        {
            _shopingCartItems = _unitOfWork.GetRepositoryInstance<ShoppingCartItem>();
            _accessor = accessor;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            IHttpContextAccessor httpContext = services.GetRequiredService<IHttpContextAccessor>();  
            return new ShoppingCart(httpContext) { };
        }


        public void AddItemToCart(Movie movie)
        {
            var shoppingCartItem = _shopingCartItems
                                    .GetAllEntitiesIQueryable()
                                    .FirstOrDefault(n => n.Movie.Id == movie.Id);

            if (shoppingCartItem == null)
            {
                _shopingCartItems.Add(
                    new ShoppingCartItem()
                    {
                        MovieId = movie.Id,
                        Amount = 1,
                        UserId = _accessor.HttpContext.User.Identity.GetUserId()

                    });
            }else
                shoppingCartItem.Amount ++;

            _unitOfWork.SaveChanges();

        }


        public void RemoveItemFromCart(Movie movie)
        {
            var shoppingCartItem = _shopingCartItems
                                    .GetAllEntitiesIQueryable()
                                    .FirstOrDefault(n => n.Movie.Id == movie.Id && n.UserId == _accessor.HttpContext.User.Identity.GetUserId());

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                    shoppingCartItem.Amount--;
                else
                    _shopingCartItems.Remove(shoppingCartItem.Id);
            }
            _unitOfWork.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
           => ShoppingCartItems = _shopingCartItems.GetAllEntitiesIQueryable()
                                                    .Where(n => n.UserId == _accessor.HttpContext.User.Identity.GetUserId())
                                                    .Include(m => m.Movie)
                                                    .ToList();
                                                                              
        

        public double GetShoppingCartTotal()
            => _shopingCartItems.GetAllEntitiesIQueryable()
                                .Where(n => n.UserId == _accessor.HttpContext.User.Identity.GetUserId())
                                .Select(m => m.Movie.Price * m.Amount)
                                .Sum();



        public void ClearShoppingCart()
        {
            var items = _shopingCartItems.GetAllEntitiesIQueryable()
                                         .Where(n => n.UserId == _accessor.HttpContext.User.Identity.GetUserId())
                                         .ToList();

            _shopingCartItems.RemoveRange(items);

            ShoppingCartItems = new();
        }



    }
}
