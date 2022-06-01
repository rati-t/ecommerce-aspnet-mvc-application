using eTickets.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public interface IOrderService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> orderItems, string userId, string userEmailAddress);

        Task<List<Order>> GetOrdersByUserIdAsycn(string userId);
    }
}
