using System.Collections.Generic;

namespace CommercialAgriEnterprise.Models
{
    public class RegisterDetails
    {
        public string Mobile_No { get; set; }
        public string Passwd { get; set; }
        public string IMEI { get; set; }
        public string SIM_SerialNo { get; set; }
        public string APIKey { get; set; }
        public string UserID { get; set; }
        public string Usertype { get; set; }
        public string msg { get; set; }
        public List<Userid> Userids { get; set; }
    }

    public class Userid
    {
        public string user_id { get; set; }
        public string BlockName { get; set; }
    }

    public class ReferenceNo
    {
        public string user_id { get; set; }
        public string Rno { get; set; }
        public string msg { get; set; }
    }

    public class Fesibilityreportcls
    {
        public string ReferenceNo { get; set; }
        public string DPRPreparedStatus { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Photo { get; set; }
        public string RoadConnectivity { get; set; }
        public string DistanceFromVillage { get; set; }
        public string ElectrificationStatus { get; set; }
        public string PollutionControlClearanceStatus { get; set; }
        public string SelfFinance { get; set; }
        public string UncensoredLoan { get; set; }
        public string MarginComponent { get; set; }
        public string BankConsentLetterStatus { get; set; }
    }
}