using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Model;

namespace Restaurant.Pages.Administration
{
    public class AdminPanelModel : PageModel
    {
        public readonly ApplicationContext _context;

        public int TotalUsers { get; set; }
        public int TotalOrders { get; set; }
        public double TotalRevenue { get; set; }
        public List<User>? Users { get; set; }
        public List<Employee>? Employees { get; set; }
        public List<Order>? Orders { get; set; }
        public List<Dish>? MenuItems { get; set; }

        public AdminPanelModel(ApplicationContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Users = _context.Users!.ToList();
            Employees = _context.Employees!.ToList();
            Orders = _context.Orders!.ToList();
            MenuItems = _context.Dishes!.ToList();
            TotalUsers = Users.Count;
            TotalOrders = Orders.Count;
            TotalRevenue = _context.Orders!
                .Join(_context.Order_Dishes!, o => o.Id, od => od.OrderId, (o, od) => new { o, od })
                .Join(_context.Dishes!, od => od.od.DishId, d => d.Id, (od, d) => new { od.o, d.Price })
                .Sum(x => x.Price);
        }
    }
}
