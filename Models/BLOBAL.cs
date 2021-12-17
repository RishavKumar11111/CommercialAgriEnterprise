using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommercialAgriEnterprise.Models
{
    public class BLOBAL
    {
        CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();
        FARMERDBEntities orm1 = new FARMERDBEntities();

        public List<GetUserVM> get_distid(string userid, int blockcode, string deptcode)
        {
            string blckcode = Convert.ToString(blockcode);
            var result = orm.Payments.Where(a => a.BeneficiaryDetail.NICBlockCode == blckcode && a.BeneficiaryDetail.RegistrationStatus == "completed" && a.BeneficiaryDetail.DPRStatu.Status == 1 && a.BeneficiaryDetail.BLORecord.BLOStatus == 0 && (a.BeneficiaryDetail.DNORecord.DNOStatus == 0 || a.BeneficiaryDetail.DDARecord.DDAStatus == 0) && a.BeneficiaryDetail.BLORecord.ReferenceNo == a.ReferenceNo && (a.BeneficiaryDetail.DNORecord.ReferenceNo == a.ReferenceNo || a.BeneficiaryDetail.DDARecord.ReferenceNo == a.ReferenceNo) && a.BeneficiaryDetail.PaymentStatus == "Success" && a.TransactionStatus == "Success" && (a.BeneficiaryDetail.IntegratedProjectLog.NewDeptCode == deptcode || a.BeneficiaryDetail.BeneficiaryProjectDetail.DepartmentCode == deptcode) && a.BeneficiaryDetail.BLORecord.BLOUserName == userid).Select(a => new GetUserVM { refno = a.ReferenceNo }).ToList();
            return result;
        }

        public List<GetBloActionVM> getbloactionlist(string userid, int blockcode, string deptcode)
        {
            string blckcode = Convert.ToString(blockcode);
            var result = orm.BeneficiaryDetails.Where(a => a.NICBlockCode == blckcode && a.BeneficiaryProjectDetail.DepartmentCode == deptcode && (a.BLORecord.BLOStatus == 1 || a.BLORecord.BLOStatus == 2) && a.ReferenceNo == a.BLORecord.ReferenceNo).Select(a => new GetBloActionVM { refno = a.ReferenceNo, farmerid = a.FarmerID, statusBlo = a.BLORecord.BLOStatus }).ToList();
            return result;
        }

        public byte[] GetPdfFromDB(string refno)
        {
            return orm.BeneficiaryDocuments.Where(a => a.ReferenceNo == refno).Select(a => a.IdentityProof).FirstOrDefault();
        }

        public string CheckforFeasibilitystudy(string ReferenceNo)
        {
            var k = orm.FeasibilityReports.Where(z => z.ReferenceNo == ReferenceNo).Select(z => z.ReferenceNo).FirstOrDefault();
            var IP = orm.BeneficiaryProjectDetails.Where(z => z.ReferenceNo == ReferenceNo && z.ProjectTypeCode == "04P15").Select(z => z.ReferenceNo).FirstOrDefault();
            var chkintegrated = orm.IntegratedProjectLogs.Where(z => z.PreviousReferenceNo == ReferenceNo).Select(z => z.PreviousReferenceNo).FirstOrDefault();
            if (k != null)
            {
                return "Feasibility Study is prepared.";
            }
            else
            {
                if (chkintegrated != null)
                {
                    return "Integrated Project is already sent.";
                }
                else if (IP != null)
                {
                    return "Integrated Project.";
                }
                else
                {
                    return "Feasibility Study is not prepared.";
                }
            }
        }

        public string BLOFeasible(BLORecordValidation x)
        {
            var k = orm.BLORecords.Where(a => a.ReferenceNo == x.ReferenceNo).FirstOrDefault();
            k.BLOStatus = 1;
            k.BLODate = DateTime.Now;
            k.IPAddress = HttpContext.Current.Request.UserHostAddress;
            k.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            try
            {
                orm.SaveChanges();
                var s = orm1.M_FARMER_REGISTRATION.Where(z => z.NICFARMERID == k.BeneficiaryDetail.FarmerID).Select(g => g.VCHFARMERNAME).FirstOrDefault();
                var t = orm.BeneficiaryDetails.Where(z => z.ReferenceNo == x.ReferenceNo).Select(i => new { i.NICDistrictCode, i.BeneficiaryProjectDetail.DepartmentCode }).FirstOrDefault();
                int p = Convert.ToInt32(t.NICDistrictCode);
                var r = orm.DNODetailEntries.Where(z => z.DepartmentCode == t.DepartmentCode && z.DistrictCode == p).Select(d => d.MobileNo).FirstOrDefault();
                var q = Convert.ToString(k.BLODate.Value.AddDays(15));
                string h = q.ToString().Substring(0, 10);
                string SMS = "APICOL - Feasibility Report of Smt / Sri " + s + " submitted. Upload Remarks by " + h;
                SMSGateway.SendSMS(r, SMS);
                return "Record is marked as feasible.";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string BLONotFeasible(BLORecordValidation x)
        {
            var k = orm.BLORecords.Where(a => a.ReferenceNo == x.ReferenceNo).FirstOrDefault();
            k.BLOStatus = 2;
            k.RejectionReason = x.RejectionReason;
            k.BLODate = DateTime.Now;
            k.IPAddress = HttpContext.Current.Request.UserHostAddress;
            k.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            try
            {
                orm.SaveChanges();
                var s = orm1.M_FARMER_REGISTRATION.Where(z => z.NICFARMERID == k.BeneficiaryDetail.FarmerID).Select(g => g.VCHFARMERNAME).FirstOrDefault();
                var t = orm.BeneficiaryDetails.Where(z => z.ReferenceNo == x.ReferenceNo).Select(i => new { i.NICDistrictCode, i.BeneficiaryProjectDetail.DepartmentCode }).FirstOrDefault();
                int p = Convert.ToInt32(t.NICDistrictCode);
                var r = orm.DNODetailEntries.Where(z => z.DepartmentCode == t.DepartmentCode && z.DistrictCode == p).Select(d => d.MobileNo).FirstOrDefault();
                var q = Convert.ToString(k.BLODate.Value.AddDays(15));
                string h = q.ToString().Substring(0, 10);
                string SMS = "APICOL - Feasibility Report of Smt / Sri " + s + " submitted. Upload Remarks by " + h;
                SMSGateway.SendSMS(r, SMS);
                return "Record is marked as not feasible.";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public FeasibilityReportVM GetFeasibilityReport(string referenceNo)
        {
            return orm.FeasibilityReports.Where(z => z.ReferenceNo == referenceNo).Select(x => new FeasibilityReportVM { ReferenceNo = x.ReferenceNo, PreviousExperience = x.PreviousExperience, Latitude = x.Latitude, Longitude = x.Longitude, RoadConnectivity = x.RoadConnectivity, DistanceFromVillage = x.DistanceFromVillage, ElectrificationStatus = x.ElectrificationStatus, PollutionControlClearanceStatus = x.PollutionControlClearanceStatus, SelfFinance = x.SelfFinance, UncensoredLoan = x.UncensoredLoan, MarginComponent = x.MarginComponent, BankConsentLetterStatus = x.BankConsentLetterStatus, UserName = x.UserName, Photograph = x.Photo, ProjectName = x.BeneficiaryDetail.BeneficiaryProjectDetail.ProjectType.Name, Capacity = x.BeneficiaryDetail.BeneficiaryProjectDetail.Capacity, CapacityUnit = x.BeneficiaryDetail.BeneficiaryProjectDetail.CapacityUnit, TotalProjectCost = x.BeneficiaryDetail.CapitalInvestment.TotalCapitalInvestmentCost }).FirstOrDefault();
        }

        public string BloIntegrationInsert(IntegratedProjectLog IP, string username)
        {
            var k = orm.BeneficiaryProjectDetails.Where(z => z.ReferenceNo == IP.PreviousReferenceNo).FirstOrDefault();
            var l = orm.BeneficiaryDetails.Where(z => z.ReferenceNo == IP.PreviousReferenceNo).FirstOrDefault();
            var b = orm.BLORecords.Where(z => z.ReferenceNo == IP.PreviousReferenceNo).FirstOrDefault();
            IP.PreviousUserName = username;
            if (IP.NewDeptCode == "01")
            {
                IP.NewUserName = "aao_" + username.Substring(4, 4);
            }
            if (IP.NewDeptCode == "02")
            {
                IP.NewUserName = "aho_" + username.Substring(4, 4);
            }
            if (IP.NewDeptCode == "03")
            {
                IP.NewUserName = "afo_" + username.Substring(4, 4);
            }
            IP.PreviousDeptCode = k.DepartmentCode;
            IP.UserDateTime = DateTime.Now;
            IP.IPAddress = HttpContext.Current.Request.UserHostAddress;
            IP.FinancialYear = GenerateReferencenumber.GetCurrentFinancialYear();
            b.BLOUserName = IP.NewUserName;
            orm.IntegratedProjectLogs.Add(IP);
            try
            {
                orm.SaveChanges();
                var ipSMS = orm.IntegratedProjectDetails.Where(z => z.ReferenceNo == IP.PreviousReferenceNo && z.BLOUserName.Substring(0, 3) != "bvo").Select(x => x.BLOUserName).Distinct().ToList();
                foreach (var i in ipSMS)
                {
                    int bCode = Convert.ToInt32(i.Substring(4, 4));
                    string deptcode = i.Substring(0, 3) == "aao" ? "01" : i.Substring(0, 3) == "aho" ? "02" : "03";
                    var mobileNo = orm.BLODetailEntries.Where(z => z.DepartmentCode == deptcode && z.BlockCode == bCode).Select(z => z.MobileNo).FirstOrDefault();
                    if (mobileNo != null)
                    {
                        string SMS = "APICOL - The Reference No. " + IP.PreviousReferenceNo + " having Integrated Project has been sent to you for your consent.";
                        SMSGateway.SendSMS(mobileNo, SMS);
                    }
                }
                return "Project is sent to the concerned BLO.";
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return null;
            }
        }

        public BLONameSign GetBLODetail(int blockCode, string departmentCode)
        {
            var k = orm.BLODetailEntries.Where(z => z.BlockCode == blockCode && z.DepartmentCode == departmentCode).Select(x => new BLONameSign { Name = x.Name, Signature = x.Signature }).FirstOrDefault();
            if (k != null)
            {
                return k;
            }
            else
            {
                return null;
            }
        }

        public string CheckCapacity(string projectName, decimal? capacity)
        {
            var k = orm.ProjectTypes.Where(z => z.Name == projectName).Select(x => x.MinCapacity).FirstOrDefault();
            if (capacity >= k)
            {
                return "OK";
            }
            else
            {
                return "The Capacity must be equal to or above " + k;
            }
        }

        public string UpdateProjectInformations(ProjectInformations x, string userID)
        {
            var resC = "OK";
            var k = orm.BeneficiaryProjectDetails.Where(z => z.ReferenceNo == x.ReferenceNo).FirstOrDefault();
            if (k.Capacity != null && x.Capacity == null)
            {
                return "Capacity must be entered.";
            }
            if (x.FinanceType == "Bank" && (Convert.ToInt32(x.BankCode) == 0 || x.BankCode == null || Convert.ToInt32(x.BranchCode) == 0 || x.BranchCode == null || x.BankLoan == 0))
            {
                return "Please fill all the fields.";
            }
            if (x.Capacity != null)
            {
                resC = CheckCapacity(x.ProjectName, x.Capacity);
            }
            if (resC == "OK")
            {
                if (x.FinanceType == "Self")
                {
                    x.BankLoan = 0;
                }
                decimal? cost = 0;
                decimal? totalCost = 0;
                decimal d = 0;
                cost = x.BankLoan == 0 ? x.OwnContribution : x.OwnContribution + x.BankLoan;
                totalCost = cost;
                RegistrationBAL obj = new RegistrationBAL();
                if ((totalCost >= 1000000.00M && x.FinanceType == "Bank" && x.BankLoan >= totalCost / 10) || (totalCost < 1000000.00M && x.ApproximateCost == "Below 1 Crore"))
                {
                    if (obj.CheckContribution(x.ApproximateCost, Convert.ToDecimal(totalCost)) == "")
                    {
                        if (userID != null && k != null)
                        {
                            BLORegdLog brl = new BLORegdLog();
                            brl.ReferenceNo = k.ReferenceNo;
                            brl.ProductToBeProducedOrMarketed = k.ProductToBeProducedOrMarketed;
                            brl.Capacity = k.Capacity;
                            brl.FinanceType = k.FinanceType;
                            brl.BankCode = k.BankCode;
                            brl.BranchCode = k.BranchCode;
                            brl.BankConsentLetter = k.BankConsentLetter;
                            brl.SubsidyAmount = k.BeneficiaryDetail.SubsidyAmount;
                            brl.BankLoan = k.BankLoan;
                            brl.ApproximateCost = k.ApproximateCost;
                            brl.OwnContribution = k.OwnContribution;
                            brl.BankLoan = k.BankLoan;
                            brl.BLOUserName = userID;
                            brl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                            brl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                            brl.UserDateTime = DateTime.Now;
                            orm.BLORegdLogs.Add(brl);
                        }
                        if (k != null)
                        {
                            k.ProductToBeProducedOrMarketed = x.ProductToBeProducedOrMarketed;
                            if (x.Capacity != null)
                            {
                                k.Capacity = x.Capacity;
                                var fd = orm1.M_FARMER_REGISTRATION.Where(a => a.NICFARMERID == k.BeneficiaryDetail.FarmerID).FirstOrDefault();
                                k.BeneficiaryDetail.SubsidyAmount = obj.CalculateSubsidy(k.ProjectTypeCode, x.Capacity, fd.INTGENDER, fd.INTCATEGOGY, Convert.ToDecimal(totalCost), k.BeneficiaryDetail.PercentageOfSubsidy);
                            }
                            else
                            {
                                d = Convert.ToDecimal(totalCost * k.BeneficiaryDetail.PercentageOfSubsidy / 100);
                                k.BeneficiaryDetail.SubsidyAmount = Math.Round(d, 2);
                            }
                            k.FinanceType = x.FinanceType;
                            if (x.FinanceType == "Bank")
                            {
                                k.BankCode = x.BankCode;
                                k.BranchCode = x.BranchCode;
                                k.BankLoan = x.BankLoan;
                            }
                            else
                            {
                                k.BankCode = null;
                                k.BranchCode = null;
                                k.BankConsentLetter = null;
                                k.BankLoan = null;
                            }
                            k.ApproximateCost = x.ApproximateCost;
                            k.OwnContribution = x.OwnContribution;
                        }
                        try
                        {
                            orm.SaveChanges();
                            return "Record updated successfully.";
                        }
                        catch (Exception ex)
                        {
                            var excep = ex.Message.ToString();
                            return excep;
                        }
                    }
                    else
                    {
                        return "Approximate Cost must be compatible with Total Cost.";
                    }
                }
                else
                {
                    return "Finance Type must be 'Bank' and Bank Loan must be greater than or equal to 10 % of Rs " + totalCost + " (Total Cost)";
                }
            }
            else
            {
                return resC;
            }
        }

        public bool UpdateApplicantDetails(ApplicantDetails x, string userID)
        {
            var k = orm.BeneficiaryDetails.Where(z => z.ReferenceNo == x.ReferenceNo).FirstOrDefault();
            if (userID != null && k != null)
            {
                BLORegdLog brl = new BLORegdLog();
                brl.ReferenceNo = k.ReferenceNo;
                brl.AnnualIncome = k.AnnualIncome;
                brl.PresentOccupation = k.PresentOccupation;
                brl.PreviousExperience = k.PreviousExperience;
                if (k.BeneficiaryType == "Group")
                {
                    brl.GroupTypeCode = k.GroupTypeCode;
                    brl.FirmName = k.FirmName;
                }
                brl.BLOUserName = userID;
                brl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                brl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                brl.UserDateTime = DateTime.Now;
                orm.BLORegdLogs.Add(brl);
            }
            if (k != null)
            {
                k.AnnualIncome = x.AnnualIncome;
                k.PresentOccupation = x.PresentOccupation;
                k.PreviousExperience = x.PreviousExperience;
                if (k.BeneficiaryType == "Group")
                {
                    k.GroupTypeCode = x.GroupTypeCode;
                    k.FirmName = x.FirmName;
                }
            }
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

        public bool UpdateApplicantAddressDetails(ApplicantAddressDetails x, string userID)
        {
            RegistrationBAL obj = new RegistrationBAL();
            if (x.EmailID == null || obj.CheckEmailID(x.ReferenceNo, x.EmailID) == null)
            {
                var k = orm.BeneficiaryDetails.Where(z => z.ReferenceNo == x.ReferenceNo).FirstOrDefault();
                if (userID != null && k != null)
                {
                    BLORegdLog brl = new BLORegdLog();
                    brl.ReferenceNo = k.ReferenceNo;
                    brl.CorrespondenceAddress = k.CorrespondenceAddress;
                    brl.PermanentAddress = k.PermanentAddress;
                    brl.Pin = k.Pin;
                    brl.EmailID = k.EmailID;
                    brl.BLOUserName = userID;
                    brl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                    brl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                    brl.UserDateTime = DateTime.Now;
                    orm.BLORegdLogs.Add(brl);
                }
                if (k != null)
                {
                    k.CorrespondenceAddress = x.CorrespondenceAddress;
                    k.PermanentAddress = x.PermanentAddress;
                    k.Pin = x.Pin;
                    k.EmailID = x.EmailID;
                }
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
            else
            {
                return false;
            }
        }

        public bool UpdateFeasibility(FeasibilityReport x, string userID)
        {
            var k = orm.FeasibilityReports.Where(z => z.ReferenceNo == x.ReferenceNo).FirstOrDefault();
            if (userID != null && k != null)
            {
                BLOFeasibilityLog bfl = new BLOFeasibilityLog();
                bfl.ReferenceNo = k.ReferenceNo;
                bfl.PreviousExperience = k.PreviousExperience;
                bfl.RoadConnectivity = k.RoadConnectivity;
                bfl.ElectrificationStatus = k.ElectrificationStatus;
                bfl.PollutionControlClearanceStatus = k.PollutionControlClearanceStatus;
                bfl.BankConsentLetterStatus = k.BankConsentLetterStatus;
                bfl.DistanceFromVillage = k.DistanceFromVillage;
                bfl.SelfFinance = k.SelfFinance;
                bfl.UncensoredLoan = k.UncensoredLoan;
                bfl.MarginComponent = k.MarginComponent;
                bfl.BLOUserName = userID;
                bfl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                bfl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                bfl.UserDateTime = DateTime.Now;
                orm.BLOFeasibilityLogs.Add(bfl);
            }
            if (k != null)
            {
                k.PreviousExperience = x.PreviousExperience;
                k.RoadConnectivity = x.RoadConnectivity;
                k.ElectrificationStatus = x.ElectrificationStatus;
                k.PollutionControlClearanceStatus = x.PollutionControlClearanceStatus;
                k.BankConsentLetterStatus = x.BankConsentLetterStatus;
                k.DistanceFromVillage = x.DistanceFromVillage;
                k.SelfFinance = x.SelfFinance;
                k.UncensoredLoan = x.UncensoredLoan;
                k.MarginComponent = x.MarginComponent;
            }
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

        public List<FeasibilitytoBLO> GetFeasibilityReportBLO(string userID)
        {
            var result = orm.IntegratedProjectDetails.Where(z => z.ReferenceNo == z.BeneficiaryDetail.BLORecord.ReferenceNo && z.BLOUserName == userID && (z.IPBLOStatus == 0 || z.IPBLOStatus == 1)).Except(orm.IntegratedProjectDetails.Where(z => z.BeneficiaryDetail.BLORecord.BLOUserName == z.BLOUserName)).Select(z => new FeasibilitytoBLO { ReferenceNo = z.ReferenceNo, FarmerID = z.BeneficiaryDetail.FarmerID, IPBLOStatus = z.IPBLOStatus }).Distinct().ToList();
            return result;
        }

        public List<IntegratedProjectDetail> GetDeptOfficer(string referenceNo)
        {
            return orm.IntegratedProjectDetails.Where(z => z.ReferenceNo == referenceNo && z.BLOUserName.Substring(0, 3) != "bvo").ToList();
        }

        public string MakeitFeasible(string refNo, string userID)
        {
            var k = orm.IntegratedProjectDetails.Where(z => z.ReferenceNo == refNo && z.BLOUserName == userID && z.IPBLOStatus == 0).Except(orm.IntegratedProjectDetails.Where(z => z.BeneficiaryDetail.BLORecord.BLOUserName == z.BLOUserName)).ToList();
            if (k != null)
            {
                var l = orm.BLORecords.Where(z => z.ReferenceNo == refNo).FirstOrDefault();
                if (l.MobileUploadStatus == 1)
                {
                    foreach (var i in k)
                    {
                        i.IPBLOStatus = 1;
                    }
                    try
                    {
                        orm.SaveChanges();
                        return "The record is accepted.";
                    }
                    catch (Exception e)
                    {
                        return e.ToString();
                    }
                }
                else
                {
                    return "The Feasibility Report has not been submitted by the concerned BLO.";
                }
            }
            else
            {
                return "All OK.";
            }
        }

        public string CheckFeasibilityPending(int blockCode, string refNo)
        {
            string bCode = Convert.ToString(blockCode);
            var m = orm.IntegratedProjectDetails.Where(z => z.BLOUserName.Substring(4, 4) == bCode && z.IPBLOStatus == 0 && z.ReferenceNo == refNo && z.BeneficiaryDetail.BLORecord.ReferenceNo == refNo).Except(orm.IntegratedProjectDetails.Where(z => z.BeneficiaryDetail.BLORecord.BLOUserName == z.BLOUserName)).Select(x => x.BLOUserName);
            var k = m.Count();
            var l = m.ToList();
            string kl = string.Empty;
            if (k > 0)
            {
                foreach (var i in l)
                {
                    if (kl == string.Empty)
                    {
                        kl = i;
                    }
                    else
                    {
                        kl = kl + ", " + i;
                    }
                }
                return "Feasibility Report is pending at " + kl;
            }
            else
            {
                return "All OK";
            }
        }
    }

    public class BLONameSign
    {
        public string Name { get; set; }
        public byte[] Signature { get; set; }
        public string Sign { get { return Convert.ToBase64String(Signature); } }
    }

    public class FeasibilitytoBLO
    {
        public string ReferenceNo { get; set; }
        public string FarmerID { get; set; }
        public int IPBLOStatus { get; set; }
    }
}