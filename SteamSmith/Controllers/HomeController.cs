using System.Web.Mvc;

namespace SteamSmith.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}