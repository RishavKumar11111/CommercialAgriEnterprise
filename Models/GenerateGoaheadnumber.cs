using System;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class GenerateGoaheadnumber
    {
        public static string goaheadnumber(string FarmerID, string refno)
        {
            CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();
            var Distname = orm.LGDDistricts.Where(a => a.DistrictName.Substring(0, 3) == FarmerID.Substring(0, 3).ToUpper()).Select(a => a.DistrictName).FirstOrDefault().Trim();
            string Deptcode = orm.BeneficiaryProjectDetails.Where(a => a.ReferenceNo == refno).Select(a => a.DepartmentCode).FirstOrDefault();
            string Deptname = orm.Departments.Where(a => a.Code == Deptcode).Select(a => a.Name).FirstOrDefault();
            if (Deptname == "Animal Resources Development")
            {
                Deptname = "ARD";
            }
            string GOAheadNo = string.Empty;
            string FinYear = GetCurrentFinancialYear();
            var k = orm.GoAheads.Where(a => a.ReferenceNo == refno).Select(a => a.GoAheadNumber).FirstOrDefault();
            if (k == null)
            {
                int Count = orm.GoAheads.Where(z => z.GoAheadNumber.Substring(0, 3) == "CAE" && z.GoAheadNumber.Substring(4, 3) == Distname.Substring(0, 3) && z.GoAheadNumber.Substring(8, 3) == refno.Substring(4, 3) && z.GoAheadNumber.Substring(12, 7) == FinYear).Select(z => z.GoAheadNumber).Count();
                if (Count > 0)
                {
                    Count += 1;
                    GOAheadNo = "CAE" + "/" + Distname.Substring(0, 3).ToUpper() + "/" + Deptname.Substring(0, 3).ToUpper() + "/" + GetCurrentFinancialYear() + "/" + Count;
                }
                else
                {
                    GOAheadNo = "CAE" + "/" + Distname.Substring(0, 3).ToUpper() + "/" + Deptname.Substring(0, 3).ToUpper() + "/" + GetCurrentFinancialYear() + "/" + 1;
                }
                return GOAheadNo;
            }
            return GOAheadNo;
        }

        public static string GetCurrentFinancialYear()
        {
            int CurrentYear = DateTime.Today.Year;
            int PreviousYear = DateTime.Today.Year - 1;
            int NextYear = DateTime.Today.Year + 1;
            string PreYear = PreviousYear.ToString();
            string NexYear = NextYear.ToString();
            string CurYear = CurrentYear.ToString();
            string FinYear = null;
            if (DateTime.Today.Month > 3)
                FinYear = CurYear + "-" + NexYear.Substring(2, 2);
            else
                FinYear = PreYear + "-" + CurYear.Substring(2, 2);
            return FinYear.Trim();
        }
    }
}