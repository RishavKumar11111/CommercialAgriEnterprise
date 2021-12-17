using CommercialAgriEnterprise.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CommercialAgriEnterprise.Controllers
{
    [Authorize(Roles = "collector")]
    [NicSecurity]

    public class CollectorController : Controller
    {
        private UserDetailsCollector userDetails { get; set; }
        private int collectornotice { get; set; }

        public CollectorController()
        {
            if (System.Web.HttpContext.Current.Session["uid"] != null)
            {
                var usernm = System.Web.HttpContext.Current.Session["uid"].ToString();
                userDetails = UserLogin.CollectorLogin(usernm);
                string DCode = Convert.ToString(userDetails.DistrictCode);
                collectornotice = UserLogin.collectornotification(usernm, DCode);
                ViewBag.notification = collectornotice;
                ViewBag.pendingrecord = new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Where(z => (z.DNORecord.ReferenceNo == z.CollectorRecord.ReferenceNo || z.DDARecord.ReferenceNo == z.CollectorRecord.ReferenceNo) && (z.BLORecord.BLOStatus == 1 || z.BLORecord.BLOStatus == 2) && (z.DNORecord.DNOStatus == 1 || z.DNORecord.DNOStatus == 2 || z.DDARecord.IntegratedFarmingStatus == 1 || z.DDARecord.IntegratedFarmingStatus == 2) && z.CollectorRecord.CollectorStatus == 0 && z.RegistrationStatus == "completed" && z.DPRStatu.Status == 1 && z.PaymentStatus == "Success" && z.NICDistrictCode == DCode).Select(a => a.ReferenceNo).ToList();
            }
        }

        public ActionResult Home()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                ViewBag.notification = collectornotice;
                ViewBag.DistrictName = userDetails.DistrictName;
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

        public ActionResult Dashboard()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                ViewBag.notification = collectornotice;
                ViewBag.DistrictName = userDetails.DistrictName;
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Records()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                ViewBag.notification = collectornotice;
                ViewBag.DistrictName = userDetails.DistrictName;
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        [NoDirectAccess]
        public JsonResult GetUserDetails()
        {
            string DCode = Convert.ToString(userDetails.DistrictCode);
            return Json(new CollectorUserDetailsVM(userDetails.DistrictName, DCode).UserDetails, JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult BLONotFeasibleReason(string ReferenceNo)
        {
            return Json(new CollectorBAL().BLONotFeasibleReason(ReferenceNo), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult DNONotRecommendedReason(string ReferenceNo)
        {
            return Json(new CollectorBAL().DNONotRecommendedReason(ReferenceNo), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult AddRefNoDate(List<CollectorRecord> lcr, string dateOfMeeting)
        {
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), Session["uid"].ToString(), Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CollectorRecord", "UPDATE", "NA", "POST");
                return Json(new CollectorBAL().AddRefNoDateTime(lcr, dateOfMeeting), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateRecords(int lotNo, string dateOfMeeting, string timeOfMeeting)
        {
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), Session["uid"].ToString(), Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CollectorRecord", "UPDATE", "NA", "POST");
                return Json(new CollectorBAL().UpdateRecords(lotNo, dateOfMeeting, timeOfMeeting), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult CompareMeetingDate(string meetingDate)
        {
            return Json(new CollectorBAL().CompareMeetingDate(meetingDate), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult CompareMeetingDateUpdate(string meetingDate, int lotNo)
        {
            return Json(new CollectorBAL().CompareMeetingDateUpdate(meetingDate, lotNo), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetLotNo()
        {
            return Json(new CollectorBAL().GetLotNo(Session["uid"].ToString()).OrderBy(z => z.LotNo).Select(x => x.LotNo).Distinct(), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetRecordsByLotNo(int lotNo)
        {
            string DCode = Convert.ToString(userDetails.DistrictCode);
            return Json(new CollectorDDAApprovalListVM(userDetails.DistrictName, DCode, lotNo).GetRecordsByLotNo, JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetDetails(string referenceNo)
        {
            return Json(new GetBeneficiaryDetailVM() { ReferenceNo = referenceNo }.Data, JsonRequestBehavior.AllowGet);
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
        public JsonResult GetFeasibilityReport(string referenceNo)
        {
            return Json(new BLOBAL().GetFeasibilityReport(referenceNo), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult BLODetail(string refNo)
        {
            string blockCode = new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Where(z => z.ReferenceNo == refNo).Select(x => x.NICBlockCode).FirstOrDefault();
            string deptCode = new CommercialAgriEnterpriseEntities().BeneficiaryProjectDetails.Where(z => z.ReferenceNo == refNo).Select(x => x.DepartmentCode).FirstOrDefault();
            int bCode = Convert.ToInt32(blockCode);
            return Json(new BLOBAL().GetBLODetail(bCode, deptCode), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult Alllandrecord(string refno)
        {
            return Json(new RegistrationBAL().Alllandrecord(refno), JsonRequestBehavior.AllowGet);
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
        public JsonResult GetRegdDetails(string refno)
        {
            return Json(new RegistrationBAL().GetApplicantAcknowledgement(refno), JsonRequestBehavior.AllowGet);
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
        public JsonResult DDARejectionReason(string ReferenceNo)
        {
            return Json(new CollectorBAL().DDARejectionReason(ReferenceNo), JsonRequestBehavior.AllowGet);
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
            ViewBag.notification = collectornotice;
            ViewBag.DistrictName = userDetails.DistrictName;
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
            ba.ToList().ForEach(z => hex.AppendFormat("{0:x2}", z));
            return hex.ToString();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}