using System;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class CheckReferenceNoStatus
    {
        public string ReferenceNo { get; set; }
        public string RegistrationStatus { get; set; }
        public int? DPRStatus { get; set; }
        public string PaymentStatus { get; set; }
        public bool? RStatus { get; set; }
        public bool? DStatus { get; set; }
        public bool? PStatus { get; set; }
        public int BLOStatus { get; set; }
        public int? DNOStatus { get; set; }
        public int? DDNOStatus { get; set; }
        public int CollectorStatus { get; set; }
        public int DDAStatus { get; set; }
        public GoAheadDetail GoAheadDetail { get { return new CommercialAgriEnterpriseEntities().GoAheads.Where(z => z.ReferenceNo == ReferenceNo).Select(x => new GoAheadDetail { GoAheadNo = x.GoAheadNumber, GoAheadStatus = x.GoAheadStatus, GoAheadDOI = x.GoAheadDOI, GoAheadValidDate = x.GoAheadValidDate }).FirstOrDefault(); } }
        public string FarmerID { get; set; }
        public CardDetail CardNo { get { return new FARMERDBEntities().M_FARMER_REGISTRATION.Where(z => z.NICFARMERID == FarmerID).Select(x => new CardDetail { VoterIDCardNo = x.VCHVOTERIDCARDNO, AadhaarCardNo = x.VCHAADHARNO }).FirstOrDefault(); } }
    }

    public class CardDetail
    {
        public string VoterIDCardNo { get; set; }
        public string AadhaarCardNo { get; set; }
    }

    public class GoAheadDetail
    {
        public string GoAheadNo { get; set; }
        public int GoAheadStatus { get; set; }
        public DateTime GoAheadDOI { get; set; }
        public DateTime GoAheadValidDate { get; set; }
    }
}