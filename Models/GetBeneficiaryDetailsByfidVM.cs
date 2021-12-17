using System;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class GetBeneficiaryDetailsByfidVM
    {
        public GetBeneficiaryDetailsByfidVM(string _FarmerID)
        {
            FarmerID = _FarmerID;
        }

        private string FarmerID { get; set; }
        public string Referenceno { get { return new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Where(a => a.FarmerID == FarmerID).Select(a => a.ReferenceNo).FirstOrDefault(); } }
        public BeneficiaryDetailBYID GETBENEFECIARYBYID { get { return new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Where(a => a.ReferenceNo == Referenceno).Select(g => new BeneficiaryDetailBYID { farmerid = FarmerID, Refno = Referenceno, Date = System.DateTime.Now, Photo = g.BeneficiaryDocument.Signature, Projctnm = g.BeneficiaryProjectDetail.ProjectType.Name }).FirstOrDefault(); } }
    }

    public class BeneficiaryDetailBYID
    {
        public string farmerid { get; set; }
        public string Refno { get; set; }
        public string GOAheadno { get { return new CommercialAgriEnterpriseEntities().GoAheads.Where(a => a.ReferenceNo == Refno).Select(a => a.GoAheadNumber).FirstOrDefault(); } }
        public DateTime Date { get; set; }
        public string TodayDate { get { return Date.ToString("dd-MM-yyyy").Substring(0, 10); } }
        public byte[] Photo { get; set; }
        public string PhotoSIgn { get { return Convert.ToBase64String(Photo); } }
        private M_FARMER_REGISTRATION _farmerdtls { get { return new FARMERDBEntities().M_FARMER_REGISTRATION.Where(a => a.NICFARMERID == farmerid).FirstOrDefault(); } }
        public string Farmernm { get { return _farmerdtls.VCHFARMERNAME; } }
        public string Fathernm { get { return _farmerdtls.VCHFATHERNAME; } }
        public string Distnm { get { return new FARMERDBEntities().PDS_DISTRICTMASTER.Where(a => a.int_DistrictID == _farmerdtls.VCHDISTID).Select(a => a.vch_DistrictName).FirstOrDefault(); } }
        public string Blocknm { get { return new FARMERDBEntities().PDS_BLOCKMASTER.Where(a => a.int_BlockID == _farmerdtls.VCHBLOCKID).Select(a => a.vch_BlockName).FirstOrDefault(); } }
        public string GPnm { get { return new FARMERDBEntities().PDS_GPMASTER.Where(a => a.int_GPID == _farmerdtls.VCHGPID).Select(a => a.vch_GPName).FirstOrDefault(); } }
        public string Villagenm { get { return new FARMERDBEntities().PDS_VILLAGEMASTER.Where(a => a.int_VillageID == _farmerdtls.VCHVILLAGEID).Select(a => a.vch_VillageName).FirstOrDefault(); } }
        public string Projctnm { get; set; }
    }
}