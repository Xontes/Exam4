using Exam4.DAL;
using Exam4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exam4.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<MChef> chefs = _context.mChefs.ToList();
            return View(chefs);
        }
    }
}
