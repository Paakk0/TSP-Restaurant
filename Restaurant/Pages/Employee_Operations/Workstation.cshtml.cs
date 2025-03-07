using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Restaurant.Model;

namespace Restaurant.Pages.Employee_Operations
{
    public class WorkstationModel : PageModel
    {
        public readonly ApplicationContext _context;

        public WorkstationModel(ApplicationContext context)
        {
            _context = context;
        }

        public Employee? Employee { get; set; }
        public List<Order>? Orders { get; set; }
        public List<Order>? UnfinishedOrders { get; set; }
        public List<Order>? FinishedOrders { get; set; }

        public IActionResult OnGet()
        {
            Employee = HttpContext.Session.GetObject<Employee>("Employee");
            Orders = _context.Orders!
                .Where(o => o.EmployeeId == Employee!.Id)
                .Include(o => o.User)
                .ToList();

            UnfinishedOrders = Orders!
                .Where(o => o.Status == 0)
                .OrderBy(o => o.Id)
                .ToList();

            FinishedOrders = Orders!
                .Where(o => o.Status == 1)
                .OrderBy(o => o.Id)
                .ToList();

            return Page();
        }
    }
}
