using System;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class GoAheadVM
    {
        CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();

        public GoAheadVM(string _ReferenceNo, string _FarmerID)
        {
            ReferenceNo = _ReferenceNo;
            FarmerID = _FarmerID;
        }
        public string ReferenceNo { private get; set; }
        public string FarmerID { private get; set; }
        public _GADetail GADetails { get { return orm.BeneficiaryDetails.Where(z => z.ReferenceNo == ReferenceNo).Select(x => new _GADetail { ProjectCode = x.BeneficiaryProjectDetail.ProjectTypeCode, ProjectName = x.BeneficiaryProjectDetail.ProjectType.Name, ApplicantDepartmentCode = x.BeneficiaryProjectDetail.DepartmentCode, ApplicantDepartment = x.BeneficiaryProjectDetail.Department.Name, ApplicantFinanceType = x.BeneficiaryProjectDetail.FinanceType, ApplicantSubsidyPercentage = x.PercentageOfSubsidy, ReferenceNo = x.ReferenceNo, FarmerID = x.FarmerID, DistrictCode = x.NICDistrictCode, BlockCode = x.NICBlockCode, ApplicantPIN = x.Pin }).FirstOrDefault(); } }
    }

    public class _GADetail
    {
        CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();
        FARMERDBEntities orm1 = new FARMERDBEntities();

        public string ReferenceNo { get; set; }
        public string FarmerID { get; set; }
        public GADetail goAheadDetail { get { return orm.GoAheads.Where(z => z.ReferenceNo == ReferenceNo).Select(x => new GADetail { GoAheadNo = x.GoAheadNumber, GoAheadStatus = x.GoAheadStatus, GoAheadDOI = x.GoAheadDOI.ToString().Substring(0, 10), GoAheadValidDate = x.GoAheadValidDate.ToString().Substring(0, 10) }).FirstOrDefault(); } }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string ApplicantFinanceType { get; set; }
        public decimal ApplicantSubsidyPercentage { get; set; }
        public string ApplicantDepartmentCode { get; set; }
        public string ApplicantDepartment { get; set; }
        public string ApplicantPIN { get; set; }
        public string DistrictCode { get; set; }
        public int DCode { get { return Convert.ToInt32(DistrictCode); } }
        public string BlockCode { get; set; }
        public int BCode { get { return Convert.ToInt32(BlockCode); } }
        public LandDetail landDetails { get { return orm.BeneFiciaryLandRecordDetails.Where(z => z.ReferenceNo == ReferenceNo).Select(x => new LandDetail { DistrictCode = x.DistrictCode, BlockCode = x.BlockCode, TehsilCode = x.TehsilCode, GPCode = x.GPCode, RICircleCode = x.RICircleCode, VillageCode = x.VillageCode, VillageRCode = x.VillageCode }).FirstOrDefault(); } }
        public byte[] Signature { get { if (ProjectCode == "04P15") { return orm.DNODetailEntries.Where(a => a.DepartmentCode == "01" && a.DistrictCode == DCode).Select(a => a.Signature).FirstOrDefault(); } else { return orm.DNODetailEntries.Where(a => a.DepartmentCode == ApplicantDepartmentCode && a.DistrictCode == DCode).Select(a => a.Signature).FirstOrDefault(); } } }
        public string Sign { get { return Convert.ToBase64String(Signature); } }
        public FDetail fDetail { get { return orm1.M_FARMER_REGISTRATION.Where(z => z.NICFARMERID == FarmerID).Select(x => new FDetail { ApplicantName = x.VCHFARMERNAME, RelationWithApplicant = x.VCHFATHERNAME, ApplicantPIN = x.VCHPINCODE, ACategory = x.INTCATEGOGY, ADistrict = x.VCHDISTID, ABlock = x.VCHBLOCKID, AGP = x.VCHGPID, AVillage = x.VCHVILLAGEID }).FirstOrDefault(); } }
        public string ApplicantDistName { get { if (landDetails.DistrictName != null) { return landDetails.DistrictName.Trim(); } else { return landDetails.DistrictRName.Trim(); } } }
        public string ApplicantBlockName { get { if (landDetails.BlockName != null) { return landDetails.BlockName.Trim(); } else { return landDetails.TehsilName.Trim(); } } }
        public string ApplicantGPName { get { if (landDetails.GPCode != null) { return landDetails.GPName.Trim(); } else { return landDetails.RICircleName.Trim(); } } }
        public string ApplicantVillageName { get { if (landDetails.VillageName != null) { return landDetails.VillageName.Trim(); } else { return landDetails.VillageRName.Trim(); } } }
        public string DNOFullName { get { return orm.DNODetailEntries.Where(z => z.DepartmentCode == ApplicantDepartmentCode && z.DistrictCode == DCode).Select(x => x.Name).FirstOrDefault(); } }
    }

    public class LandDetail
    {
        CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();

        public int DistrictCode { get; set; }
        public string DCode { get { if (DistrictCode != 0) { return orm.rDistricts.Where(z => z.LGDDistrictCode == DistrictCode).Select(x => x.DCode).FirstOrDefault(); } else { return null; } } }
        public string DistrictName { get { if (DistrictCode != 0) { return orm.LGDDistricts.Where(z => z.DistrictCode == DistrictCode).Select(x => x.DistrictName).FirstOrDefault(); } else { return null; } } }
        public string DistrictRName { get { if (DCode != null) { return orm.rDistricts.Where(z => z.DCode == DCode).Select(x => x.DName).FirstOrDefault(); } else { return null; } } }
        public int BlockCode { get; set; }
        public string BlockName { get { if (BlockCode != 0) { return orm.LGDBlocks.Where(z => z.BlockCode == BlockCode).Select(x => x.BlockName).FirstOrDefault(); } else { return null; } } }
        public int? TehsilCode { get; set; }
        public string TCode { get { if (TehsilCode != null) { return Convert.ToString(TehsilCode); } else { return null; } } }
        public string TehsilName { get { if (TCode != null) { return orm.rTehsils.Where(z => z.TCode == TCode && z.DCode == DCode).Select(x => x.TName).FirstOrDefault(); } else { return null; } } }
        public int? GPCode { get; set; }
        public string GPName { get { if (GPCode != null) { return orm.LGDGPs.Where(z => z.GPCode == GPCode).Select(x => x.GPName).FirstOrDefault(); } else { return null; } } }
        public int? RICircleCode { get; set; }
        public string RICircleName { get { if (RICircleCode != null) { return orm.riCircles.Where(z => z.TCode == TCode && z.DCode == DCode).Select(x => x.riName).FirstOrDefault(); } else { return null; } } }
        public int VillageCode { get; set; }
        public string VillageName { get { if (VillageCode != 0) { return orm.LGDVillages.Where(z => z.VillageCode == VillageCode).Select(x => x.VillageName).FirstOrDefault(); } else { return null; } } }
        public int VillageRCode { get; set; }
        public string VRCode { get { if (VillageRCode != 0) { return Convert.ToString(VillageRCode); } else { return null; } } }
        public string VillageRName { get { if (VRCode != null) { return orm.rVillages.Where(z => z.VCode == VRCode && z.TCode == TCode && z.DCode == DCode).Select(x => x.voName).FirstOrDefault(); } else { return null; } } }
    }

    public class GADetail
    {
        public string GoAheadNo { get; set; }
        public int GoAheadStatus { get; set; }
        public string GoAheadDOI { get; set; }
        public string GoAheadValidDate { get; set; }
    }

    public class FDetail
    {
        FARMERDBEntities orm1 = new FARMERDBEntities();

        public string ApplicantName { get; set; }
        public string RelationWithApplicant { get; set; }
        public string ApplicantPIN { get; set; }
        public int? ACategory { get; set; }
        public string AC { get { return Convert.ToString(ACategory); } }
        public string Category { get { return orm1.Tbl_Category.Where(z => z.Category_ID == AC).Select(x => x.Category_Value).FirstOrDefault(); } }
        public string ADistrict { get; set; }
        public string District { get { return orm1.PDS_DISTRICTMASTER.Where(z => z.int_DistrictID == ADistrict).Select(x => x.vch_DistrictName).FirstOrDefault(); } }
        public string ABlock { get; set; }
        public string Block { get { return orm1.PDS_BLOCKMASTER.Where(z => z.int_BlockID == ABlock).Select(x => x.vch_BlockName).FirstOrDefault(); } }
        public string AGP { get; set; }
        public string GP { get { return orm1.PDS_GPMASTER.Where(z => z.int_GPID == AGP).Select(x => x.vch_GPName).FirstOrDefault(); } }
        public string AVillage { get; set; }
        public string Village { get { return orm1.PDS_VILLAGEMASTER.Where(z => z.int_VillageID == AVillage).Select(x => x.vch_VillageName).FirstOrDefault(); } }
    }
}