using CommercialAgriEnterprise.Models;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CommercialAgriEnterprise.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult PrePayment()
        {
            return View();
        }

        [NoDirectAccess]
        public JsonResult CheckReferenceNoStatus(string referenceNo, string voterIDNo, string aadhaarCardNo)
        {
            if (referenceNo != null && voterIDNo != null && aadhaarCardNo != null)
            {
                Session["ReferenceNo"] = referenceNo;
                return Json(new PaymentBAL().CheckReferenceNoStatus(referenceNo, voterIDNo, aadhaarCardNo), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult PaymentDetail(string referenceNo)
        {
            if (referenceNo != null && referenceNo == Session["ReferenceNo"].ToString())
            {
                return Json(new PaymentBAL().GetPaymentDetail(referenceNo), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult RequestCaptcha(string captcha, string referenceNo)
        {
            if (captcha == null || captcha != Session["Captcha"].ToString())
            {
                Session.Remove("Captcha");
                return Json("Invalid Captcha", JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (referenceNo != null && referenceNo == Session["ReferenceNo"].ToString())
                {
                    var s = new CommercialAgriEnterpriseEntities().BeneficiaryProjectDetails.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
                    string returnMsg = new RegistrationBAL().CheckTarget(s.ProjectTypeCode, s.BeneficiaryDetail.FarmerID);
                    if (returnMsg == "OK")
                    {
                        Session.Remove("Captcha");
                        return Json(GenerateRandomNo(), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (returnMsg == "Not OK")
                        {
                            return Json("No more projects can be alloted as the Target limit is reached.", JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json("The target value has not been set. Please try after sometime.", JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else
                {
                    Session.Remove("Captcha");
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult PRequest(string referenceNo, string randomNo)
        {
            if (randomNo == Session["RandomKey"].ToString() && referenceNo == Session["ReferenceNo"].ToString())
            {
                Session.Remove("RandomKey");
                ViewBag.ReferenceNo = referenceNo;
                return View();
            }
            else
            {
                Session.Remove("RandomKey");
                return null;
            }
        }

        [NoDirectAccess]
        public JsonResult TransactionDetail(string referenceNo)
        {
            if (referenceNo == Session["ReferenceNo"].ToString())
            {
                string payNowURL = PayNow(referenceNo);
                var k = new { t = new PaymentBAL().GetTransactionDetail(referenceNo), payNowURL };
                Session.Remove("ReferenceNo");
                return Json(k, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public string PayNow(string referenceNo)
        {
            if (referenceNo == Session["ReferenceNo"].ToString())
            {
                return new EasyPay(referenceNo, 10000, "http://apicol.nic.in/Payment/PResponse", Request.UserHostAddress.ToString()).url;
            }
            else
            {
                return null;
            }
        }

        public ActionResult PResponse()
        {
            ViewBag.EncryptedCode = EasyPay.GenerateSHA512String(Request.Params["ID"] + "|" + Request.Params["Response Code"] + "|" + Request.Params["Unique Ref Number"] + "|" + Request.Params["Service Tax Amount"] + "|" + Request.Params["Processing Fee Amount"] + "|" + Request.Params["Total Amount"] + "|" + Request.Params["Transaction Amount"] + "|" + Request.Params["Transaction Date"] + "|" + Request.Params["Interchange Value"] + "|" + Request.Params["TDR"] + "|" + Request.Params["Payment Mode"] + "|" + Request.Params["Submerchantid"] + "|" + Request.Params["referenceno"] + "|" + Request.Params["TPS"] + "|" + EasyPay.key);
            ViewBag.Match = Request.Params["RS"].ToString().Equals(ViewBag.EncryptedCode);
            if (ViewBag.Match == true)
            {
                //ViewBag.ResponseCode = Request.Params["Response Code"];
                //ViewBag.UniqueRefNumber = Request.Params["Unique Ref Number"];
                //ViewBag.ServiceTaxAmount = Request.Params["Service Tax Amount"];
                //ViewBag.ProcessingFeeAmount = Request.Params["Processing Fee Amount"];
                //ViewBag.TotalAmount = Request.Params["Total Amount"];
                //ViewBag.TransactionAmount = Request.Params["Transaction Amount"];
                //ViewBag.TransactionDate = Request.Params["Transaction Date"];
                //ViewBag.InterchangeValue = Request.Params["Interchange Value"];
                //ViewBag.TDR = Request.Params["TDR"];
                //ViewBag.PaymentMode = Request.Params["Payment Mode"];
                //ViewBag.SubMerchantID = Request.Params["Submerchantid"];
                //ViewBag.ReferenceNo = Request.Params["referenceno"];
                //ViewBag.TPS = Request.Params["TPS"];
                //ViewBag.ID = Request.Params["ID"];
                //ViewBag.RS = Request.Params["RS"];

                new PaymentBAL().BankResponseDetail(Request.Params["referenceno"], Request.Params["Unique Ref Number"], Request.Params["Response Code"], Request.Params["Transaction Date"], Request.Params["Service Tax Amount"], Request.Params["Processing Fee Amount"], Request.Params["Total Amount"], Request.Params["Transaction Amount"], Request.Params["Interchange Value"], Request.Params["TDR"], Request.Params["Payment Mode"], Request.Params["TPS"]);
                ViewBag.ReferenceNo = Request.Params["referenceno"];
                TransactionReceipt(Request.Params["referenceno"]);
            }
            else
            {
                return RedirectToAction("Error400", "Error");
            }
            Session.Clear();
            Session.RemoveAll();
            return View();
        }

        [NoDirectAccess]
        public JsonResult ReceiptDetails(string referenceNo)
        {
            return Json(new PaymentBAL().GetReceiptDetails(referenceNo), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult TransactionReceipt(string transactionID)
        {
            return Json(new PaymentBAL().GetTransactionReceipt(transactionID), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public string GenerateRandomNo()
        {
            DateTime o = DateTime.Now;
            ViewBag.text = o;
            string input = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@_*";
            Random randomClass = new Random();
            StringBuilder s = new StringBuilder();
            char oneChar;
            for (int i = 0; i < 32; i++)
            {
                oneChar = input[randomClass.Next(0, input.Length)];
                s.Append(oneChar);
            }
            HttpContext.Session.Add("T", o);
            HttpContext.Session.Add("RandomKey", s);
            string randomKey = s.ToString();
            return randomKey;
        }

        [AllowAnonymous]
        public ActionResult CaptchaImage()
        {
            bool noisy = false;
            var rand = new Random((int)DateTime.Now.Ticks);
            int a = rand.Next(10, 99);
            int b = rand.Next(0, 9);
            var captcha = string.Format("{0} + {1} = ?", a, b);
            Session["Captcha"] = a + b;
            FileContentResult img = null;
            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 30))
            using (var gfx = Graphics.FromImage(bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));
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
                gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Gray, 2, 3);
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = File(mem.GetBuffer(), "image/Jpeg");
            }
            return img;
        }
    }
}