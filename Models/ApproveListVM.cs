namespace CommercialAgriEnterprise.Models
{
    public class ApproveListVM
    {
        public string refno { get; set; }
        public int? status { get; set; }
        public int? statusBlo { get; set; }
        public int? statusApicol { get; set; }
        public int? statusDda { get; set; }
        public int? statusCollector { get; set; }
        public int? integratedStatus { get; set; }
        public string BloStatus { get { if (statusBlo == 2) { return "Not Feasible"; } else if (statusBlo == 1) { return "Feasible"; } else { return "Pending"; } } }
        public string Approvestatus { get { if (status == 2) { return "Not Recommended"; } else if (status == 1) { return "Recommended"; } else { return "Pending"; } } }
        public string Apicolstatus { get { if (statusApicol == 2) { return "Rejected"; } else if (statusApicol == 1) { return "Approved"; } else { return "Pending"; } } }
        public string DDASTATUS { get { if (statusDda == 2) { return "Rejected"; } else if (statusDda == 1) { return "Approved"; } else { return "Pending"; } } }
        public string CollectorStatus { get { if (statusCollector == 2) { return "Rejected"; } else if (statusCollector == 1) { return "Approved"; } else { return "Pending"; } } }
        public bool GoAhead { get { if (status == 1) { return true; } else { return false; } } }
        public string pStatus { get; set; }
        public string paymentStatus { get { if (pStatus == "Pending") { return "Pending"; } else { return "Success"; } } }
        public string IntegratedFarmingStatus { get { if (integratedStatus == 2) { return "Rejected"; } else if (integratedStatus == 1) { return "Approved"; } else { return "Pending"; } } }
        public int? BTBLOStatus { get; set; }
        public string BackToBLOStatus { get { if (BTBLOStatus == null) { return "show"; } else { return "hide"; } } }
    }
}