using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace CommercialAgriEnterprise.Models
{
    public class PaymentBAL
    {
        CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();
        FARMERDBEntities orm1 = new FARMERDBEntities();

        public List<PaymentDetails> GetPaymentDetail(string _ReferenceNo)
        {
            var k = orm.Payments.Where(g => g.ReferenceNo == _ReferenceNo).OrderByDescending(g => new { g.TransactionDate, g.TransactionTime }).Select(g => new PaymentDetails { ReferenceNo = g.ReferenceNo, TransactionID = g.TransactionID, TransactionStatus = g.TransactionStatus, TransactionDate = g.TransactionDate, TransactionTime = g.TransactionTime, BankResponseMessage = g.EasyPayErrorCode.Description }).ToList();
            return k;
        }

        public string BankResponseDetail(string transactionID, string bankTranID, string bankResponseCode, string bankTranDateTime, string serviceTax, string processingFee, string totalAmount, string tranAmount, string interchangeValue, string TDR, string paymentMode, string TPS)
        {
            var k = orm.Payments.Where(z => z.TransactionID == transactionID).OrderByDescending(z => new { z.TransactionDate, z.TransactionTime }).FirstOrDefault();
            var l = orm.BeneficiaryDetails.Where(z => z.ReferenceNo == k.ReferenceNo).FirstOrDefault();
            var m = orm.BLORecords.Where(z => z.ReferenceNo == k.ReferenceNo).FirstOrDefault();
            k.BankTransactionID = bankTranID;
            k.BankResponseCode = bankResponseCode;
            //string tranDate = bankTranDateTime.Substring(0, 10);
            string yyyy = bankTranDateTime.Substring(6, 4);
            string MM = bankTranDateTime.Substring(3, 2);
            string dd = bankTranDateTime.Substring(0, 2);
            k.BankResponseDate = Convert.ToDateTime(yyyy + "-" + MM + "-" + dd);
            string i = bankTranDateTime.ToString().Substring(11, 8);
            k.BankResponseTime = TimeSpan.Parse(i);
            k.ServiceTax = Convert.ToDecimal(serviceTax);
            k.ProcessingFee = Convert.ToDecimal(processingFee);
            k.TotalAmount = Convert.ToDecimal(totalAmount);
            k.TransactionAmount = Convert.ToDecimal(tranAmount);
            k.InterchangeValue = interchangeValue;
            k.TDR = TDR;
            k.PaymentMode = paymentMode;
            k.TPS = TPS;
            if (k.BankResponseCode == "E000")
            {
                k.TransactionStatus = "Success";
                l.PaymentStatus = "Success";
                m.PaymentStatus = true;
            }
            else
            {
                k.TransactionStatus = "Pending";
                l.PaymentStatus = "Pending";
            }
            try
            {
                orm.SaveChanges();
                if (l.PaymentStatus == "Success")
                {
                    InsertAfterSuccessfulPayment(k.ReferenceNo, l.PaymentStatus, k.BankResponseCode, k.TransactionStatus);
                }
                return k.EasyPayErrorCode.Description;
            }
            catch (Exception e)
            {
                e.ToString();
                return "An error occured. Please try again.";
            }
        }

        public string CheckReferenceNoStatus(string referenceNo, string voterIDNo, string aadhaarCardNo)
        {
            var k = orm.BeneficiaryDetails.Where(z => z.ReferenceNo == referenceNo).Select(x => new CheckReferenceNoStatus { ReferenceNo = x.ReferenceNo, RegistrationStatus = x.RegistrationStatus, DPRStatus = x.DPRStatu.Status, PaymentStatus = x.PaymentStatus, FarmerID = x.FarmerID }).FirstOrDefault();
            if (k != null)
            {
                var s = orm.BeneficiaryProjectDetails.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
                if (voterIDNo == ConvertToSHA256(k.CardNo.VoterIDCardNo.ToUpper()))
                {
                    if (aadhaarCardNo == ConvertToSHA256(k.CardNo.AadhaarCardNo))
                    {
                        if (k.ReferenceNo == null)
                        {
                            return "You have entered an invalid Reference No.";
                        }
                        else if (k.ReferenceNo != null && k.RegistrationStatus == "incomplete")
                        {
                            return "Please complete the Registration.";
                        }
                        else if (k.ReferenceNo != null && k.RegistrationStatus == "completed" && k.DPRStatus == null)
                        {
                            return "Please start the DPR.";
                        }
                        else if (k.ReferenceNo != null && k.RegistrationStatus == "completed" && k.DPRStatus == 0)
                        {
                            return "Please complete the DPR.";
                        }
                        else if (k.ReferenceNo != null && k.RegistrationStatus == "completed" && k.DPRStatus == 1 && k.PaymentStatus == "Pending")
                        {
                            string message = string.Empty;
                            var l = orm.Payments.Where(z => z.ReferenceNo == referenceNo).OrderByDescending(x => new { x.TransactionDate, x.TransactionTime }).FirstOrDefault();
                            var u = orm.BeneficiaryDetails.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
                            if (l != null)
                            {
                                if (l.BankResponseCode != "E000")
                                {
                                    string verifyURL = "https://eazypay.icicibank.com/EazyPGVerify?ezpaytranid=&amount=&paymentmode=&merchantid=153596&trandate=&pgreferenceno=" + l.TransactionID;
                                    string result = new WebClient().DownloadString(verifyURL);
                                    string[] splitResult = result.Split('&');
                                    if (l.TransactionID == splitResult[4].Split('=')[1])
                                    {
                                        string description = splitResult[0].Split('=')[1];
                                        if (description != "NotInitiated")
                                        {
                                            l.BankTransactionID = splitResult[1].Split('=')[1];
                                            l.TotalAmount = Convert.ToDecimal(splitResult[2].Split('=')[1]);
                                            string tranDate = splitResult[3].Split('=')[1].Substring(0, 10);
                                            string yyyy = tranDate.Substring(0, 4);
                                            string MM = tranDate.Substring(5, 2);
                                            string dd = tranDate.Substring(8, 2);
                                            l.BankResponseDate = Convert.ToDateTime(yyyy + "-" + MM + "-" + dd);
                                            string i = splitResult[3].Split('=')[1].Substring(11, 8);
                                            l.BankResponseTime = TimeSpan.Parse(i);
                                            l.TransactionAmount = Convert.ToDecimal(splitResult[6].Split('=')[1]);
                                            l.ProcessingFee = Convert.ToDecimal(splitResult[7].Split('=')[1]);
                                            l.ServiceTax = Convert.ToDecimal(splitResult[8].Split('=')[1]);
                                            l.PaymentMode = splitResult[9].Split('=')[1];
                                            l.PullResponse = result;
                                        }
                                        try
                                        {
                                            if (description == "RIP" || description == "SIP" || description == "Success")
                                            {
                                                l.BankResponseCode = "E000";
                                                u.PaymentStatus = "Success";
                                                l.TransactionStatus = "Success";
                                                u.BLORecord.PaymentStatus = true;
                                            }
                                            else if (description == "NotInitiated" || description == "Failed")
                                            {
                                                l.BankResponseCode = "E007";
                                            }
                                            else if (description == "Initiated")
                                            {
                                                l.BankResponseCode = "E99999";
                                            }
                                            orm.SaveChanges();
                                            if (u.PaymentStatus == "Success")
                                            {
                                                InsertAfterSuccessfulPayment(referenceNo, u.PaymentStatus, l.BankResponseCode, l.TransactionStatus);
                                            }
                                            return u.ReferenceNo + "," + u.PaymentStatus;
                                        }
                                        catch (Exception e)
                                        {
                                            var ex = e.Message;
                                            ex.ToString();
                                        }
                                    }
                                    else
                                    {
                                        message = "Wrong Pull response.";
                                        return message;
                                    }
                                }
                                else
                                {
                                    return u.ReferenceNo + "," + u.PaymentStatus;
                                }
                            }
                            return u.ReferenceNo + "," + u.PaymentStatus;
                        }
                        else if (k.ReferenceNo != null && k.RegistrationStatus == "completed" && k.DPRStatus == 1 && k.PaymentStatus == "Success")
                        {
                            return k.ReferenceNo + "," + k.PaymentStatus;
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

        public TransactionDetail GetTransactionDetail(string referenceNo)
        {
            return orm.Payments.Where(z => z.ReferenceNo == referenceNo).OrderByDescending(g => new { g.TransactionDate, g.TransactionTime }).Select(x => new TransactionDetail { ReferenceNo = x.ReferenceNo, FarmerID = x.BeneficiaryDetail.FarmerID, TransactionID = x.TransactionID, PaymentAmount = x.BeneficiaryDetail.RegistrationAmount }).FirstOrDefault();
        }

        public TransactionReceipt GetTransactionReceipt(string transactionID)
        {
            var p = orm.Payments.Where(z => z.TransactionID == transactionID).Select(x => new TransactionReceipt { ReferenceNo = x.ReferenceNo, FarmerID = x.BeneficiaryDetail.FarmerID, TransactionStatus = x.TransactionStatus, BankTransactionID = x.BankTransactionID, BankResponseCode = x.BankResponseCode, BankResponseName = x.EasyPayErrorCode.Description, BankTransactionDate = x.BankResponseDate, BankTransactionTime = x.BankResponseTime, PayMode = x.PaymentMode, TotalAmount = x.TotalAmount }).FirstOrDefault();
            if (p.TransactionStatus == "Success" && p.BankResponseCode == "E000")
            {
                var k = orm.BeneficiaryDetails.Where(z => z.ReferenceNo == p.ReferenceNo).Select(x => new { x.FarmerID, x.MobileNo, x.BLORecord.BLOUserName }).FirstOrDefault();
                var l = orm1.M_FARMER_REGISTRATION.Where(z => z.NICFARMERID == k.FarmerID).Select(x => x.VCHFARMERNAME);
                var n = orm.BLOBlockMappings.Where(z => z.UserID == k.BLOUserName).FirstOrDefault();
                dynamic i = null;
                if (n != null)
                {
                    i = orm.BLODetailEntries.Where(z => z.DepartmentCode == n.DeptCode && z.BlockCode == n.BlockCode).Select(x => x.MobileNo).FirstOrDefault();
                }
                else
                {
                    i = "9437181840";
                }
                var q = Convert.ToString(p.BankTransactionDate.Value.AddDays(15));
                string h = q.ToString().Substring(0, 10);
                string SMSBLO = "APICOL - CAE Application of Smt / Sri " + l + " is pending for feasibility report by " + h;
                string SMSE = "APICOL - Registration is successful. Payment is done.";
                SMSGateway.SendSMS(k.MobileNo, SMSE);
                SMSGateway.SendSMS(i, SMSBLO);
            }
            return p;
        }

        public TransactionReceipt GetReceiptDetails(string referenceNo)
        {
            return orm.Payments.Where(z => z.ReferenceNo == referenceNo && z.TransactionStatus == "Success").Select(x => new TransactionReceipt { ReferenceNo = x.ReferenceNo, FarmerID = x.BeneficiaryDetail.FarmerID, TransactionStatus = x.TransactionStatus, BankTransactionID = x.BankTransactionID, BankResponseCode = x.BankResponseCode, BankResponseName = x.EasyPayErrorCode.Description, BankTransactionDate = x.BankResponseDate, BankTransactionTime = x.BankResponseTime, PayMode = x.PaymentMode, TotalAmount = x.TotalAmount }).FirstOrDefault();
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

        public void InsertAfterSuccessfulPayment(string referenceNo, string pStatus, string brCode, string tCode)
        {
            orm.INSERTRECORDAFTERPAYMENT(referenceNo, pStatus, brCode, tCode);
        }
    }
}