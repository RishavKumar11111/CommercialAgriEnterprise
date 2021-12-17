using CommercialAgriEnterprise.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CommercialAgriEnterprise.Controllers
{
    [Authorize(Roles = "bank")]
    [NicSecurity]

    public class BankController : Controller
    {
        // GET: Bank
        public ActionResult Home()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                var k = Session["uid"].ToString();
                var bankid = new CommercialAgriEnterpriseEntities().BankMappings.Where(z => z.BankUserID == k).Select(z => z.BankCode).FirstOrDefault();
                int bnkid = Convert.ToInt32(bankid);
                ViewBag.BankName = new FARMERDBEntities().M_PDS_FARMERBANK.Where(z => z.intId == bnkid).Select(z => z.vchBankName).FirstOrDefault();
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

        public ActionResult MIS()
        {
            if (CheckPasswordChange.CheckPassword(Session["uid"].ToString()) == "OK")
            {
                ViewBag.BankName = Session["uid"].ToString().Substring(0, Session["uid"].ToString().Length - 5).ToUpper();
                return View();
            }
            else
            {
                Logoff();
                return RedirectToAction("Login", "Account");
            }
        }

        [NoDirectAccess]
        public JsonResult MisDetail()
        {
            return Json(new BankBAL().GetMisDetail(Session["uid"].ToString()), JsonRequestBehavior.AllowGet);
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
            ViewBag.BankName = Session["uid"].ToString().Substring(0, Session["uid"].ToString().Length - 5).ToUpper();
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
    }
}