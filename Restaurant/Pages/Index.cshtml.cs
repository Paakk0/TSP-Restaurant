using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationContext _context;
        public new User? User { get; set; }
        public List<Dish>? Dishes { get; set; }
        public List<Ingredient>? Ingredients { get; set; }
        public List<DishType>? DishTypes { get; set; }
        public List<Dish_Ingredient>? Dish_Ingredients { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public string? Place { get; set; }

        [BindProperty]
        public double Tip { get; set; }

        [BindProperty]
        public List<int>? SelectedDishes { get; set; }

        [BindProperty]
        public Dictionary<int, int>? DishQuantities { get; set; } = new Dictionary<int, int>();

        [BindProperty(SupportsGet = true)]
        public bool EnableFilterOptions { get; set; }

        [BindProperty]
        public string PriceFilterOption { get; set; }

        [BindProperty]
        public double PriceFilterValue { get; set; }

        [BindProperty]
        public string IngredientFilterOption { get; set; }

        [BindProperty]
        public List<int> IngredientFilterValues { get; set; } = new List<int>();

        public async Task<IActionResult> OnGetAsync(
            bool? enableFilterOptions,
            string? priceFilterOption,
            double? priceFilterValue,
            string? ingredientFilterOption,
            List<int>? ingredientFilterValues)
        {
            var employee = HttpContext.Session.GetObject<Employee>("Employee");
            if (employee != null)
                return RedirectToPage("/Employee_Operations/Workstation");

            Dishes = await _context.Dishes!.ToListAsync();
            DishTypes = await _context.DishTypes!.ToListAsync();
            Ingredients = await _context.Ingredients!.ToListAsync();
            Dish_Ingredients = await _context.Dish_Ingredients!.ToListAsync();

            EnableFilterOptions = enableFilterOptions ?? false;
            PriceFilterOption = priceFilterOption ?? "";
            PriceFilterValue = priceFilterValue ?? 0;
            IngredientFilterOption = ingredientFilterOption ?? "";
            IngredientFilterValues = ingredientFilterValues ?? new List<int>();

            Console.WriteLine("ENABLE FILTER OPTIONS IS: " + EnableFilterOptions);
            if (EnableFilterOptions)
                ApplyFilters();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = HttpContext.Session.GetObject<User>("User");

            if (user == null)
            {
                return RedirectToPage("Account/Log_in");
            }

            if (SelectedDishes == null || !SelectedDishes.Any())
            {
                TempData["Error"] = "You must select at least one dish!";
                return RedirectToAction("OnGet");
            }

            if (Place == null)
            {
                TempData["Error"] = "You must select a location for the order!";
                return RedirectToAction("OnGet");
            }

            var onlineStaff = _context.Employees!
                .Where(e => e.IsOnline == true)
                .ToList();

            if (!onlineStaff.Any() && !Place.ToLower().Equals("home"))
            {
                TempData["Error"] = "Cannot choose this option at the moment!";
                return RedirectToAction("OnGet");
            }

            return await OrderCombination(user, Place, SelectedDishes);
        }

        private async Task<IActionResult> OrderCombination(User user, string place, List<int> selectedDishes)
        {
            DateTime now = DateTime.Now;
            DateTime twoHoursAgo = now.AddHours(-2);

            var existingOrder = _context.Orders!
                .Where(o => o.UserId == user.Id &&
                            o.Location == place &&
                            o.Date >= twoHoursAgo &&
                            o.Status == 0)
                .OrderByDescending(o => o.Date)
                .FirstOrDefault();

            if (existingOrder != null)
            {
                foreach (var dishId in selectedDishes)
                {
                    var existingDish = _context.Order_Dishes!
                        .FirstOrDefault(od => od.OrderId == existingOrder.Id && od.DishId == dishId);
                    if (existingDish != null)
                    {
                        existingDish.Quantity += DishQuantities![dishId];
                    }
                    else
                    {
                        Order_Dish orderDish = new Order_Dish
                        {
                            OrderId = existingOrder.Id,
                            DishId = dishId,
                            Quantity = DishQuantities![dishId]
                        };
                        _context.Order_Dishes!.Add(orderDish);
                    }
                }
                existingOrder.Date = now;
                existingOrder.Tip += Tip;
                await _context.SaveChangesAsync();
                return RedirectToPage("/Account/OrderHistory");
            }

            int employeeId = 0;
            if (!place.ToLower().Equals("home"))
            {
                var onlineStaff = _context.Employees!
                    .Where(e => e.IsOnline == true)
                    .ToList();
                if (onlineStaff.Any())
                {
                    Random random = new Random();
                    employeeId = onlineStaff[random.Next(onlineStaff.Count)].Id;
                }
            }

            var order = new Order
            {
                UserId = user.Id,
                EmployeeId = employeeId,
                Status = 0,
                Location = place,
                Date = DateTime.Now,
                Tip = Tip
            };

            _context.Orders!.Add(order);
            await _context.SaveChangesAsync();

            foreach (var dishId in selectedDishes)
            {
                Order_Dish orderDish = new Order_Dish
                {
                    OrderId = order.Id,
                    DishId = dishId,
                    Quantity = DishQuantities![dishId]
                };
                _context.Order_Dishes!.Add(orderDish);
            }
            await _context.SaveChangesAsync();
            Mailer.SendMail(user, "Received", order.Id);
            return RedirectToPage("/Account/OrderHistory");
        }

        private void ApplyFilters()
        {
            if (!Dishes!.Any()) return;

            Console.WriteLine("Applying filters...");

            if (!string.IsNullOrEmpty(PriceFilterOption))
            {
                if (PriceFilterOption == "Lower")
                {
                    Dishes = Dishes!.Where(d => d.Price <= PriceFilterValue).ToList();
                }
                else if (PriceFilterOption == "Higher")
                {
                    Dishes = Dishes!.Where(d => d.Price >= PriceFilterValue).ToList();
                }
            }

            if (!string.IsNullOrEmpty(IngredientFilterOption) && IngredientFilterValues.Any())
            {
                if (IngredientFilterOption == "Include")
                {
                    Dishes = Dishes!.Where(d => IngredientFilterValues.All(id =>
                        Dish_Ingredients!.Any(di => di.DishId == d.Id && di.IngredientId == id)
                    )).ToList();
                }
                else if (IngredientFilterOption == "Exclude")
                {
                    Dishes = Dishes!.Where(d => IngredientFilterValues.All(id =>
                        !Dish_Ingredients!.Any(di => di.DishId == d.Id && di.IngredientId == id)
                    )).ToList();
                }
            }
        }
    }
}
