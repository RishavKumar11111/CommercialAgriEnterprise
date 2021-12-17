using CommercialAgriEnterprise.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CommercialAgriEnterprise.Controllers
{
    [Authorize(Roles = "aao, afo, bvo, aho")]
    [NicSecurity]

    public class BLOController : Controller
    {
        private UserDetailsBLO userDetails { get; set; }
        private int blonoti { get; set; }

        public BLOController()
        {
            if (System.Web.HttpContext.Current.Session["uid"] != null)
            {
                var g = System.Web.HttpContext.Current.Session["uid"].ToString();
                userDetails = UserLogin.BLOLogin(g);
                if (g != null && userDetails != null)
                {
                    userDetails = UserLogin.BLOLogin(g);
                    blonoti = UserLogin.blonotification(g, userDetails.DepartmentCode);
                    ViewBag.notification = blonoti;
                    var usernm = g;
                    ViewBag.pendingrecord = new CommercialAgriEnterpriseEntities().Payments.Where(a => a.BeneficiaryDetail.RegistrationStatus == "completed" && a.BeneficiaryDetail.DPRStatu.Status == 1 && a.BeneficiaryDetail.BLORecord.BLOStatus == 0 && a.BeneficiaryDetail.PaymentStatus == "Success" && a.TransactionStatus == "Success" && (a.BeneficiaryDetail.IntegratedProjectLog.NewDeptCode == userDetails.DepartmentCode || a.BeneficiaryDetail.BeneficiaryProjectDetail.DepartmentCode == userDetails.DepartmentCode) && a.BeneficiaryDetail.BLORecord.BLOUserName == usernm && DbFunctions.AddDays(a.TransactionDate, 15) >= DateTime.Now).Select(a => new GetRecord { ReferenceNo = a.ReferenceNo, Date = DbFunctions.AddDays(a.TransactionDate, 15), Daysremaining = DbFunctions.DiffDays(DateTime.Now, DbFunctions.AddDays(a.TransactionDate, 15)) }).ToList();
                    if (userDetails == null)
                    {
                        try
                        {
                            if (userDetails.Department == null)
                            {
                                g = null;
                            }
                            else
                            {
                                g = null;
                            }
                        }
                        catch (NullReferenceException nre)
                        {
                            nre.Message.ToString();
                            g = null;
                        }
                    }
                }
            }
        }

        public ActionResult Dashboard()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                ViewBag.notification = blonoti;
                ViewBag.Department = userDetails.Department;
                ViewBag.BlockName = userDetails.BlockName.Trim();
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Home()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                ViewBag.notification = blonoti;
                ViewBag.Department = userDetails.Department;
                ViewBag.BlockName = userDetails.BlockName.Trim();
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        public void Logoff()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Session.Remove("uid");
            Session.Remove("NICSec_AuthToken");
            Response.Cookies.Add(new HttpCookie("Auth_Token", ""));
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult BloConfirmation()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                ViewBag.notification = blonoti;
                ViewBag.Department = userDetails.Department;
                ViewBag.BlockName = userDetails.BlockName.Trim();
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        [NoDirectAccess]
        public JsonResult BLoConfirmationlist(string startdate, string enddate, string fyr)
        {
            DateTime strtdt = Convert.ToDateTime(startdate.Substring(4, 11));
            string a = strtdt.ToString("MM/dd/yyyy");
            return Json(new BloConfirmationApprovalVM(startdate, enddate, Session["uid"].ToString(), fyr).getapprovelist, JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult CheckFeasibility(string refno)
        {
            return Json(new BLOBAL().CheckforFeasibilitystudy(refno), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(string refno)
        {
            CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();
            byte[] imageData = orm.BeneficiaryDocuments.Where(a => a.ReferenceNo == refno).Select(a => a.IdentityProof).FirstOrDefault();
            return File(imageData, "image/png");
        }

        [NoDirectAccess]
        public JsonResult GetUserList()
        {
            return Json(new BLOBAL().get_distid(Session["uid"].ToString(), userDetails.BlockID, userDetails.DepartmentCode).Select(a => new { a.refno, a.EMAILID, DOA = a.DOA == null ? "" : a.DOA.ToString().Substring(0, 10) }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public ActionResult DisplayPDF(string refno)
        {
            byte[] byteArray = new BLOBAL().GetPdfFromDB(refno);
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }

        [NoDirectAccess]
        public JsonResult DisplayAll(string refno)
        {
            Session["refno"] = refno;
            return Json(new RegistrationBAL().GetApplicantAcknowledgement(refno), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult Alllandrecord(string refno)
        {
            return Json(new RegistrationBAL().Alllandrecord(refno), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetRegdDetails(string refno)
        {
            return Json(new RegistrationBAL().GetApplicantAcknowledgement(refno), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult AllGroupMembers(string refno)
        {
            return Json(new RegistrationBAL().AllGroupMembers(refno), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetIProjects(string referenceNo)
        {
            return Json(new RegistrationBAL().GetIProjects(referenceNo), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetDetails(string referenceNo)
        {
            Session["refno"] = referenceNo;
            return Json(new GetBeneficiaryDetailVM() { ReferenceNo = referenceNo }.Data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult BLOFeasible(BLORecordValidation x)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (x.ReferenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), Session["uid"].ToString(), Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BLORecord", "UPDATE", "NA", "POST");
                    return Json(new BLOBAL().BLOFeasible(x), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult BLONotFeasible(BLORecordValidation x)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (x.ReferenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), Session["uid"].ToString(), Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BLORecord", "UPDATE", "NA", "POST");
                    return Json(new BLOBAL().BLONotFeasible(x), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult GetFynYR()
        {
            return Json(new FynYearVM(Session["uid"].ToString()).FYR, JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetFeasibilityReport(string referenceNo)
        {
            Session["refno"] = referenceNo;
            return Json(new BLOBAL().GetFeasibilityReport(referenceNo), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetProfitabilityProjectionDetails(string referenceNo)
        {
            return Json(new DPRBAL().GetProfitabilityProjectionDetails(referenceNo).Select(z => new { z.ReferenceNo, z.Year, z.GrossRevenue, z.TotalExpenditure, z.GrossProfit, z.NetProfit }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetCashFlowStatementDetails(string referenceNo)
        {
            return Json(new DPRBAL().GetCashFlowStatementDetails(referenceNo).Select(z => new { z.ReferenceNo, z.Year, z.TotalCashInflow, z.TotalCashOutflow, z.NetCashInflow }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetProfitabilityProjectionData(string referenceNo, string year)
        {
            var k = new DPRBAL().GetProfitabilityProjectionData(referenceNo, year);
            return Json(new { k.Year, k.GrossRevenue, k.TotalExpenditure, k.GrossProfit, k.NetProfit }, JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetCashFlowStatementData(string referenceNo, string year)
        {
            var k = new DPRBAL().GetCashFlowStatementData(referenceNo, year);
            return Json(new { k.Year, k.TotalCashInflow, k.TotalCashOutflow, k.NetCashInflow }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateInvestmentLand(string referenceNo, CapitalInvestmentLand cil, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CapitalInvestmentLand/CapitalInvestment", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateInvestmentLand(referenceNo, cil, ci, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateInvestmentCivilConstruction(string referenceNo, CapitalInvestmentCivilConstruction cicc, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CapitalInvestmentCivilConstruction/CapitalInvestment", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateInvestmentCivilConstruction(referenceNo, cicc, ci, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateInvestmentWaterSupply(string referenceNo, CapitalInvestmentWaterSupply ciws, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CapitalInvestmentWaterSupply/CapitalInvestment", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateInvestmentWaterSupply(referenceNo, ciws, ci, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateInvestmentElectrification(string referenceNo, CapitalInvestmentElectrification cie, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CapitalInvestmentElectrification/CapitalInvestment", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateInvestmentElectrification(referenceNo, cie, ci, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateInvestmentPlantMachinery(string referenceNo, CapitalInvestmentPlantMachinery cipm, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CapitalInvestmentPlantMachinery/CapitalInvestment", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateInvestmentPlantMachinery(referenceNo, cipm, ci, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateInvestmentAnimalPlant(string referenceNo, CapitalInvestmentAnimalPlant ciap, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CapitalInvestmentAnimalPlant/CapitalInvestment", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateInvestmentAnimalPlant(referenceNo, ciap, ci, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateInvestmentMiscellaneous(string referenceNo, CapitalInvestmentMiscellaneou cim, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CapitalInvestmentMiscellaneous/CapitalInvestment", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateInvestmentMiscellaneous(referenceNo, cim, ci, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateRecurringExpenditure(string referenceNo, RecurringExpenditure re, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "RecurringExpenditure/CapitalInvestment", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateRecurringExpenditure(referenceNo, re, ci, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateMeansOfFinance(string referenceNo, MeansOfFinance mof)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "MeansOfFinance", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateMeansOfFinance(referenceNo, mof, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateKeyBusinessMatrix(string referenceNo, KeyBusinessMatrix kbm)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "KeyBusinessMatrix", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateKeyBusinessMatrix(referenceNo, kbm, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateProfitabilityProjection(string referenceNo, string year, ProfitabilityProjection lpp)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "ProfitabilityProjection", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateProfitabilityProjection(referenceNo, year, lpp, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateCashFlowStatement(string referenceNo, string year, CashFlowStatement lcfs)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CashFlowStatement", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateCashFlowStatement(referenceNo, year, lcfs, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        private string randnum()
        {
            string input = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@_*";
            Random randomclass = new Random();
            StringBuilder s = new StringBuilder();
            char oneChar;
            for (int i = 0; i < 32; i++)
            {
                oneChar = input[randomclass.Next(0, input.Length)];
                s.Append(oneChar);
            }
            return s.ToString();
        }

        public ActionResult ChangePassword()
        {
            DateTime o = DateTime.Now;
            ViewBag.text = o;
            HttpContext.Session.Add("T", o);
            HttpContext.Session.Add("Key", randnum());
            ViewBag.notification = blonoti;
            ViewBag.Department = userDetails.Department;
            ViewBag.BlockName = userDetails.BlockName.Trim();
            return View();
        }

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string id = Session["uid"].ToString();
            var user = await UserManager.FindByNameAsync(id);
            if (user == null)
            {
                ModelState.AddModelError("Msg", "Invalid");
                return View(model);
            }
            ApplicationDbContext x = new ApplicationDbContext();
            var pass = x.Users.Where(g => g.UserName == id).Select(g => g.PasswordHash).FirstOrDefault();
            var username = x.Users.Where(g => g.UserName == id).Select(g => g.UserName).FirstOrDefault();
            var hashedpassword = ConvertToSHA256(Session["Key"].ToString() + pass);
            var result = SignInManager.UserManager.PasswordHasher.VerifyHashedPassword(hashedpassword, model.OldPassword);
            if (result.ToString() == "Success")
            {
                var newpass = ConvertToSHA256(model.NewPassword);
                new CollectorBAL().UpdateCollectorPassword(user.UserName, newpass);
                HttpContext.Session.Remove("Key");
                HttpContext.Session.Add("Key", randnum());
                ModelState.AddModelError("Msg", "Password Changed succesfully.");
                CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();
                var chkpasswordstatusentry = orm.CheckPasswordStatus.Where(z => z.UserName == user.UserName).FirstOrDefault();
                if (chkpasswordstatusentry == null)
                {
                    CheckPasswordStatu chkpwd = new CheckPasswordStatu();
                    chkpwd.UserName = user.UserName;
                    chkpwd.PasswordStatus = true;
                    chkpwd.DateTime = DateTime.Now;
                    chkpwd.IPAddress = HttpContext.Request.UserHostAddress;
                    chkpwd.FinancialYear = GenerateReferencenumber.GetCurrentFinancialYear();
                    orm.CheckPasswordStatus.Add(chkpwd);
                    orm.SaveChanges();
                }
                return RedirectToAction("MessageChanged", "Account");
            }
            else
            {
                ModelState.AddModelError("Msg", "Curent Password is Wrong.");
            }
            return View();
        }

        private string ConvertToSHA256(string pass)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(pass);
            SHA256Managed sha256hasstring = new SHA256Managed();
            byte[] hash = sha256hasstring.ComputeHash(bytes);
            return ByteArrayToHexString(hash);
        }

        private static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult IntegratedProjectChangebyBLO(IntegratedProjectLog X)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), "ONE", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "IntegratedProjectLog", "INSERT", "NA", "POST");
                return Json(new BLOBAL().BloIntegrationInsert(X, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }

        [NoDirectAccess]
        public JsonResult BLODetail()
        {
            return Json(new BLOBAL().GetBLODetail(userDetails.BlockID, userDetails.DepartmentCode), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult MakeitFeasible(string refNo)
        {
            return Json(new BLOBAL().MakeitFeasible(refNo, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult BLODetail1(string refNo)
        {
            string blockCode = new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Where(z => z.ReferenceNo == refNo).Select(x => x.NICBlockCode).FirstOrDefault();
            int bCode = Convert.ToInt32(blockCode);
            return Json(new BLOBAL().GetBLODetail(bCode, userDetails.DepartmentCode), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetBank()
        {
            return Json(new RegistrationBAL().listBank().OrderBy(z => z.vchBankName).Select(x => new { x.intId, x.vchBankName }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetBranch(int bankCode)
        {
            return Json(new RegistrationBAL().listBranch(bankCode).OrderBy(z => z.vchBranchName).Select(x => new { x.intBranchId, x.vchBranchName, x.NEW_IFSC_CODE }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateProjectInformations(ProjectInformations x)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (x.ReferenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Registration Edit", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BLORegdLog", "UPDATE", "NA", "POST");
                    return Json(new BLOBAL().UpdateProjectInformations(x, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateApplicantDetails(ApplicantDetails x)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (x.ReferenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Registration Edit", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BLORegdLog", "UPDATE", "NA", "POST");
                    return Json(new BLOBAL().UpdateApplicantDetails(x, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateApplicantAddressDetails(ApplicantAddressDetails x)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (x.ReferenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Registration Edit", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BLORegdLog", "UPDATE", "NA", "POST");
                    return Json(new BLOBAL().UpdateApplicantAddressDetails(x, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateFeasibility(FeasibilityReport x)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (x.ReferenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Feasibility Edit", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "FeasibilityReport/BLOFeasibilityLog", "UPDATE", "NA", "POST");
                    return Json(new BLOBAL().UpdateFeasibility(x, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateGradCert(string refno, GraduationDocument GraduationCertificate)
        {
            if (GraduationCertificate.CastGraduationCertificate != null)
            {
                if ((GraduationCertificate.CastGraduationCertificate.Length / 1024) > 950 || MimeCheck.getMimeFromFile(GraduationCertificate.CastGraduationCertificate) != "application/pdf")
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (refno == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "New Registration", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BeneFiciaryDetail", "UPDATE", "NA", "POST");
                    return Json(new RegistrationBAL().UpdateGradCert(refno, GraduationCertificate, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateBankCL(string refno, BankConsent BankCL)
        {
            if (BankCL.BankConsentLetter != null)
            {
                if ((BankCL.BankConsentLetter.Length / 1024) > 950 || MimeCheck.getMimeFromFile(BankCL.BankConsentLetter) != "application/pdf")
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (refno == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "New Registration", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BeneFiciaryDetail", "UPDATE", "NA", "POST");
                    return Json(new RegistrationBAL().UpdateBankCL(refno, BankCL, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateLandDoc1(string refno, PDFFilesUpload1 pdfland)
        {
            if (pdfland.LandPdf1 != null)
            {
                if ((pdfland.LandPdf1.Length / 1024) > 950 || MimeCheck.getMimeFromFile(pdfland.LandPdf1) != "application/pdf")
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (refno == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "New Registration", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BeneFiciaryDetail", "UPDATE", "NA", "POST");
                    return Json(new RegistrationBAL().UpdateLandDoc1(refno, pdfland, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateLandDoc2(string refno, PDFFilesUpload2 pdflandrec)
        {
            if (pdflandrec.LandPdf2 != null)
            {
                if ((pdflandrec.LandPdf2.Length / 1024) > 950 || MimeCheck.getMimeFromFile(pdflandrec.LandPdf2) != "application/pdf")
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (refno == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "New Registration", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BeneFiciaryDetail", "UPDATE", "NA", "POST");
                    return Json(new RegistrationBAL().UpdateLandDoc2(refno, pdflandrec, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult Chk_Emaiid(string EmailID)
        {
            return Json(new RegistrationBAL().Chk_Emailid(EmailID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateIDProof(string referenceNo, IDProof identityProof)
        {
            if (identityProof.IdentityProof != null)
            {
                if ((identityProof.IdentityProof.Length / 1024) > 400 || MimeCheck.getMimeFromFile(identityProof.IdentityProof) != "application/pdf")
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old Registration", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BeneFiciaryDetail", "UPDATE", "NA", "POST");
                    return Json(new RegistrationBAL().UpdateIDProof(referenceNo, identityProof, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BLOAction()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                ViewBag.notification = blonoti;
                ViewBag.Department = userDetails.Department;
                ViewBag.BlockName = userDetails.BlockName.Trim();
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        [NoDirectAccess]
        public JsonResult GetBloAcionList()
        {
            return Json(new BLOBAL().getbloactionlist(Session["uid"].ToString(), userDetails.BlockID, userDetails.DepartmentCode).Select(a => new { a.refno, a.statusBlo, a.farmerid }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult viewPTCCGAAD(string refno, string filetype)
        {
            byte[] byteArray = new RegistrationBAL().GetPTCCGAAD(refno, filetype);
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }

        public ActionResult ViewBankCL(string refno, string filetype)
        {
            byte[] byteArray = new RegistrationBAL().GetBankCL(refno, filetype);
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }

        public ActionResult viewCISBankCC(string refno, string filetype)
        {
            byte[] byteArray = new RegistrationBAL().GetBankCC(refno, filetype);
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }

        public ActionResult DisplayPDFDocument(string refno, string filetype)
        {
            byte[] byteArray = new RegistrationBAL().GetPdfFromDB(refno, filetype);
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }

        public ActionResult DisplayIDProof(string refno, string filetype)
        {
            byte[] byteArray = new RegistrationBAL().GetIdentityProof(refno, filetype);
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }

        [NoDirectAccess]
        public JsonResult GetDocumentfiles(string refno)
        {
            var jsonResult = Json(new RegistrationBAL().GetDocumentfiles(refno), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [NoDirectAccess]
        public JsonResult GetGroupTypes()
        {
            return Json(new RegistrationBAL().GetGroupTypes().OrderBy(a => a.Name).Select(a => new { a.Code, a.Name }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult BlonotfeasibleReason(string ReferenceNo)
        {
            return Json(new DNOBAL().BlonotfeasibleReason(ReferenceNo), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFeasibilitytoConcernedBLO()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                ViewBag.notification = blonoti;
                ViewBag.Department = userDetails.Department;
                ViewBag.BlockName = userDetails.BlockName.Trim();
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        [NoDirectAccess]
        public JsonResult GetFeasibilityReportBLO()
        {
            return Json(new BLOBAL().GetFeasibilityReportBLO(Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult CheckFeasibilityPending(string refNo)
        {
            return Json(new BLOBAL().CheckFeasibilityPending(userDetails.BlockID, refNo), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetDeptOfficer(string referenceNo)
        {
            return Json(new BLOBAL().GetDeptOfficer(referenceNo).Select(x => x.BLOUserName.Substring(0, 3).ToUpper()).Distinct(), JsonRequestBehavior.AllowGet);
        }
    }

    public class GetRecord
    {
        public string ReferenceNo { get; set; }
        public DateTime? Date { get; set; }
        public int? Daysremaining { get; set; }
    }
}