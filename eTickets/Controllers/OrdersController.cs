using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMoviesService _service;
        private readonly IOrderService _orderService;
        private readonly ShoppingCart _shoppingCart;
        public OrdersController(IMoviesService service, IOrderService orderService, ShoppingCart shoppingCart)
        {
            _service = service;
            _shoppingCart = shoppingCart;
            _orderService = orderService;
        }
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }

        public async Task<ActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _orderService.GetOrdersByUserIdAndRoleAsycn(userId, userRole);

            return View(orders);
        }

        [HttpGet]
        public async Task<RedirectToActionResult> AddToCart(int id)
        {
            var movie = await _service.GetByIdAsync(id);

            if (movie != null)
            {
                await _shoppingCart.AddCartItem(movie);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<RedirectToActionResult> RemoveItemFromShoppingCart(int id)
        {
            var movie = await _service.GetByIdAsync(id);

            if (movie != null)
            {
                await _shoppingCart.RemoveCartItem(movie);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<RedirectToActionResult> AddItemToShoppingCart(int id)
        {
            var movie = await _service.GetByIdAsync(id);
            if(movie != null)
            {
                await _shoppingCart.AddCartItem(movie);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }

        
        public async Task<IActionResult> CompleteOrder()
        {
            var orderItems = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string emailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _orderService.StoreOrderAsync(orderItems, userId, emailAddress);
            await _shoppingCart.ClearShoppingCartAsycn();

            return View("OrderCompleted");
        }
    }
}
