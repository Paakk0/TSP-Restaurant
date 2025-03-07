using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Restaurant.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Pages.Administration
{
    public class OrdersManagerModel : PageModel
    {
        public readonly ApplicationContext _context;

        public OrdersManagerModel(ApplicationContext context)
        {
            _context = context;
        }

        public List<Order>? Orders { get; set; }

        public List<Order>? UnfinishedOrders { get; set; }
        public List<Order>? FinishedOrders { get; set; }

        public async Task OnGetAsync()
        {
            Orders = await _context.Orders!
                .Include(o => o.User)
                .ToListAsync();

            UnfinishedOrders = Orders!
                .Where(o => o.Status == 0)
                .OrderBy(o => o.Id)
                .ToList();

            FinishedOrders = Orders!
                .Where(o => o.Status == 1)
                .OrderBy(o => o.Id)
                .ToList();
        }
    }
}
