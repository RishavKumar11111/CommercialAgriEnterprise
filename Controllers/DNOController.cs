using CommercialAgriEnterprise.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
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
    [Authorize(Roles = "dno")]
    [NicSecurity]

    public class DNOController : Controller
    {
        private UserDetailsDNO userDetails { get; set; }
        private int dnonoti { get; set; }

        public DNOController()
        {
            if (System.Web.HttpContext.Current.Session["uid"] != null)
            {
                userDetails = UserLogin.DNOLogin(System.Web.HttpContext.Current.Session["uid"].ToString());
                string DCode = Convert.ToString(userDetails.DistrictID);
                dnonoti = UserLogin.dnonotification(System.Web.HttpContext.Current.Session["uid"].ToString(), userDetails.DepartmentCode, DCode);
                ViewBag.notification = dnonoti;
                var usernm = System.Web.HttpContext.Current.Session["uid"].ToString();
                ViewBag.pendingrecord = new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Where(a => (a.BeneficiaryProjectDetail.DepartmentCode == userDetails.DepartmentCode || a.DDARecord.IntegratedFarmingStatus == 0) && (a.BLORecord.ReferenceNo == a.DNORecord.ReferenceNo || a.BLORecord.ReferenceNo == a.DDARecord.ReferenceNo) && (a.BLORecord.BLOStatus == 1 || a.BLORecord.BLOStatus == 2) && a.RegistrationStatus == "completed" && a.DPRStatu.Status == 1 && a.PaymentStatus == "Success" && (a.DNORecord.DNOStatus == 0 || a.DDARecord.DDAStatus == 0) && DbFunctions.AddDays(a.BLORecord.BLODate, 15) >= DateTime.Now && a.NICDistrictCode == DCode).Select(a => new GetRecord { ReferenceNo = a.ReferenceNo, Date = DbFunctions.AddDays(a.BLORecord.BLODate, 15), Daysremaining = DbFunctions.DiffDays(DateTime.Now, DbFunctions.AddDays(a.BLORecord.BLODate, 15)) }).ToList();

            }
        }

        public class GetRecord
        {
            public string ReferenceNo { get; set; }
            public DateTime? Date { get; set; }
            public int? Daysremaining { get; set; }
        }

        public ActionResult Dashboard()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                ViewBag.notification = dnonoti;
                ViewBag.Department = userDetails.Department;
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

        public ActionResult Home()
        {
            var k = Session["uid"].ToString();
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                ViewBag.notification = dnonoti;
                ViewBag.Department = userDetails.Department;
                ViewBag.DistrictName = userDetails.DistrictName;
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult DnoConfirmation()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                ViewBag.notification = dnonoti;
                ViewBag.Department = userDetails.Department;
                ViewBag.DistrictName = userDetails.DistrictName;
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult DnoEvaluation()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                ViewBag.notification = dnonoti;
                ViewBag.Department = userDetails.Department;
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
        public JsonResult DnoConfirmationlist(string startdate, string enddate, string username, string fyr)
        {
            return Json(new DnoConfirmationApprovalVM(startdate, enddate, Session["uid"].ToString(), fyr).getapprovelist, JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetApprovalList()
        {
            return Json(new DNOBAL().Approvallist(userDetails.DepartmentCode, userDetails.DistrictID).Select(a => new { a.refno, a.status, a.Approvestatus, a.statusApicol, a.Apicolstatus, a.DDASTATUS, a.statusDda, a.statusCollector, a.CollectorStatus, a.paymentStatus, a.statusBlo, a.BloStatus, a.BackToBLOStatus }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetRegdDetails(string refno)
        {
            return Json(new RegistrationBAL().GetApplicantAcknowledgement(refno), JsonRequestBehavior.AllowGet);
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

        public ActionResult DisplayPDF(string refno)
        {
            byte[] byteArray = new BLOBAL().GetPdfFromDB(refno);
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }

        [NoDirectAccess]
        public string Dept()
        {
            return userDetails.Department.Substring(0, 3).ToLower();
        }

        public ActionResult RegisterBLO()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                ViewBag.notification = dnonoti;
                ViewBag.Department = userDetails.Department;
                ViewBag.DistrictName = userDetails.DistrictName;
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult RecordsApproveReject()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                ViewBag.notification = dnonoti;
                ViewBag.Department = userDetails.Department;
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
        public JsonResult GetBlocks()
        {
            return Json(new DNOBAL().GetBlocks(userDetails.DistrictID, userDetails.DepartmentCode).Select(x => new { x.BlockCode, x.BlockName }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public string GetDepartment()
        {
            return userDetails.Department.Substring(0, 3).ToLower();
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult AddBLO(List<BLOBlockMapping> lbbm)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), Session["uid"].ToString(), Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BLOBlockMapping", "INSERT", "NA", "POST");
                return Json(new DNOBAL().AddBLO(userDetails.DepartmentCode, lbbm), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult GetRegisteredBLO()
        {
            return Json(new DNOBAL().GetRegisteredBLO(userDetails.DistrictID, userDetails.DepartmentCode).Select(x => new { x.UserID, BlockName = x.LGDBlock.BlockName.Trim(), DepartmentName = x.Department.Name }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult RemoveBLO(string userID, BLOBlockLog bbl)
        {
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), Session["uid"].ToString(), Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BLOBlockMapping/BLOBlockLog", "DELETE/INSERT", "NA", "POST");
                return Json(new DNOBAL().RemoveBLO(userID, bbl), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult GetUserDetails()
        {
            if (Convert.ToString(Session["NICSec_AuthToken"]) != "" && Convert.ToString(Session["NICSec_AuthToken"]) != null && Request.Cookies["NICSec_AuthToken"].Value != null && Request.Cookies["NICSec_AuthToken"].Value != "" && Session["uid"] != null && Convert.ToString(Session["NICSec_AuthToken"]) == Request.Cookies["NICSec_AuthToken"].Value)
            {
                string DCode = Convert.ToString(userDetails.DistrictID);
                return Json(new DDAUserDetailsVM(Session["uid"].ToString().Substring(9), DCode).userDetails, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }

        [NoDirectAccess]
        public JsonResult CheckDnolist(string refno)
        {
            return Json(new DNOBAL().CheckDnolist(refno), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult DNORecordSave(string refNo)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), Session["uid"].ToString(), Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "DNORecord", "UPDATE", "NA", "POST");
                return Json(new DNOBAL().DNORecordSave(refNo), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult DDAApprove(DDARecord dr, string referenceNo)
        {
            AuditLog.Log(Request.UserHostAddress.ToString(), Session["uid"].ToString(), Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "DDARecord", "INSERT", "NA", "POST");
            return Json(new DNOBAL().DDAApprove(dr, referenceNo), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult DDAReject(DDARecord myData)
        {
            AuditLog.Log(Request.UserHostAddress.ToString(), Session["uid"].ToString(), Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "DDARecord", "INSERT", "NA", "POST");
            return Json(new DNOBAL().DDAReject(myData), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetFYR()
        {
            return Json(new GetdnoFYRVM(Session["uid"].ToString()).getfyr, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Completion()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                ViewBag.notification = dnonoti;
                ViewBag.Department = userDetails.Department;
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
            var k = new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Where(z => z.ReferenceNo == refNo).FirstOrDefault();
            int bCode = Convert.ToInt32(k.NICBlockCode);
            if (k.BeneficiaryProjectDetail.ProjectTypeCode == "04P15")
            {
                return Json(new BLOBAL().GetBLODetail(bCode, k.IntegratedProjectLog.NewDeptCode), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new BLOBAL().GetBLODetail(bCode, userDetails.DepartmentCode), JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult BLODetailDDA(string refNo)
        {
            var k = new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Where(z => z.ReferenceNo == refNo).FirstOrDefault();
            int bCode = Convert.ToInt32(k.NICBlockCode);
            return Json(new BLOBAL().GetBLODetail(bCode, k.BeneficiaryProjectDetail.DepartmentCode), JsonRequestBehavior.AllowGet);
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
            ViewBag.notification = dnonoti;
            ViewBag.Department = userDetails.Department;
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
            var hashedpassword = ConvertToSHA256(Session["Key"].ToString() + pass);
            var result = SignInManager.UserManager.PasswordHasher.VerifyHashedPassword(hashedpassword, model.OldPassword);
            if (result.ToString() == "Success")
            {
                var newresult = await UserManager.ChangePasswordAsync(user.Id, pass, model.NewPassword);
                if (newresult.Succeeded)
                {
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
                    HttpContext.Session.Remove("Key");
                    HttpContext.Session.Add("Key", randnum());
                    AddErrors(newresult);
                }
            }
            else
            {
                ModelState.AddModelError("Msg", "Invalid Password");
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

        public ActionResult BLODetailEntry()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                ViewBag.notification = dnonoti;
                ViewBag.Department = userDetails.Department;
                ViewBag.DistrictName = userDetails.DistrictName;
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult DNODetailEntry()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                ViewBag.notification = dnonoti;
                ViewBag.Department = userDetails.Department;
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
        public JsonResult Block()
        {
            return Json(new DNOBAL().GetBlock(userDetails.DepartmentCode, userDetails.DistrictID).Select(x => new { x.BlockCode, BlockName = x.BlockName.Trim() }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetIntegratedFarming()
        {
            return Json(new DNOBAL().IntegratedFarmingList(userDetails.DistrictID).Select(z => new { z.refno, z.status, z.Approvestatus, z.statusApicol, z.Apicolstatus, z.DDASTATUS, z.statusDda, z.statusCollector, z.CollectorStatus, z.paymentStatus, z.integratedStatus, z.IntegratedFarmingStatus, z.BloStatus, z.BackToBLOStatus }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult SubmitBLORecords(BLODetailEntry x, BLOBlockMapping y)
        {
            if (x.Signature != null)
            {
                if ((x.Signature.Length / 1024) > 50 || MimeCheck.getMimeFromFile(x.Signature) != "image/pjpeg")
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), "BLO Details Entry", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BLODetailEntry", "INSERT", "NA", "POST");
                return Json(new DNOBAL().SubmitBLORecords(x, y, userDetails.DistrictID, userDetails.DepartmentCode), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateBLORecord(string blockCode, BLODetailEntry x)
        {
            if (x.Signature != null)
            {
                if ((x.Signature.Length / 1024) > 50 || MimeCheck.getMimeFromFile(x.Signature) != "image/pjpeg")
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), "BLO Details Entry", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BLODetailEntry", "INSERT", "NA", "POST");
                return Json(new DNOBAL().UpdateBLORecord(blockCode, x, userDetails.DistrictID, userDetails.DepartmentCode), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult GetBLORecords()
        {
            return Json(new BLODetailVM(userDetails.DepartmentCode, userDetails.DistrictID).GetBLODetails, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetBLOSignatures()
        {
            var jsonResult = Json(new DNOBAL().GetBLOSignatures(userDetails.DepartmentCode, userDetails.DistrictID), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [NoDirectAccess]
        public JsonResult GetBLORecordsbyBlock(string blockCode)
        {
            var k = new DNOBAL().GetBLORecordsbyBlock(blockCode, userDetails.DepartmentCode);
            return Json(new { k.LGDBlock.BlockName, k.Name, k.MobileNo, k.EmailID }, JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult BlonotfeasibleReason(string ReferenceNo)
        {
            return Json(new DNOBAL().BlonotfeasibleReason(ReferenceNo), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult SubmitDNORecords(DNODetailEntry x)
        {
            if ((x.Signature.Length / 1024) > 50 || MimeCheck.getMimeFromFile(x.Signature) != "image/pjpeg")
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), "DNO Details Entry", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "DNODetailEntry", "INSERT", "NA", "POST");
                return Json(new DNOBAL().SubmitDNORecords(x, userDetails.DistrictID, userDetails.DepartmentCode), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult GetDNORecords()
        {
            return Json(new DNODetailVM(userDetails.DepartmentCode, userDetails.DistrictID).GetDNODetails, JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult DNODetail()
        {
            return Json(new DNOBAL().GetDNODetail(userDetails.DistrictID, userDetails.DepartmentCode), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult DNONotRecommended(DNORecordValidation x)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), Session["uid"].ToString(), Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BLORecord", "UPDATE", "NA", "POST");
                return Json(new DNOBAL().DNONotRecommended(x, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult DDANotRecommended(DDARecordValidation x)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), Session["uid"].ToString(), Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BLORecord", "UPDATE", "NA", "POST");
                return Json(new DNOBAL().DDANotRecommended(x, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult DNORecommended(DNORecordValidation x)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), Session["uid"].ToString(), Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "DNORecord", "UPDATE", "NA", "POST");
                return Json(new DNOBAL().DNORecommended(x, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult DDARecommended(DDARecordValidation x)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), Session["uid"].ToString(), Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "DNORecord", "UPDATE", "NA", "POST");
                return Json(new DNOBAL().DDARecommended(x, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult DNOBackToBLO(DNORecordValidation x)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), Session["uid"].ToString(), Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "DNORecord", "UPDATE", "NA", "POST");
                return Json(new DNOBAL().DNOBackToBLO(x, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult DDABackToBLO(DDARecordValidation x)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), Session["uid"].ToString(), Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "DNORecord", "UPDATE", "NA", "POST");
                return Json(new DNOBAL().DDABackToBLO(x, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
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
        public JsonResult DNONotRecommendedReason(string ReferenceNo)
        {
            return Json(new CollectorBAL().DNONotRecommendedReason(ReferenceNo), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult DDARejectionReason(string ReferenceNo)
        {
            return Json(new CollectorBAL().DDARejectionReason(ReferenceNo), JsonRequestBehavior.AllowGet);
        }
    }
}