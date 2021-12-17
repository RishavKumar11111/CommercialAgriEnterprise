using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommercialAgriEnterprise.Models
{
    public class DNOBAL
    {
        CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();
        FARMERDBEntities orm1 = new FARMERDBEntities();

        public List<ApproveListVM> Approvallist(string deptcode, int dCode)
        {
            string DistCode = Convert.ToString(dCode);
            var result = orm.BeneficiaryDetails.Where(a => a.BeneficiaryProjectDetail.DepartmentCode == deptcode && a.BLORecord.ReferenceNo == a.DNORecord.ReferenceNo && (a.BLORecord.BLOStatus == 1 || a.BLORecord.BLOStatus == 2) && a.RegistrationStatus == "completed" && a.DPRStatu.Status == 1 && a.PaymentStatus == "Success" && a.NICDistrictCode == DistCode && a.BeneficiaryProjectDetail.ProjectTypeCode != "04P15").Select(a => new ApproveListVM { refno = a.ReferenceNo, status = a.DNORecord.DNOStatus, statusDda = a.DDARecord.DDAStatus, statusCollector = a.CollectorRecord.CollectorStatus, pStatus = a.PaymentStatus, statusBlo = a.BLORecord.BLOStatus, BTBLOStatus = a.DNORecord.BackToBLOStatus }).ToList();
            return result;
        }

        public string DNORecommended(DNORecordValidation x, string dnoName)
        {
            var k = orm.DNORecords.Where(a => a.ReferenceNo == x.ReferenceNo).FirstOrDefault();
            k.DNOStatus = 1;
            k.DNODateTime = DateTime.Now;
            k.IPAddress = HttpContext.Current.Request.UserHostAddress;
            k.DNOUserName = dnoName;
            k.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            try
            {
                orm.SaveChanges();
                return "Record is marked as recommended.";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string DDARecommended(DDARecordValidation x, string ddaName)
        {
            var k = orm.DDARecords.Where(z => z.ReferenceNo == x.ReferenceNo).FirstOrDefault();
            k.IntegratedFarmingStatus = 1;
            k.IntegratedFarmingDate = DateTime.Now;
            k.IPAddress = HttpContext.Current.Request.UserHostAddress;
            k.UserName = ddaName;
            k.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            try
            {
                orm.SaveChanges();
                return "Record is marked as recommended.";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string DNOBackToBLO(DNORecordValidation x, string dnoName)
        {
            var k = orm.DNORecords.Where(a => a.ReferenceNo == x.ReferenceNo).FirstOrDefault();
            k.BackToBLOStatus = 1;
            k.DNODateTime = DateTime.Now;
            k.IPAddress = HttpContext.Current.Request.UserHostAddress;
            k.DNOUserName = dnoName;
            k.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            var br = orm.BLORecords.Where(z => z.ReferenceNo == x.ReferenceNo).FirstOrDefault();
            DNOBackToBLOLog dbb = new DNOBackToBLOLog();
            dbb.ReferenceNo = x.ReferenceNo;
            dbb.DNOUserName = dnoName;
            dbb.UserDateTime = DateTime.Now;
            dbb.IPAddress = HttpContext.Current.Request.UserHostAddress;
            dbb.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dbb.BLOStatus = br.BLOStatus;
            dbb.BLODate = Convert.ToDateTime(br.BLODate);
            dbb.BLORejectionReason = br.RejectionReason;
            dbb.BLOUserName = br.BLOUserName;
            dbb.BLOIPAddress = br.IPAddress;
            dbb.BLOFinancialYear = br.FinancialYear;
            br.BLOStatus = 0;
            br.BLODate = null;
            br.RejectionReason = null;
            br.IPAddress = null;
            br.FinancialYear = null;
            orm.DNOBackToBLOLogs.Add(dbb);
            try
            {
                orm.SaveChanges();
                return "Record is reverted back to BLO.";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string DDABackToBLO(DDARecordValidation x, string ddaName)
        {
            var k = orm.DDARecords.Where(a => a.ReferenceNo == x.ReferenceNo).FirstOrDefault();
            k.BackToBLOStatus = 1;
            k.IntegratedFarmingDate = DateTime.Now;
            k.IPAddress = HttpContext.Current.Request.UserHostAddress;
            k.UserName = ddaName;
            k.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            var br = orm.BLORecords.Where(z => z.ReferenceNo == x.ReferenceNo).FirstOrDefault();
            DDABackToBLOLog dbb = new DDABackToBLOLog();
            dbb.ReferenceNo = x.ReferenceNo;
            dbb.DDAUserName = ddaName;
            dbb.IntegratedFarmingDate = DateTime.Now;
            dbb.IPAddress = HttpContext.Current.Request.UserHostAddress;
            dbb.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dbb.BLOStatus = br.BLOStatus;
            dbb.BLODate = Convert.ToDateTime(br.BLODate);
            dbb.BLORejectionReason = br.RejectionReason;
            dbb.BLOUserName = br.BLOUserName;
            dbb.BLOIPAddress = br.IPAddress;
            dbb.BLOFinancialYear = br.FinancialYear;
            br.BLOStatus = 0;
            br.BLODate = null;
            br.RejectionReason = null;
            br.IPAddress = null;
            br.FinancialYear = null;
            orm.DDABackToBLOLogs.Add(dbb);
            try
            {
                orm.SaveChanges();
                return "Record is reverted back to BLO.";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string DNONotRecommended(DNORecordValidation x, string dnoName)
        {
            var k = orm.DNORecords.Where(a => a.ReferenceNo == x.ReferenceNo).FirstOrDefault();
            k.DNOStatus = 2;
            k.DNODateTime = DateTime.Now;
            k.RejectionReason = x.RejectionReason;
            k.IPAddress = HttpContext.Current.Request.UserHostAddress;
            k.DNOUserName = dnoName;
            k.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            try
            {
                orm.SaveChanges();
                return "Record is marked as not recommended.";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string DDANotRecommended(DDARecordValidation x, string ddaName)
        {
            var k = orm.DDARecords.Where(z => z.ReferenceNo == x.ReferenceNo).FirstOrDefault();
            k.IntegratedFarmingStatus = 2;
            k.IntegratedFarmingDate = DateTime.Now;
            k.DDARejectionReason = x.DDARejectionReason;
            k.UserName = ddaName;
            k.IPAddress = HttpContext.Current.Request.UserHostAddress;
            k.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            try
            {
                orm.SaveChanges();
                return "Record is marked as not recommended.";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string CheckDnolist(string refno)
        {
            var m = orm.GoAheads.Where(z => z.ReferenceNo == refno).Select(x => new { x.BeneficiaryDetail.NICDistrictCode, x.BeneficiaryDetail.BeneficiaryProjectDetail.DepartmentCode, x.GoAheadStatus }).FirstOrDefault();
            var p = Convert.ToInt32(m.NICDistrictCode);
            var k = orm.DNODetailEntries.Where(z => z.DistrictCode == p && z.DepartmentCode == m.DepartmentCode).Select(x => new { x.Name, x.Signature }).FirstOrDefault();
            var L = orm.BeneficiaryProjectDetails.Where(z => z.ReferenceNo == refno).Select(z => z.ProjectTypeCode).FirstOrDefault();
            var J = orm.DNODetailEntries.Where(z => z.DistrictCode == p && z.DepartmentCode == "01").Select(x => new { x.Name, x.Signature }).FirstOrDefault();
            if (m.GoAheadStatus == 0)
            {
                if (L == "04P15")
                {
                    if (J != null)
                    {
                        return "false";
                    }
                    else
                    {
                        return "true";
                    }
                }
                else
                {
                    if (k != null)
                    {
                        return "false";
                    }
                    else
                    {
                        return "true";
                    }
                }
            }
            else
            {
                return "Already generated.";
            }
        }

        public string DNORecordSave(string refNo)
        {
            var k = orm.GoAheads.Where(z => z.ReferenceNo == refNo).FirstOrDefault();
            k.GoAheadStatus = 1;
            try
            {
                orm.SaveChanges();
                string SMS = "APICOL - Generate and Print Go-Ahead Letter.";
                var l = orm.BeneficiaryDetails.Where(z => z.ReferenceNo == refNo).Select(g => g.MobileNo).FirstOrDefault();
                SMSGateway.SendSMS(l, SMS);
                return "Go-Ahead Letter is generated.";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public List<ApproveListVM> IntegratedFarmingList(int dCode)
        {
            string DistCode = Convert.ToString(dCode);
            var result = orm.BeneficiaryDetails.Where(z => z.BeneficiaryProjectDetail.ProjectTypeCode == "04P15" && (z.BLORecord.BLOStatus == 1 || z.BLORecord.BLOStatus == 2) && z.BLORecord.ReferenceNo == z.DDARecord.ReferenceNo && z.BeneficiaryProjectDetail.DepartmentCode == "04" && (z.DDARecord.IntegratedFarmingStatus == 0 || z.DDARecord.IntegratedFarmingStatus == 1 || z.DDARecord.IntegratedFarmingStatus == 2) && z.RegistrationStatus == "completed" && z.DPRStatu.Status == 1 && z.PaymentStatus == "Success" && z.NICDistrictCode == DistCode).Select(z => new ApproveListVM { refno = z.ReferenceNo, statusDda = z.DDARecord.DDAStatus, statusCollector = z.CollectorRecord.CollectorStatus, pStatus = z.PaymentStatus, integratedStatus = z.DDARecord.IntegratedFarmingStatus, statusBlo = z.BLORecord.BLOStatus, BTBLOStatus = z.DDARecord.BackToBLOStatus }).ToList();
            return result;
        }

        public BeneficiaryLandRecordVM GetDocumentfiles(string refno)
        {
            var k = orm.BeneficiaryDetails.Where(a => a.ReferenceNo == refno).Select(a => new BeneficiaryLandRecordVM { pdffile1 = a.LandPdf1, pdffile2 = a.LandPdf2 }).FirstOrDefault();
            return k;
        }

        public byte[] GetPdfFromDB(string refno, string filetype)
        {
            if (filetype == "LandPdf1")
            {
                return orm.BeneficiaryDetails.Where(a => a.ReferenceNo == refno).Select(a => a.LandPdf1).FirstOrDefault();
            }
            else
            {
                return orm.BeneficiaryDetails.Where(a => a.ReferenceNo == refno).Select(a => a.LandPdf2).FirstOrDefault();
            }
        }

        public bool BloUserBlkMap(string _DepartmentID, int _BlockID, string _UserName)
        {
            orm.BLOBlockMappings.Add(new BLOBlockMapping { BlockCode = _BlockID, DeptCode = _DepartmentID, UserID = _UserName });
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                var h = e;
                return false;
            }
        }

        public List<LGDBlock> GetBlocks(int _DistCode, string _DeptCode)
        {
            return orm.LGDBlocks.Where(g => g.DistrictCode == _DistCode).Except(orm.BLOBlockMappings.Where(g1 => g1.DeptCode == _DeptCode).Select(g1 => g1.LGDBlock)).ToList();
        }

        public bool AddBLO(string deptCode, List<BLOBlockMapping> lbbm)
        {
            try
            {
                if (lbbm != null)
                {
                    foreach (var i in lbbm)
                    {
                        i.DeptCode = deptCode;
                        i.DNOUserDateTime = DateTime.Now;
                        i.DNOUserName = HttpContext.Current.Session["uid"].ToString();
                        i.IPAddress = HttpContext.Current.Request.UserHostAddress;
                        i.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                        i.IsActive = 1;
                    }
                    orm.BLOBlockMappings.AddRange(lbbm);
                    orm.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public List<BLOBlockMapping> GetRegisteredBLO(int districtCode, string departmentCode)
        {
            return orm.BLOBlockMappings.Where(z => z.LGDBlock.DistrictCode == districtCode && z.DeptCode == departmentCode).ToList();
        }

        public bool RemoveBLO(string userID, BLOBlockLog bbl)
        {
            var k = orm.BLOBlockMappings.Where(z => z.UserID == userID).FirstOrDefault();
            bbl.UserID = k.UserID; bbl.BlockCode = k.BlockCode; bbl.DeptCode = k.DeptCode; bbl.IPAddress = k.IPAddress; bbl.DNOUserName = k.DNOUserName; bbl.DNOUserDateTime = k.DNOUserDateTime; bbl.FinancialYear = k.FinancialYear; bbl.DisableDateTime = DateTime.Now; bbl.IsActive = 0;
            orm.BLOBlockLogs.Add(bbl);
            orm.BLOBlockMappings.Remove(k);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool DDAApprove(DDARecord dr, string referenceNo)
        {
            var m = orm.CollectorRecords.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            bool result = false;
            if (m.UpdatedDOM != null && m.UpdatedTOM != null)
            {
                if (DateTime.Now.Date >= m.UpdatedDOM)
                {
                    if (DateTime.Now.Hour >= m.UpdatedTOM.Value.Hours)
                    {
                        if (DateTime.Now.Minute >= m.UpdatedTOM.Value.Minutes)
                        {
                            result = true;
                        }
                    }
                }
            }
            else
            {
                if (DateTime.Today >= m.DateOfMeeting)
                {
                    if (DateTime.Now.Hour >= m.TimeOfMeeting.Value.Hours)
                    {
                        if (DateTime.Now.Minute >= m.TimeOfMeeting.Value.Minutes)
                        {
                            result = true;
                        }
                    }
                }
            }
            if (result == true)
            {
                var l = orm.BeneficiaryDetails.Where(a => a.ReferenceNo == referenceNo).Select(a => new { a.FarmerID, a.NICDistrictCode, a.BeneficiaryProjectDetail.DepartmentCode }).FirstOrDefault();
                var k = orm.DDARecords.Where(a => a.ReferenceNo == referenceNo).FirstOrDefault();
                k.DDAStatus = 1;
                k.DDAStatusDate = DateTime.Now;
                k.UserName = HttpContext.Current.Session["uid"].ToString();
                k.IPAddress = HttpContext.Current.Request.UserHostAddress;
                k.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                GoAhead ga = new GoAhead();
                ga.ReferenceNo = referenceNo;
                ga.GoAheadStatus = 0;
                ga.GoAheadDOI = DateTime.Now;
                ga.GoAheadNumber = GenerateGoaheadnumber.goaheadnumber(l.FarmerID, referenceNo);
                ga.IPAddress = HttpContext.Current.Request.UserHostAddress;
                ga.GoAheadValidDate = ga.GoAheadDOI.AddYears(2);
                ga.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                ga.UserDateTime = DateTime.Now;
                orm.GoAheads.Add(ga);
                try
                {
                    orm.SaveChanges();
                    var g = orm1.M_FARMER_REGISTRATION.Where(z => z.NICFARMERID == l.FarmerID).Select(x => x.VCHFARMERNAME).FirstOrDefault();
                    string SMS = "APICOL - Upload your signature in Go-Ahead Letter of Smt / Sri " + g;
                    var p = Convert.ToInt32(l.NICDistrictCode);
                    var r = orm.DNODetailEntries.Where(z => z.DepartmentCode == l.DepartmentCode && z.DistrictCode == p).Select(d => d.MobileNo).FirstOrDefault();
                    SMSGateway.SendSMS(r, SMS);
                    return true;
                }
                catch (Exception ex)
                {
                    var e = ex;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool DDAReject(DDARecord ds)
        {
            var m = orm.CollectorRecords.Where(z => z.ReferenceNo == ds.ReferenceNo).FirstOrDefault();
            bool result = false;
            if (m.UpdatedDOM != null && m.UpdatedTOM != null)
            {
                if (DateTime.Now.Date >= m.UpdatedDOM)
                {
                    if (DateTime.Now.Hour >= m.UpdatedTOM.Value.Hours)
                    {
                        if (DateTime.Now.Minute >= m.UpdatedTOM.Value.Minutes)
                        {
                            result = true;
                        }
                    }
                }
            }
            else
            {
                if (DateTime.Today >= m.DateOfMeeting)
                {
                    if (DateTime.Now.Hour >= m.TimeOfMeeting.Value.Hours)
                    {
                        if (DateTime.Now.Minute >= m.TimeOfMeeting.Value.Minutes)
                        {
                            result = true;
                        }
                    }
                }
            }
            if (result == true)
            {
                var k = orm.DDARecords.Where(z => z.ReferenceNo == ds.ReferenceNo).FirstOrDefault();
                k.DDAStatus = 2;
                k.DDARejectionReason = ds.DDARejectionReason;
                k.DDAStatusDate = DateTime.Now;
                k.UserName = HttpContext.Current.Session["uid"].ToString();
                k.IPAddress = HttpContext.Current.Request.UserHostAddress;
                k.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                try
                {
                    orm.SaveChanges();
                    string SMS = "APICOL - Your Application is rejected for the following reasons : " + k.DDARejectionReason;
                    var l = orm.BeneficiaryDetails.Where(z => z.ReferenceNo == ds.ReferenceNo).Select(x => x.MobileNo).FirstOrDefault();
                    SMSGateway.SendSMS(l, SMS);
                    return true;
                }
                catch (Exception ex)
                {
                    var e = ex;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public List<LGDBlock> GetBlock(string departmentCode, int districtCode)
        {
            var k = orm.LGDBlocks.Where(z => z.DistrictCode == districtCode).ToList();
            var l = orm.BLODetailEntries.Where(z => z.DepartmentCode == departmentCode && z.DistrictCode == districtCode).ToList();
            if (l.Count != 0)
            {
                foreach (var i in l)
                {
                    k.Remove(k.Where(a => a.BlockCode == i.BlockCode).FirstOrDefault());
                }
            }
            return k;
        }

        public BLODetailEntry GetBLORecordsbyBlock(string blockCode, string departmentCode)
        {
            int bCode = Convert.ToInt32(blockCode);
            return orm.BLODetailEntries.Where(z => z.BlockCode == bCode && z.DepartmentCode == departmentCode).FirstOrDefault();
        }

        public bool SubmitBLORecords(BLODetailEntry x, BLOBlockMapping y, int districtCode, string departmentCode)
        {
            var s = new BLODetailEntry { BlockCode = x.BlockCode, Name = x.Name, MobileNo = x.MobileNo, EmailID = x.EmailID, Signature = x.Signature, UserDateTime = DateTime.Now, IPAddress = HttpContext.Current.Request.UserHostAddress, FinancialYear = GenerateReferencenumber.GetCurrentFinancialYear(), DistrictCode = districtCode, DepartmentCode = departmentCode };
            orm.BLODetailEntries.Add(s);
            var r = new BLOBlockMapping { UserID = y.UserID, MobileNo = x.MobileNo, BlockCode = x.BlockCode, DeptCode = departmentCode, IPAddress = HttpContext.Current.Request.UserHostAddress, FinancialYear = GenerateReferencenumber.GetCurrentFinancialYear(), DNOUserDateTime = DateTime.Now, DNOUserName = HttpContext.Current.Session["uid"].ToString(), IsActive = 1 };
            orm.BLOBlockMappings.Add(r);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return false;
            }
        }

        public bool UpdateBLORecord(string blockCode, BLODetailEntry x, int districtCode, string departmentCode)
        {
            string desg = string.Empty;
            string userID = string.Empty;
            if (departmentCode == "01") { desg = "aao"; } else if (departmentCode == "02") { desg = "aho"; } else if (departmentCode == "03") { desg = "afo"; } else if (departmentCode == "04") { desg = "bvo"; }
            userID = desg + "_" + blockCode;
            int bCode = Convert.ToInt32(blockCode);
            var k = orm.BLODetailEntries.Where(z => z.DepartmentCode == departmentCode && z.BlockCode == bCode).FirstOrDefault();
            var l = orm.BLOBlockMappings.Where(g => g.UserID == userID).FirstOrDefault();
            BLOBlockLog bbl = new BLOBlockLog();
            bbl.UserID = l.UserID; bbl.DisableDateTime = DateTime.Now; bbl.MobileNo = k.MobileNo; bbl.BlockCode = l.BlockCode; bbl.DeptCode = l.DeptCode; bbl.DistrictCode = k.DistrictCode; bbl.BLOName = k.Name; bbl.EmailID = k.EmailID; bbl.Signature = k.Signature; bbl.IPAddress = HttpContext.Current.Request.UserHostAddress; bbl.DNOUserName = l.DNOUserName; bbl.DNOUserDateTime = l.DNOUserDateTime; bbl.FinancialYear = k.FinancialYear; bbl.IsActive = 0;
            orm.BLOBlockLogs.Add(bbl);
            k.Name = x.Name; k.MobileNo = x.MobileNo; k.EmailID = x.EmailID; k.Signature = x.Signature; k.UserDateTime = DateTime.Now; k.IPAddress = HttpContext.Current.Request.UserHostAddress; k.FinancialYear = GenerateReferencenumber.GetCurrentFinancialYear();
            l.MobileNo = x.MobileNo; l.IPAddress = HttpContext.Current.Request.UserHostAddress; l.DNOUserDateTime = DateTime.Now; l.FinancialYear = GenerateReferencenumber.GetCurrentFinancialYear();
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return false;
            }
        }

        public string BlonotfeasibleReason(string ReferenceNo)
        {
            var k = orm.BLORecords.Where(z => z.ReferenceNo == ReferenceNo).Select(z => z.RejectionReason).FirstOrDefault();
            return k;
        }

        public bool SubmitDNORecords(DNODetailEntry x, int districtCode, string departmentCode)
        {
            var s = new DNODetailEntry { Name = x.Name, MobileNo = x.MobileNo, Signature = x.Signature, UserDateTime = DateTime.Now, IPAddress = HttpContext.Current.Request.UserHostAddress, FinancialYear = GenerateReferencenumber.GetCurrentFinancialYear(), DistrictCode = districtCode, DepartmentCode = departmentCode };
            orm.DNODetailEntries.Add(s);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return false;
            }
        }

        public DNONameSign GetDNODetail(int districtCode, string departmentCode)
        {
            var k = orm.DNODetailEntries.Where(z => z.DistrictCode == districtCode && z.DepartmentCode == departmentCode).Select(x => new DNONameSign { Name = x.Name, Signature = x.Signature }).FirstOrDefault();
            if (k != null)
            {
                return k;
            }
            else
            {
                return null;
            }
        }

        public List<BLOSignatures> GetBLOSignatures(string departmentCode, int districtCode)
        {
            return orm.BLODetailEntries.Where(z => z.DepartmentCode == departmentCode && z.DistrictCode == districtCode).Select(x => new BLOSignatures { BlockCode = x.BlockCode, BlockName = x.LGDBlock.BlockName, Name = x.Name, MobileNo = x.MobileNo, EmailID = x.EmailID, BLOSignature = x.Signature }).ToList();
        }
    }

    public class DNONameSign
    {
        public string Name { get; set; }
        public byte[] Signature { get; set; }
        public string Sign { get { return Convert.ToBase64String(Signature); } }
    }
}