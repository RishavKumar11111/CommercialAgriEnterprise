using System;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class GenerateReferencenumber
    {
        public static string generaterefno(string FarmerID, string Deptcode)
        {
            CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();
            var dnm = FarmerID.Substring(0, 3);
            var Distname = orm.LGDDistricts.Where(a => a.PDSDistrictName.Substring(0, 3) == FarmerID.Substring(0, 3).ToUpper()).Select(a => a.PDSDistrictName).FirstOrDefault().Trim();
            string Deptname = orm.Departments.Where(a => a.Code == Deptcode).Select(a => a.Name).FirstOrDefault();
            if (Deptname == "Animal Resources Development")
            {
                Deptname = "ARD";
            }
            string FinYear = GetCurrentFinancialYear();
            int Count = orm.BeneficiaryProjectDetails.Where(a => a.DepartmentCode == Deptcode && a.ReferenceNo.Substring(0, 3) == Distname.Substring(0, 3) && a.ReferenceNo.Substring(8, 7) == FinYear).Select(a => a.ReferenceNo).Count();
            string referenceNo = string.Empty;
            if (Count > 0)
            {
                var dt = orm.BeneficiaryProjectDetails.Where(a => a.DepartmentCode == Deptcode && a.ReferenceNo.Substring(0, 3) == Distname.Substring(0, 3) && a.ReferenceNo.Substring(8, 7) == FinYear).OrderByDescending(a => a.BeneficiaryDetail.DateOfRegd).Select(a => a.BeneficiaryDetail.DateOfRegd).First();
                string sqlFormattedDate = dt.ToString("yyyy-MM-dd HH:mm:ss.fff");
                var lastrefno = orm.BeneficiaryProjectDetails.Where(a => a.DepartmentCode == Deptcode && a.ReferenceNo.Substring(0, 3) == Distname.Substring(0, 3) && a.ReferenceNo.Substring(8, 7) == FinYear).OrderByDescending(a => a.BeneficiaryDetail.DateOfRegd).Select(a => a.ReferenceNo).First();
                int count1 = int.Parse(lastrefno.Substring(16, lastrefno.Length - 16));
                count1 += 1;
                referenceNo = FarmerID.Substring(0, 3).ToUpper() + "/" + Deptname.Substring(0, 3).ToUpper() + "/" + GetCurrentFinancialYear() + "/" + count1;
                var LogRefno = orm.IntegratedProjectLogs.Where(z => z.PreviousReferenceNo == referenceNo).Select(z => z.PreviousReferenceNo).FirstOrDefault();
                if (LogRefno != null)
                {
                    count1 += 1;
                    referenceNo = FarmerID.Substring(0, 3).ToUpper() + "/" + Deptname.Substring(0, 3).ToUpper() + "/" + GetCurrentFinancialYear() + "/" + count1;
                }
            }
            else
            {
                referenceNo = FarmerID.Substring(0, 3).ToUpper() + "/" + Deptname.Substring(0, 3).ToUpper() + "/" + GetCurrentFinancialYear() + "/" + 1;
                var LogRefno = orm.IntegratedProjectLogs.Where(z => z.PreviousReferenceNo == referenceNo).Select(z => z.PreviousReferenceNo).FirstOrDefault();
                if (LogRefno != null)
                {
                    int Lastno = int.Parse(LogRefno.Substring(16, LogRefno.Length - 16));
                    Lastno += 1;
                    referenceNo = FarmerID.Substring(0, 3).ToUpper() + "/" + Deptname.Substring(0, 3).ToUpper() + "/" + GetCurrentFinancialYear() + "/" + Lastno;
                }
            }
            return referenceNo;
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