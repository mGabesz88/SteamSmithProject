using System.Web.Mvc;

namespace SteamSmith.Controllers
{
    public class FaqController : Controller
    {
        public ActionResult Questions()
        {
            return View();
        }

    }
}