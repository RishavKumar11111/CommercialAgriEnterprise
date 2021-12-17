using CommercialAgriEnterprise.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using static CommercialAgriEnterprise.Models.Crypt;

namespace CommercialAgriEnterprise.Controllers
{
    public class FeasibilityController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public FeasibilityController()
        {
        }

        public FeasibilityController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
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
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public bool checkcredential(string userid, string pwd)
        {
            ClearPassword obj = new ClearPassword();
            ApplicationDbContext x = new ApplicationDbContext();
            var pass = x.Users.Where(g => g.UserName == userid).Select(g => g.PasswordHash).FirstOrDefault();
            // var hashedpassword = obj.ConvertToSHA256(pass);
            var result = SignInManager.UserManager.PasswordHasher.VerifyHashedPassword(pass, pwd);
            if (result.ToString() == "Success")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        public string Uploaddata()
        {
            string status = "0";
            try
            {
                HttpFileCollection hfc = HttpContext.Current.Request.Files;
                var referenceNo = HttpContext.Current.Request.Form["ReferenceNo"];
                var Lat = HttpContext.Current.Request.Form["latitude"];
                var Lng = HttpContext.Current.Request.Form["longitude"];
                var userid = HttpContext.Current.Request.Form["userid"];
                var pwd = HttpContext.Current.Request.Form["pass"];
                var rbDPR = Convert.ToBoolean(HttpContext.Current.Request.Form["dpr"]);
                var rbRoad = Convert.ToBoolean(HttpContext.Current.Request.Form["road"]);
                var txtDistance = Convert.ToDecimal(HttpContext.Current.Request.Form["distance"]);
                var rbElectrification = Convert.ToBoolean(HttpContext.Current.Request.Form["electrification"]);
                var rbPollution = Convert.ToBoolean(HttpContext.Current.Request.Form["pollution"]);
                var txtFinance = Convert.ToDecimal(HttpContext.Current.Request.Form["finance"]);
                var txtLoan = Convert.ToDecimal(HttpContext.Current.Request.Form["loan"]);
                var txtMargin = Convert.ToDecimal(HttpContext.Current.Request.Form["margin"]);
                var txtBank = Convert.ToBoolean(HttpContext.Current.Request.Form["bank"]);
                bool rescredential = this.checkcredential(userid, pwd);
                if (rescredential)
                {
                    for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                    {
                        HttpPostedFile hpf = hfc[iCnt];
                        if (hpf.ContentLength > 0)
                        {
                            byte[] ImageData = new byte[hpf.ContentLength];
                            string FileName = hpf.FileName;
                            // string userid = hpf.paramas;
                            byte[] imgBin = new byte[hpf.ContentLength];
                            hpf.InputStream.Read(imgBin, 0, (int)hpf.ContentLength);
                            using (var ctx = new CommercialAgriEnterpriseEntities())
                            {
                                using (var trans = ctx.Database.BeginTransaction())
                                {
                                    FeasibilityReport fr = new FeasibilityReport();
                                    fr.ReferenceNo = referenceNo;
                                    fr.Longitude = Lng;
                                    fr.Latitude = Lat;
                                    fr.Photo = imgBin;
                                    fr.ReferenceNo = referenceNo;
                                    fr.RoadConnectivity = rbRoad;
                                    fr.PreviousExperience = rbDPR;
                                    fr.DistanceFromVillage = txtDistance;
                                    fr.ElectrificationStatus = rbElectrification;
                                    fr.PollutionControlClearanceStatus = rbPollution;
                                    fr.SelfFinance = txtFinance;
                                    fr.UncensoredLoan = txtLoan;
                                    fr.MarginComponent = txtMargin;
                                    fr.BankConsentLetterStatus = txtBank;
                                    fr.UserName = userid;
                                    fr.Status = false;
                                    ctx.FeasibilityReports.Add(fr);
                                    ctx.SaveChanges();
                                    trans.Commit();
                                    status = referenceNo;
                                }
                            }
                        }
                    }
                }
                else
                {
                    status = "0";
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                status = "2";
            }
            return status;
        }
    }
}