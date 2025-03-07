using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Restaurant.Model;

namespace Restaurant.Pages.Account
{
    public class Log_outModel : PageModel
    {
        private readonly ApplicationContext _context;

        public Log_outModel(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetObject<User>("User") != null)
                HttpContext.Session.Remove("User");
            else
            {
                var employee = HttpContext.Session.GetObject<Employee>("Employee");
                _context.Employees!.FirstOrDefault(e => e.Id == employee!.Id)!.IsOnline = false;
                _context.SaveChanges();
                HttpContext.Session.Remove("Employee");
            }
            return RedirectToPage("../Index");
        }
    }
}
