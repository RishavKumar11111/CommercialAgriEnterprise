using System.Collections.Generic;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class CheckBeneficiaryStatusVM
    {
        public CheckBeneficiaryStatusVM(string _refno)
        {
            refno = _refno;
        }
        public string refno { get; set; }
        public List<GetAllStatus> GetALLStatus
        {
            get
            {
                var k = new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Where(z => z.ReferenceNo == refno).Select(g => new GetAllStatus { referenceNo=g.ReferenceNo,regdstatus = g.RegistrationStatus, dprStatus = g.DPRStatu.Status, paymentStatus = g.PaymentStatus, bloStatus = g.BLORecord.BLOStatus, dnoStatus = g.DNORecord.DNOStatus, ddaStatus = g.DDARecord.DDAStatus, collectorStatus = g.CollectorRecord.CollectorStatus }).ToList();
                return k;
            }
        }
    }

    public class GetAllStatus
    {
        public string referenceNo { get; set; }
        public string regdstatus { get; set; }
        public string RegdStatus { get { if (regdstatus == "completed") { return "Success"; } else { return "Pending"; } } }
        public int? dprStatus { get; set; }
        public string DprStatus { get { if (dprStatus == 2) { return "Success"; } else if (dprStatus == 1) { return "Success"; } else { return "Pending"; } } }
        public string paymentStatus { get; set; }
        public int? bloStatus { get; set; }
        public string BloStatus { get { if (bloStatus == 2) { return "Success"; } else if (bloStatus == 1) { return "Success"; } else { return "Pending"; } } }
        public int? dnoStatus { get; set; }
        public string DnoStatus { get { if (dnoStatus == 2) { return "Success"; } else if (dnoStatus == 1) { return "Success"; } else { return "Pending"; } } }
        public int? ddaStatus { get; set; }
        public string DdaStatus { get { if (ddaStatus == 2) { return "Success"; } else if (ddaStatus == 1) { return "Success"; } else { return "Pending"; } } }
        public int? collectorStatus { get; set; }
        public string CollectorStatus { get { if (collectorStatus == 2) { return "Success"; } else if (collectorStatus == 1) { return "Success"; } else { return "Pending"; } } }
        public int? goaheadStatus { get { return new CommercialAgriEnterpriseEntities().GoAheads.Where(z => z.ReferenceNo == referenceNo).Select(z => z.GoAheadStatus).FirstOrDefault(); } }
        public string GoaheadStatus { get {  if (goaheadStatus == 1) { return "Success"; } else { return "Pending"; } } }
    }
}