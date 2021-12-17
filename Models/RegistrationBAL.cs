using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CommercialAgriEnterprise.Models
{
    public class RegistrationBAL
    {
        CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();
        FARMERDBEntities orm1 = new FARMERDBEntities();

        public List<EducationalQualification> ListEducationalQualification()
        {
            return orm.EducationalQualifications.ToList();
        }

        public List<Department> ListDepartment()
        {
            return orm.Departments.ToList();
        }

        public List<ProjectType> ProjectType()
        {
            return orm.ProjectTypes.Where(z => z.Code != "04P15").ToList();
        }

        public List<ProjectType> AllProjectType()
        {
            return orm.ProjectTypes.ToList();
        }

        public List<GroupType> GetGroupTypes()
        {
            return orm.GroupTypes.ToList();
        }

        public List<ProjectType> AllProjectType(string _deptCode)
        {
            return orm.ProjectTypes.Where(g => g.DepartmentCode == _deptCode).ToList();
        }

        public List<ProjectType> ListProjectType(string departmentCode)
        {
            return orm.ProjectTypes.Where(z => z.DepartmentCode == departmentCode).ToList();
        }

        public List<ProjectType> ListCapacityUnit()
        {
            return orm.ProjectTypes.Where(z => z.CapacityUnit != null).OrderBy(a => a.CapacityUnit).ToList();
        }

        public List<M_PDS_FARMERBANK> listBank()
        {
            return orm1.M_PDS_FARMERBANK.Where(z => z.intDeletedFlag == 0).ToList();
        }

        public List<M_PDS_BANKBRANCH> listBranch(int bankCode)
        {
            return orm1.M_PDS_BANKBRANCH.Where(z => z.intBankId == bankCode && z.intDeletedFlag == 0).ToList();
        }

        public List<M_PDS_BANKBRANCH> GetAllBranch()
        {
            return orm1.M_PDS_BANKBRANCH.Where(z => z.intDeletedFlag == 0).ToList();
        }

        public List<M_PDS_BANKBRANCH> GetAllBranch(int bankCode)
        {
            return orm1.M_PDS_BANKBRANCH.Where(z => z.intBankId == bankCode && z.intDeletedFlag == 0).ToList();
        }

        public List<LGDDistrict> GetDistrict()
        {
            return orm.LGDDistricts.ToList();
        }

        public List<LGDBlock> GetBlockAll(string DistCode)
        {
            int dstcd = Convert.ToInt32(DistCode);
            return orm.LGDBlocks.Where(z => z.DistrictCode == dstcd).ToList();
        }
        public List<LGDGP> GetGPAll(string BlockCode)
        {
            int blkcd = Convert.ToInt32(BlockCode);
            return orm.LGDGPs.Where(z => z.BlockCode == blkcd).ToList();
        }

        public List<LGDVillage> GetVillageAll(string Gp)
        {
            int gpcd = Convert.ToInt32(Gp);
            return orm.LGDVillages.Where(z => z.GPCode == gpcd).ToList();
        }

        public List<rDistrict> GetRDistrict()
        {
            return orm.rDistricts.ToList();
        }

        public List<rTehsil> GetRTehsil(string rDCode)
        {
            return orm.rTehsils.Where(z => z.DCode == rDCode).ToList();
        }

        public List<riCircle> GetRICircle(string rDCode, string rTCode)
        {
            return orm.riCircles.Where(z => z.DCode == rDCode && z.TCode == rTCode).ToList();
        }

        public List<rVillage> GetRVillage(string dCode, string tCode)
        {
            return orm.rVillages.Where(z => z.DCode == dCode && z.TCode == tCode).ToList();
        }

        public List<LGDVillage> VillageList(string farmedID, string refno)
        {
            int distcode = orm.LGDDistricts.Where(a => a.DistrictName.Substring(0, 3) == refno.Substring(0, 3)).Select(a => a.DistrictCode).FirstOrDefault();
            return orm.LGDVillages.Where(a => a.DistrictCode == distcode).ToList();
        }

        public List<LGDGP> GPList(string farmedID, string refno)
        {
            int distcode = orm.LGDDistricts.Where(a => a.DistrictName.Substring(0, 3) == refno.Substring(0, 3)).Select(a => a.DistrictCode).FirstOrDefault();
            return orm.LGDGPs.Where(a => a.DistrictCode == distcode).ToList();
        }

        public List<LGDGP> GPPopulate(int blockCode)
        {
            return orm.LGDGPs.Where(z => z.BlockCode == blockCode).ToList();
        }

        public List<LGDVillage> VillagePopulate(int gpCode)
        {
            return orm.LGDVillages.Where(z => z.GPCode == gpCode).ToList();
        }

        public List<Relationship> ListRelationship()
        {
            return orm.Relationships.ToList();
        }

        public List<FarmerFamilyRelation_Result> FIDRelationship(string farmerID)
        {
            var fCode = orm1.M_FARMER_REGISTRATION.Where(z => z.NICFARMERID == farmerID).Select(x => x.VCHFARMERCODE).FirstOrDefault();
            return orm1.FarmerFamilyRelation(fCode).ToList();
        }

        public string CheckExistFarmerID(string farmerID)
        {
            var existFarmerID = orm.BeneficiaryDetails.Where(z => z.FarmerID.ToUpper() == farmerID.ToUpper()).Select(z => z.FarmerID).FirstOrDefault();
            var groupExistFarmerID = orm.BeneficiaryMemberDetails.Where(z => z.GroupMemberFarmerID.ToUpper() == farmerID.ToUpper()).Select(z => z.GroupMemberFarmerID).FirstOrDefault();
            string msg = "";
            if ((existFarmerID != null && existFarmerID != "") || (groupExistFarmerID != null && groupExistFarmerID != ""))
            {
                return msg = "Farmer ID already exists.";
            }
            else
            {
                return msg;
            }
        }

        public CAEFarmerDBVM GetApplicantDetail(string farmerID, string rType, int? relation)
        {
            var validateFarmerID = orm1.M_FARMER_REGISTRATION.Where(z => z.NICFARMERID == farmerID).FirstOrDefault();
            if (validateFarmerID != null)
            {
                string genderCode = string.Empty;
                string genderValue = string.Empty;
                string farmerName = string.Empty;
                string voterID = string.Empty;
                string aadhaarNo = string.Empty;
                string fName = string.Empty;
                if (rType == "Relative")
                {
                    var k = orm1.FarmerFamilyDetails(validateFarmerID.VCHFARMERCODE, relation).ToList();
                    foreach (var i in k)
                    {
                        if (k.Count == 1) { fName = i.VCHNAME; }
                        else { fName = fName + "," + i.VCHNAME; }
                    }
                    farmerName = fName.TrimStart(',');
                    if (relation == 1 || relation == 4 || relation == 5 || relation == 7 || relation == 10 || relation == 11)
                    {
                        genderCode = "1"; genderValue = "Male";
                    }
                    else
                    {
                        genderCode = "2"; genderValue = "Female";
                    }
                    voterID = null;
                    aadhaarNo = null;
                }
                else if (rType == "Self")
                {
                    farmerName = validateFarmerID.VCHFARMERNAME;
                    string genderID = Convert.ToString(validateFarmerID.INTGENDER);
                    var gender = orm1.Tbl_Gender.Where(z => z.Gender_ID == genderID).FirstOrDefault();
                    genderCode = gender.Gender_ID;
                    genderValue = gender.Gender_Value;
                    if (validateFarmerID.VCHVOTERIDCARDNO.Length > 4)
                    {
                        voterID = "******" + validateFarmerID.VCHVOTERIDCARDNO.Substring(validateFarmerID.VCHVOTERIDCARDNO.Length - 4, 4);
                    }
                    if (validateFarmerID.VCHAADHARNO.Length > 4)
                    {
                        aadhaarNo = "********" + validateFarmerID.VCHAADHARNO.Substring(validateFarmerID.VCHAADHARNO.Length - 4, 4);
                    }
                }
                string categoryID = Convert.ToString(validateFarmerID.INTCATEGOGY);
                var category = orm1.Tbl_Category.Where(z => z.Category_ID == categoryID).FirstOrDefault();
                return orm1.M_FARMER_REGISTRATION.Where(z => z.NICFARMERID == farmerID).Select(x => new CAEFarmerDBVM
                {
                    FarmerID = x.NICFARMERID,
                    ApplicantName = farmerName == null ? "NA" : farmerName,
                    FatherHusbandName = x.VCHFATHERNAME == null ? "NA" : x.VCHFATHERNAME,
                    GenderID = genderCode,
                    GenderName = genderValue,
                    CategoryID = category.Category_ID,
                    CategoryName = category.Category_Value,
                    VoterIDCardNo = x.VCHVOTERIDCARDNO == null ? "NA" : voterID,
                    AadharIDCardNo = x.VCHAADHARNO == null ? "NA" : aadhaarNo,
                    MobileNo = x.VCHMOBILENO == null ? "NA" : x.VCHMOBILENO,
                }).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public string Chk_Emailid(string EmailID)
        {
            var k = orm.BeneficiaryDetails.Where(a => a.EmailID == EmailID).FirstOrDefault();
            if (k != null)
            {
                return "This EmailID is already in use.";
            }
            else
            {
                return null;
            }
        }

        public string CheckEmailID(string referenceNo, string emailID)
        {
            var k = orm.BeneficiaryDetails.Where(z => z.ReferenceNo == referenceNo && z.EmailID == emailID).FirstOrDefault();
            if (k != null)
            {
                return null;
            }
            else
            {
                return Chk_Emailid(emailID);
            }
        }

        public string CheckRefGroupMemberFarmerID(List<BeneficiaryMemberDetail> memberDetail, string farmerID, string referenceNo)
        {
            string m = string.Empty;
            if (referenceNo != null)
            {
                if (memberDetail != null)
                {
                    if (memberDetail.Count > 0)
                    {
                        foreach (var i in memberDetail)
                        {
                            var s = orm.BeneficiaryDetails.Where(h => h.FarmerID == i.GroupMemberFarmerID).Select(x => x.FarmerID).FirstOrDefault();
                            var k = orm.BeneficiaryMemberDetails.Where(g => g.ReferenceNo == referenceNo && g.GroupMemberFarmerID == i.GroupMemberFarmerID).Select(x => x.GroupMemberFarmerID).FirstOrDefault();
                            int index = memberDetail.IndexOf(i);
                            if (i.GroupMemberFarmerID == null)
                            {
                                return m = "Please fill all fields !!!";
                            }
                            var res = memberDetail.Distinct().GroupBy(a => a.GroupMemberFarmerID).ToList();
                            if (res.Count != memberDetail.Count)
                            {
                                return m = "Farmer ID should be different";
                            }
                            if (s != null)
                            {
                                return m = i.GroupMemberFarmerID + " already exists";
                            }
                            if (k != null)
                            {
                                m = "";
                            }
                            if (s == null && k == null)
                            {
                                var BGM = orm.BeneficiaryMemberDetails.Where(z => z.GroupMemberFarmerID == i.GroupMemberFarmerID).Except(orm.BeneficiaryMemberDetails.Where(z => z.ReferenceNo == referenceNo)).FirstOrDefault();
                                var M = orm1.M_FARMER_REGISTRATION.Where(z => z.NICFARMERID == i.GroupMemberFarmerID).FirstOrDefault();
                                if (M == null)
                                {
                                    return m = i.GroupMemberFarmerID + " is invalid";
                                }
                                if (BGM != null)
                                {
                                    return m = i.GroupMemberFarmerID + " already exists";
                                }
                            }
                        }
                        return m;
                    }
                    return m;
                }
                else
                {
                    return m;
                }
            }
            else
            {
                return CheckGroupMemberFarmerID(memberDetail, farmerID);
            }
        }

        public string CheckGroupMemberFarmerID(List<BeneficiaryMemberDetail> memberDetail, string farmerID)
        {
            string message = "";
            int x = 0;
            if (memberDetail != null)
            {
                if (memberDetail.Count > 0)
                {
                    foreach (var i in memberDetail)
                    {
                        int index = memberDetail.IndexOf(i);
                        if (i.GroupMemberFarmerID == null)
                        {
                            return message = "Please fill all fields !!!";
                        }
                    }
                    var res = memberDetail.Distinct().GroupBy(a => a.GroupMemberFarmerID).ToList();
                    if (res.Count != memberDetail.Count)
                    {
                        return message = "Farmer ID should be different";
                    }
                    if (message == "")
                    {
                        foreach (var i in memberDetail)
                        {
                            var chkid = orm.BeneficiaryMemberDetails.Where(z => z.GroupMemberFarmerID == i.GroupMemberFarmerID).Select(z => z.GroupMemberFarmerID).FirstOrDefault();
                            var gchkid = orm.BeneficiaryDetails.Where(z => z.FarmerID == i.GroupMemberFarmerID).Select(g => g.FarmerID).FirstOrDefault();
                            if (chkid == null && gchkid == null)
                            {
                                if (i.GroupMemberFarmerID != null)
                                {
                                    if (i.GroupMemberFarmerID.ToUpper() == farmerID.ToUpper())
                                    {
                                        return message = "Group member farmer ID should not match with " + farmerID.ToUpper();
                                    }
                                    var k = orm1.M_FARMER_REGISTRATION.Where(z => z.NICFARMERID == i.GroupMemberFarmerID).Select(a => a.NICFARMERID).FirstOrDefault();
                                    if (k == null)
                                    {
                                        x++;
                                        message = message + i.GroupMemberFarmerID.ToUpper() + ", ";
                                    }
                                }
                            }
                            else
                            {
                                if (chkid != null)
                                {
                                    return message = "Group member farmer ID " + chkid + " already exists";
                                }
                                if (gchkid != null)
                                {
                                    return message = "Group member farmer ID " + gchkid + " already exists";
                                }
                            }
                        }
                        if (x == 1)
                        {
                            message = message.Replace(", ", " ") + "is invalid";
                        }
                        if (x > 1)
                        {
                            message = message + " are invalid";
                        }
                    }
                }
                return message;
            }
            else
            {
                return message;
            }
        }

        public decimal GenerateSubsidyforGroup(int? gender, int? category, string educationalQualification)
        {
            decimal subsidy = 0;
            if (gender == 1 && category == 1)
            {
                if (educationalQualification == "4" || educationalQualification == "5")
                {
                    subsidy = 50;
                }
                else
                {
                    subsidy = 40;
                }
            }
            else
            {
                subsidy = 50;
            }
            return subsidy;
        }

        public decimal ReCheckSubsidy(List<MemberDetail> md)
        {
            decimal subsidy = 0;
            List<decimal> arrSubsidy = new List<decimal>();
            foreach (var i in md)
            {
                if (i.Gender == 1 && i.Category == 1)
                {
                    if (i.EQualification == "4")
                    {
                        subsidy = 50;
                        arrSubsidy.Add(subsidy);
                    }
                    else
                    {
                        subsidy = 40;
                        arrSubsidy.Add(subsidy);
                    }
                }
                else
                {
                    subsidy = 50;
                    arrSubsidy.Add(subsidy);
                }
            }
            var s = arrSubsidy.Min();
            return s;
        }

        public decimal GenerateSubsidy(int? gender, int? category, string educationalQualification, List<MemberDetail> md)
        {
            decimal subsidy = 0;
            if (md.Count != 0)
            {
                if (gender == 1 && category == 1)
                {
                    if (educationalQualification == "4")
                    {
                        subsidy = ReCheckSubsidy(md);
                    }
                    else
                    {
                        subsidy = 40;
                    }
                }
                else
                {
                    subsidy = ReCheckSubsidy(md);
                }
            }
            else
            {
                if (gender == 1 && category == 1)
                {
                    if (educationalQualification == "4")
                    {
                        subsidy = 50;
                    }
                    else
                    {
                        subsidy = 40;
                    }
                }
                else
                {
                    subsidy = 50;
                }
            }
            return subsidy;
        }

        public decimal CalculateSubsidy(string projectTypeCode, decimal? capacity, int? gender, int? category, decimal totalCost, decimal percentageOfSubsidy)
        {
            decimal? subsidyAmount = 0;
            decimal a = 0;
            decimal? Amount = 0;
            var k = orm.ProjectTypes.Where(z => z.Code == projectTypeCode).FirstOrDefault();
            if (k.CostPerUnitGenMale == null || k.CostPerUnitSCSTFemale == null)
            {
                a = totalCost * percentageOfSubsidy / 100;
                subsidyAmount = Math.Round(a, 2);
            }
            else
            {
                if (gender == 1 && category == 1)
                {
                    subsidyAmount = capacity * k.CostPerUnitGenMale;
                }
                else
                {
                    subsidyAmount = capacity * k.CostPerUnitSCSTFemale;
                }
            }
            Amount = Math.Round(Convert.ToDecimal(subsidyAmount), 2);
            return Convert.ToDecimal(Amount);
        }

        public string CheckTarget(string projectTypeCode, string farmerid)
        {
            if (projectTypeCode == "01P1" || projectTypeCode == "04P20" || projectTypeCode == "04P16")
            {
                dynamic p = null;
                var g = orm.LGDDistricts.Where(z => z.PDSDistrictName.Substring(0, 3).ToUpper() == farmerid.Substring(0, 3).ToUpper()).Select(z => z.DistrictCode).FirstOrDefault();
                var l = orm1.M_FARMER_REGISTRATION.Where(z => z.NICFARMERID == farmerid).Select(z => z.INTCATEGOGY).FirstOrDefault();
                var k = orm.BeneficiaryDetails.Where(z => z.FarmerID.Substring(0, 3).ToUpper() == farmerid.Substring(0, 3).ToUpper() && z.BeneficiaryProjectDetail.ProjectTypeCode == projectTypeCode && z.PaymentStatus == "Success").Count();
                var j = orm.Targets.Where(z => z.DistrictCode == g && z.ProjectTypeCode == projectTypeCode).FirstOrDefault();
                if (j != null)
                {
                    if (l == 1) { p = j.GeneralValue; } else if (l == 2) { p = j.SCValue; } else if (l == 3) { p = j.STValue; }
                    if (l == 1) { if (k < p) { return "OK"; } else { return "Not OK"; } }
                    else if (l == 2) { if (k < p) { return "OK"; } else { return "Not OK"; } }
                    else if (l == 3) { if (k < p) { return "OK"; } else { return "Not OK"; } }
                    else { return null; }
                }
                else
                {
                    return "The target value has not been set. Please try after sometime.";
                }
            }
            else
            {
                return "OK";
            }
        }

        public string Submitapplicationandprojectdetails(BeneficiaryDetail BD, List<BeneficiaryMemberDetail> memberDetails, List<IntegratedProjectDetail> IPD, BeneficiaryProjectDetail BPD, decimal totalcost)
        {
            if (BD.RelationWithFIDType == "Relative")
            {
                var rfDetails = orm1.M_FARMER_REGISTRATION.Where(z => z.VCHVOTERIDCARDNO == BD.RVoterIDNo || z.VCHAADHARNO == BD.RAadhaarNo).Select(z => z.NICFARMERID).FirstOrDefault();
                if (rfDetails != null)
                {
                    var chkRFID = orm.BeneficiaryDetails.Where(z => z.FarmerID == rfDetails).FirstOrDefault();
                    if (chkRFID != null)
                    {
                        return "You have already applied for a project with Farmer ID " + chkRFID.FarmerID;
                    }
                    return "You already possess a Farmer ID. Please apply for the project using the same.";
                }
            }
            else
            {
                var fid = orm1.M_FARMER_REGISTRATION.Where(z => z.NICFARMERID == BD.FarmerID).FirstOrDefault();
                var checkIDs = orm.BeneficiaryDetails.Where(z => z.RAadhaarNo == fid.VCHAADHARNO || z.RVoterIDNo == fid.VCHVOTERIDCARDNO).FirstOrDefault();
                if (checkIDs != null)
                {
                    return "You have already applied as the relative of one's Farmer ID.";
                }
            }
            if ((BD.RelationWithFIDType == "Self" && BD.RelationWithFID == null && BD.RelationApplicantName == null && BD.RAadhaarNo == null && BD.RVoterIDNo == null) || (BD.RelationWithFIDType == "Relative" && BD.RelationWithFID != null && BD.RAadhaarNo != null && BD.RelationApplicantName != null && BD.RVoterIDNo != null))
            {
                if (BPD.CISAmount == null)
                {
                    BPD.CISAmount = 0;
                }
                if (BPD.CISAmount < 5000000)
                {
                    if ((BPD.IsCISAvailedPreviously == "No" && BPD.CISProjectTypeCode == null && BPD.CISLocation == null && BPD.CISAvailedYear == null && BPD.CISFinanceType == null && BPD.CISAmount == 0 && BPD.CISBankClearanceCertificate == null) || (BPD.IsCISAvailedPreviously == "Yes" && BPD.CISProjectTypeCode != null && BPD.CISLocation != null && BPD.CISAvailedYear != null && BPD.CISAmount != 0 && ((BPD.CISFinanceType == "Self" && BPD.CISBankClearanceCertificate == null) || (BPD.CISFinanceType == "Bank" && BPD.CISBankClearanceCertificate != null))))
                    {
                        if ((BPD.ProjectTypeCode == "04P15" && IPD != null) || (BPD.ProjectTypeCode != "04P15" && IPD == null))
                        {
                            dynamic imsg = "";
                            if (IPD != null)
                            {
                                foreach (var i in IPD)
                                {
                                    if ((i.Capacity == null && i.CapacityUnit != null) || (i.Capacity != null && i.CapacityUnit == null))
                                    {
                                        imsg = "The Capacity must be entered incase of Integrated Farming.";
                                    }
                                }
                                if (imsg != "")
                                {
                                    return imsg;
                                }
                            }
                            string returnMsg = CheckTarget(BPD.ProjectTypeCode, BD.FarmerID);
                            if (returnMsg == "OK")
                            {
                                List<MemberDetail> md = new List<MemberDetail>();
                                var k = CheckGroupMemberFarmerID(memberDetails, BD.FarmerID);
                                if (k == "" && BD.EmailID != null ? Chk_Emailid(BD.EmailID) == null : BD.EmailID == null)
                                {
                                    if ((totalcost >= 1000000.00M && BPD.FinanceType == "Bank" && BPD.BankLoan >= totalcost / 10) || (totalcost < 1000000.00M && BPD.ApproximateCost == "Below 1 Crore"))
                                    {
                                        if (CheckContribution(BPD.ApproximateCost, totalcost) == "")
                                        {
                                            if (BD.BeneficiaryType == "Group" && BD.GroupTypeCode != null && BD.FirmName != null && memberDetails.Count != 0)
                                            {
                                                foreach (var item in memberDetails)
                                                {
                                                    MemberDetail mDtls = new MemberDetail();
                                                    var fDetails = orm1.M_FARMER_REGISTRATION.Where(z => z.NICFARMERID == item.GroupMemberFarmerID).FirstOrDefault();
                                                    mDtls.FarmerID = item.GroupMemberFarmerID;
                                                    mDtls.EQualification = item.HighestEducationalQualificationCode;
                                                    mDtls.Category = fDetails.INTCATEGOGY;
                                                    mDtls.Gender = fDetails.INTGENDER;
                                                    md.Add(mDtls);
                                                }
                                            }
                                            var farmerDtls = orm1.M_FARMER_REGISTRATION.Where(a => a.NICFARMERID == BD.FarmerID).FirstOrDefault();
                                            BD.FVoterIDNo = farmerDtls.VCHVOTERIDCARDNO;
                                            BD.FAadhaarNo = farmerDtls.VCHAADHARNO;
                                            BD.DateOfRegd = DateTime.Now;
                                            int? gender = null;
                                            if (BD.RelationWithFID != null)
                                            {
                                                if (BD.RelationWithFID == 1 || BD.RelationWithFID == 4 || BD.RelationWithFID == 5 || BD.RelationWithFID == 7 || BD.RelationWithFID == 10 || BD.RelationWithFID == 11)
                                                {
                                                    gender = 1;
                                                }
                                                else
                                                {
                                                    gender = 2;
                                                }
                                            }
                                            if (BD.RelationWithFIDType == "Self")
                                            {
                                                BD.PercentageOfSubsidy = GenerateSubsidy(farmerDtls.INTGENDER, farmerDtls.INTCATEGOGY, BD.HighestEducationalQualificationCode, md);
                                            }
                                            else
                                            {
                                                BD.PercentageOfSubsidy = GenerateSubsidy(gender, farmerDtls.INTCATEGOGY, BD.HighestEducationalQualificationCode, md);
                                            }
                                            decimal g = 0;
                                            if (BPD.Capacity == null && BPD.CapacityUnit == null)
                                            {
                                                g = totalcost * BD.PercentageOfSubsidy / 100;
                                                BD.SubsidyAmount = Math.Round(g, 2);
                                            }
                                            else
                                            {
                                                if (BD.RelationWithFIDType == "Self")
                                                {
                                                    BD.SubsidyAmount = CalculateSubsidy(BPD.ProjectTypeCode, BPD.Capacity, farmerDtls.INTGENDER, farmerDtls.INTCATEGOGY, totalcost, BD.PercentageOfSubsidy);
                                                }
                                                else
                                                {
                                                    BD.SubsidyAmount = CalculateSubsidy(BPD.ProjectTypeCode, BPD.Capacity, gender, farmerDtls.INTCATEGOGY, totalcost, BD.PercentageOfSubsidy);
                                                }
                                            }
                                            if (BPD.ProjectTypeCode != "03P15")
                                            {
                                                BD.SubsidyAmount = BD.SubsidyAmount > 5000000.00M ? 5000000.00M : BD.SubsidyAmount;
                                            }
                                            else
                                            {
                                                BD.SubsidyAmount = BD.SubsidyAmount > 2500000.00M ? 2500000.00M : BD.SubsidyAmount;
                                            }
                                            BD.RegistrationStatus = "incomplete";
                                            BD.RegistrationAmount = Convert.ToDecimal(10000.00);
                                            BD.PaymentStatus = "Pending";
                                            BPD.ReferenceNo = GenerateReferencenumber.generaterefno(BD.FarmerID, BPD.DepartmentCode);
                                            BD.ReferenceNo = BPD.ReferenceNo;
                                            if (memberDetails != null)
                                            {
                                                foreach (var i in memberDetails)
                                                {
                                                    i.ReferenceNo = BPD.ReferenceNo;
                                                }
                                                orm.BeneficiaryMemberDetails.AddRange(memberDetails);
                                            }
                                            if (IPD != null)
                                            {
                                                foreach (var i in IPD)
                                                {
                                                    i.ReferenceNo = BD.ReferenceNo;
                                                    i.IPBLOStatus = 0;
                                                }
                                                orm.IntegratedProjectDetails.AddRange(IPD);
                                            }
                                            RegistrationLog rl = new RegistrationLog();
                                            rl.ReferenceNo = BPD.ReferenceNo;
                                            rl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
                                            rl.UserDateTime = DateTime.Now;
                                            rl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                                            rl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                                            try
                                            {
                                                orm.BeneficiaryDetails.Add(BD);
                                                orm.BeneficiaryProjectDetails.Add(BPD);
                                                orm.RegistrationLogs.Add(rl);
                                                orm.SaveChanges();
                                                string SMS = "APICOL - Your Reference No. is" + BD.ReferenceNo + ". Keep your Reference No. for future use.";
                                                SMSGateway.SendSMS(BD.MobileNo, SMS);
                                                return BPD.ReferenceNo;
                                            }
                                            catch (Exception ex)
                                            {
                                                return ex.ToString();
                                            }
                                        }
                                        else
                                        {
                                            return "";
                                        }
                                    }
                                    else
                                    {
                                        return "Finance Type must be 'Bank' and Bank Loan must be greater than or equal to 10% of Rs " + totalcost + " (Total Cost)";
                                    }
                                }
                                else
                                {
                                    return k;
                                }
                            }
                            else
                            {
                                if (returnMsg == "Not OK")
                                {
                                    return "No more projects can be alloted as the Target limit is reached.";
                                }
                                else
                                {
                                    return "The target value has not been set. Please try after sometime.";
                                }
                            }
                        }
                        else
                        {
                            return "Please enter the Integrated Projects to updated.";
                        }
                    }
                    else
                    {
                        return "Fill all the required details regarding CIS and upload the Bank Clearance Certificate.";
                    }
                }
                else
                {
                    return "You can't register as you have already received Rs. 5000000 subsidy.";
                }
            }
            else
            {
                return "Please fill up all the fields related to your Relations.";
            }
        }

        public string CheckContribution(string approximatecost, decimal totalcost)
        {
            string msg = string.Empty;
            if (approximatecost == "Below 1 Crore")
            {
                if (totalcost >= 10000000.00M)
                {
                    return msg = "Total cost must be below one crore";
                }
            }
            else
            {
                if (totalcost < 10000000.00M)
                {
                    return msg = "Total cost must be one crore or above";
                }
            }
            return msg;
        }

        public string SubmitDocumentdtls(BeneficiaryDocument mydata, BLOCheck y, DNOCheck z)
        {
            var m = orm.BeneFiciaryLandRecordDetails.Where(a => a.ReferenceNo == mydata.ReferenceNo).FirstOrDefault();
            if (m != null)
            {
                var t = (dynamic)null;
                var k = orm.BeneficiaryDetails.Where(a => a.ReferenceNo == mydata.ReferenceNo).FirstOrDefault();
                t = new BeneficiaryDocument { ReferenceNo = mydata.ReferenceNo, Photograph = mydata.Photograph, IdentityProof = mydata.IdentityProof, Signature = mydata.Signature };
                orm.BeneficiaryDocuments.Add(t);
                RegistrationLog rl = new RegistrationLog();
                rl.ReferenceNo = mydata.ReferenceNo;
                rl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
                rl.UserDateTime = DateTime.Now;
                rl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                rl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                orm.RegistrationLogs.Add(rl);
                try
                {
                    orm.SaveChanges();
                    return t.ReferenceNo;
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
            else
            {
                return null;
            }
        }

        public string SubmitRegistration(string referenceNo)
        {
            var p = orm.BeneficiaryDocuments.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (p != null)
            {
                var k = orm.BeneficiaryDetails.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
                if (k != null)
                {
                    k.RegistrationStatus = "completed";
                    BLORecord obj = new BLORecord();
                    obj.BLOStatus = 0;
                    obj.ReferenceNo = referenceNo;
                    obj.RegdStatus = true;
                    var L = orm.BeneficiaryDetails.Where(a => a.ReferenceNo == referenceNo).FirstOrDefault();
                    string department = L.ReferenceNo.Substring(4, 3);
                    string username = string.Empty;
                    if (department == "AGR")
                    {
                        username = "aao_" + L.NICBlockCode;
                        obj.BLOUserName = username;
                    }
                    else if (department == "ARD")
                    {
                        username = "bvo_" + L.NICBlockCode;
                        obj.BLOUserName = username;
                    }
                    else if (department == "FIS")
                    {
                        username = "afo_" + L.NICBlockCode;
                        obj.BLOUserName = username;
                    }
                    else if (department == "HOR")
                    {
                        username = "aho_" + L.NICBlockCode;
                        obj.BLOUserName = username;
                    }
                    orm.BLORecords.Add(obj);
                    try
                    {
                        orm.SaveChanges();
                        return k.ReferenceNo;
                    }
                    catch (Exception e)
                    {
                        return e.ToString();
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public FirstFormVM FirstSubmit(string refno)
        {
            try
            {
                var k = orm.BeneficiaryDetails.Where(a => a.ReferenceNo == refno).Select(a => new FirstFormVM { Apptype = a.BeneficiaryType, GroupTypeCode = a.GroupTypeCode, GroupTypeName = a.GroupType.Name, FirmName = a.FirmName, NoOfMembers = a.NoOfMember, Qualification = a.EducationalQualification.Name, Anualincome = a.AnnualIncome, PresentOccupation = a.PresentOccupation, PrevExp = a.PreviousExperience, EmailID = a.EmailID, Pincode = a.Pin, CorespondenceAddress = a.CorrespondenceAddress, PermanentAddress = a.PermanentAddress, FarmerID = a.FarmerID, QualificationID = a.HighestEducationalQualificationCode }).FirstOrDefault();
                return k;
            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public List<IProjectDetail> GetIProjects(string referenceNo)
        {
            return orm.IntegratedProjectDetails.Where(z => z.ReferenceNo == referenceNo).Select(x => new IProjectDetail { ReferenceNo = x.ReferenceNo, DepartmentCode = x.ProjectTypeCode.Substring(0, 2), ProjectTypeCode = x.ProjectTypeCode, Capacity = x.Capacity, Unit = x.CapacityUnit }).ToList();
        }

        public List<BeneficiaryMemberDetail> GetMemberIDs(string referenceNo)
        {
            return orm.BeneficiaryMemberDetails.Where(z => z.ReferenceNo == referenceNo).ToList();
        }

        public SecondFormVM SecondSubmit(string refno)
        {
            var k = orm.BeneficiaryProjectDetails.Where(a => a.ReferenceNo == refno).FirstOrDefault();
            if (k != null)
            {
                decimal? totalCost;
                if (k.BankLoan == null)
                {
                    totalCost = k.OwnContribution;
                }
                else
                {
                    totalCost = k.OwnContribution + k.BankLoan;
                }
                int bankCode = Convert.ToInt32(k.BankCode);
                int branchCode = Convert.ToInt32(k.BranchCode);
                string bank = orm1.M_PDS_FARMERBANK.Where(a => a.intId == bankCode).Select(a => a.vchBankName).FirstOrDefault();
                string branch = orm1.M_PDS_BANKBRANCH.Where(a => a.intBranchId == branchCode).Select(a => a.vchBranchName).FirstOrDefault();
                if (k.IsCISAvailedPreviously == "No")
                {
                    return orm.BeneficiaryProjectDetails.Where(a => a.ReferenceNo == refno).Select(a => new SecondFormVM { Department = a.Department.Name, ProductsToBeProduced = a.ProductToBeProducedOrMarketed, Capacity = a.Capacity, rbIsCISAvailed = a.IsCISAvailedPreviously, OwnContribution = a.OwnContribution, BankLoan = a.BankLoan, TotalCost = totalCost, CapacityUnit = a.CapacityUnit, DepartmentID = a.DepartmentCode, ProjectTypeID = a.ProjectTypeCode, FinanceType = a.FinanceType, Cisavailed = a.IsCISAvailedPreviously, approxCost = a.ApproximateCost, BankCode = a.BankCode, BranchCode = a.BranchCode, BankName = bank, BranchName = branch, BankConsentLetter = a.BankConsentLetter }).FirstOrDefault();
                }
                else
                {
                    return orm.BeneficiaryProjectDetails.Where(a => a.ReferenceNo == refno).Select(a => new SecondFormVM { Department = a.Department.Name, ProductsToBeProduced = a.ProductToBeProducedOrMarketed, Capacity = a.Capacity, rbIsCISAvailed = a.IsCISAvailedPreviously, OwnContribution = a.OwnContribution, BankLoan = a.BankLoan, TotalCost = totalCost, CapacityUnit = a.CapacityUnit, DepartmentID = a.DepartmentCode, ProjectTypeID = a.ProjectTypeCode, FinanceType = a.FinanceType, Cisavailed = a.IsCISAvailedPreviously, approxCost = a.ApproximateCost, BankCode = a.BankCode, BranchCode = a.BranchCode, BankName = bank, BranchName = branch, CISProjectTypeCode = a.CISProjectTypeCode, CISAmount = a.CISAmount, CISFinanceType = a.CISFinanceType, CISLocation = a.CISLocation, CISYear = a.CISAvailedYear, CISBankCertificate = a.CISBankClearanceCertificate, BankConsentLetter = a.BankConsentLetter }).FirstOrDefault();
                }
            }
            else
            {
                return null;
            }
        }

        public ThirdFormVM ThirdSubmit(string refno)
        {
            var k = orm.BeneFiciaryLandRecordDetails.Where(a => a.ReferenceNo == refno).FirstOrDefault();
            if (k != null)
            {
                var relation = orm.BeneFiciaryLandRecordDetails.Where(a => a.ReferenceNo == k.ReferenceNo).Select(a => a.RelationshipCode).FirstOrDefault();
                var distname = orm.LGDDistricts.Where(a => a.DistrictCode == k.DistrictCode).Select(a => a.DistrictName).FirstOrDefault();
                var l = orm.LGDDistricts.Where(a => a.DistrictCode == k.DistrictCode).FirstOrDefault();
                string village = orm.LGDVillages.Where(a => a.VillageCode == k.VillageCode).Select(a => a.VillageName).FirstOrDefault();
                return orm.BeneFiciaryLandRecordDetails.Where(a => a.ReferenceNo == refno).Select(a => new ThirdFormVM { relation = relation, dist = l.DistrictName, distID = l.DistrictCode, Village = village, villageID = k.VillageCode }).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public List<BeneFiciaryLandRecordDetail> Getlanddetails(string refno)
        {
            return orm.BeneFiciaryLandRecordDetails.Where(a => a.ReferenceNo == refno).ToList();
        }

        public BeneficiaryLandRecordVM GetDocumentfiles(string refno)
        {
            return orm.BeneficiaryDetails.Where(a => a.ReferenceNo == refno).Select(a => new BeneficiaryLandRecordVM { pdffile1 = a.LandPdf1, pdffile2 = a.LandPdf2 }).FirstOrDefault();
        }

        public BankDocumentVM GetBankDocument(string refno)
        {
            return orm.BeneficiaryProjectDetails.Where(a => a.ReferenceNo == refno).Select(a => new BankDocumentVM { CISBankClearanceCertificate = a.CISBankClearanceCertificate }).FirstOrDefault();
        }

        public BankConsentVM GetBankCLDoc(string refno)
        {
            return orm.BeneficiaryProjectDetails.Where(a => a.ReferenceNo == refno).Select(a => new BankConsentVM { BankConsentLetter = a.BankConsentLetter }).FirstOrDefault();
        }

        public GraduationDocumentVM GetGraduationDocument(string refno)
        {
            return orm.BeneficiaryDetails.Where(a => a.ReferenceNo == refno).Select(a => new GraduationDocumentVM { CastGraduationCertificate = a.CastGraduationCertificate }).FirstOrDefault();
        }

        public byte[] GetIdentityProof(string referenceNo, string filetype)
        {
            if (filetype == "IdentityProof")
            {
                return orm.BeneficiaryDocuments.Where(a => a.ReferenceNo == referenceNo).Select(a => a.IdentityProof).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public byte[] GetBankCC(string referenceNo, string filetype)
        {
            if (filetype == "BankDocument")
            {
                return orm.BeneficiaryProjectDetails.Where(a => a.ReferenceNo == referenceNo).Select(a => a.CISBankClearanceCertificate).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public byte[] GetBankCL(string referenceNo, string filetype)
        {
            if (filetype == "BankConsent")
            {
                return orm.BeneficiaryProjectDetails.Where(a => a.ReferenceNo == referenceNo).Select(a => a.BankConsentLetter).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public byte[] GetPTCCGAAD(string referenceNo, string filetype)
        {
            if (filetype == "CGCertificate")
            {
                return orm.BeneficiaryDetails.Where(a => a.ReferenceNo == referenceNo).Select(a => a.CastGraduationCertificate).FirstOrDefault();
            }
            else
            {
                return null;
            }
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

        public string LastFormDtls(string refno, string AadharNO)
        {
            var k = orm.BeneficiaryDetails.Where(a => a.ReferenceNo == refno).FirstOrDefault();
            if (k != null)
            {
                var L = orm1.M_FARMER_REGISTRATION.Where(a => a.NICFARMERID == k.FarmerID).Select(g => new { g.VCHAADHARNO, g.VCHVOTERIDCARDNO }).FirstOrDefault();
                if (ConvertToSHA256(L.VCHAADHARNO) != AadharNO)
                {
                    return "Invalid AadharNumber";
                }
                else
                {
                    var result = orm.BeneficiaryDetails.Where(a => a.ReferenceNo == refno && a.RegistrationStatus == "completed").FirstOrDefault();
                    if (result != null)
                    {
                        return "success";
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                return "Reference Number is Invalid";
            }
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

        public string UpdateBeneficiaryDtls(BeneficiaryUpdateDetails x, List<BeneficiaryMemberDetail> memberDetails)
        {
            List<MemberDetail> md = new List<MemberDetail>();
            var p = orm.BeneficiaryProjectDetails.Where(z => z.ReferenceNo == x.ReferenceNo).FirstOrDefault();
            decimal? totalcost = 0;
            if (p.BankLoan == null)
            {
                totalcost = p.OwnContribution;
            }
            else
            {
                totalcost = p.OwnContribution + p.BankLoan;
            }
            if ((totalcost >= 1000000.00M && p.FinanceType == "Bank" && p.BankLoan >= totalcost / 10) || (totalcost < 1000000.00M && p.ApproximateCost == "Below 1 Crore"))
            {
                var k = orm.BeneficiaryDetails.Where(a => a.ReferenceNo == x.ReferenceNo).FirstOrDefault();
                if ((x.HighestEducationalQualificationCode == "4" && (x.CastGraduationCertificate != null || k.CastGraduationCertificate != null)) || (x.HighestEducationalQualificationCode != "4" && x.CastGraduationCertificate == null))
                {
                    if (k != null && CheckEmailID(x.ReferenceNo, x.EmailID) == null && CheckRefGroupMemberFarmerID(memberDetails, k.FarmerID, x.ReferenceNo) == "")
                    {
                        k.HighestEducationalQualificationCode = x.HighestEducationalQualificationCode;
                        k.CastGraduationCertificate = x.CastGraduationCertificate;
                        k.FirmName = x.FirmName;
                        var bmd = orm.BeneficiaryMemberDetails.Where(z => z.ReferenceNo == x.ReferenceNo).ToList();
                        if (memberDetails != null)
                        {
                            for (int t = 0; t < bmd.Count; t++)
                            {
                                bmd[t].GroupMemberFarmerID = memberDetails[t].GroupMemberFarmerID;
                                bmd[t].HighestEducationalQualificationCode = memberDetails[t].HighestEducationalQualificationCode;
                            }
                            foreach (var item in memberDetails)
                            {
                                MemberDetail mDtls = new MemberDetail();
                                var fDetails = orm1.M_FARMER_REGISTRATION.Where(z => z.NICFARMERID == item.GroupMemberFarmerID).FirstOrDefault();
                                mDtls.FarmerID = item.GroupMemberFarmerID;
                                mDtls.EQualification = item.HighestEducationalQualificationCode;
                                mDtls.Category = fDetails.INTCATEGOGY;
                                mDtls.Gender = fDetails.INTGENDER;
                                md.Add(mDtls);
                            }
                        }
                        k.AnnualIncome = x.AnnualIncome;
                        k.PresentOccupation = x.PresentOccupation;
                        k.PreviousExperience = x.PreviousExperience;
                        k.EmailID = x.EmailID;
                        k.CorrespondenceAddress = x.CorrespondenceAddress;
                        k.PermanentAddress = x.PermanentAddress;
                        k.Pin = x.Pin;
                        k.GroupTypeCode = x.GroupTypeCode;
                        var fd = orm1.M_FARMER_REGISTRATION.Where(a => a.NICFARMERID == k.FarmerID).FirstOrDefault();
                        int? gender = null;
                        if (k.RelationWithFID != null)
                        {
                            if (k.RelationWithFID == 1 || k.RelationWithFID == 4 || k.RelationWithFID == 5 || k.RelationWithFID == 7 || k.RelationWithFID == 10 || k.RelationWithFID == 11)
                            {
                                gender = 1;
                            }
                            else
                            {
                                gender = 2;
                            }
                        }
                        if (k.RelationWithFIDType == "Self")
                        {
                            k.PercentageOfSubsidy = GenerateSubsidy(fd.INTGENDER, fd.INTCATEGOGY, x.HighestEducationalQualificationCode, md);
                        }
                        else
                        {
                            k.PercentageOfSubsidy = GenerateSubsidy(gender, fd.INTCATEGOGY, x.HighestEducationalQualificationCode, md);
                        }
                        var bpd = orm.BeneficiaryProjectDetails.Where(z => z.ReferenceNo == x.ReferenceNo).FirstOrDefault();
                        decimal? cost = 0;
                        decimal? totalCost = 0;
                        decimal d = 0;
                        if (bpd.BankLoan == null)
                        {
                            cost = bpd.OwnContribution;
                        }
                        else
                        {
                            cost = bpd.OwnContribution + bpd.BankLoan;
                        }
                        totalCost = cost;
                        if (bpd.Capacity == null && bpd.CapacityUnit == null)
                        {
                            d = Convert.ToDecimal(totalCost * k.PercentageOfSubsidy / 100);
                            k.SubsidyAmount = Math.Round(d, 2);
                        }
                        else
                        {
                            if (k.RelationWithFIDType == "Self")
                            {
                                k.SubsidyAmount = CalculateSubsidy(bpd.ProjectTypeCode, bpd.Capacity, fd.INTGENDER, fd.INTCATEGOGY, Convert.ToDecimal(totalCost), k.PercentageOfSubsidy);
                            }
                            else
                            {
                                k.SubsidyAmount = CalculateSubsidy(bpd.ProjectTypeCode, bpd.Capacity, gender, fd.INTCATEGOGY, Convert.ToDecimal(totalCost), k.PercentageOfSubsidy);
                            }
                        }
                        if (bpd.ProjectTypeCode != "03P15")
                        {
                            k.SubsidyAmount = k.SubsidyAmount > 5000000.00M ? 5000000.00M : k.SubsidyAmount;
                        }
                        else
                        {
                            k.SubsidyAmount = k.SubsidyAmount > 2500000.00M ? 2500000.00M : k.SubsidyAmount;
                        }
                        RegistrationLog rl = new RegistrationLog();
                        rl.ReferenceNo = x.ReferenceNo;
                        rl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
                        rl.UserDateTime = DateTime.Now;
                        rl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                        rl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                        orm.RegistrationLogs.Add(rl);
                        try
                        {
                            orm.SaveChanges();
                            return "Records updated successfully.";
                        }
                        catch (Exception e)
                        {
                            return e.ToString();
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "Please upload Preferential Treatment Certificate (Caste / Graduation in Agriculture and Allied Discipline)";
                }
            }
            else
            {
                return "Finance Type must be 'Bank' and Bank Loan must be greater than or equal to 10% of Rs " + totalcost + " (Total Cost)";
            }
        }

        public string UpdateProjectINfoDetails(BeneficiaryProjectUpdateDetails x, List<IntegratedProjectDetail> IPD, string selectCapacity, decimal totalcost)
        {
            var p = orm.BeneficiaryDetails.Where(z => z.ReferenceNo == x.ReferenceNo).FirstOrDefault();
            var k = orm.BeneficiaryProjectDetails.Where(a => a.ReferenceNo == x.ReferenceNo).FirstOrDefault();
            if (x.CISAmount == null)
            {
                x.CISAmount = 0;
            }
            if (x.CISAmount < 5000000)
            {
                if ((x.IsCISAvailedPreviously == "No" && x.CISProjectTypeCode == null && x.CISLocation == null && x.CISAvailedYear == null && x.CISFinanceType == null && x.CISAmount == 0 && x.CISBankClearanceCertificate == null) || (x.IsCISAvailedPreviously == "Yes" && x.CISProjectTypeCode != null && x.CISLocation != null && x.CISAvailedYear != null && x.CISAmount != 0 && ((x.CISFinanceType == "Self" && x.CISBankClearanceCertificate == null) || (x.CISFinanceType == "Bank" && (x.CISBankClearanceCertificate != null || k.CISBankClearanceCertificate != null)))))
                {
                    if ((x.ProjectTypeCode == "04P15" && IPD != null) || (x.ProjectTypeCode != "04P15" && IPD == null))
                    {
                        dynamic imsg = "";
                        if (IPD != null)
                        {
                            foreach (var i in IPD)
                            {
                                if ((i.Capacity == null && i.CapacityUnit != null) || (i.Capacity != null && i.CapacityUnit == null))
                                {
                                    imsg = "The Capacity must be entered incase of Integrated Farming.";
                                }
                            }
                            if (imsg != "")
                            {
                                return imsg;
                            }
                        }
                        string returnMsg = CheckTarget(x.ProjectTypeCode, p.FarmerID);
                        if (returnMsg == "OK")
                        {
                            if ((totalcost >= 1000000.00M && x.FinanceType == "Bank" && x.BankLoan >= totalcost / 10) || (totalcost < 1000000.00M && x.ApproximateCost == "Below 1 Crore"))
                            {
                                if (CheckContribution(x.ApproximateCost, totalcost) == "")
                                {
                                    decimal? cost = 0;
                                    decimal? totalCost = 0;
                                    decimal d = 0;
                                    if (x.BankLoan == 0)
                                    {
                                        cost = x.OwnContribution;
                                    }
                                    else
                                    {
                                        cost = x.OwnContribution + x.BankLoan;
                                    }
                                    totalCost = cost;
                                    if (x.Capacity == null && x.CapacityUnit == null)
                                    {
                                        d = Convert.ToDecimal(totalCost * p.PercentageOfSubsidy / 100);
                                        p.SubsidyAmount = Math.Round(d, 2);
                                    }
                                    else
                                    {
                                        int? gender = null;
                                        if (p.RelationWithFID != null)
                                        {
                                            if (p.RelationWithFID == 1 || p.RelationWithFID == 4 || p.RelationWithFID == 5 || p.RelationWithFID == 7 || p.RelationWithFID == 10 || p.RelationWithFID == 11)
                                            {
                                                gender = 1;
                                            }
                                            else
                                            {
                                                gender = 2;
                                            }
                                        }
                                        var fd = orm1.M_FARMER_REGISTRATION.Where(a => a.NICFARMERID == p.FarmerID).FirstOrDefault();
                                        if (p.RelationWithFIDType == "Self")
                                        {
                                            p.SubsidyAmount = CalculateSubsidy(x.ProjectTypeCode, x.Capacity, fd.INTGENDER, fd.INTCATEGOGY, Convert.ToDecimal(totalCost), p.PercentageOfSubsidy);
                                        }
                                        else
                                        {
                                            p.SubsidyAmount = CalculateSubsidy(x.ProjectTypeCode, x.Capacity, gender, fd.INTCATEGOGY, Convert.ToDecimal(totalCost), p.PercentageOfSubsidy);
                                        }
                                    }
                                    if (x.ProjectTypeCode != "03P15")
                                    {
                                        p.SubsidyAmount = p.SubsidyAmount > 5000000.00M ? 5000000.00M : p.SubsidyAmount;
                                    }
                                    else
                                    {
                                        p.SubsidyAmount = p.SubsidyAmount > 2500000.00M ? 2500000.00M : p.SubsidyAmount;
                                    }
                                    k.ProjectTypeCode = x.ProjectTypeCode;
                                    k.ProductToBeProducedOrMarketed = x.ProductToBeProducedOrMarketed;
                                    k.Capacity = x.Capacity;
                                    k.CapacityUnit = x.CapacityUnit;
                                    k.FinanceType = x.FinanceType;
                                    k.BankCode = x.BankCode;
                                    k.BranchCode = x.BranchCode;
                                    k.IsCISAvailedPreviously = x.IsCISAvailedPreviously;
                                    k.ApproximateCost = x.ApproximateCost;
                                    k.OwnContribution = x.OwnContribution;
                                    k.BankLoan = x.BankLoan;
                                    k.CISProjectTypeCode = x.CISProjectTypeCode;
                                    k.CISLocation = x.CISLocation;
                                    k.CISAvailedYear = x.CISAvailedYear;
                                    k.CISAmount = x.CISAmount;
                                    k.CISFinanceType = x.CISFinanceType;
                                    if (x.FinanceType == "Self")
                                    {
                                        k.BankConsentLetter = null;
                                    }
                                    else
                                    {
                                        if (x.BankConsentLetter != null)
                                        {
                                            k.BankConsentLetter = x.BankConsentLetter;
                                        }
                                    }
                                    if (x.CISFinanceType == "Self" || x.IsCISAvailedPreviously == "No")
                                    {
                                        k.CISBankClearanceCertificate = null;
                                    }
                                    else
                                    {
                                        if (x.CISBankClearanceCertificate != null)
                                        {
                                            k.CISBankClearanceCertificate = x.CISBankClearanceCertificate;
                                        }
                                    }
                                    var w = orm.IntegratedProjectDetails.Where(z => z.ReferenceNo == x.ReferenceNo).ToList();
                                    if (w.Count > 0) { orm.IntegratedProjectDetails.RemoveRange(w); }
                                    if (IPD != null)
                                    {
                                        var lrd = orm.BeneFiciaryLandRecordDetails.Where(z => z.ReferenceNo == x.ReferenceNo).ToList();
                                        foreach (var i in IPD)
                                        {
                                            i.ReferenceNo = x.ReferenceNo;
                                            i.IPBLOStatus = 0;
                                            if (lrd.Count != 0)
                                            {
                                                i.BLOUserName = i.ProjectTypeCode.Substring(0, 2) == "01" ? "aao_" + p.NICBlockCode : i.ProjectTypeCode.Substring(0, 2) == "02" ? "aho_" + p.NICBlockCode : i.ProjectTypeCode.Substring(0, 2) == "03" ? "afo_" + p.NICBlockCode : "bvo_" + p.NICBlockCode;
                                            }
                                        }
                                        orm.IntegratedProjectDetails.AddRange(IPD);
                                    }
                                    RegistrationLog rl = new RegistrationLog();
                                    rl.ReferenceNo = x.ReferenceNo;
                                    rl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
                                    rl.UserDateTime = DateTime.Now;
                                    rl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                                    rl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                                    orm.RegistrationLogs.Add(rl);
                                    try
                                    {
                                        orm.SaveChanges();
                                        return "Records updated successfully.";
                                    }
                                    catch (Exception e)
                                    {
                                        return e.ToString();
                                    }
                                }
                                else
                                {
                                    return "";
                                }
                            }
                            else
                            {
                                return "Finance Type must be 'Bank' and Bank Loan must be greater than or equal to 10 % of Rs " + totalcost + " (Total Cost)";
                            }
                        }
                        else
                        {
                            if (returnMsg == "Not OK")
                            {
                                return "No more projects can be alloted as the Target limit is reached.";
                            }
                            else
                            {
                                return "The target value has not been set. Please try after sometime.";
                            }
                        }
                    }
                    else
                    {
                        return "Please enter the Integrated Projects to updated.";
                    }
                }
                else
                {
                    return "Fill all the required details regarding CIS and upload the Bank Clearance Certificate.";
                }
            }
            else
            {
                return "CIS Amount can't be Rs. 5000000 or above.";
            }
        }

        public BeneFiciaryLandRecordDetail GetBeneficiaryBhulekhRecord(string dCode, string tCode, string vCode, string khataNo, string plotNo)
        {
            System.Data.DataTable L;
            try
            {
                int tehsilCode = Convert.ToInt32(tCode);
                int villageCode = Convert.ToInt32(vCode);
                L = new BhulekhOdishaR.AgriCensusServiceSoapClient().AgriRORData(dCode, tehsilCode, villageCode, khataNo, plotNo);
                if (L.Columns.Count > 1)
                {
                    BeneFiciaryLandRecordDetail obj = new BeneFiciaryLandRecordDetail();
                    obj.AreaInAcre = Convert.ToDecimal(L.Rows[0][0]);
                    obj.AreaInHectare = Convert.ToDecimal(L.Rows[0][1]);
                    obj.Kisam = L.Rows[0][2].ToString();
                    obj.TenantName = L.Rows[0][3].ToString();
                    return obj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                var f = ex;
                return null;
            }
        }

        public BeneFiciaryLandRecordDetail GetBeneficiaryLandrecord(string khataNo, string plotNo, string rDistrictID, int villageCode)
        {
            System.Data.DataTable L;
            try
            {
                L = new BhulekhOdisha.OdishaCropInsuranceSoapClient().AgriCensusRORData(rDistrictID, villageCode, khataNo, plotNo);
                if (L.Columns.Count > 1)
                {
                    BeneFiciaryLandRecordDetail obj = new BeneFiciaryLandRecordDetail();
                    obj.AreaInAcre = Convert.ToDecimal(L.Rows[0][0]);
                    obj.AreaInHectare = Convert.ToDecimal(L.Rows[0][1]);
                    obj.Kisam = L.Rows[0][2].ToString();
                    obj.TenantName = L.Rows[0][3].ToString();
                    return obj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                var f = ex;
                return null;
            }
        }

        public string LandRecordSubmitDetails(List<BeneFiciaryLandRecordDetail> x, string farmerID, string refno, PdfFilesforBeneficiaryDetail pdfland, string locality)
        {
            string msg = string.Empty;
            var m = orm.BeneficiaryProjectDetails.Where(a => a.ReferenceNo == refno).FirstOrDefault();
            if (m != null)
            {
                foreach (var item in x)
                {
                    if (locality == "Rural")
                    {
                        var u = orm.BeneFiciaryLandRecordDetails.Where(r => r.DistrictCode == item.DistrictCode && r.BlockCode == item.BlockCode && r.GPCode == item.GPCode && r.VillageCode == item.VillageCode && r.KhataNo == item.KhataNo && r.PlotNo == item.PlotNo).Select(r => new TehsilRICircleVillageName { DistrictName = r.LGDDistrict.DistrictName, BlockName = r.LGDBlock.BlockName, GPName = r.LGDGP.GPName, VillageCode = r.VillageCode, PlotNo = r.PlotNo, KhataNo = r.KhataNo }).FirstOrDefault();
                        if (u == null)
                        {
                            msg = "OK";
                        }
                        else
                        {
                            return "The District : " + u.DistrictName.Trim() + ", Block : " + u.BlockName.Trim() + ", GP : " + u.GPName.Trim() + ", Village : " + u.VillageName.Trim() + ", Khata No. : " + u.KhataNo + " & the Plot No. : " + u.PlotNo + " already exist.";
                        }
                    }
                    else if (locality == "Urban")
                    {
                        var u = orm.BeneFiciaryLandRecordDetails.Where(r => r.DistrictCode == item.DistrictCode && r.TehsilCode == item.TehsilCode && r.RICircleCode == item.RICircleCode && r.VillageCode == item.VillageCode && r.KhataNo == item.KhataNo && r.PlotNo == item.PlotNo).Select(r => new TehsilRICircleVillageName { DistrictCode = r.DistrictCode, TehsilCode = r.TehsilCode, RICircleCode = r.RICircleCode, VillageRCode = r.VillageCode, PlotNo = r.PlotNo, KhataNo = r.KhataNo }).FirstOrDefault();
                        if (u == null)
                        {
                            msg = "OK";
                        }
                        else
                        {
                            return "The District : " + u.DistrictRName.Trim() + ", Tehsil : " + u.TehsilName.Trim() + ", RI Circle : " + u.RICircleName.Trim() + ", Village : " + u.VillageRName.Trim() + ", Khata No. : " + u.KhataNo + " & the Plot No. : " + u.PlotNo + " already exist.";
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                foreach (var i in x)
                {
                    var k = orm.BeneficiaryDetails.Where(a => a.ReferenceNo == refno).FirstOrDefault();
                    if (pdfland.LandPdf1 != null || pdfland.LandPdf2 != null)
                    {
                        k.NICDistrictCode = Convert.ToString(i.DistrictCode);
                        k.NICBlockCode = Convert.ToString(i.BlockCode);
                        k.LandPdf1 = pdfland.LandPdf1;
                        k.LandPdf2 = pdfland.LandPdf2;
                        orm.BeneFiciaryLandRecordDetails.AddRange(x);
                        var d = orm.IntegratedProjectDetails.Where(z => z.ReferenceNo == refno).ToList();
                        foreach (var j in d)
                        {
                            j.BLOUserName = j.ProjectTypeCode.Substring(0, 2) == "01" ? "aao_" + k.NICBlockCode : j.ProjectTypeCode.Substring(0, 2) == "02" ? "aho_" + k.NICBlockCode : j.ProjectTypeCode.Substring(0, 2) == "03" ? "afo_" + k.NICBlockCode : "bvo_" + k.NICBlockCode;
                        }
                    }
                }
                RegistrationLog rl = new RegistrationLog();
                rl.ReferenceNo = refno;
                rl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
                rl.UserDateTime = DateTime.Now;
                rl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                rl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                orm.RegistrationLogs.Add(rl);
                try
                {
                    orm.SaveChanges();
                    return refno;
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
            else
            {
                return null;
            }
        }

        public string UpdateLandRecord(BeneFiciaryLandRecordDetail x, int gpCode, int villageCode, string khataNo, string plotNo)
        {
            var k = orm.BeneFiciaryLandRecordDetails.Where(g => g.ReferenceNo == x.ReferenceNo && g.DistrictCode == x.DistrictCode && g.BlockCode == x.BlockCode && g.GPCode == gpCode && g.VillageCode == villageCode && g.KhataNo == khataNo && g.PlotNo == plotNo).FirstOrDefault();
            var l = orm.BeneFiciaryLandRecordDetails.Where(g => g.ReferenceNo == x.ReferenceNo && g.DistrictCode == x.DistrictCode && g.BlockCode == x.BlockCode && g.GPCode == x.GPCode && g.VillageCode == x.VillageCode && g.KhataNo == x.KhataNo && g.PlotNo == x.PlotNo).FirstOrDefault();
            if (l == null || (l.ReferenceNo == k.ReferenceNo && l.DistrictCode == k.DistrictCode && l.BlockCode == k.BlockCode && l.GPCode == k.GPCode && l.VillageCode == k.VillageCode && l.KhataNo == k.KhataNo && l.PlotNo == k.PlotNo))
            {
                if (k != null)
                {
                    orm.BeneFiciaryLandRecordDetails.Remove(k);
                    orm.BeneFiciaryLandRecordDetails.Add(x);
                }
                RegistrationLog rl = new RegistrationLog();
                rl.ReferenceNo = x.ReferenceNo;
                rl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
                rl.UserDateTime = DateTime.Now;
                rl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                rl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                orm.RegistrationLogs.Add(rl);
                try
                {
                    orm.SaveChanges();
                    return "Records updated successfully.";
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
            else
            {
                return "The land record exist already.";
            }
        }

        public string UpdateGradCert(string refno, GraduationDocument GraduationCertificate, string userID)
        {
            var k = orm.BeneficiaryDetails.Where(a => a.ReferenceNo == refno).FirstOrDefault();
            if (k != null && userID != null)
            {
                BLORegdLog brl = new BLORegdLog();
                brl.CastGraduationCertificate = k.CastGraduationCertificate;
                brl.ReferenceNo = k.ReferenceNo;
                brl.BLOUserName = userID;
                brl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                brl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                brl.UserDateTime = DateTime.Now;
                orm.BLORegdLogs.Add(brl);
            }
            if (k != null)
            {
                if (GraduationCertificate.CastGraduationCertificate != null)
                {
                    k.CastGraduationCertificate = GraduationCertificate.CastGraduationCertificate;
                }
            }
            RegistrationLog rl = new RegistrationLog();
            rl.ReferenceNo = refno;
            rl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            rl.UserDateTime = DateTime.Now;
            rl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            rl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.RegistrationLogs.Add(rl);
            try
            {
                orm.SaveChanges();
                return "Records updated successfully.";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string UpdateBankDoc(string refno, BankDocument BankCertificate)
        {
            var k = orm.BeneficiaryDetails.Where(a => a.ReferenceNo == refno).FirstOrDefault();
            var m = orm.BeneficiaryProjectDetails.Where(a => a.ReferenceNo == refno).FirstOrDefault();
            if (m != null)
            {
                if (k != null)
                {
                    if (BankCertificate.CISBankClearanceCertificate != null)
                    {
                        m.CISBankClearanceCertificate = BankCertificate.CISBankClearanceCertificate;
                    }
                }
            }
            RegistrationLog rl = new RegistrationLog();
            rl.ReferenceNo = refno;
            rl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            rl.UserDateTime = DateTime.Now;
            rl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            rl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.RegistrationLogs.Add(rl);
            try
            {
                orm.SaveChanges();
                return "Records updated successfully.";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string UpdateBankCL(string refno, BankConsent BankCL, string userID)
        {
            var k = orm.BeneficiaryDetails.Where(a => a.ReferenceNo == refno).FirstOrDefault();
            var m = orm.BeneficiaryProjectDetails.Where(a => a.ReferenceNo == refno).FirstOrDefault();
            if (k != null && m != null && userID != null)
            {
                BLORegdLog brl = new BLORegdLog();
                brl.BankConsentLetter = m.BankConsentLetter;
                brl.ReferenceNo = k.ReferenceNo;
                brl.BLOUserName = userID;
                brl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                brl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                brl.UserDateTime = DateTime.Now;
                orm.BLORegdLogs.Add(brl);
            }
            if (m != null)
            {
                if (k != null)
                {
                    if (BankCL.BankConsentLetter != null)
                    {
                        m.BankConsentLetter = BankCL.BankConsentLetter;
                    }
                }
            }
            RegistrationLog rl = new RegistrationLog();
            rl.ReferenceNo = refno;
            rl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            rl.UserDateTime = DateTime.Now;
            rl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            rl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.RegistrationLogs.Add(rl);
            try
            {
                orm.SaveChanges();
                return "Records updated successfully.";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string UpdateLandDoc1(string refno, PDFFilesUpload1 pdfland, string userID)
        {
            var k = orm.BeneficiaryDetails.Where(a => a.ReferenceNo == refno).FirstOrDefault();
            var m = orm.BeneFiciaryLandRecordDetails.Where(a => a.ReferenceNo == refno).FirstOrDefault();
            if (k != null && m != null && userID != null)
            {
                BLORegdLog brl = new BLORegdLog();
                brl.LandPdf1 = k.LandPdf1;
                brl.ReferenceNo = k.ReferenceNo;
                brl.BLOUserName = userID;
                brl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                brl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                brl.UserDateTime = DateTime.Now;
                orm.BLORegdLogs.Add(brl);
            }
            if (m != null)
            {
                if (k != null)
                {
                    if (pdfland.LandPdf1 != null)
                    {
                        k.LandPdf1 = pdfland.LandPdf1;
                    }
                }
            }
            RegistrationLog rl = new RegistrationLog();
            rl.ReferenceNo = refno;
            rl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            rl.UserDateTime = DateTime.Now;
            rl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            rl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.RegistrationLogs.Add(rl);
            try
            {
                orm.SaveChanges();
                return "Records updated successfully.";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string UpdateLandDoc2(string refno, PDFFilesUpload2 pdfland, string userID)
        {
            var k = orm.BeneficiaryDetails.Where(a => a.ReferenceNo == refno).FirstOrDefault();
            var m = orm.BeneFiciaryLandRecordDetails.Where(a => a.ReferenceNo == refno).FirstOrDefault();
            if (k != null && m != null && userID != null)
            {
                BLORegdLog brl = new BLORegdLog();
                brl.LandPdf2 = k.LandPdf2;
                brl.ReferenceNo = k.ReferenceNo;
                brl.BLOUserName = userID;
                brl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                brl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                brl.UserDateTime = DateTime.Now;
                orm.BLORegdLogs.Add(brl);
            }
            if (m != null)
            {
                if (k != null)
                {
                    if (pdfland.LandPdf2 != null)
                    {
                        k.LandPdf2 = pdfland.LandPdf2;
                    }
                }
            }
            RegistrationLog rl = new RegistrationLog();
            rl.ReferenceNo = refno;
            rl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            rl.UserDateTime = DateTime.Now;
            rl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            rl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.RegistrationLogs.Add(rl);
            try
            {
                orm.SaveChanges();
                return "Records updated successfully.";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string UpdateIDProof(string referenceNo, IDProof identityProof, string userID)
        {
            var k = orm.BeneficiaryDetails.Where(a => a.ReferenceNo == referenceNo).FirstOrDefault();
            var m = orm.BeneficiaryDocuments.Where(a => a.ReferenceNo == referenceNo).FirstOrDefault();
            if (k != null && m != null && userID != null)
            {
                BLORegdLog brl = new BLORegdLog();
                brl.IdentityProof = m.IdentityProof;
                brl.ReferenceNo = k.ReferenceNo;
                brl.BLOUserName = userID;
                brl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                brl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                brl.UserDateTime = DateTime.Now;
                orm.BLORegdLogs.Add(brl);
            }
            if (m != null)
            {
                if (k != null)
                {
                    if (identityProof.IdentityProof != null)
                    {
                        m.IdentityProof = identityProof.IdentityProof;
                    }
                }
            }
            RegistrationLog rl = new RegistrationLog();
            rl.ReferenceNo = referenceNo;
            rl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            rl.UserDateTime = DateTime.Now;
            rl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            rl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.RegistrationLogs.Add(rl);
            try
            {
                orm.SaveChanges();
                return "Records updated successfully.";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string UpdatePhoto(string referenceNo, Photo p)
        {
            var k = orm.BeneficiaryDetails.Where(a => a.ReferenceNo == referenceNo).FirstOrDefault();
            var m = orm.BeneficiaryDocuments.Where(a => a.ReferenceNo == referenceNo).FirstOrDefault();
            if (m != null)
            {
                if (k != null)
                {
                    if (p.UPhoto != null)
                    {
                        m.Photograph = p.UPhoto;
                    }
                }
            }
            RegistrationLog rl = new RegistrationLog();
            rl.ReferenceNo = referenceNo;
            rl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            rl.UserDateTime = DateTime.Now;
            rl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            rl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.RegistrationLogs.Add(rl);
            try
            {
                orm.SaveChanges();
                return "Records updated successfully.";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string UpdateSign(string referenceNo, Signature s)
        {
            var k = orm.BeneficiaryDetails.Where(a => a.ReferenceNo == referenceNo).FirstOrDefault();
            var m = orm.BeneficiaryDocuments.Where(a => a.ReferenceNo == referenceNo).FirstOrDefault();
            if (m != null)
            {
                if (k != null)
                {
                    if (s.USign != null)
                    {
                        m.Signature = s.USign;
                    }
                }
            }
            RegistrationLog rl = new RegistrationLog();
            rl.ReferenceNo = referenceNo;
            rl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            rl.UserDateTime = DateTime.Now;
            rl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            rl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.RegistrationLogs.Add(rl);
            try
            {
                orm.SaveChanges();
                return "Records updated successfully.";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public ApplicantACKVM GetApplicantAcknowledgement(string refno)
        {
            string applicantName = string.Empty;
            string gender = string.Empty;
            string eduCert = string.Empty;
            string bcl = string.Empty;
            string cisBCL = string.Empty;
            string groupType = string.Empty;
            var bd = orm.BeneficiaryDetails.Where(a => a.ReferenceNo == refno).FirstOrDefault();
            decimal? totalCost;
            totalCost = bd.BeneficiaryProjectDetail.BankLoan == null ? bd.BeneficiaryProjectDetail.OwnContribution : bd.BeneficiaryProjectDetail.OwnContribution + bd.BeneficiaryProjectDetail.BankLoan;
            if (bd.GroupTypeCode != null)
            {
                groupType = Convert.ToString(bd.GroupType.Name);
            }
            if (bd.CastGraduationCertificate != null)
            {
                eduCert = Convert.ToBase64String(bd.CastGraduationCertificate);
            }
            if (bd.BeneficiaryProjectDetail.BankConsentLetter != null)
            {
                bcl = Convert.ToBase64String(bd.BeneficiaryProjectDetail.BankConsentLetter);
            }
            if (bd.BeneficiaryProjectDetail.CISBankClearanceCertificate != null)
            {
                cisBCL = Convert.ToBase64String(bd.BeneficiaryProjectDetail.CISBankClearanceCertificate);
            }
            var blrd = orm.BeneFiciaryLandRecordDetails.Where(a => a.ReferenceNo == refno).FirstOrDefault();
            var photo = Convert.ToBase64String(bd.BeneficiaryDocument.Photograph);
            string ApplicationDate = DateTime.Parse(bd.DateOfRegd.ToString()).ToString("dd-MM-yyyy");
            string dist = orm.LGDDistricts.Where(a => a.DistrictCode == blrd.DistrictCode).Select(a => a.PDSDistrictName).FirstOrDefault();
            var block = orm.LGDBlocks.Where(a => a.BlockCode == blrd.BlockCode).Select(a => a.BlockName).FirstOrDefault();
            var gp = orm.LGDGPs.Where(a => a.GPCode == blrd.GPCode).Select(a => a.GPName).FirstOrDefault();
            var village = orm.LGDVillages.Where(a => a.VillageCode == blrd.VillageCode).Select(a => a.VillageName).FirstOrDefault();
            var fd = orm1.M_FARMER_REGISTRATION.Where(a => a.NICFARMERID == bd.FarmerID).FirstOrDefault();
            if (bd.RelationWithFIDType == "Relative")
            {
                applicantName = bd.RelationApplicantName;
                if (bd.RelationWithFID == 1 || bd.RelationWithFID == 4 || bd.RelationWithFID == 5 || bd.RelationWithFID == 7 || bd.RelationWithFID == 10 || bd.RelationWithFID == 11)
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }
            }
            else
            {
                applicantName = fd.VCHFARMERNAME;
                string gen = Convert.ToString(fd.INTGENDER);
                gender = orm1.Tbl_Gender.Where(a => a.Gender_ID == gen).Select(a => a.Gender_Value).FirstOrDefault();
            }
            string cat = Convert.ToString(fd.INTCATEGOGY);
            string category = orm1.Tbl_Category.Where(a => a.Category_ID == cat).Select(a => a.Category_Value).FirstOrDefault();
            int bankCode = Convert.ToInt32(bd.BeneficiaryProjectDetail.BankCode);
            int branchCode = Convert.ToInt32(bd.BeneficiaryProjectDetail.BranchCode);
            var bank = orm1.M_PDS_FARMERBANK.Where(a => a.intId == bankCode).Select(a => a.vchBankName).FirstOrDefault();
            var branch = orm1.M_PDS_BANKBRANCH.Where(a => a.intBranchId == branchCode).Select(a => a.vchBranchName).FirstOrDefault();
            var project = orm.ProjectTypes.Where(a => a.Code == bd.BeneficiaryProjectDetail.ProjectTypeCode).Select(a => a.Name).FirstOrDefault();
            var cisProject = orm.ProjectTypes.Where(a => a.Code == bd.BeneficiaryProjectDetail.CISProjectTypeCode).Select(a => a.Name).FirstOrDefault();
            var idProof = Convert.ToBase64String(bd.BeneficiaryDocument.IdentityProof);
            var sign = Convert.ToBase64String(bd.BeneficiaryDocument.Signature);
            return orm.BeneficiaryDetails.Where(a => a.ReferenceNo == refno).Select(z => new ApplicantACKVM { ReferenceNumber = refno, Photo = photo, DateOfApplication = ApplicationDate, AppliedTo = z.BeneficiaryProjectDetail.Department.Name, District = dist, Block = block, GP = gp, Village = village, FarmerID = z.FarmerID, ApplicantType = z.BeneficiaryType, ApplicantName = applicantName, FatherorHusbandName = fd.VCHFATHERNAME, Gender = gender, Category = category, MobileNumber = z.MobileNo, GroupTypeCode = z.GroupTypeCode, GroupType = groupType, GMNumber = z.NoOfMember, FirmName = z.FirmName, EducationalQualification = z.EducationalQualification.Name, EducationalCertificate = eduCert, AnnualIncome = z.AnnualIncome, PresentOccupation = z.PresentOccupation, PreviousExperience = z.PreviousExperience, EmailID = z.EmailID, Pincode = z.Pin, CorrespondenceAddress = z.CorrespondenceAddress, PermanentAddress = z.PermanentAddress, Department = z.BeneficiaryProjectDetail.Department.Name, Project = project, ProductsProduced = z.BeneficiaryProjectDetail.ProductToBeProducedOrMarketed, Capacity = z.BeneficiaryProjectDetail.Capacity, CapacityUnit = z.BeneficiaryProjectDetail.CapacityUnit, FinanceType = z.BeneficiaryProjectDetail.FinanceType, BankConsentLeter = bcl, BankCode = bankCode, Bank = bank, BranchCode = branchCode, Branch = branch, IsCISPreviouslyAvailed = z.BeneficiaryProjectDetail.IsCISAvailedPreviously, CISProject = cisProject, CISLocation = z.BeneficiaryProjectDetail.CISLocation, CISYear = z.BeneficiaryProjectDetail.CISAvailedYear, CISFinanceType = z.BeneficiaryProjectDetail.CISFinanceType, CISAmount = z.BeneficiaryProjectDetail.CISAmount, CISBankConsentLeter = cisBCL, ApproximateCost = z.BeneficiaryProjectDetail.ApproximateCost, OwnContribution = z.BeneficiaryProjectDetail.OwnContribution, BankLoan = z.BeneficiaryProjectDetail.BankLoan, TotalCost = totalCost, IdentityProof = idProof, Signature = sign }).FirstOrDefault();
        }

        public List<BeneFiciaryLandRecordDetail> landDetailsAck(string refno)
        {
            return orm.BeneFiciaryLandRecordDetails.Where(a => a.ReferenceNo == refno).ToList();
        }

        public List<AllLandRecordVM> Alllandrecord(string refno)
        {
            return orm.BeneFiciaryLandRecordDetails.Where(a => a.ReferenceNo == refno).Select(a => new AllLandRecordVM { RefNo = a.ReferenceNo, DistCode = a.DistrictCode, BlockCode = a.BlockCode, GPCode = a.GPCode, KhataNo = a.KhataNo, PlotNo = a.PlotNo, VillageCode = a.VillageCode, TenantName = a.TenantName, Kisam = a.Kisam, AreaInHectare = a.AreaInHectare, AreaInAcre = a.AreaInAcre, Relationship = a.Relationship.Name, RelationshipCode = a.RelationshipCode, ExecArea = a.ExecutionArea, ExecUnit = a.UnitExecution, RDistCode = a.LGDDistrict.RevenueDistrictCode, TehsilCode = a.TehsilCode, RICircleCode = a.RICircleCode, Locality = a.Locality }).ToList();
        }

        public List<AllGroupMembersVM> AllGroupMembers(string refno)
        {
            return orm.BeneficiaryMemberDetails.Where(a => a.ReferenceNo == refno).Select(a => new AllGroupMembersVM { GroupMemberFarmerID = a.GroupMemberFarmerID, EducationalQualificationCode = a.HighestEducationalQualificationCode }).ToList();
        }
    }

    public class MemberDetail
    {
        public string FarmerID { get; set; }
        public string EQualification { get; set; }
        public int? Category { get; set; }
        public int? Gender { get; set; }
    }

    public class IProjectDetail
    {
        public string ReferenceNo { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get { return new CommercialAgriEnterpriseEntities().Departments.Where(z => z.Code == DepartmentCode).Select(x => x.Name).FirstOrDefault(); } }
        public string ProjectTypeCode { get; set; }
        public string ProjectTypeName { get { return new CommercialAgriEnterpriseEntities().ProjectTypes.Where(z => z.Code == ProjectTypeCode).Select(x => x.Name).FirstOrDefault(); } }
        public decimal? Capacity { get; set; }
        public string Unit { get; set; }
    }

    public class TehsilRICircleVillageName
    {
        public int DistrictCode { get; set; }
        public string DCode { get { return new CommercialAgriEnterpriseEntities().rDistricts.Where(z => z.LGDDistrictCode == DistrictCode).Select(x => x.DCode).FirstOrDefault(); } }
        public string DistrictName { get; set; }
        public string DistrictRName { get { return new CommercialAgriEnterpriseEntities().rDistricts.Where(z => z.DCode == DCode).Select(x => x.DName).FirstOrDefault(); } }
        public string BlockName { get; set; }
        public int? TehsilCode { get; set; }
        public string TCode { get { return Convert.ToString(TehsilCode); } }
        public string TehsilName { get { return new CommercialAgriEnterpriseEntities().rTehsils.Where(z => z.TCode == TCode && z.DCode == DCode).Select(x => x.TName).FirstOrDefault(); } }
        public string GPName { get; set; }
        public int? RICircleCode { get; set; }
        public string RICircleName { get { return new CommercialAgriEnterpriseEntities().riCircles.Where(z => z.TCode == TCode && z.DCode == DCode).Select(x => x.riName).FirstOrDefault(); } }
        public int VillageCode { get; set; }
        public string VillageName { get { return new CommercialAgriEnterpriseEntities().LGDVillages.Where(z => z.VillageCode == VillageCode).Select(x => x.VillageName).FirstOrDefault(); } }
        public int VillageRCode { get; set; }
        public string VRCode { get { return Convert.ToString(VillageRCode); } }
        public string VillageRName { get { return new CommercialAgriEnterpriseEntities().rVillages.Where(z => z.VCode == VRCode && z.TCode == TCode && z.DCode == DCode).Select(x => x.voName).FirstOrDefault(); } }
        public string KhataNo { get; set; }
        public string PlotNo { get; set; }
    }
}