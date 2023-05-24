using Microsoft.AspNetCore.Mvc;
using StudioTemplate.DAL;
using StudioTemplate.Models;

namespace StudioTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Team> teams = _context.Teams.ToList();
            return View(teams);
        }
    }
}
