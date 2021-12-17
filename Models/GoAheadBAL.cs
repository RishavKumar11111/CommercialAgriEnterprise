using System;
using System.Linq;
using System.Text;

namespace CommercialAgriEnterprise.Models
{
    public class GoAheadBAL
    {
        CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();

        public string CheckReferenceNoStatus(string referenceNo, string voterIDNo, string aadhaarCardNo)
        {
            var k = orm.BeneficiaryDetails.Where(z => z.ReferenceNo == referenceNo).Select(x => new CheckReferenceNoStatus { ReferenceNo = x.ReferenceNo, RegistrationStatus = x.RegistrationStatus, DPRStatus = x.DPRStatu.Status, PaymentStatus = x.PaymentStatus, RStatus = x.BLORecord.RegdStatus, DStatus = x.BLORecord.DPRStatus, PStatus = x.BLORecord.PaymentStatus, BLOStatus = x.BLORecord.BLOStatus, DNOStatus = x.DNORecord.DNOStatus, DDNOStatus = x.DDARecord.IntegratedFarmingStatus, CollectorStatus = x.CollectorRecord.CollectorStatus, DDAStatus = x.DDARecord.DDAStatus, FarmerID = x.FarmerID }).FirstOrDefault();
            if (k != null)
            {
                if (voterIDNo == ConvertToSHA256(k.CardNo.VoterIDCardNo))
                {
                    if (aadhaarCardNo == ConvertToSHA256(k.CardNo.AadhaarCardNo))
                    {
                        if (k.ReferenceNo == null)
                        {
                            return "You have entered an invalid Reference No.";
                        }
                        else if (k.ReferenceNo != null && k.RegistrationStatus == "incomplete" && k.RStatus == null)
                        {
                            return "Please complete the Registration.";
                        }
                        else if (k.ReferenceNo != null && k.RegistrationStatus == "completed" && k.RStatus == true && k.DPRStatus == null && k.DStatus == null)
                        {
                            return "Please start the DPR.";
                        }
                        else if (k.ReferenceNo != null && k.RegistrationStatus == "completed" && k.RStatus == true && k.DPRStatus == 0 && k.DStatus == null)
                        {
                            return "Please complete the DPR.";
                        }
                        else if (k.ReferenceNo != null && k.RegistrationStatus == "completed" && k.RStatus == true && k.DPRStatus == 1 && k.DStatus == true && k.PaymentStatus == "Pending" && k.PStatus == null)
                        {
                            return "Please complete the Payment.";
                        }
                        else if (k.ReferenceNo != null && k.RegistrationStatus == "completed" && k.RStatus == true && k.DPRStatus == 1 && k.DStatus == true && k.PaymentStatus == "Success" && k.PStatus == true && (k.BLOStatus == 0 || k.BLOStatus == 1 || k.BLOStatus == 2) && (k.DNOStatus == 0 || k.DNOStatus == 1 || k.DNOStatus == 2 || k.DDNOStatus == 0 || k.DDNOStatus == 1 || k.DDNOStatus == 2) && (k.CollectorStatus == 0 || k.CollectorStatus == 1 || k.CollectorStatus == 2) && k.DDAStatus == 0)
                        {
                            return "Your Go-Ahead is pending and Go-Ahead No. is not yet generated.";
                        }
                        else if (k.ReferenceNo != null && k.RegistrationStatus == "completed" && k.RStatus == true && k.DPRStatus == 1 && k.DStatus == true && k.PaymentStatus == "Success" && k.PStatus == true && (k.BLOStatus == 1 || k.BLOStatus == 2) && (k.DNOStatus == 1 || k.DNOStatus == 2 || k.DDNOStatus == 1 || k.DDNOStatus == 2) && k.CollectorStatus == 1 && k.DDAStatus == 2)
                        {
                            return "Your Go-Ahead is rejected and Go-Ahead No. will not be generated.";
                        }
                        else if (k.ReferenceNo != null && k.RegistrationStatus == "completed" && k.RStatus == true && k.DPRStatus == 1 && k.DStatus == true && k.PaymentStatus == "Success" && k.PStatus == true && (k.BLOStatus == 1 || k.BLOStatus == 2) && (k.DNOStatus == 1 || k.DNOStatus == 2 || k.DDNOStatus == 1 || k.DDNOStatus == 2) && k.CollectorStatus == 1 && k.DDAStatus == 1 && k.GoAheadDetail.GoAheadNo != null && k.GoAheadDetail.GoAheadStatus == 0)
                        {
                            return "The DNO has not given his consent and not uploaded his signature.";
                        }
                        else if (k.ReferenceNo != null && k.RegistrationStatus == "completed" && k.RStatus == true && k.DPRStatus == 1 && k.DStatus == true && k.PaymentStatus == "Success" && k.PStatus == true && (k.BLOStatus == 1 || k.BLOStatus == 2) && (k.DNOStatus == 1 || k.DNOStatus == 2 || k.DDNOStatus == 1 || k.DDNOStatus == 2) && k.CollectorStatus == 1 && k.DDAStatus == 1 && k.GoAheadDetail.GoAheadNo != null && k.GoAheadDetail.GoAheadStatus == 1)
                        {
                            DateTime dt = k.GoAheadDetail.GoAheadDOI.AddDays(7);
                            string rdt = Convert.ToString((dt - DateTime.Now).Days);
                            if (DateTime.Now >= dt)
                            {
                                return k.ReferenceNo + "," + k.FarmerID;
                            }
                            else
                            {
                                return rdt;
                            }
                        }
                        else
                        {
                            return "Oops!!! Something is wrong. Please try again.";
                        }
                    }
                    else
                    {
                        return "Invalid Aadhaar Card No.";
                    }
                }
                else
                {
                    return "Invalid Voter ID Card No.";
                }
            }
            else
            {
                return "You have entered an invalid Reference No.";
            }
        }

        private string ConvertToSHA256(string pass)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(pass);
            System.Security.Cryptography.SHA256Managed sha256hasstring = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hasstring.ComputeHash(bytes);
            return ByteArrayToHexString(hash);
        }

        private string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            ba.ToList().ForEach(z => hex.AppendFormat("{0:x2}", z));
            return hex.ToString();
        }

        public string CheckBeneficiaryStatus(string referenceNo, string voterIDNo, string aadhaarCardNo)
        {
            var k = orm.BeneficiaryDetails.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (k != null)
            {
                var Fdetails = new FARMERDBEntities().M_FARMER_REGISTRATION.Where(z => z.NICFARMERID == k.FarmerID).FirstOrDefault();
                if (voterIDNo == ConvertToSHA256(Fdetails.VCHVOTERIDCARDNO))
                {
                    if (aadhaarCardNo == ConvertToSHA256(Fdetails.VCHAADHARNO))
                    {
                        if (k.ReferenceNo == null)
                        {
                            return "You have entered an invalid Reference No.";
                        }
                        else
                        {
                            return "NULL";
                        }
                    }
                    else
                    {
                        return "Invalid Aadhaar Card No.";
                    }
                }
                else
                {
                    return "Invalid Voter ID Card No.";
                }
            }
            else
            {
                return "You have entered an invalid Reference No.";
            }
        }
    }
}