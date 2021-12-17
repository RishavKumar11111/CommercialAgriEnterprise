using CommercialAgriEnterprise.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using static CommercialAgriEnterprise.Models.Crypt;

namespace CommercialAgriEnterprise.Controllers
{
    public class RegisterController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public RegisterController()
        {
        }

        public RegisterController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        public List<RegisterDetails> Register(RegisterDetails obj)
        {
            List<RegisterDetails> Registration = new List<RegisterDetails>();
            BussinessAril bal = new BussinessAril();
            bool Authenticate = false;
            try
            {
                string apikey = bal.ApikeyGenerate(obj.Mobile_No);
                if (obj != null)
                {
                    using (var con = new CommercialAgriEnterpriseEntities())
                    {
                        List<Userid> checkuserid = (from s in con.View_UserDetails where s.MobileNo == obj.Mobile_No && s.Utype == obj.Usertype select new Userid { user_id = s.UserName, BlockName = s.BlockName }).ToList<Userid>();
                        if (checkuserid != null)
                        {
                            foreach (var item in checkuserid)
                            {
                                Authenticate = this.checkcredential(item.user_id, obj.Passwd);
                                if (Authenticate)
                                {
                                    break;
                                }
                            }
                            if (Authenticate == true)
                            {
                                string Apikey = bal.ApikeyGenerate(obj.Mobile_No);
                                foreach (var itm in checkuserid)
                                {
                                    var aconn = new CommercialAgriEnterpriseEntities();
                                    MobileReg checkuser = (from a in aconn.MobileRegs where a.UserName == itm.user_id select a).FirstOrDefault();
                                    if (checkuser != null)
                                    {
                                        if (checkuser.IMEI == obj.IMEI && checkuser.SIMNo == obj.SIM_SerialNo)
                                        {
                                            checkuser.Api_key = Apikey;
                                            checkuser.reg_date = DateTime.Now;
                                            aconn.SaveChanges();
                                            Registration.Add(new RegisterDetails() { APIKey = Apikey, UserID = itm.user_id, Userids = checkuserid, msg = "Successfully Registered." });
                                        }
                                        else
                                        {
                                            Registration.Add(new RegisterDetails() { msg = "You Are already Register in Server." });
                                            return Registration;
                                        }
                                    }
                                    else
                                    {
                                        MobileReg rb = new MobileReg();
                                        rb.UserName = itm.user_id;
                                        rb.MobileNo = obj.Mobile_No;
                                        rb.IMEI = obj.IMEI;
                                        rb.SIMNo = obj.SIM_SerialNo;
                                        rb.Api_key = Apikey;
                                        rb.reg_date = DateTime.Now;
                                        aconn.MobileRegs.Add(rb);
                                        aconn.SaveChanges();
                                        // list.Add(new JAL() { APIKey = Apikey, UserID = itm.UserID });
                                        Registration.Add(new RegisterDetails() { APIKey = Apikey, UserID = itm.user_id, Userids = checkuserid, msg = "Successfully Registered." });
                                    }
                                }
                                return Registration;
                            }
                            else
                            {
                                Registration.Add(new RegisterDetails() { msg = "Wrong Mobile number or Password." });
                                return Registration;
                            }
                        }
                        else
                        {
                            Registration.Add(new RegisterDetails() { msg = "Wrong Mobile number or Password." });
                            return Registration;
                        }
                    }
                }
                else
                {
                    Registration.Add(new RegisterDetails() { msg = "Blank data can't be allowed." });
                    return Registration;
                }
            }
            catch (Exception ex)
            {
                Registration.Add(new RegisterDetails() { msg = "Oops! Something Error happen." });
                ex.ToString();
                return Registration;
            }
        }

        public List<ReferenceNo> GetRegerenceNo(string Userid, string pwd, string Apikey)
        {
            List<ReferenceNo> Registration = new List<ReferenceNo>();
            try
            {
                using (var con = new CommercialAgriEnterpriseEntities())
                {
                    var apikey = (from s in con.MobileRegs where s.UserName == Userid select s.Api_key).SingleOrDefault();
                    if (apikey != null)
                    {
                        if (apikey == Apikey)
                        {
                            bool Authenticate = this.checkcredential(Userid, pwd);
                            if (Authenticate)
                            {
                                Registration = (from s in con.BLORecords where s.BLOUserName == Userid && s.MobileUploadStatus == 0 && s.BLOStatus == 0 && s.PaymentStatus == true && s.DPRStatus == true && s.RegdStatus == true select new ReferenceNo { user_id = s.BLOUserName, Rno = s.ReferenceNo, msg = "" }).ToList<ReferenceNo>();
                                return Registration;
                            }
                            else
                            {
                                Registration.Add(new ReferenceNo() { msg = "Wrong Password." });
                                return Registration;
                            }
                        }
                        else
                        {
                            Registration.Add(new ReferenceNo() { msg = "Wrong Apikey." });
                            return Registration;
                        }
                    }
                    else
                    {
                        Registration.Add(new ReferenceNo() { msg = "Check your Mobile." });
                        return Registration;
                    }
                }
            }
            catch (Exception)
            {
                Registration.Add(new ReferenceNo() { msg = "Oops! Something Error happen." });
                return Registration;
            }
        }
    }
}