using System;
using System.Linq;
using System.Web;

namespace CommercialAgriEnterprise.Models
{
    public class BeneficiaryCompletionBAL
    {
        CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();
        FARMERDBEntities orm1 = new FARMERDBEntities();

        public string getgoahead(string Aadharno, string FarmerID)
        {
            var farmerid = orm1.M_FARMER_REGISTRATION.Where(a => a.NICFARMERID == FarmerID).FirstOrDefault();
            var fid = orm.BeneficiaryDetails.Where(a => a.FarmerID == FarmerID && a.RegistrationStatus == "completed").FirstOrDefault();
            if (farmerid != null)
            {
                var aadharnumber = ConvertToSHA256(farmerid.VCHAADHARNO);
                if (aadharnumber != null)
                {
                    if (ConvertToSHA256(farmerid.VCHAADHARNO) == Aadharno)
                    {
                        if (farmerid != null)
                        {
                            if (fid != null)
                            {
                                var goaheadno = orm.GoAheads.Where(a => a.ReferenceNo == fid.ReferenceNo).Select(a => a.GoAheadNumber).FirstOrDefault();
                                if (goaheadno != null)
                                {
                                    var k = orm.BeneficiaryCompletions.Where(a => a.GoAheadNumber == goaheadno).FirstOrDefault();
                                    if (k == null)
                                    {
                                        return "OK";
                                    }
                                    else
                                    {
                                        return "Beneficiary Completion already generated against this GoAhead number.";
                                    }
                                }
                                else
                                {
                                    return "Go-ahead is not generated.";
                                }
                            }
                            else
                            {
                                return "You have not registered.";
                            }
                        }
                        return "Invalid Farmer ID.";
                    }
                    return "Invalid Aadhar Number.";
                }
                else
                {
                    return "Invalid Aadhar Number.";
                }
            }
            else
            {
                return "Invalid Farmer ID.";
            }
        }

        public string BeneficiaryCompletionInsert(BeneficiaryCompletion BC)
        {
            var k = orm.BeneficiaryCompletions.Where(a => a.GoAheadNumber == BC.GoAheadNumber).FirstOrDefault();
            if (k == null)
            {
                try
                {
                    BC.FinancialYear = GetCurrentFinancialYear();
                    BC.UserDateTime = System.DateTime.Now;
                    BC.IPAddress = HttpContext.Current.Request.UserHostAddress;
                    orm.BeneficiaryCompletions.Add(BC);
                    orm.SaveChanges();
                    return "Record saved successfully.";
                }
                catch (Exception e)
                {
                    e.ToString();
                    return "false";
                }
            }
            else
            {
                return "false";
            }
        }

        private string ConvertToSHA256(string pass)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(pass);
            System.Security.Cryptography.SHA256Managed sha256hasstring = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hasstring.ComputeHash(bytes);
            return ByteArrayToHexString(hash);
        }

        private static string ByteArrayToHexString(byte[] ba)
        {
            System.Text.StringBuilder hex = new System.Text.StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
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

        //public PRCBENEFICIARYCOMPLETIONBRPT_Result GetCompletionDetails(string goAheadNo)
        //{
        //    return orm.PRCBENEFICIARYCOMPLETIONBRPT(goAheadNo).FirstOrDefault();
        //}
    }
}