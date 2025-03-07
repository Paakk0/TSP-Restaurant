using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Restaurant.Model;

namespace Restaurant.Pages.Administration
{
    public class Edit_DishModel : PageModel
    {
        public readonly ApplicationContext _context;
        public List<int>? ContainingIngredients { get; set; }

        [BindProperty]
        public Dish? Dish { get; set; }
        [BindProperty]
        public List<int>? SelectedIngredients { get; set; } = new List<int>();
        public Edit_DishModel(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int id, string task)
        {
            {
                Dish = _context.Dishes!.Find(id);

                if (Dish == null)
                {
                    TempData["Error"] = "Dish not found.";
                    return RedirectToPage("./AdminPanel");
                }


                if (task == "edit")
                {
                    ContainingIngredients = _context.Dish_Ingredients!
                        .Where(di => di.DishId == Dish.Id)
                        .Select(di => di.IngredientId)
                        .ToList();
                }
                else if (task == "delete")
                {
                    var item = _context.Dishes.Find(id);
                    if (item != null)
                        _context.Dishes.Remove(item);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("AdminPanel");
                }
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _context.Dishes!.Update(Dish!);
            if (SelectedIngredients == null || !SelectedIngredients.Any())
            {
                TempData["Error"] = "All dishes must contain ingredients!";
                return Page();
            }
            _context.Dish_Ingredients!.RemoveRange(_context.Dish_Ingredients
                .Where(di => di.DishId == Dish!.Id)
                .ToList());
            foreach (var id in SelectedIngredients)
            {
                _context.Dish_Ingredients.Add(new Dish_Ingredient()
                {
                    DishId = Dish!.Id,
                    IngredientId = id
                });
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("./AdminPanel");
        }
    }
}
