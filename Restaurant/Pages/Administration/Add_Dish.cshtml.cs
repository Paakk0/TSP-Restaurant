using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Restaurant.Model;

namespace Restaurant.Pages.Administration
{
    public class Add_DishModel : PageModel
    {
        public readonly ApplicationContext _context;

        public Add_DishModel(ApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Dish? Dish { get; set; }

        [BindProperty]
        public List<int> SelectedIngredients { get; set; } = new List<int>();
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Dish.DishType");
            ModelState.Remove("Dish.Price");

            if (!SelectedIngredients.Any())
            {
                TempData["Error"] = "Dish must contain atleast 1 ingredient!";
                return Page();
            }

            if (!ModelState.IsValid)
            {
                var invalidFields = ModelState
                    .Where(kvp => kvp.Value!.Errors.Count > 0)
                    .Select(kvp => kvp.Key);

                TempData["Error"] = "Invalid fields: " + string.Join(", ", invalidFields);
                return Page();
            }

            if (_context.Dishes!.Any(d => d.Name!.Equals(Dish!.Name)))
            {
                TempData["Error"] = $"Dish '{Dish!.Name}' already exists!";
                return Page();
            }

            _context.Dishes!.Add(Dish!);
            await _context.SaveChangesAsync();

            foreach (var ingredientId in SelectedIngredients)
            {
                _context.Dish_Ingredients!.Add(new Dish_Ingredient
                {
                    DishId = Dish!.Id,
                    IngredientId = ingredientId
                });
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("./AdminPanel");
        }
    }
}
