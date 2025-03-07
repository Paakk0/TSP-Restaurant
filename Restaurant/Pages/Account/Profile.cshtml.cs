using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Restaurant.Model;

namespace Restaurant.Pages.Account
{
    public class ProfileModel : PageModel
    {
        private readonly ApplicationContext _context;

        public ProfileModel(ApplicationContext context)
        {
            _context = context;
        }

        public int Id { get; set; }
        public bool isEmployee { get; set; }

        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Role { get; set; }
        public DateTime? Date { get; set; }
        public IActionResult OnGet()
        {
            User? user = HttpContext.Session.GetObject<User>("User");
            Employee? employee = HttpContext.Session.GetObject<Employee>("Employee");
            if (user != null)
            {
                Name = user!.Name;
                Email = user.Email;
                Address = user.Address;
                City = user.City;
                Role = user.Role == 1 ? "Admin" : "User";
                Date = user.RegistrationDate;

                Id = user.Id;
                isEmployee = false;
            }
            else
            {
                Name = employee!.Name;
                Email = employee.Email;
                Role = "Employee";
                Date = employee.EmploymentDate;

                Id = employee.Id;
                isEmployee = true;
            }
            return Page();
        }
    }
}
