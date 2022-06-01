using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
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
            string userId = "";
            var orders = await _orderService.GetOrdersByUserIdAsycn(userId);

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
            string userId = "";
            string emailAddress = "";

            await _orderService.StoreOrderAsync(orderItems, userId, emailAddress);
            await _shoppingCart.ClearShoppingCartAsycn();

            return View("OrderCompleted");
        }
    }
}
