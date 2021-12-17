using CommercialAgriEnterprise.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace CommercialAgriEnterprise.Controllers
{
    [AllowAnonymous]
    [ValidateAntiForgeryTokenOnAllPosts]

    public class RegistrationController : Controller
    {
        public ActionResult Registration()
        {
            return View();
        }

        [NoDirectAccess]
        public JsonResult GetEducationalQualification()
        {
            return Json(new RegistrationBAL().ListEducationalQualification().OrderBy(z => z.Name).Select(x => new { x.Code, x.Name }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetGroupTypes()
        {
            return Json(new RegistrationBAL().GetGroupTypes().OrderBy(a => a.Name).Select(a => new { a.Code, a.Name }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetIProjects(string referenceNo)
        {
            return Json(new RegistrationBAL().GetIProjects(referenceNo), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetGroupMemberIDs(string referenceNo)
        {
            return Json(new RegistrationBAL().GetMemberIDs(referenceNo).Select(x => new { x.GroupMemberFarmerID, x.HighestEducationalQualificationCode }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetDepartment()
        {
            return Json(new RegistrationBAL().ListDepartment().OrderBy(z => z.Name).Select(x => new { x.Code, x.Name }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult ProjectType()
        {
            return Json(new RegistrationBAL().ProjectType().Select(x => new { x.Code, x.Name, x.MinCapacity, x.CapacityUnit }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetAllProjectType(string deptcode)
        {
            return Json(new RegistrationBAL().AllProjectType(deptcode).OrderBy(a => a.Name).Select(a => new { a.Code, a.Name }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetProjectType(string departmentCode)
        {
            return Json(new RegistrationBAL().ListProjectType(departmentCode).OrderBy(z => z.Name).Select(x => new { x.Code, x.Name }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult ProjecttypeChange(string pCode)
        {
            return Json(ProjectDetailsforEntry.SearchProjectType(pCode), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult ProjectCapacityChange(string pCode, decimal Capacity, string Cunit)
        {
            return Json(ProjectDetailsforEntry.SearchProjectCapacity(pCode, Capacity, Cunit), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetCapacityUnit()
        {
            return Json(new RegistrationBAL().ListCapacityUnit().Select(x => x.CapacityUnit).Distinct(), JsonRequestBehavior.AllowGet);
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

        [NoDirectAccess]
        public JsonResult GetAllBranch(int bankCode)
        {
            return Json(new RegistrationBAL().GetAllBranch(bankCode), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetDistrictAll()
        {
            return Json(new RegistrationBAL().GetDistrict().Select(z => new { z.DistrictCode, DistrictName = z.DistrictName.Trim(), z.RevenueDistrictCode }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetAllBlock(string DistCode)
        {
            if (DistCode != null)
            {
                return Json(new RegistrationBAL().GetBlockAll(DistCode).Select(z => new { z.BlockCode, z.BlockName }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult GetAllGP(string BlockCode)
        {
            if (BlockCode != null)
            {
                return Json(new RegistrationBAL().GetGPAll(BlockCode).Select(z => new { z.GPCode, z.GPName }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult GetAllVillage(string Gp)
        {
            if (Gp != null)
            {
                return Json(new RegistrationBAL().GetVillageAll(Gp).Select(z => new { z.VillageCode, z.VillageName }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult GetAllRDistrict()
        {
            return Json(new RegistrationBAL().GetRDistrict().Select(x => new { x.DCode, x.DName, x.LGDDistrictCode }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetAllRTehsil(string rDCode)
        {
            if (rDCode != null)
            {
                return Json(new RegistrationBAL().GetRTehsil(rDCode).Select(z => new { z.TCode, z.TName, z.LGDBlockCode }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult GetAllRICircle(string rDCode, string rTCode)
        {
            if (rTCode != null)
            {
                return Json(new RegistrationBAL().GetRICircle(rDCode, rTCode).Select(z => new { z.riCode, z.riName }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult GetAllRVillage(string dCode, string tCode)
        {
            if (dCode != null && tCode != null)
            {
                return Json(new RegistrationBAL().GetRVillage(dCode, tCode).Select(z => new { z.VCode, z.voName }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult GetVillageList(string farmerid, string refno)
        {
            return Json(new RegistrationBAL().VillageList(farmerid, refno).OrderBy(a => a.VillageName).Select(a => new { a.VillageCode, a.VillageName }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetGPList(string farmerid, string refno)
        {
            return Json(new RegistrationBAL().GPList(farmerid, refno).OrderBy(a => a.GPName).Select(a => new { a.GPCode, a.GPName }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GPPopulate(int blockCode)
        {
            return Json(new RegistrationBAL().GPPopulate(blockCode).OrderBy(a => a.GPName).Select(x => new { x.GPCode, x.GPName }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult VillagePopulate(int gpCode)
        {
            return Json(new RegistrationBAL().VillagePopulate(gpCode).OrderBy(a => a.VillageName).Select(x => new { x.VillageCode, x.VillageName }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetRelationship()
        {
            return Json(new RegistrationBAL().ListRelationship().OrderBy(z => z.Name).Select(x => new { x.Code, x.Name }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult FIDRelationship(string farmerID)
        {
            return Json(new RegistrationBAL().FIDRelationship(farmerID).OrderBy(z => z.VCHORIYAREL).Select(x => new { x.INTRELATION, x.VCHORIYAREL }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult landDetailsAck(string refno)
        {
            return Json(new RegistrationBAL().landDetailsAck(refno).Select(a => new { a.KhataNo, a.PlotNo }), JsonRequestBehavior.AllowGet);
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
        public JsonResult GetBeneficiaryLandrecord(string khataNo, string plotNo, string rDistrictID, int villageCode)
        {
            return Json(new RegistrationBAL().GetBeneficiaryLandrecord(khataNo, plotNo, rDistrictID, villageCode), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetBeneficiaryBhulekhRecord(string dCode, string tCode, string vCode, string khataNo, string plotNo)
        {
            return Json(new RegistrationBAL().GetBeneficiaryBhulekhRecord(dCode, tCode, vCode, khataNo, plotNo), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult BhulekhData(string District, int VillageCode, string KhataNo, string PlotNo)
        {
            var k = new BhulekhOdisha.OdishaCropInsuranceSoapClient().AgriCensusRORData(District, VillageCode, KhataNo, PlotNo).Rows;
            return Json(new BhulekhData() { AreainAcre = k[0][0].ToString(), AreainHectare = k[0][1].ToString(), Kisam = k[0][2].ToString(), TenantName = k[0][3].ToString() }, JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult Chk_Emaiid(string EmailID)
        {
            return Json(new RegistrationBAL().Chk_Emailid(EmailID), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult CheckEmailID(string referenceNo, string emailID)
        {
            return Json(new RegistrationBAL().CheckEmailID(referenceNo, emailID), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult CheckExistFarmerID(string farmerID)
        {
            return Json(new RegistrationBAL().CheckExistFarmerID(farmerID), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetApplicantDetail(string farmerID, string rType, int? relation)
        {
            var farmerdata = new RegistrationBAL().GetApplicantDetail(farmerID, rType, relation);
            try
            {
                // TempData["Fdata"] = farmerdata.FarmerID;
                Session["Fdata"] = farmerdata.FarmerID;
                return Json(farmerdata, JsonRequestBehavior.AllowGet);
            }
            catch (NullReferenceException nre)
            {
                nre.Message.ToString();
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [NoDirectAccess]
        public JsonResult FirstSubmit(string refno)
        {
            Session["refno"] = refno;
            return Json(new RegistrationBAL().FirstSubmit(refno), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult SecondSubmit(string refno)
        {
            return Json(new RegistrationBAL().SecondSubmit(refno), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult ThirdSubmit(string refno)
        {
            return Json(new RegistrationBAL().ThirdSubmit(refno), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult ThirdFormLandDetails(string refno)
        {
            return Json(new RegistrationBAL().Getlanddetails(refno).Select(x => new { x.ReferenceNo, x.KhataNo, x.PlotNo, x.AreaInAcre, x.AreaInHectare, x.DistrictCode, x.Kisam, x.RelationshipCode, x.TenantName, x.VillageCode, x.Relationship.Name }), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult LastFormDtls(string refno, string AadharNO)
        {
            Session["refno"] = refno;
            return Json(new RegistrationBAL().LastFormDtls(refno, AadharNO), JsonRequestBehavior.AllowGet);
        }

        [NoDirectAccess]
        public JsonResult GetDocumentfiles(string refno)
        {
            var jsonResult = Json(new RegistrationBAL().GetDocumentfiles(refno), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [NoDirectAccess]
        public JsonResult GetBankDocument(string refno)
        {
            var jsonResult = Json(new RegistrationBAL().GetBankDocument(refno), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [NoDirectAccess]
        public JsonResult GetBankCLDoc(string refno)
        {
            var jsonResult = Json(new RegistrationBAL().GetBankCLDoc(refno), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [NoDirectAccess]
        public JsonResult GetGraduationDocument(string refno)
        {
            var jsonResult = Json(new RegistrationBAL().GetGraduationDocument(refno), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [NoDirectAccess]
        public JsonResult GetDOcumentDtls(string refno)
        {
            var jsonResult = Json(new BeneficiaryDocVM(refno).GetDocumentDetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [NoDirectAccess]
        public JsonResult GetApplicantAcknowledgement(string refno)
        {
            return Json(new RegistrationBAL().GetApplicantAcknowledgement(refno), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SubmitData(List<BeneficiaryMemberDetail> memberDetail, string farmerID, string referenceNo)
        {
            AuditLog.Log(Request.UserHostAddress.ToString(), "New Registration", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BeneficiaryMemberDetail", "INSERT", "NA", "POST");
            if (referenceNo == null)
            {
                return Json(new RegistrationBAL().CheckGroupMemberFarmerID(memberDetail, farmerID), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new RegistrationBAL().CheckRefGroupMemberFarmerID(memberDetail, farmerID, referenceNo), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Submitappandprojectdetails(BeneficiaryDtls BD, List<BeneficiaryMemberDetail> memberDetail, List<IntegratedProjectDetail> IPD, string emailID, BeneficiaryProjectDetail BPD, string pCode, string gender, string catagory, decimal totalcost)
        {
            // if (TempData.ContainsKey("Fdata"))
            if (Session["Fdata"].ToString() == BD.FarmerID)
            {
                if (BPD.BankConsentLetter != null)
                {
                    if ((BPD.BankConsentLetter.Length / 1024) > 500 || MimeCheck.getMimeFromFile(BPD.BankConsentLetter) != "application/pdf")
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                if (BPD.CISBankClearanceCertificate != null)
                {
                    if ((BPD.CISBankClearanceCertificate.Length / 1024) > 500 || MimeCheck.getMimeFromFile(BPD.CISBankClearanceCertificate) != "application/pdf")
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                if (BD.CastGraduationCertificate != null)
                {
                    if ((BD.CastGraduationCertificate.Length / 1024) > 500 || MimeCheck.getMimeFromFile(BD.CastGraduationCertificate) != "application/pdf")
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                if (ModelState.IsValid && Session["Fdata"].ToString() == BD.FarmerID)
                {
                    if ((BD.HighestEducationalQualificationCode == "4" && BD.CastGraduationCertificate != null) || (BD.HighestEducationalQualificationCode != "4" && BD.CastGraduationCertificate == null))
                    {
                        AuditLog.Log(Request.UserHostAddress.ToString(), "New Registration", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BeneficiaryDetail/BeneficiaryProjectDetail", "INSERT", "NA", "POST");
                        return Json(new RegistrationBAL().Submitapplicationandprojectdetails(new BeneficiaryDetail() { FarmerID = BD.FarmerID.ToUpper(), RelationWithFIDType = BD.RelationWithFIDType, RelationWithFID = BD.RelationWithFID, RelationApplicantName = BD.RelationApplicantName, RAadhaarNo = BD.RAadhaarNo, RVoterIDNo = BD.RVoterIDNo, NoOfMember = BD.NoOfMember, PermanentAddress = BD.PermanentAddress, CorrespondenceAddress = BD.CorrespondenceAddress, BeneficiaryType = BD.BeneficiaryType, FirmName = BD.FirmName, EmailID = BD.EmailID, PresentOccupation = BD.PresentOccupation, PreviousExperience = BD.PreviousExperience, HighestEducationalQualificationCode = BD.HighestEducationalQualificationCode, AnnualIncome = BD.AnnualIncome, MobileNo = BD.MobileNo, Pin = BD.Pin, GroupTypeCode = BD.GroupTypeCode, CastGraduationCertificate = BD.CastGraduationCertificate }, memberDetail, IPD, BPD, totalcost), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Please upload Preferential Treatment Certificate (Caste / Graduation in Agriculture and Allied Discipline)", JsonRequestBehavior.AllowGet);
                    }
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
        public JsonResult UpdateBeneficiaryDtls(BeneficiaryUpdateDetails x, List<BeneficiaryMemberDetail> memberDetails)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (x.ReferenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "New Registration", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BeneficiaryDetail", "UPDATE", "NA", "POST");
                    return Json(new RegistrationBAL().UpdateBeneficiaryDtls(x, memberDetails), JsonRequestBehavior.AllowGet);
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
        public JsonResult UpdateProjectInformationDetails(BeneficiaryProjectUpdateDetails x, List<IntegratedProjectDetail> IPD, string selectCapacity, decimal totalcost)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (x.ReferenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "New Registration", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BeneficiaryProjectDetail", "UPDATE", "NA", "POST");
                    return Json(new RegistrationBAL().UpdateProjectINfoDetails(x, IPD, selectCapacity, totalcost), JsonRequestBehavior.AllowGet);
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
        public JsonResult LandRecordSubmitDetails(List<BeneFiciaryLandRecordDetail> x, string farmerID, string refno, PdfFilesforBeneficiaryDetail pdfland, string locality)
        {
            if (x.Count > 1)
            {
                if (locality == "Rural")
                {
                    var result = x.Distinct().GroupBy(a => new { a.DistrictCode, a.BlockCode, a.GPCode, a.VillageCode, a.KhataNo, a.PlotNo }).ToList();
                    if (result.Count != x.Count)
                    {
                        return Json("Duplicate record must not be entered.");
                    }
                    var res = x.Distinct().GroupBy(a => new { a.DistrictCode, a.BlockCode }).ToList();
                    if (res.Count == x.Count)
                    {
                        return Json("District and Block must be same for all records.");
                    }
                }
                else if (locality == "Urban")
                {
                    var result1 = x.Distinct().GroupBy(a => new { a.DistrictCode, a.TehsilCode, a.RICircleCode, a.KhataNo, a.PlotNo }).ToList();
                    if (result1.Count != x.Count)
                    {
                        return Json("Duplicate record must not be entered.");
                    }
                    var res1 = x.Distinct().GroupBy(a => new { a.DistrictCode, a.TehsilCode }).ToList();
                    if (res1.Count == x.Count)
                    {
                        return Json("District and Tehsil must be same for all records.");
                    }
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            if ((pdfland.LandPdf1.Length / 1024) > 950 || MimeCheck.getMimeFromFile(pdfland.LandPdf1) != "application/pdf")
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            if (pdfland.LandPdf2 != null)
            {
                if ((pdfland.LandPdf1.Length / 1024) > 950 || MimeCheck.getMimeFromFile(pdfland.LandPdf2) != "application/pdf")
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), "New Registration", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BeneFiciaryLandRecordDetail", "INSERT", "NA", "POST");
                List<BeneFiciaryLandRecordDetail> t = x.Select(g => new BeneFiciaryLandRecordDetail { ReferenceNo = refno, VillageCode = g.VillageCode, KhataNo = g.KhataNo, PlotNo = g.PlotNo, TenantName = g.TenantName, Kisam = g.Kisam, AreaInHectare = g.AreaInHectare, AreaInAcre = g.AreaInAcre, ExecutionArea = g.ExecutionArea, DistrictCode = g.DistrictCode, RelationshipCode = g.RelationshipCode, BlockCode = g.BlockCode, GPCode = g.GPCode, UnitExecution = g.UnitExecution, Locality = locality, RICircleCode = g.RICircleCode, TehsilCode = g.TehsilCode }).ToList();
                return Json(new RegistrationBAL().LandRecordSubmitDetails(t, farmerID, refno, pdfland, locality), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateLandRecord(BeneFiciaryLandRecordDetail x, int gpCode, int villageCode, string khataNo, string plotNo)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                if (x.ReferenceNo == Session["refno"].ToString())
                {
                    AuditLog.Log(Request.UserHostAddress.ToString(), "New Registration", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BeneFiciaryLandRecordDetail", "UPDATE", "NA", "POST");
                    return Json(new RegistrationBAL().UpdateLandRecord(x, gpCode, villageCode, khataNo, plotNo), JsonRequestBehavior.AllowGet);
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
        public JsonResult UpdateBankDoc(string refno, BankDocument BankCertificate)
        {
            if (BankCertificate.CISBankClearanceCertificate != null)
            {
                if ((BankCertificate.CISBankClearanceCertificate.Length / 1024) > 950 || MimeCheck.getMimeFromFile(BankCertificate.CISBankClearanceCertificate) != "application/pdf")
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
                    return Json(new RegistrationBAL().UpdateBankDoc(refno, BankCertificate), JsonRequestBehavior.AllowGet);
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
                    return Json(new RegistrationBAL().UpdateBankCL(refno, BankCL, null), JsonRequestBehavior.AllowGet);
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
                    return Json(new RegistrationBAL().UpdateGradCert(refno, GraduationCertificate, null), JsonRequestBehavior.AllowGet);
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
                    return Json(new RegistrationBAL().UpdateLandDoc1(refno, pdfland, null), JsonRequestBehavior.AllowGet);
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
                    return Json(new RegistrationBAL().UpdateLandDoc2(refno, pdflandrec, null), JsonRequestBehavior.AllowGet);
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
        public JsonResult SubmitDocumentdtls(BeneficiaryDocument mydata, BLOCheck y, DNOCheck z)
        {
            if ((mydata.Photograph.Length / 1024) > 50 || MimeCheck.getMimeFromFile(mydata.Photograph) != "image/pjpeg")
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            if ((mydata.IdentityProof.Length / 1024) > 400 || MimeCheck.getMimeFromFile(mydata.IdentityProof) != "application/pdf")
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            if ((mydata.Signature.Length / 1024) > 50 || MimeCheck.getMimeFromFile(mydata.Signature) != "image/pjpeg")
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), "New Registration", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BeneficiaryDocument", "INSERT", "NA", "POST");
                return Json(new RegistrationBAL().SubmitDocumentdtls(mydata, y, z), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdatePhoto(string referenceNo, Photo p)
        {
            if (p.UPhoto != null)
            {
                if ((p.UPhoto.Length / 1024) > 50 || MimeCheck.getMimeFromFile(p.UPhoto) != "image/pjpeg")
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
                    return Json(new RegistrationBAL().UpdatePhoto(referenceNo, p), JsonRequestBehavior.AllowGet);
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
                    return Json(new RegistrationBAL().UpdateIDProof(referenceNo, identityProof, null), JsonRequestBehavior.AllowGet);
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
        public JsonResult UpdateSign(string referenceNo, Signature s)
        {
            if (s.USign != null)
            {
                if ((s.USign.Length / 1024) > 50 || MimeCheck.getMimeFromFile(s.USign) != "image/pjpeg")
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
                    return Json(new RegistrationBAL().UpdateSign(referenceNo, s), JsonRequestBehavior.AllowGet);
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
        public JsonResult SubmitRegistration(string referenceNo)
        {
            if (referenceNo == Session["refno"].ToString())
            {
                AuditLog.Log(Request.UserHostAddress.ToString(), "Old Registration", Request.Url.AbsolutePath.ToString(), Request.Browser.IsMobileDevice.ToString(), Request.Browser.MobileDeviceModel.ToString(), Request.Browser.MobileDeviceManufacturer.ToString(), Request.Browser.Platform.ToString(), Request.Browser.Type.ToString(), Request.Browser.ScreenPixelsWidth.ToString() + "X" + Request.Browser.ScreenPixelsHeight.ToString(), DateTime.Now, "BeneFiciaryDetail", "UPDATE", "NA", "POST");
                Session.Abandon();
                Session.Clear();
                Session.RemoveAll();
                Session.Remove("refno");
                return Json(new RegistrationBAL().SubmitRegistration(referenceNo), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DisplayPDFDocument(string refno, string filetype)
        {
            byte[] byteArray = new RegistrationBAL().GetPdfFromDB(refno, filetype);
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

        public ActionResult ViewBankCL(string refno, string filetype)
        {
            byte[] byteArray = new RegistrationBAL().GetBankCL(refno, filetype);
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }

        public ActionResult viewPTCCGAAD(string refno, string filetype)
        {
            byte[] byteArray = new RegistrationBAL().GetPTCCGAAD(refno, filetype);
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
    }
}