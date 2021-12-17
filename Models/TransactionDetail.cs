using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class TransactionDetail
    {
        CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();
        FARMERDBEntities orm1 = new FARMERDBEntities();

        public string ReferenceNo { get; set; }
        public string TransactionID { get; set; }
        public string FarmerID { get; set; }
        public string FarmerName { get { if ((orm.BeneficiaryDetails.Where(z => z.ReferenceNo == ReferenceNo).Select(x => x.RelationApplicantName).FirstOrDefault()) != null) { return orm.BeneficiaryDetails.Where(z => z.ReferenceNo == ReferenceNo).Select(x => x.RelationApplicantName).FirstOrDefault(); } else { return new FARMERDBEntities().M_FARMER_REGISTRATION.Where(z => z.NICFARMERID == FarmerID).Select(x => x.VCHFARMERNAME).FirstOrDefault(); } } }
        public decimal? PaymentAmount { get; set; }
    }
}