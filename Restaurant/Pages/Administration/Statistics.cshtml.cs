using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Restaurant.Model;
using System.Globalization;

namespace Restaurant.Pages.Administration
{
    public class StatisticsModel : PageModel
    {
        public readonly ApplicationContext _context;

        public List<string>? RevenueLabels { get; set; }
        public List<double>? RevenueData { get; set; }
        public double RevenueFromGraph { get; set; }
        public double TotalRevenue { get; set; }

        public List<string>? TopDishesLabels { get; set; }
        public List<int>? TopDishesData { get; set; }
        public int TotalOrders { get; set; }

        public List<string>? CategoryLabels { get; set; }
        public List<int>? CategoryData { get; set; }
        public int TotalCategories { get; set; }

        public List<string> UserEmployeeGrowthLabels { get; set; } = new List<string>();
        public List<int> UserGrowthData { get; set; } = new List<int>();
        public List<int> EmployeeGrowthData { get; set; } = new List<int>();

        public StatisticsModel(ApplicationContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            var revenueStats = await _context.Orders!
                .Where(o => o.Date >= DateTime.Now.AddMonths(-12))
                .Join(_context.Order_Dishes!, o => o.Id, od => od.OrderId, (o, od) => new { o.Date, od.DishId, od.Quantity })
                .Join(_context.Dishes!, od => od.DishId, d => d.Id, (od, d) => new { od.Date, d.Price, od.Quantity })
                .GroupBy(o => o.Date.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Revenue = g.Sum(o => o.Price * o.Quantity)
                })
                .ToListAsync();

            RevenueLabels = revenueStats.Select(r => $"Month {r.Month}").ToList();
            RevenueData = revenueStats.Select(r => r.Revenue).ToList();
            RevenueFromGraph = revenueStats.Sum(r => r.Revenue);
            TotalRevenue = _context.Orders!
                .Join(_context.Order_Dishes!, o => o.Id, od => od.OrderId, (o, od) => new { o.Date, od.DishId, od.Quantity })
                .Join(_context.Dishes!, od => od.DishId, d => d.Id, (od, d) => new { od.Date, d.Price, od.Quantity })
                .GroupBy(o => o.Date.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Revenue = g.Sum(o => o.Price * o.Quantity)
                })
                .Sum(o => o.Revenue);

            var topDishesStats = await _context.Order_Dishes!
                .Where(od => od.Order!.Date >= DateTime.Now.AddMonths(-12))
                .GroupBy(od => od.DishId)
                .Select(g => new
                {
                    DishId = g.Key,
                    Quantity = g.Sum(od => od.Quantity),
                    DishName = g.FirstOrDefault()!.Dish!.Name,
                    DishPrice = g.FirstOrDefault()!.Dish!.Price,
                    DishTypeId = g.FirstOrDefault()!.Dish!.DishTypeId
                })
                .OrderByDescending(g => g.Quantity)
                .Take(5)
                .ToListAsync();

            var dishTypeIds = topDishesStats.Select(td => td.DishTypeId).Distinct().ToList();
            var dishTypes = await _context.DishTypes!.Where(dt => dishTypeIds.Contains(dt.Id)).ToListAsync();

            TopDishesLabels = topDishesStats.Select(td =>
            {
                var dishType = dishTypes.FirstOrDefault(dt => dt.Id == td.DishTypeId);
                return $"Id - {td.DishId}\n{td.DishName}\nType - {dishType!.Type}";
            }).ToList();
            TopDishesData = topDishesStats.Select(td => td.Quantity).ToList();
            TotalOrders = _context.Orders!.Count(o => o.Status == 1);

            var categoryStats = await _context.Dishes!
                .GroupBy(d => d.DishType!.Type)
                .Select(g => new { Category = g.Key, Count = g.Count() })
                .ToListAsync();

            CategoryLabels = categoryStats.Select(c => c.Category!).ToList();
            CategoryData = categoryStats.Select(c => c.Count).ToList();
            TotalCategories = categoryStats.Count();

            var userGrowthStats = await _context.Users!
                .Where(u => u.RegistrationDate >= DateTime.Now.AddMonths(-12))
                .GroupBy(u => new { Year = u.RegistrationDate.Year, Month = u.RegistrationDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToListAsync();

            var employeeGrowthStats = await _context.Employees!
                .Where(u => u.EmploymentDate >= DateTime.Now.AddMonths(-12))
                .GroupBy(u => new { Year = u.EmploymentDate.Year, Month = u.EmploymentDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToListAsync();

            for (int i = 11; i >= 0; i--)
            {
                var date = DateTime.Now.AddMonths(-i);
                UserEmployeeGrowthLabels.Add(date.ToString("yyyy-MM", CultureInfo.InvariantCulture));
            }

            UserGrowthData = Enumerable.Repeat(0, 12).ToList();
            EmployeeGrowthData = Enumerable.Repeat(0, 12).ToList();

            foreach (var stat in userGrowthStats)
            {
                var label = $"{stat.Year}-{stat.Month:D2}";
                var index = UserEmployeeGrowthLabels.IndexOf(label);
                if (index >= 0)
                {
                    UserGrowthData[index] = stat.Count;
                }
            }

            foreach (var stat in employeeGrowthStats)
            {
                var label = $"{stat.Year}-{stat.Month:D2}";
                var index = UserEmployeeGrowthLabels.IndexOf(label);
                if (index >= 0)
                {
                    EmployeeGrowthData[index] = stat.Count;
                }
            }
        }
    }
}

