using CommercialAgriEnterprise.Models;
using System.Linq;
using System.Web.Mvc;

namespace CommercialAgriEnterprise.Controllers
{
    [ValidateAntiForgeryTokenOnAllPosts]

    public class BeneficiaryCompletionController : Controller
    {
        public ActionResult BCompletion()
        {
            return View();
        }

        [NoDirectAccess]
        public JsonResult GetGOAheadnumber(string Aadharno, string FarmerID)
        {
            return Json(new BeneficiaryCompletionBAL().getgoahead(Aadharno, FarmerID), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetBeneficiarydetailsfromFarmerID(string FarmerID)
        {
            return Json(new GetBeneficiaryDetailsByfidVM(FarmerID).GETBENEFECIARYBYID, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertCompletionBeneficiary(BeneficiaryCompletion BC)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                return Json(new BeneficiaryCompletionBAL().BeneficiaryCompletionInsert(BC), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        //[NoDirectAccess]
        //public JsonResult CompletionDetailsreport(string Goaheadnum)
        //{
        //    return Json(new BeneficiaryCompletionBAL().GetCompletionDetails(Goaheadnum), JsonRequestBehavior.AllowGet);
        //}
    }
}