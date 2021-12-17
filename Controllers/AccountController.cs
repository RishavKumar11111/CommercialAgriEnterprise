using CommercialAgriEnterprise.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CommercialAgriEnterprise.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

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

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            DateTime o = DateTime.Now;
            ViewBag.text = o;
            string input = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@_*";
            Random randomclass = new Random();
            StringBuilder s = new StringBuilder();
            char oneChar;
            for (int i = 0; i < 32; i++)
            {
                oneChar = input[randomclass.Next(0, input.Length)];
                s.Append(oneChar);
            }
            HttpContext.Session.Add("T", o);
            HttpContext.Session.Add("Key", s);
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel data)
        {
            if (data.Captcha == null || data.Captcha != Session["Captcha"].ToString())
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                ModelState.AddModelError("Msg", "Invalid Captcha");
                return View(data);
            }
            if (!ModelState.IsValid || data.Key != Session["Key"].ToString())
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                ModelState.AddModelError("Msg", "Invalid Login Credentials");
                return View(data);
            }
            else
            {
                Session.Remove("Captcha");
                LoginViewModel model = new LoginViewModel { UserName = data._email, Password = data._em, Key = data.Key };
                ApplicationDbContext x = new ApplicationDbContext();
                var pass = x.Users.Where(g => g.Email == model.UserName).Select(g => g.PasswordHash).FirstOrDefault();
                var hashedpassword = ConvertToSHA256(Session["Key"].ToString() + pass);
                var result = SignInManager.UserManager.PasswordHasher.VerifyHashedPassword(hashedpassword, model.Password);
                var username = x.Users.Where(g => g.Email == model.UserName).Select(g => g.UserName).FirstOrDefault();
                if (username != null)
                {
                    var k = UserManager.FindByName(username);
                    if (k != null)
                    {
                        if (k.AccessFailedCount == 3)
                        {
                            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                            ModelState.AddModelError("Msg", "Your account has been locked !!! Contact ADMIN");
                            return View(data);
                        }
                        else
                        {
                            if (result.ToString() == "Success")
                            {
                                SignInManager.PasswordSignIn(username, pass, false, shouldLockout: true);
                                string guid = Guid.NewGuid().ToString();
                                Session["uid"] = username;
                                Session["NICSec_AuthToken"] = guid;
                                HttpCookie nicseccokie = new HttpCookie("NICSec_AuthToken", guid.ToString());
                                nicseccokie.HttpOnly = true;
                                Response.Cookies.Add(nicseccokie);
                                var RolesForUser = UserManager.GetRoles(x.Users.Where(g => g.UserName == username).Select(g => g.Id).FirstOrDefault()).FirstOrDefault();
                                if (RolesForUser == "dno")
                                {
                                    var chkpasswordstatus = new CommercialAgriEnterpriseEntities().CheckPasswordStatus.Where(z => z.UserName == username && z.PasswordStatus == true).FirstOrDefault();
                                    if (chkpasswordstatus != null)
                                    {
                                        k.AccessFailedCount = 0;
                                        UserManager.Update(k);
                                        AuditLog.Log(Request.UserHostAddress.ToString(), username, Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "LOGIN", "SUCCESS", "NA", "DNO");
                                        return RedirectToAction("Home", "DNO");
                                    }
                                    else
                                    {
                                        AuditLog.Log(Request.UserHostAddress.ToString(), username, Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "LOGIN", "SUCCESS", "NA", "DNO");
                                        return RedirectToAction("ChangePassword", "DNO");
                                    }
                                }
                                else if (RolesForUser == "aho" || RolesForUser == "bvo" || RolesForUser == "afo" || RolesForUser == "aao")
                                {
                                    var chkpasswordstatus = new CommercialAgriEnterpriseEntities().CheckPasswordStatus.Where(z => z.UserName == username && z.PasswordStatus == true).FirstOrDefault();
                                    if (chkpasswordstatus != null)
                                    {
                                        k.AccessFailedCount = 0;
                                        UserManager.Update(k);
                                        CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();
                                        var m = Session["uid"].ToString();
                                        var g = orm.BLOBlockMappings.Where(z => z.UserID == m).FirstOrDefault();
                                        if (g != null)
                                        {
                                            AuditLog.Log(Request.UserHostAddress.ToString(), model.UserName, Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "LOGIN", "SUCCESS", "NA", RolesForUser);
                                            return RedirectToAction("Home", "BLO");
                                        }
                                        else
                                        {
                                            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                                            ModelState.AddModelError("Msg", "Your User ID is not mapped.");
                                            return View(data);
                                        }
                                    }
                                    else
                                    {
                                        CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();
                                        var m = Session["uid"].ToString();
                                        var g = orm.BLOBlockMappings.Where(z => z.UserID == m).FirstOrDefault();
                                        if (g != null)
                                        {
                                            AuditLog.Log(Request.UserHostAddress.ToString(), model.UserName, Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "LOGIN", "SUCCESS", "NA", RolesForUser);
                                            return RedirectToAction("ChangePassword", "BLO");
                                        }
                                        else
                                        {
                                            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                                            ModelState.AddModelError("Msg", "Your User ID is not mapped.");
                                            return View(data);
                                        }
                                    }
                                }
                                else if (RolesForUser == "collector")
                                {
                                    var chkpasswordstatus = new CommercialAgriEnterpriseEntities().CheckPasswordStatus.Where(z => z.UserName == username && z.PasswordStatus == true).FirstOrDefault();
                                    if (chkpasswordstatus != null)
                                    {
                                        k.AccessFailedCount = 0;
                                        UserManager.Update(k);
                                        AuditLog.Log(Request.UserHostAddress.ToString(), model.UserName, Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "LOGIN", "SUCCESS", "NA", "collector");
                                        return RedirectToAction("Home", "Collector");
                                    }
                                    else
                                    {
                                        AuditLog.Log(Request.UserHostAddress.ToString(), model.UserName, Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "LOGIN", "SUCCESS", "NA", "collector");
                                        return RedirectToAction("ChangePassword", "Collector");
                                    }
                                }
                                else if (RolesForUser == "bank")
                                {
                                    var chkpasswordstatus = new CommercialAgriEnterpriseEntities().CheckPasswordStatus.Where(z => z.UserName == username && z.PasswordStatus == true).FirstOrDefault();
                                    if (chkpasswordstatus != null)
                                    {
                                        k.AccessFailedCount = 0;
                                        UserManager.Update(k);
                                        AuditLog.Log(Request.UserHostAddress.ToString(), model.UserName, Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "LOGIN", "SUCCESS", "NA", "bank");
                                        return RedirectToAction("Home", "Bank");
                                    }
                                    else
                                    {
                                        AuditLog.Log(Request.UserHostAddress.ToString(), model.UserName, Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "LOGIN", "SUCCESS", "NA", "bank");
                                        return RedirectToAction("ChangePassword", "Bank");
                                    }

                                }
                                else if (RolesForUser == "admin")
                                {
                                    var chkpasswordstatus = new CommercialAgriEnterpriseEntities().CheckPasswordStatus.Where(z => z.UserName == username && z.PasswordStatus == true).FirstOrDefault();
                                    if (chkpasswordstatus != null)
                                    {
                                        k.AccessFailedCount = 0;
                                        UserManager.Update(k);
                                        AuditLog.Log(Request.UserHostAddress.ToString(), model.UserName, Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "LOGIN", "SUCCESS", "NA", "admin");
                                        return RedirectToAction("Home", "Admin");
                                    }
                                    else
                                    {
                                        AuditLog.Log(Request.UserHostAddress.ToString(), model.UserName, Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "LOGIN", "SUCCESS", "NA", "admin");
                                        return RedirectToAction("ChangePassword", "Admin");
                                    }
                                }
                                else
                                {
                                    AuditLog.Log(Request.UserHostAddress.ToString(), model.UserName, Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "LOGIN", "FAILED", model.UserName + " , " + model.Password, "NA");
                                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                                    return RedirectToAction("Login", "Account");
                                }
                            }
                            else
                            {
                                var user = x.Users.Where(g => g.UserName == username).SingleOrDefault();
                                if (k != null)
                                {
                                    k.AccessFailedCount += 1;
                                    new CollectorBAL().UpdateFailedCount(k.Email, k.AccessFailedCount);
                                    // UserManager.SetLockoutEnabled(k.Id, true);
                                    AuditLog.Log(Request.UserHostAddress.ToString(), model.UserName, Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "LOGIN", "FAILED", model.UserName + " , " + model.Password, "NA");
                                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                                    ModelState.AddModelError("Msg", "Invalid Login Credentials");
                                    return View(data);
                                }
                                else
                                {
                                    AuditLog.Log(Request.UserHostAddress.ToString(), model.UserName, Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "LOGIN", "FAILED", model.UserName + " , " + model.Password, "NA");
                                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                                    ModelState.AddModelError("Msg", "Invalid Login Credentials");
                                    return View(data);
                                }
                            }
                        }
                    }
                    else
                    {
                        AuditLog.Log(Request.UserHostAddress.ToString(), model.UserName, Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "LOGIN", "FAILED", model.UserName + " , " + model.Password, "NA");
                        AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        ModelState.AddModelError("Msg", "Invalid User ID");
                        return View(data);
                    }
                }
                else
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), model.UserName, Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "LOGIN", "FAILED", model.UserName + " , " + model.Password, "NA");
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    ModelState.AddModelError("Msg", "Invalid User ID");
                    return View(data);
                }
            }
        }

        //
        // GET: /Account/Register
        // [AllowAnonymous]
        [NonAction]
        private ActionResult Register()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");
            return View();
        }

        //
        // POST: /Account/Register
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        [NonAction]
        private async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await this.UserManager.AddToRoleAsync(user.Id, model.UserRoles);
                    return RedirectToAction("Login", "Account");
                }
                AddErrors(result);
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //[AllowAnonymous]
        [NonAction]
        public async Task<ActionResult> CreateLoginIDs()
        {
            foreach (var j in new CommercialAgriEnterpriseEntities().LGDBlocks.ToList())
            {
                var p = new RegisterViewModel { UserName = "_" + j.BlockCode, Email = "_" + j.BlockCode + "@gmail.com", Password = "Test@1234", ConfirmPassword = "Test@1234", UserRoles = "" };
                var user = new ApplicationUser { UserName = p.UserName, Email = p.Email };
                var result = await UserManager.CreateAsync(user, p.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, p.UserRoles);
                }
            }
            return View();
        }

        //
        // GET: /Account/ConfirmEmail
        [NonAction]
        private async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [NonAction]
        private ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        [NonAction]
        private async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult MessageChanged()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Session.Remove("uid");
            Session.Remove("NICSec_AuthToken");
            Response.Cookies.Add(new HttpCookie("Auth_Token", ""));
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return View();
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        // [AllowAnonymous]
        [NonAction]
        private ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        // [AllowAnonymous]
        [NonAction]
        private ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        // [HttpPost]
        // [AllowAnonymous]
        // [ValidateAntiForgeryToken]
        [NonAction]
        private async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        // [AllowAnonymous]
        [NonAction]
        public ActionResult ResetPasswordConfirmation()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Session.Remove("uid");
            Session.Remove("NICSec_AuthToken");
            Response.Cookies.Add(new HttpCookie("Auth_Token", ""));
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return View();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            Session.Remove("uid");
            Session.Remove("NICSec_AuthToken");
            Response.Cookies.Add(new HttpCookie("Auth_Token", ""));
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }

        internal ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Session.Remove("uid");
            Session.Remove("NICSec_AuthToken");
            Response.Cookies.Add(new HttpCookie("Auth_Token", ""));
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Login", "Account");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        [AllowAnonymous]
        public void CreateRoles()
        {
            IdentityDbContext k = new IdentityDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(k));
            roleManager.Create(new IdentityRole("bank"));
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

        [AllowAnonymous]
        public ActionResult CaptchaImage()
        {
            // string prefix = "45";
            bool noisy = false;
            var rand = new Random((int)DateTime.Now.Ticks);
            // generate new question 
            int a = rand.Next(10, 99);
            int b = rand.Next(0, 9);
            var captcha = string.Format("{0} + {1} = ?", a, b);
            // store answer 
            Session["Captcha"] = a + b;
            // image stream 
            FileContentResult img = null;
            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 30))
            using (var gfx = Graphics.FromImage(bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));
                // add noise 
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)));
                        r = rand.Next(0, (130 / 3));
                        x = rand.Next(0, 130);
                        y = rand.Next(0, 30);
                        gfx.DrawEllipse(pen, x - r, y - r, r, r);
                    }
                }
                // add question 
                gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Gray, 2, 3);
                // render as Jpeg 
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = File(mem.GetBuffer(), "image/Jpeg");
            }
            return img;
        }
    }
}