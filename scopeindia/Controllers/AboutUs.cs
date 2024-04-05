using Microsoft.AspNetCore.Mvc;

namespace scopeindia.Controllers
{
    public class AboutUs : Controller
    {
        public IActionResult  About()
        {
            return View();
        }
    }
}
