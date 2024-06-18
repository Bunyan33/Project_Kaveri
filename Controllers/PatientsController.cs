using Microsoft.AspNetCore.Mvc;

namespace Project_Kaveri.Controllers
{
    public class PatientsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
