using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Restaurant.Model;
using static NuGet.Packaging.PackagingConstants;

namespace Restaurant.Pages.Administration
{
    public class View_OrderModel : PageModel
    {
        public readonly ApplicationContext _context;

        public View_OrderModel(ApplicationContext context)
        {
            _context = context;
        }

        public Order? Order { get; set; }
        public List<Order_Dish>? OrderItems { get; set; }
        public double TotalPrice { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, string task)
        {
            if (task == "delete")
            {
                _context.Orders!.Remove(_context.Orders.Find(id)!);
                _context.SaveChanges();
                return RedirectToPage("./OrdersManager");
            }
            else if (task == "complete")
                return await OnPostAsync(id);

            Order = await _context.Orders!
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (Order == null)
            {
                return NotFound();
            }

            OrderItems = await _context.Order_Dishes!
                .Where(od => od.OrderId == id)
                .Include(od => od.Dish)
                .ToListAsync();

            TotalPrice = OrderItems.Sum(od => od.Dish!.Price * od.Quantity);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int orderId)
        {
            var order = await _context.Orders!.FindAsync(orderId);
            if (order == null)
            {
                TempData["Error"] = "Order not found!";
                return Page();
            }

            DateTime now = DateTime.Now;
            DateTime twoHoursAgo = now.AddHours(-2);

            var existingOrder = _context.Orders
                .Where(o => o.UserId == order.UserId
                    && o.Location == order.Location
                    && o.Date >= twoHoursAgo
                    && o.Id != orderId)
                .OrderByDescending(o => o.Date)
                .FirstOrDefault();

            if (existingOrder != null)
            {
                var orderDishes = _context.Order_Dishes!.Where(od => od.OrderId == orderId).ToList();

                foreach (var dish in orderDishes)
                {
                    var existingDish = _context.Order_Dishes!
                        .FirstOrDefault(od => od.OrderId == existingOrder.Id && od.DishId == dish.DishId);

                    if (existingDish != null)
                        existingDish.Quantity += dish.Quantity;
                    else
                        _context.Order_Dishes!.Add(new Order_Dish
                        {
                            OrderId = existingOrder.Id,
                            DishId = dish.DishId,
                            Quantity = dish.Quantity
                        });
                }

                _context.Order_Dishes!.RemoveRange(orderDishes);
                await _context.SaveChangesAsync();
                existingOrder.Tip += order.Tip;
                _context.Orders.Remove(order);
            }
            else
                order.Status = 1;

            await _context.SaveChangesAsync();
            Mailer.SendMail(_context.Users!.Find(order.UserId)!, "Completed", order.Id);
            var user = HttpContext.Session.GetObject<User>("User");
            if (user != null && user.Role == 1)
                return RedirectToPage("./OrdersManager");
            else
                return RedirectToPage("/Index");
        }
    }
}
