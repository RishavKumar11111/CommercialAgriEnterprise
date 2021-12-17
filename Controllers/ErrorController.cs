using System.Web.Mvc;

namespace CommercialAgriEnterprise.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Error400()
        {
            Session["nkey"] = "error";
            return View();
        }

        public ActionResult Error401()
        {
            Session["nkey"] = "error";
            return View();
        }

        public ActionResult Error403()
        {
            Session["nkey"] = "error";
            return View();
        }

        public ActionResult Error404()
        {
            Session["nkey"] = "error";
            return View();
        }

        public ActionResult Error500()
        {
            Session["nkey"] = "error";
            return View();
        }
    }
}