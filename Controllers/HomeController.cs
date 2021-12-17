using CommercialAgriEnterprise.Models;
using System.Web.Mvc;

namespace CommercialAgriEnterprise.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult ProcessFlow()
        {
            return View();
        }

        public ActionResult Video()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult RTI()
        {
            return View();
        }

        public ActionResult Manual()
        {
            return View();
        }

        public ActionResult Graph1()
        {
            return View();
        }

        public ActionResult Graph2()
        {
            return View();
        }

        public ActionResult Graph3()
        {
            return View();
        }

        [NoDirectAccess]
        public JsonResult GetFarmerDetailsDistWise()
        {
            return Json(new CommercialAgriEnterpriseEntities().FarmerDetailsDistWiseProcedure(), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult FarmerDetailsRegdDPRDistWise()
        {
            return Json(new CommercialAgriEnterpriseEntities().FarmerDetailsRegdDPRDistWiseProcedure(), JsonRequestBehavior.AllowGet);
        }
    }
}