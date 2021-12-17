using System;
using System.Collections.Generic;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class DnoConfirmationApprovalVM
    {
        public DnoConfirmationApprovalVM(string startdate, string enddate, string username, string fyr)
        {
            DateTime strtdt = Convert.ToDateTime(startdate.Substring(4, 11));
            string startingdt = strtdt.ToString("yyyy/MM/dd");
            a = Convert.ToDateTime(startingdt);
            DateTime enddt = Convert.ToDateTime(enddate.Substring(4, 11));
            string endingdt = Convert.ToDateTime(enddt).ToString("yyyy/MM/dd");
            b = Convert.ToDateTime(endingdt);
            Username = username;
            FYR = fyr;
        }

        private DateTime a { get; set; }
        private DateTime b { get; set; }
        private string Username { get; set; }
        private string FYR { get; set; }

        public List<_dnoapprovelist> getapprovelist
        {
            get
            {
                return new CommercialAgriEnterpriseEntities().DNORecords.Where(z => z.DNOUserName == Username && z.DNOStatus == 1 && z.DNODateTime >= a && z.DNODateTime <= b && z.BeneficiaryDetail.ReferenceNo == z.ReferenceNo && z.FinancialYear == FYR).Select(z => new _dnoapprovelist { refno = z.ReferenceNo, farmerid = z.BeneficiaryDetail.FarmerID }).ToList();
            }
        }
    }

    public class _dnoapprovelist
    {
        public string refno { get; set; }
        public string farmerid { get; set; }
        public string farmername { get { return new FARMERDBEntities().M_FARMER_REGISTRATION.Where(g => g.NICFARMERID == farmerid).Select(g => g.VCHFARMERNAME).FirstOrDefault(); } }
    }
}