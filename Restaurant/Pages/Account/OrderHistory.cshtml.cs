using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Model;

namespace Restaurant.Pages.Account
{
    public class OrderHistoryModel : PageModel
    {
        public readonly ApplicationContext _context;

        public OrderHistoryModel(ApplicationContext context)
        {
            _context = context;
        }

        public new User? User { get; set; }
        public List<Order>? OrderList { get; set; }
        public List<Dish>? Dishes { get; set; }
        public List<Order_Dish>? OrderDishes { get; set; }

        public void OnGet()
        {
            User = HttpContext.Session.GetObject<User>("User");

            if (User != null)
            {
                OrderList = _context.Orders!
                    .Where(o => o.UserId == User.Id)
                    .OrderByDescending(o => o.Status == 0)
                    .ThenByDescending(o => o.Date)
                    .ToList();

                var orderIds = OrderList.Select(o => o.Id).ToList();

                OrderDishes = _context.Order_Dishes!
                    .Where(od => orderIds.Contains(od.OrderId))
                    .ToList();

                Dishes = _context.Dishes!
                    .Where(d => OrderDishes.Select(od => od.DishId).Contains(d.Id))
                    .ToList();
            }
        }
    }

}
