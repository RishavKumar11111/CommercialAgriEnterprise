using CommercialAgriEnterprise.Models;
using System.Web.Mvc;

namespace CommercialAgriEnterprise.Controllers
{
    [HandleError(View = "Error")]

    public class GoAheadController : Controller
    {
        // GET: GoAhead
        public ActionResult GenGoAhead()
        {
            return View();
        }

        [NoDirectAccess]
        public JsonResult CheckReferenceNoStatus(string referenceNo, string voterIDNo, string aadhaarCardNo)
        {
            if (referenceNo != null && voterIDNo != null && aadhaarCardNo != null)
            {
                Session["ReferenceNo"] = referenceNo;
                ViewBag.RefNo = referenceNo;
                return Json(new GoAheadBAL().CheckReferenceNoStatus(referenceNo, voterIDNo, aadhaarCardNo), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult GetGoAhead(string referenceNo, string farmerID)
        {
            if (referenceNo != null && referenceNo == Session["ReferenceNo"].ToString())
            {
                return Json(new GoAheadVM(referenceNo, farmerID) { ReferenceNo = referenceNo, FarmerID = farmerID }.GADetails, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CheckBeneficiaryStatus()
        {
            return View();
        }

        [NoDirectAccess]
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

        [NoDirectAccess]
        public JsonResult ChkBeneficiaryStatus1(string ReferenceNo)
        {
            return Json(new CheckBeneficiaryStatusVM(ReferenceNo).GetALLStatus, JsonRequestBehavior.AllowGet);
        }
    }
}