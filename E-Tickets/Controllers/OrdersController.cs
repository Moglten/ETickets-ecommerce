using E_Tickets.Models;
using E_Tickets.Models.Cart;
using E_Tickets.Models.ModelsDB;
using E_Tickets.Models.Operations;
using E_Tickets.Models.RepositoryService;
using E_Tickets.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Tickets.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class OrdersController : Controller
    {
        private readonly UnitOfWork _unitofwork = new();
        private readonly ShoppingCart _shoppingcart;
        private readonly MovieOperations _movieOperation;
        private readonly OrderOperations _orderOperation;

        public OrdersController(IHostingEnvironment appEnvironment, ShoppingCart shoppingCart)
        {
            _orderOperation = new();
            _shoppingcart = shoppingCart;
            _movieOperation = new(appEnvironment);
        }

        public IActionResult Index()
        {
            var theUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = _orderOperation.GetOrdersByUserId(theUser);

            return View(orders);
        }

        [HttpGet]
        public IActionResult ShoppingCart()
        {
            var items = _shoppingcart.GetShoppingCartItems();
            _shoppingcart.ShoppingCartItems = items;

            var respone = new ShoppingCartViewModel()
            {
                ShoppingCart = _shoppingcart,
                ShoppingCartTotal = _shoppingcart.GetShoppingCartTotal()
            };

            return View(respone);
        }


        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = _movieOperation.GetSpacificMovie(id);

            if (item != null)
            {
                _shoppingcart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = _movieOperation.GetSpacificMovie(id);

            if (item != null)
            {
                _shoppingcart.RemoveItemFromCart(item);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }

        
        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingcart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            string userEmailAddress = User.Identity.Name; 

            _orderOperation.StoreOrder(items, userId, userEmailAddress);
            _shoppingcart.ClearShoppingCart();

            return View("CompleteOrder");
        }

    }
}
