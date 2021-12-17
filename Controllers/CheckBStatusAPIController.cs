using CommercialAgriEnterprise.Models;
using System.Web.Mvc;

namespace CommercialAgriEnterprise.Controllers
{
    [AllowAnonymous]
    [ValidateAntiForgeryTokenOnAllPosts]

    public class CheckBStatusAPIController : Controller
    {
        public ActionResult CheckBeneficiaryStatus()
        {
            return View();
        }

        public JsonResult CheckBStatus(string referenceNo, string voterIDNo, string aadhaarCardNo)
        {
            if (referenceNo != null && voterIDNo != null && aadhaarCardNo != null)
            {
                Session["ReferenceNo"] = referenceNo;
                ViewBag.RefNo = referenceNo;
                return Json(new GoAheadBAL().CheckBeneficiaryStatus(referenceNo, voterIDNo, aadhaarCardNo), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ChkBeneficiaryStatus1(string ReferenceNo)
        {
            return Json(new CheckBeneficiaryStatusVM(ReferenceNo).GetALLStatus, JsonRequestBehavior.AllowGet);
        }
    }
}