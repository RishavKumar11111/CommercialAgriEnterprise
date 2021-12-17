using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommercialAgriEnterprise.Models
{
    public class CollectorBAL
    {
        CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();

        public string AddRefNoDateTime(List<CollectorRecord> lcr, string dateOfMeeting)
        {
            DateTime startDate = Convert.ToDateTime(dateOfMeeting.Substring(4, 11));
            string sDate = startDate.ToString("yyyy/MM/dd");
            DateTime meetingDate = Convert.ToDateTime(sDate);
            var compareDate = orm.CollectorRecords.Where(z => z.DateOfMeeting == meetingDate).FirstOrDefault();
            if (compareDate == null)
            {
                var gLN = GenerateLotNo();
                var gFY = GenerateGoaheadnumber.GetCurrentFinancialYear();
                foreach (var i in lcr)
                {
                    var k = orm.CollectorRecords.Where(z => z.ReferenceNo == i.ReferenceNo).FirstOrDefault();
                    k.CollectorStatus = 1;
                    k.LotNo = gLN;
                    k.DateOfMeeting = meetingDate;
                    k.TimeOfMeeting = i.TimeOfMeeting;
                    k.IPAddress = HttpContext.Current.Request.UserHostAddress;
                    k.UserName = HttpContext.Current.Session["uid"].ToString();
                    k.UserDateTime = DateTime.Now;
                    k.FinancialYear = gFY;
                }
                try
                {
                    orm.SaveChanges();
                    foreach (var i in lcr)
                    {
                        var s = orm.BeneficiaryDetails.Where(z => z.ReferenceNo == i.ReferenceNo).Select(g => new { g.NICDistrictCode, g.BeneficiaryProjectDetail.DepartmentCode }).FirstOrDefault();
                        int p = Convert.ToInt32(s.NICDistrictCode);
                        var t = orm.DNODetailEntries.Where(z => z.DistrictCode == p && z.DepartmentCode == s.DepartmentCode).Select(g => g.MobileNo).FirstOrDefault();
                        var r = orm.DNODetailEntries.Where(z => z.DistrictCode == p && z.DepartmentCode == "01").Select(g => g.MobileNo).FirstOrDefault();
                        var L = orm.BeneficiaryProjectDetails.Where(z => z.ReferenceNo == i.ReferenceNo).Select(z => z.ProjectTypeCode).FirstOrDefault();
                        var l = orm.CollectorRecords.Where(z => z.ReferenceNo == i.ReferenceNo).Select(x => x.DateOfMeeting).FirstOrDefault();
                        string h = l.ToString().Substring(0, 10);
                        string SMS = "APICOL - Convene DLSSC meeting by " + h;
                        if (L == "04P15" && r != null)
                        {
                            SMSGateway.SendSMS(r, SMS);
                        }
                        SMSGateway.SendSMS(t, SMS);
                        if (r != null)
                        {
                            SMSGateway.SendSMS(r, SMS);
                        }
                    }
                    return gLN.ToString();
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
            {
                return "This date is already booked for a meeting. Please select an another date.";
            }
        }

        public bool UpdateRecords(int lotNo, string dateOfMeeting, string timeOfMeeting)
        {
            if (lotNo != 0 && dateOfMeeting != null && timeOfMeeting != null)
            {
                DateTime startDate = Convert.ToDateTime(dateOfMeeting.Substring(4, 11));
                string sDate = startDate.ToString("yyyy/MM/dd");
                DateTime meetingDate = Convert.ToDateTime(sDate);
                var k = orm.CollectorRecords.Where(z => z.LotNo == lotNo && z.CollectorUpdateStatus == null).ToList();
                if (k != null)
                {
                    foreach (var i in k)
                    {
                        i.CollectorUpdateStatus = 1;
                        i.UpdatedDOM = Convert.ToDateTime(meetingDate);
                        i.UpdatedTOM = TimeSpan.Parse(timeOfMeeting);
                        i.UpdatedIPAddress = HttpContext.Current.Request.UserHostAddress;
                        i.UpdatedUserName = HttpContext.Current.Session["uid"].ToString();
                        i.UpdatedUserDateTime = DateTime.Now;
                        i.UpdatedFinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                    }
                    try
                    {
                        orm.SaveChanges();
                        foreach (var i in k)
                        {
                            var s = orm.BeneficiaryDetails.Where(z => z.ReferenceNo == i.ReferenceNo).Select(g => new { g.NICDistrictCode, g.BeneficiaryProjectDetail.DepartmentCode }).FirstOrDefault();
                            int p = Convert.ToInt32(s.NICDistrictCode);
                            var t = orm.DNODetailEntries.Where(z => z.DistrictCode == p && z.DepartmentCode == s.DepartmentCode).Select(g => g.MobileNo).FirstOrDefault();
                            var r = orm.DNODetailEntries.Where(z => z.DistrictCode == p && z.DepartmentCode == "01").Select(g => g.MobileNo).FirstOrDefault();
                            var L = orm.BeneficiaryProjectDetails.Where(z => z.ReferenceNo == i.ReferenceNo).Select(z => z.ProjectTypeCode).FirstOrDefault();
                            var l = orm.CollectorRecords.Where(z => z.ReferenceNo == i.ReferenceNo).Select(x => x.DateOfMeeting).FirstOrDefault();
                            string h = l.ToString().Substring(0, 10);
                            string SMS = "APICOL - Convene DLSSC meeting by " + h;
                            if (L == "04P15" && r != null)
                            {
                                SMSGateway.SendSMS(r, SMS);
                            }
                            SMSGateway.SendSMS(t, SMS);
                            if (r != null)
                            {
                                SMSGateway.SendSMS(r, SMS);
                            }
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        var e = ex;
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public List<CollectorRecord> GetLotNo(string userID)
        {
            return orm.CollectorRecords.Where(z => z.LotNo != null && z.UserName == userID).ToList();
        }

        public string BLONotFeasibleReason(string ReferenceNo)
        {
            return orm.BLORecords.Where(z => z.ReferenceNo == ReferenceNo).Select(z => z.RejectionReason).FirstOrDefault();
        }

        public string DNONotRecommendedReason(string ReferenceNo)
        {
            var m = orm.DNORecords.Where(z => z.ReferenceNo == ReferenceNo).Select(z => z.RejectionReason).FirstOrDefault();
            if (m == null)
            {
                var k = orm.DDARecords.Where(z => z.ReferenceNo == ReferenceNo).Select(z => z.DDARejectionReason).FirstOrDefault();
                return k;
            }
            else
            {
                return m;
            }
        }

        public string DDARejectionReason(string ReferenceNo)
        {
            return orm.DDARecords.Where(z => z.ReferenceNo == ReferenceNo).Select(z => z.DDARejectionReason).FirstOrDefault();
        }

        public string CompareMeetingDate(string meetingDate)
        {
            DateTime dt = Convert.ToDateTime(meetingDate.Substring(4, 11));
            var k = orm.CollectorRecords.Where(a => a.DateOfMeeting == dt).FirstOrDefault();
            var l = orm.CollectorRecords.Where(x => x.UpdatedDOM == dt).FirstOrDefault();
            if (l != null)
            {
                return "This date is already booked for a meeting. Please select an another date.";
            }
            else
            {
                if (k != null)
                {
                    return "This date is already booked for a meeting. Please select an another date.";
                }
                else
                {
                    return "OK";
                }
            }
        }

        public string CompareMeetingDateUpdate(string meetingDate, int lotNo)
        {
            DateTime dt = Convert.ToDateTime(meetingDate.Substring(4, 11));
            var k = orm.CollectorRecords.Where(a => a.DateOfMeeting == dt).FirstOrDefault();
            var l = orm.CollectorRecords.Where(x => x.UpdatedDOM == dt).FirstOrDefault();
            if (l != null)
            {
                if (l.LotNo == lotNo)
                {
                    return "OK";
                }
                else
                {
                    return "This date is already booked for a meeting. Please select an another date.";
                }
            }
            else
            {
                if (k != null)
                {
                    if (k.UpdatedDOM != null)
                    {
                        return "OK";
                    }
                    else
                    {
                        if (k.LotNo == lotNo)
                        {
                            return "OK";
                        }
                        else
                        {
                            return "This date is already booked for a meeting. Please select an another date.";
                        }
                    }
                }
                else
                {
                    return "OK";
                }
            }
        }

        private int GenerateLotNo()
        {
            int? lot;
            int lotNo;
            int lotCount = orm.CollectorRecords.Where(z => z.LotNo != 0).Count();
            lot = orm.CollectorRecords.OrderByDescending(z => z.LotNo).Select(x => x.LotNo).FirstOrDefault();
            if (lot == null)
            {
                lot = 1;
            }
            else
            {
                lot += 1;
            }
            lotNo = Convert.ToInt32(lot);
            return lotNo;
        }

        public void UpdateCollectorPassword(string username, string password)
        {
            orm.updtcollectorPassword(username, password);
        }

        public void UpdateFailedCount(string Email, int Count)
        {
            orm.UpdtAccessFailCount(Email, Count);
        }
    }
}