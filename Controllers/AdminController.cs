using CommercialAgriEnterprise.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
    [Authorize(Roles = "admin")]
    [NicSecurity]

    public class AdminController : Controller
    {
        public ActionResult Home()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
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
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult DistWiseReport()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        [NoDirectAccess]
        public JsonResult GetDistWiseReport()
        {
            return Json(new DistWiseAdminReportVM().DistWiseReport.Select(a => new { a.DistName, a.GetFarmerDetails }), JsonRequestBehavior.AllowGet);
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

        public ActionResult DepartmentWiseReport()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        [NoDirectAccess]
        public JsonResult GetDepartmentWiseReport()
        {
            return Json(new DeptWiseAdminReportVM().GetAllDist.Select(a => new { a.DepartmentName, a.GetFarmerDetailsDeptwise }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Graph()
        {
            return View();
        }

        [NoDirectAccess]
        public JsonResult GetFarmerDetail()
        {
            return Json(new CommercialAgriEnterpriseEntities().PRCRCDCOUNT(), JsonRequestBehavior.AllowGet);
        }

        //public ActionResult BLODetailEntry()
        //{
        //    if (ChechkPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        Logoff();
        //        return RedirectToAction("Login", "Account");
        //    }
        //}

        //public ActionResult DNODetailEntry()
        //{
        //    if (ChechkPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        Logoff();
        //        return RedirectToAction("Login", "Account");
        //    }
        //}

        //public ActionResult CollectorDetailEntry()
        //{
        //    return View();
        //}

        public ActionResult LockUser()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
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

        public ActionResult ResetPassword(string unm)
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult PasswordReset()
        {
            DateTime o = DateTime.Now;
            ViewBag.text = o;
            HttpContext.Session.Add("T", o);
            HttpContext.Session.Add("Key", randnum());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (model.UserName.Substring(0, 3) == "aao" || model.UserName.Substring(0, 3) == "bvo" || model.UserName.Substring(0, 3) == "afo" || model.UserName.Substring(0, 3) == "aho")
            {
                model.UserName = model.UserName + "@gmail.com";
            }
            if (model.UserName.Substring(0, 9) == "collector")
            {
                model.UserName = model.UserName + "@gmail.com";
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            if (model.UserName.Substring(0, 3) == "aao" || model.UserName.Substring(0, 3) == "bvo" || model.UserName.Substring(0, 3) == "afo" || model.UserName.Substring(0, 3) == "aho")
            {
                model.UserName = model.UserName.Replace("@gmail.com", "");
            }
            else
            {
                if (model.UserName.Substring(0, 9) == "collector" && model.UserName.Length > 9)
                {
                    model.UserName = model.UserName.Replace("@gmail.com", "");
                }
            }
            var username = (dynamic)null;
            var user = (dynamic)null;
            if (model.UserName.Substring(0, 3) == "aao" || model.UserName.Substring(0, 3) == "bvo" || model.UserName.Substring(0, 3) == "afo" || model.UserName.Substring(0, 3) == "aho" || model.UserName.Substring(0, 9) == "collector")
            {
                ApplicationDbContext x = new ApplicationDbContext();
                username = x.Users.Where(g => g.Email == model.UserName).FirstOrDefault();
                username = await userManager.FindByEmailAsync(model.UserName);
                user = username;
            }
            else
            {
                username = await userManager.FindByEmailAsync(model.UserName);
                user = username;
            }
            if (user == null)
            {
                ModelState.AddModelError("Msg", "Invalid User ID");
                return View();
            }
            //var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            var pass = ConvertToSHA256(model.ConfirmPassword);
            new AdminBAL().ResetPassword(model.UserName, pass);
            ModelState.AddModelError("Msg", "password has been reset sucessfully of" + "  " + model.UserName);
            return View();
        }

        [NoDirectAccess]
        public JsonResult AuditTrail()
        {
            return Json(new AdminBAL().GetAuditTrail().Select(a => new { a.UserID, a.Url, a.IPAddress, DateTime = a.DateTime.ToString().Substring(0, 10), a.Action, a.Attack }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult Department()
        {
            return Json(new AdminBAL().GetDepartment().Select(x => new { x.Code, x.Name }).OrderBy(z => z.Name), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult District()
        {
            return Json(new AdminBAL().GetDistrict().Select(x => new { x.DistrictCode, x.DistrictName }).OrderBy(z => z.DistrictName), JsonRequestBehavior.AllowGet);
        }

        //[NoDirectAccess]
        //public JsonResult ExcludedDistrictByDept(string departmentCode)
        //{
        //    return Json(new AdminBAL().ExcludedDistrictByDept(departmentCode).Select(x => new { x.DistrictCode, x.DistrictName }).OrderBy(z => z.DistrictName), JsonRequestBehavior.AllowGet);
        //}

        //[NoDirectAccess]
        //public JsonResult EnteredDistrict(string departmentCode)
        //{
        //    return Json(new AdminBAL().GetEnteredDistrict(departmentCode).Select(x => new { x.DepartmentCode, DepartmentName = x.Department.Name, x.DistrictCode, DistrictName = x.LGDDistrict.DistrictName, x.Name, x.MobileNo }), JsonRequestBehavior.AllowGet);
        //}

        //[NoDirectAccess]
        //public JsonResult RecordbyDeptDist(string departmentCode, int districtCode)
        //{
        //    var k = new AdminBAL().GetRecordbyDeptDist(departmentCode, districtCode);
        //    return Json(new { k.DepartmentCode, DepartmentName = k.Department.Name, k.DistrictCode, DistrictName = k.LGDDistrict.DistrictName, k.Name, k.MobileNo }, JsonRequestBehavior.AllowGet);
        //}

        [NoDirectAccess]
        public JsonResult BlockbyDistrict(int districtCode, string departmentCode)
        {
            if (districtCode != 0 && departmentCode != null)
            {
                return Json(new AdminBAL().GetBlockbyDistrict(districtCode, departmentCode).Select(x => new { x.BlockCode, x.BlockName, x.DistrictCode }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryTokenOnAllPosts]
        //public JsonResult SubmitBLORecords(List<AdminBLODetailEntry> x)
        //{
        //    var errors = ModelState.Values.SelectMany(z => z.Errors);
        //    if (ModelState.IsValid)
        //    {
        //        AuditLog.Log(Request.UserHostAddress.ToString(), "SubmitBLORecords", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "AdminBLODetailEntry", "INSERT", "NA", "POST");
        //        return Json(new AdminBAL().SubmitBLORecords(x.Select(g => new AdminBLODetailEntry() { DepartmentCode = g.DepartmentCode, DistrictCode = g.DistrictCode, BlockCode = g.BlockCode, Name = g.Name, MobileNo = g.MobileNo, UserDateTime = DateTime.Now, IPAddress = HttpContext.Request.UserHostAddress, FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear() }).ToList()), JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryTokenOnAllPosts]
        //public JsonResult SubmitAllDNORecords(List<AdminDNODetailEntry> x)
        //{
        //    var errors = ModelState.Values.SelectMany(z => z.Errors);
        //    if (ModelState.IsValid)
        //    {
        //        AuditLog.Log(Request.UserHostAddress.ToString(), "SubmitAllDNORecords", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "AdminDNODetailEntry", "INSERT", "NA", "POST");
        //        return Json(new AdminBAL().SubmitAllDNORecords(x.Select(g => new AdminDNODetailEntry() { DepartmentCode = g.DepartmentCode, DistrictCode = g.DistrictCode, Name = g.Name, MobileNo = g.MobileNo, UserDateTime = DateTime.Now, IPAddress = HttpContext.Request.UserHostAddress, FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear() }).ToList()), JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[NoDirectAccess]
        //public JsonResult RecordbyDistDept(int districtCode, string departmentCode)
        //{
        //    if (districtCode != 0 && departmentCode != null)
        //    {
        //        return Json(new AdminBAL().GetRecordbyDistDept(districtCode, departmentCode).Select(x => new { x.BlockCode, BlockName = x.LGDBlock.BlockName.Trim(), x.DepartmentCode, DepartmentName = x.Department.Name, x.DistrictCode, DistrictName = x.LGDDistrict.DistrictName.Trim(), x.Name, x.MobileNo }), JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(null, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[NoDirectAccess]
        //public JsonResult RecordbyDeptDistBlock(string departmentCode, int districtCode, int blockCode)
        //{
        //    if (departmentCode != null && districtCode != 0 && blockCode != 0)
        //    {
        //        var k = new AdminBAL().GetRecordbyDeptDistBlock(departmentCode, districtCode, blockCode);
        //        return Json(new { k.BlockCode, BlockName = k.LGDBlock.BlockName.Trim(), k.DepartmentCode, DepartmentName = k.Department.Name, k.DistrictCode, DistrictName = k.LGDDistrict.DistrictName.Trim(), k.Name, k.MobileNo }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(null, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryTokenOnAllPosts]
        //public JsonResult UpdateBLORecord(string departmentCode, int districtCode, int blockCode, AdminBLODetailModify myData)
        //{
        //    var errors = ModelState.Values.SelectMany(z => z.Errors);
        //    if (ModelState.IsValid)
        //    {
        //        AuditLog.Log(Request.UserHostAddress.ToString(), "UpdateBLORecord", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "AdminBLODetailEntry", "UPDATE", "NA", "POST");
        //        return Json(new AdminBAL().UpdateBLORecord(departmentCode, districtCode, blockCode, myData), JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryTokenOnAllPosts]
        //public JsonResult SubmitDNORecords(AdminDNODetailEntry x)
        //{
        //    var errors = ModelState.Values.SelectMany(z => z.Errors);
        //    if (ModelState.IsValid)
        //    {
        //        AuditLog.Log(Request.UserHostAddress.ToString(), "SubmitDNORecords", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "AdminDNODetailEntry", "INSERT", "NA", "POST");
        //        return Json(new AdminBAL().SubmitDNORecords(x), JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryTokenOnAllPosts]
        //public JsonResult UpdateDNORecord(string departmentCode, int districtCode, AdminDNODetailModify myData)
        //{
        //    var errors = ModelState.Values.SelectMany(z => z.Errors);
        //    if (ModelState.IsValid)
        //    {
        //        AuditLog.Log(Request.UserHostAddress.ToString(), "UpdateDNORecord", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "AdminDNODetailEntry", "UPDATE", "NA", "POST");
        //        return Json(new AdminBAL().UpdateDNORecord(departmentCode, districtCode, myData), JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public ActionResult Target()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        [NoDirectAccess]
        public JsonResult GetTargetProject()
        {
            return Json(new AdminBAL().GetTargetProject().Select(z => new { z.Code, z.Name }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetBindDist()
        {
            return Json(new AdminBAL().BindTargetDist().Select(z => new { z.DistrictCode, z.DistrictName }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult FetchTargetRecord(string PCODE)
        {
            var k = new BindAllDistTargetWiseVM(PCODE);
            return Json(new BindAllDistTargetWiseVM(PCODE), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult FindTarget(string PCODE)
        {
            return Json(new AdminBAL().FindTarget(PCODE), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult SubmitAllTarget(List<Target> AllTarget)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), "Submit Target", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "Target", "INSERT", "NA", "POST");
                return Json(new AdminBAL().SubmitAllTarget(AllTarget), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateTargetListALL(List<Target> AllTargetList, string FYR)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), "Update Target", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "Target", "UPDATE", "NA", "POST");
                return Json(new AdminBAL().Updatetargetlist(AllTargetList, FYR), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult GetBLOLockUser()
        {
            var k = new UnlockUserALL().GetlockuserBLO;
            if (k.Contains(null))
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(k.Select(a => new { a.UserName, a.BloUsernm, a.EmailID }), JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult GetDNOLockUser()
        {
            var k = new UnlockUserALL().GetlockuserDNO;
            if (k.Contains(null))
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(k.Select(a => new { a.UserName, a.DnoUsernm, a.EmailID }), JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult GetCollectorLockUser()
        {
            var k = new UnlockUserALL().GetLockuserCOLLECTOR;
            if (k.Contains(null))
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(k.Select(a => new { a.Username }), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult ChkBlouseridforUnlock(List<LockListUser> userlst)
        {
            return Json(new AdminBAL().UnlockBlolist(userlst), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult ChkDnouseridforUnlock(List<LockListUser> userlst)
        {
            return Json(new AdminBAL().UnlockDnolist(userlst), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult ChkCollectoruseridforUnlock(List<LockListUser> userlst)
        {
            return Json(new AdminBAL().UnlockCollectorlist(userlst), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DNODetailsReport()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        [NoDirectAccess]
        public JsonResult GetDNOEnteredDetails()
        {
            return Json(new AdminBAL().DNOList().Select(z => new { z.DistrictName, z.DepartmentName, z.Name, z.MobileNo }).OrderBy(z => z.DistrictName), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BLODetailsReport()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        [NoDirectAccess]
        public JsonResult GetBlockDetailEntry(string DistCode)
        {
            return Json(new BLODetailsEntryVM(DistCode).getAlldist.Select(z => new { z.getAllBlockList, z.DistName, z.DepartmentName }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BlockwiseBLORegistration()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        [NoDirectAccess]
        public JsonResult BlockwiseBLORegdCount()
        {
            return Json(new AdminBAL().BlockwiseBLORegdCount(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BLODetailsPayment()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        [NoDirectAccess]
        public JsonResult GetBLODetailsPayment()
        {
            return Json(new BLODetailsPaymentVM().GetRecords.Select(x => new { x.DistrictName, x.GetBeneficiaryBLO }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTargetDetails()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        [NoDirectAccess]
        public JsonResult GetAllTargetList(string P_CODE, string F_YEAR)
        {
            var k = new AdminBAL().GetAllTarget(P_CODE, F_YEAR);
            return Json(new AdminBAL().GetAllTarget(P_CODE, F_YEAR), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DNODetailsModify()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        [NoDirectAccess]
        public JsonResult GetDNODetails(string deptCode, int distCode)
        {
            return Json(new DNODetailVM(deptCode, distCode).GetDNODetails, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryTokenOnAllPosts]
        public JsonResult UpdateDNODetails(DNODetailEntry x, string deptCode, int distCode)
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
                AuditLog.Log(Request.UserHostAddress.ToString(), "BLO Details Entry", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "DNODetailEntry", "INSERT", "NA", "POST");
                return Json(new AdminBAL().UpdateDNODetails(x, deptCode, distCode, Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
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
    }
}