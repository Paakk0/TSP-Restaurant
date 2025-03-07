using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Model;

namespace Restaurant.Pages.Account
{
    public class Log_inModel : PageModel
    {
        private readonly ApplicationContext _context;

        public Log_inModel(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public string Name { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

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

            var user = _context.Users!.SingleOrDefault(u => u.Name == Name && u.Password == Password);
            if (user != null)
            {
                HttpContext.Session.SetObject("User", user);
                return RedirectToPage("../Index");
            }

            var employee = _context.Employees!.SingleOrDefault(e => e.Name == Name && e.Password == Password);
            if (employee != null)
            {
                HttpContext.Session.SetObject("Employee", employee);
                _context.Entry(employee).CurrentValues.SetValues(employee.IsOnline = true);
                _context.SaveChanges();
                return RedirectToPage("../Index");
            }
            TempData["Error"] = "Account not found!";
            return Page();
        }
    }
}
