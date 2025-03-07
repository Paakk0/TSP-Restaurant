using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Model;

namespace Restaurant.Pages.Administration
{
    public class Edit_UserModel : PageModel
    {
        private readonly ApplicationContext _context;

        [BindProperty]
        public new User? User { get; set; }

        public Edit_UserModel(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id, string task)
        {
            if (task == "employee")
            {
                var employee = _context.Employees!.FirstOrDefault(e => e.Id == id);
                _context.Employees!.Remove(employee!);
                _context.SaveChanges();
                return RedirectToPage("./AdminPanel");
            }
            User = _context.Users!.FirstOrDefault(u => u.Id == id);
            if (task == "delete")
            {
                _context.Users!.Remove(User!);
                _context.SaveChanges();
                return RedirectToPage("./AdminPanel");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            ModelState.Remove("User.Password");
            if (!ModelState.IsValid)
            {
                var invalidFields = ModelState
                    .Where(kvp => kvp.Value!.Errors.Count > 0)
                    .Select(kvp => kvp.Key);

                TempData["Error"] = "Invalid fields: " + string.Join(", ", invalidFields);

                return Page();
            }

            var existingUser = _context.Users!.FirstOrDefault(u => u.Id == User!.Id);
            if (existingUser == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToPage("AdminPanel");
            }

            existingUser.Name = User!.Name;
            existingUser.Email = User.Email;
            existingUser.Address = User.Address;
            existingUser.City = User.City;
            existingUser.Role = User.Role;

            _context.SaveChanges();
            return RedirectToPage("AdminPanel");
        }
    }
}
