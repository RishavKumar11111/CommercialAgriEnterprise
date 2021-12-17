using CommercialAgriEnterprise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CommercialAgriEnterprise.Controllers
{
    [AllowAnonymous]
    [ValidateAntiForgeryTokenOnAllPosts]

    public class DPRController : Controller
    {
        public ActionResult DPR()
        {
            return View();
        }

        [NoDirectAccess]
        public JsonResult GetBeneficiaryDetails(string referenceNo, string farmerID, string aadharNo)
        {
            var k = new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Where(a => a.ReferenceNo == referenceNo).FirstOrDefault();
            var l = new FARMERDBEntities().M_FARMER_REGISTRATION.Where(a => a.NICFARMERID == farmerID).FirstOrDefault();
            if (k != null)
            {
                if (l != null && l.NICFARMERID == farmerID)
                {
                    if (new DPRBAL().ConvertToSHA256(l.VCHAADHARNO) == aadharNo)
                    {
                        return Json(new DPRBAL().GetBeneficiaryDetail(referenceNo, farmerID, aadharNo), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Invalid Aadhaar No.", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("Invalid Farmer ID.", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("Invalid Reference No.", JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult GetBeneficiaryDetailMasked(string referenceNo, string farmerID, string aadharNo)
        {
            return Json(new DPRBAL().GetBeneficiaryDetailMasked(referenceNo, farmerID, aadharNo), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetDetails(string referenceNo)
        {
            return Json(new GetBeneficiaryDetailVM() { ReferenceNo = referenceNo }.Data, JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public Data Data()
        {
            return new GetBeneficiaryDetailVM { }.Data;
        }

        [NoDirectAccess]
        public JsonResult CheckReferenceNo(string referenceNo, string farmerID, string aadhaarNo)
        {
            return Json(new DPRBAL().CheckReferenceNo(referenceNo, farmerID, aadhaarNo), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetProfitabilityProjectionDetails(string referenceNo)
        {
            Session["refno"] = referenceNo;
            return Json(new DPRBAL().GetProfitabilityProjectionDetails(referenceNo).Select(z => new { z.ReferenceNo, z.Year, z.GrossRevenue, z.TotalExpenditure, z.GrossProfit, z.NetProfit }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetProfitabilityProjectionData(string referenceNo, string year)
        {
            var k = new DPRBAL().GetProfitabilityProjectionData(referenceNo, year);
            return Json(new { k.Year, k.GrossRevenue, k.TotalExpenditure, k.GrossProfit, k.NetProfit }, JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetCashFlowStatementDetails(string referenceNo)
        {
            return Json(new DPRBAL().GetCashFlowStatementDetails(referenceNo).Select(z => new { z.ReferenceNo, z.Year, z.TotalCashInflow, z.TotalCashOutflow, z.NetCashInflow }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetCashFlowStatementData(string referenceNo, string year)
        {
            var k = new DPRBAL().GetCashFlowStatementData(referenceNo, year);
            return Json(new { k.Year, k.TotalCashInflow, k.TotalCashOutflow, k.NetCashInflow }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddInvestmentLand(CapitalInvestmentLand cil, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), "New DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CapitalInvestmentLand/CapitalInvestment/DPRStatus", "INSERT", "NA", "POST");
                return Json(new DPRBAL().AddInvestmentLand(cil, ci), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateInvestmentLand(string referenceNo, CapitalInvestmentLand cil, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CapitalInvestmentLand/CapitalInvestment", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateInvestmentLand(referenceNo, cil, ci, null), JsonRequestBehavior.AllowGet);
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
        public JsonResult AddInvestmentCivilConstuction(CapitalInvestmentCivilConstruction cicc, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), "New DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CapitalInvestmentCivilConstruction/CapitalInvestment", "INSERT/UPDATE", "NA", "POST");
                return Json(new DPRBAL().AddInvestmentCivilConstuction(cicc, ci), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateInvestmentCivilConstruction(string referenceNo, CapitalInvestmentCivilConstruction cicc, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CapitalInvestmentCivilConstruction/CapitalInvestment", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateInvestmentCivilConstruction(referenceNo, cicc, ci, null), JsonRequestBehavior.AllowGet);
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
        public JsonResult AddInvestmentWaterSupply(CapitalInvestmentWaterSupply ciws, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), "New DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CapitalInvestmentWaterSupply/CapitalInvestment", "INSERT/UPDATE", "NA", "POST");
                return Json(new DPRBAL().AddInvestmentWaterSupply(ciws, ci), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateInvestmentWaterSupply(string referenceNo, CapitalInvestmentWaterSupply ciws, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CapitalInvestmentWaterSupply/CapitalInvestment", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateInvestmentWaterSupply(referenceNo, ciws, ci, null), JsonRequestBehavior.AllowGet);
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
        public JsonResult AddInvestmentElectrification(CapitalInvestmentElectrification cie, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), "New DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CapitalInvestmentElectrification/CapitalInvestment", "INSERT/UPDATE", "NA", "POST");
                return Json(new DPRBAL().AddInvestmentElectrification(cie, ci), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateInvestmentElectrification(string referenceNo, CapitalInvestmentElectrification cie, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CapitalInvestmentElectrification/CapitalInvestment", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateInvestmentElectrification(referenceNo, cie, ci, null), JsonRequestBehavior.AllowGet);
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
        public JsonResult AddInvestmentPlantMachinery(CapitalInvestmentPlantMachinery cipm, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), "New DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CapitalInvestmentPlantMachinery/CapitalInvestment", "INSERT/UPDATE", "NA", "POST");
                return Json(new DPRBAL().AddInvestmentPlantMachinery(cipm, ci), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateInvestmentPlantMachinery(string referenceNo, CapitalInvestmentPlantMachinery cipm, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CapitalInvestmentPlantMachinery/CapitalInvestment", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateInvestmentPlantMachinery(referenceNo, cipm, ci, null), JsonRequestBehavior.AllowGet);
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
        public JsonResult AddInvestmentAnimalPlant(CapitalInvestmentAnimalPlant ciap, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), "New DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CapitalInvestmentAnimalPlant/CapitalInvestment", "INSERT/UPDATE", "NA", "POST");
                return Json(new DPRBAL().AddInvestmentAnimalPlant(ciap, ci), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateInvestmentAnimalPlant(string referenceNo, CapitalInvestmentAnimalPlant ciap, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CapitalInvestmentAnimalPlant/CapitalInvestment", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateInvestmentAnimalPlant(referenceNo, ciap, ci, null), JsonRequestBehavior.AllowGet);
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
        public JsonResult AddInvestmentMiscellaneous(CapitalInvestmentMiscellaneou cim, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), "New DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CapitalInvestmentMiscellaneous/CapitalInvestment", "INSERT/UPDATE", "NA", "POST");
                return Json(new DPRBAL().AddInvestmentMiscellaneous(cim, ci), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateInvestmentMiscellaneous(string referenceNo, CapitalInvestmentMiscellaneou cim, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CapitalInvestmentMiscellaneous/CapitalInvestment", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateInvestmentMiscellaneous(referenceNo, cim, ci, null), JsonRequestBehavior.AllowGet);
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
        public JsonResult AddRecurringExpenditure(RecurringExpenditure re, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), "New DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "RecurringExpenditure/CapitalInvestment", "INSERT/UPDATE", "NA", "POST");
                return Json(new DPRBAL().AddRecurringExpenditure(re, ci), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateRecurringExpenditure(string referenceNo, RecurringExpenditure re, CapitalInvestment ci)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "RecurringExpenditure/CapitalInvestment", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateRecurringExpenditure(referenceNo, re, ci, null), JsonRequestBehavior.AllowGet);
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
        public JsonResult AddMeansOfFinance(MeansOfFinance mof)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), "New DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "MeansOfFinance", "INSERT", "NA", "POST");
                return Json(new DPRBAL().AddMeansOfFinance(mof), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateMeansOfFinance(string referenceNo, MeansOfFinance mof)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "MeansOfFinance", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateMeansOfFinance(referenceNo, mof, null), JsonRequestBehavior.AllowGet);
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
        public JsonResult AddKeyBusinessMatrix(KeyBusinessMatrix kbm)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), "New DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "KeyBusinessMatrix", "INSERT", "NA", "POST");
                return Json(new DPRBAL().AddKeyBusinessMatrix(kbm), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateKeyBusinessMatrix(string referenceNo, KeyBusinessMatrix kbm)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "KeyBusinessMatrix", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateKeyBusinessMatrix(referenceNo, kbm, null), JsonRequestBehavior.AllowGet);
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
        public JsonResult AddProfitabilityProjection(string referenceNo, List<ProfitabilityProjection> lpp)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid && lpp.Count == 7)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), "New DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "ProfitabilityProjection", "INSERT", "NA", "POST");
                return Json(new DPRBAL().AddProfitabilityProjection(referenceNo, lpp), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateProfitabilityProjection(string referenceNo, string year, ProfitabilityProjection lpp)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "ProfitabilityProjection", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateProfitabilityProjection(referenceNo, year, lpp, null), JsonRequestBehavior.AllowGet);
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
        public JsonResult AddCashFlowStatement(string referenceNo, List<CashFlowStatement> lcfs)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid && lcfs.Count == 7)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), "New DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CashFlowStatement", "INSERT", "NA", "POST");
                return Json(new DPRBAL().AddCashFlowStatement(referenceNo, lcfs), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateCashFlowStatement(string referenceNo, string year, CashFlowStatement lcfs)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "Old DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "CashFlowStatement", "UPDATE", "NA", "POST");
                    return Json(new DPRBAL().UpdateCashFlowStatement(referenceNo, year, lcfs, null), JsonRequestBehavior.AllowGet);
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
        public JsonResult UpdateDPRStatus(string referenceNo, DPRStatu ds, BLORecord br)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (referenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "New DPR", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "DPRStatus", "INSERT", "NA", "POST");
                    Session.Abandon();
                    Session.Clear();
                    Session.RemoveAll();
                    Session.Remove("refno");

                    return Json(new DPRBAL().UpdateDPRStatus(referenceNo, ds, br), JsonRequestBehavior.AllowGet);
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
    }
}