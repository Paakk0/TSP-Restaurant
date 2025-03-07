using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Restaurant.Model;
using System.Linq;

namespace Restaurant.Pages.Administration
{
    public class Employee_StatisticsModel : PageModel
    {
        private readonly ApplicationContext _context;

        public Employee_StatisticsModel(ApplicationContext context)
        {
            _context = context;
        }

        public Employee? Employee { get; set; }
        public int TotalOrders { get; set; }
        public int CompletedOrders { get; set; }
        public int PendingOrders { get; set; }
        public double Income { get; set; }
        public double Tips { get; set; }
        public double Total { get; set; }
        public List<double>? IncomeData { get; set; }
        public const double PERCENTAGE = 0.05;

        public IActionResult OnGet(int id)
        {
            var sessionEmployee = HttpContext.Session.GetObject<Employee>("Employee");
            var sessionUser = HttpContext.Session.GetObject<User>("User");

            if (sessionEmployee == null && (sessionUser == null || sessionUser.Role != 1))
            {
                TempData["Error"] = "You do not have permission to view this page!";
                return Page();
            }

            Employee = _context.Employees!.Find(id);

            if (Employee == null)
            {
                TempData["Error"] = "Employee not found!";
                return Page();
            }

            TotalOrders = _context.Orders!.Count(o => o.EmployeeId == Employee.Id);
            CompletedOrders = _context.Orders!.Count(o => o.EmployeeId == Employee.Id && o.Status == 1);
            PendingOrders = _context.Orders!.Count(o => o.EmployeeId == Employee.Id && o.Status == 0);

            Income = _context.Order_Dishes!
                .Where(od =>
                    od.Order!.EmployeeId == Employee.Id &&
                    od.Order.Status == 1 &&
                    od.Order.Date.Month == DateTime.Now.Month)
               .Sum(od => od.Dish!.Price * od.Quantity * PERCENTAGE);

            Tips = _context.Orders!
                .Where(o =>
                    o.EmployeeId == Employee.Id &&
                    o.Status == 1 &&
                    o.Date.Month == DateTime.Now.Month)
               .Sum(o => o.Tip);

            Total = Income + Tips;
            IncomeData = new List<double>()
            {
                Income,
                Tips,
            };
            return Page();
        }
    }
}
