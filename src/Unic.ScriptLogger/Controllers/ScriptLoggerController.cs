namespace Unic.ScriptLogger.Controllers
{
    using System.Web.Mvc;

    public class ScriptLoggerController : Controller
    {
        public ActionResult Index()
        {
            return Content("this is the log", "text/plain");
        }
    }
}