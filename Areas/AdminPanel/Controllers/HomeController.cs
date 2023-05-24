using Microsoft.AspNetCore.Mvc;

namespace StudioTemplate.Areas.AdminPanel.Controllers
{
    public class HomeController : Controller
    {
        [Area("AdminPanel")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
