using System.Web.Mvc;

namespace UniversityApplicationMVC.Controllers.SystemGenerate
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Our University Application";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
