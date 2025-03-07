using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Model;

namespace Restaurant.Pages.Account
{
    public class Edit_ProfileModel : PageModel
    {
        private readonly ApplicationContext _context;

        public Edit_ProfileModel(ApplicationContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsEmployee { get; set; }

        private User? user { get; set; }
        private Employee? employee { get; set; }

        [BindProperty]
        public string? Name { get; set; }
        [BindProperty]
        public string? Email { get; set; }
        [BindProperty]
        public string? Address { get; set; }
        [BindProperty]
        public string? City { get; set; }

        public IActionResult OnGet(int id, bool isEmployee)
        {
            if (isEmployee)
            {
                employee = _context.Employees!.Find(id);
                Name = employee!.Name;
                Email = employee!.Email;
            }
            else
            {
                user = _context.Users!.Find(id);
                Name = user!.Name;
                Email = user!.Email;
                Address = user!.Address;
                City = user!.City;
            }
            IsEmployee = isEmployee;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!IsEmployee)
                return HandleUserPost();
            return HandlerEmployeePost();
        }

        private IActionResult HandleUserPost()
        {
            if (!ModelState.IsValid)
            {
                var invalidFields = ModelState
                    .Where(kvp => kvp.Value!.Errors.Count > 0)
                    .Select(kvp => kvp.Key);

                TempData["Error"] = "Invalid fields: " + string.Join(", ", invalidFields);
                return Page();
            }

            var oldUser = _context.Users!.Find(user!.Id);

            user = new User
            {
                Name = Name,
                Email = Email,
                Address = Address,
                City = City
            };

            _context.Entry(oldUser!).CurrentValues.SetValues(user);
            _context.SaveChanges();
            HttpContext.Session.SetObject("User", User);

            return RedirectToPage("./Profile");

        }

        private IActionResult HandlerEmployeePost()
        {
            if (!ModelState.IsValid)
            {
                var invalidFields = ModelState
                    .Where(kvp => kvp.Value!.Errors.Count > 0)
                    .Select(kvp => kvp.Key);

                TempData["Error"] = "Invalid fields: " + string.Join(", ", invalidFields);
                return Page();
            }

            var oldEmployee = _context.Users!.Find(employee!.Id);

            employee = new Employee
            {
                Name = Name,
                Email = Email
            };

            _context.Entry(oldEmployee!).CurrentValues.SetValues(employee);
            _context.SaveChanges();
            HttpContext.Session.SetObject("Employee", employee);

            return RedirectToPage("./Profile");

        }
    }
}
