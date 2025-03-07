using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Model;

namespace Restaurant.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationContext _context;

        public RegisterModel(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public string? Name { get; set; }
        [BindProperty]
        public string? Address { get; set; }
        [BindProperty]
        public string? City { get; set; }
        [BindProperty]
        public string? Email { get; set; }
        [BindProperty]
        public string? Password { get; set; }
        [BindProperty]
        public string? RepeatPassword { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                var invalidFields = ModelState
                    .Where(kvp => kvp.Value!.Errors.Count > 0)
                    .Select(kvp => kvp.Key);

                TempData["Error"] = "Invalid fields: " + string.Join(", ", invalidFields);
                return Page();
            }
            if (_context.Users!.Any(u => u.Name!.Equals(Name)))
            {
                TempData["Error"] = "User with this name already exists!";
                return Page();
            }
            if (Password != RepeatPassword)
            {
                TempData["Error"] = "Passwords must match!";
                return Page();
            }
            _context.Users!.Add(CreateNewUser(Name, Address, City, Email, Password));
            _context.SaveChanges();
            HttpContext.Session.SetObject("User", User);
            return RedirectToPage("../Index");
        }

        public static User CreateNewUser(string? name, string? address, string? city, string? email, string? password)
        {
            return new User
            {
                Name = name,
                Address = address,
                City = city,
                Email = email,
                Password = password,
                RegistrationDate = DateTime.Now,
                Role = 0
            };
        }
    }
}
