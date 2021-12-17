using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommercialAgriEnterprise.Models
{
    public class AdminBAL
    {
        CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();

        public List<ActivityLog> GetAuditTrail()
        {
            return orm.ActivityLogs.Where(a => a.UserID.Substring(0, 3) == "aao" || a.UserID.Substring(0, 3) == "afo" || a.UserID.Substring(0, 3) == "aho" || a.UserID.Substring(0, 3) == "bvo" || a.UserID.Substring(0, 3) == "dno").ToList();
        }

        public bool UnlockBlolist(List<LockListUser> userlst)
        {
            if (userlst.Count > 0)
            {
                var k = new ApplicationDbContext();
                foreach (var i in userlst)
                {
                    var x = k.Users.Where(p => p.UserName == i.usernm).FirstOrDefault();
                    x.AccessFailedCount = 0;
                }
                try
                {
                    k.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    var sx = ex;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool UnlockDnolist(List<LockListUser> userlst)
        {
            if (userlst != null)
            {
                var k = new ApplicationDbContext();
                foreach (var i in userlst)
                {
                    var x = k.Users.Where(p => p.UserName == i.usernm).FirstOrDefault();
                    x.AccessFailedCount = 0;
                }
                try
                {
                    k.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    var sx = ex;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool UnlockCollectorlist(List<LockListUser> userlst)
        {
            if (userlst != null)
            {
                var k = new ApplicationDbContext();
                foreach (var i in userlst)
                {
                    var x = k.Users.Where(p => p.UserName == i.usernm).FirstOrDefault();
                    x.AccessFailedCount = 0;
                }
                try
                {
                    k.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    var sx = ex;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public List<BeneficiaryProjectDetail> GetRecord()
        {
            return orm.BeneficiaryProjectDetails.ToList();
        }

        public List<Department> GetDepartment()
        {
            return orm.Departments.ToList();
        }

        public List<LGDDistrict> GetDistrict()
        {
            return orm.LGDDistricts.ToList();
        }

        //public List<AdminDNODetailEntry> GetEnteredDistrict(string departmentCode)
        //{
        //    return orm.AdminDNODetailEntries.Where(z => z.DepartmentCode == departmentCode).ToList();
        //}

        //public AdminDNODetailEntry GetRecordbyDeptDist(string departmentCode, int districtCode)
        //{
        //    return orm.AdminDNODetailEntries.Where(z => z.DepartmentCode == departmentCode && z.DistrictCode == districtCode).FirstOrDefault();
        //}

        public List<LGDBlock> GetBlockbyDistrict(int districtCode, string departmentCode)
        {
            return orm.LGDBlocks.Where(z => z.DistrictCode == districtCode).ToList();
        }

        //public bool SubmitBLORecords(List<AdminBLODetailEntry> x)
        //{
        //    orm.AdminBLODetailEntries.AddRange(x);
        //    try
        //    {
        //        orm.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //        return false;
        //    }
        //}

        //public bool SubmitAllDNORecords(List<AdminDNODetailEntry> x)
        //{
        //    orm.AdminDNODetailEntries.AddRange(x);
        //    try
        //    {
        //        orm.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //        return false;
        //    }
        //}

        //public bool UpdateBLORecord(string departmentCode, int districtCode, int blockCode, AdminBLODetailModify myData)
        //{
        //    var k = orm.AdminBLODetailEntries.Where(z => z.DepartmentCode == departmentCode && z.DistrictCode == districtCode && z.BlockCode == blockCode).FirstOrDefault();
        //    k.Name = myData.Name;
        //    k.MobileNo = myData.MobileNo;
        //    k.UserDateTime = DateTime.Now;
        //    k.IPAddress = HttpContext.Current.Request.UserHostAddress;
        //    k.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
        //    try
        //    {
        //        orm.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //        return false;
        //    }
        //}

        //public bool UpdateDNORecord(string departmentCode, int districtCode, AdminDNODetailModify myData)
        //{
        //    var k = orm.AdminDNODetailEntries.Where(z => z.DepartmentCode == departmentCode && z.DistrictCode == districtCode).FirstOrDefault();
        //    k.Name = myData.Name;
        //    k.MobileNo = myData.MobileNo;
        //    k.UserDateTime = DateTime.Now;
        //    k.IPAddress = HttpContext.Current.Request.UserHostAddress;
        //    k.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
        //    try
        //    {
        //        orm.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //        return false;
        //    }
        //}

        //public List<AdminBLODetailEntry> GetRecordbyDistDept(int districtCode, string departmentCode)
        //{
        //    return orm.AdminBLODetailEntries.Where(z => z.DistrictCode == districtCode && z.DepartmentCode == departmentCode).ToList();
        //}

        //public AdminBLODetailEntry GetRecordbyDeptDistBlock(string departmentCode, int districtCode, int blockCode)
        //{
        //    return orm.AdminBLODetailEntries.Where(z => z.DepartmentCode == departmentCode && z.DistrictCode == districtCode && z.BlockCode == blockCode).FirstOrDefault();
        //}

        //public bool SubmitDNORecords(AdminDNODetailEntry x)
        //{
        //    x.UserDateTime = DateTime.Now;
        //    x.IPAddress = HttpContext.Current.Request.UserHostAddress; ;
        //    x.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
        //    orm.AdminDNODetailEntries.Add(x);
        //    try
        //    {
        //        orm.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //        return false;
        //    }
        //}

        //public List<LGDDistrict> ExcludedDistrictByDept(string departmentCode)
        //{
        //    var k = orm.AdminDNODetailEntries.Where(z => z.DepartmentCode == departmentCode).Select(x => x.DistrictCode).ToList();
        //    var l = orm.LGDDistricts.ToList();
        //    foreach (var i in k)
        //    {
        //        l.Remove(l.Where(a => a.DistrictCode == i).FirstOrDefault());
        //    }
        //    var m = l;
        //    return m;
        //}

        public List<GEtTargetProject> GetTargetProject()
        {
            var k = orm.ProjectTypes.Where(z => z.Code == "04P16" || z.Code == "04P20" || z.Code == "01P1").Select(z => new GEtTargetProject { Code = z.Code, Name = z.Name }).ToList();
            return k;
        }

        public List<LGDDistrict> BindTargetDist()
        {
            return orm.LGDDistricts.ToList();
        }

        public string FindTarget(string PCODE)
        {
            return orm.Targets.Where(z => z.ProjectTypeCode == PCODE).Select(z => z.ProjectTypeCode).FirstOrDefault();
        }

        public string SubmitAllTarget(List<Target> AllTarget)
        {
            foreach (var i in AllTarget)
            {
                i.UserDateTime = DateTime.Now;
                i.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                i.IPAddress = HttpContext.Current.Request.UserHostAddress;
            }
            try
            {
                orm.Targets.AddRange(AllTarget);
                orm.SaveChanges();
                return "Target Saved Sucessfully.";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string Updatetargetlist(List<Target> UpdateTarget, string FYR)
        {
            foreach (var i in UpdateTarget)
            {
                TargetLog TL = new TargetLog();
                var k = orm.Targets.Where(z => z.DistrictCode == i.DistrictCode && z.FinancialYear == FYR && z.ProjectTypeCode == i.ProjectTypeCode).FirstOrDefault();
                if (k.GeneralValue != i.GeneralValue || k.SCValue != i.SCValue || k.STValue != i.STValue)
                {
                    k.GeneralValue = i.GeneralValue;
                    k.SCValue = i.SCValue;
                    k.STValue = i.STValue;
                    TL.DistrictCode = k.DistrictCode;
                    TL.ProjectTypeCode = k.ProjectTypeCode;
                    TL.GeneralValue = k.GeneralValue;
                    TL.SCValue = k.SCValue;
                    TL.STValue = k.STValue;
                    TL.FinancialYear = k.FinancialYear;
                    TL.UserDateTime = DateTime.Now;
                    TL.IPAddress = HttpContext.Current.Request.UserHostAddress;
                    orm.TargetLogs.Add(TL);
                }
            }
            try
            {
                orm.SaveChanges();
                return "Records updated successfully.";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public class GEtTargetProject
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }

        public void ResetPassword(string EmailID, string Password)
        {
            orm.ResetPassword(EmailID, Password);
        }

        public List<DNODetailRecord_Result> DNOList()
        {
            return orm.DNODetailRecord().ToList();
        }

        public List<BlockwiseBLORegistration_Result> BlockwiseBLORegdCount()
        {
            return orm.BlockwiseBLORegistration().ToList();
        }

        public List<TARGET_ALL_Result> GetAllTarget(String P_CODE, String F_YEAR)
        {
            return orm.TARGET_ALL(P_CODE, F_YEAR).ToList();
        }

        public bool UpdateDNODetails(DNODetailEntry x, string deptCode, int distCode, string userID)
        {
            string desg = string.Empty;
            string dnoUserID = string.Empty;
            if (deptCode == "01") { desg = "dno_agri_"; } else if (deptCode == "02") { desg = "dno_horti_"; } else if (deptCode == "03") { desg = "dno_fish_"; } else if (deptCode == "04") { desg = "dno_ard_"; }
            string distName = orm.LGDDistricts.Where(z => z.DistrictCode == distCode).Select(a => a.DistrictName).FirstOrDefault();
            dnoUserID = desg + distName.ToLower().Trim();
            var k = orm.DNODetailEntries.Where(z => z.DepartmentCode == deptCode && z.DistrictCode == distCode).FirstOrDefault();
            DNODetailLog ddl = new DNODetailLog();
            ddl.DNOUserID = dnoUserID; ddl.DisableDateTime = DateTime.Now; ddl.DNOName = k.Name; ddl.DNOMobileNo = k.MobileNo; ddl.DNOSignature = k.Signature; ddl.DeptCode = deptCode; ddl.DistrictCode = distCode; ddl.IPAddress = HttpContext.Current.Request.UserHostAddress; ddl.UserName = userID; ddl.UserDateTime = DateTime.Now; ddl.FinancialYear = GenerateReferencenumber.GetCurrentFinancialYear(); ddl.IsActive = 0;
            orm.DNODetailLogs.Add(ddl);
            var l = orm.DNODetailEntries.Where(z => z.DepartmentCode == deptCode && z.DistrictCode == distCode).FirstOrDefault();
            l.Name = x.Name; l.MobileNo = x.MobileNo; l.Signature = x.Signature; l.UserDateTime = DateTime.Now; l.IPAddress = HttpContext.Current.Request.UserHostAddress; l.FinancialYear = GenerateReferencenumber.GetCurrentFinancialYear();
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return false;
            }
        }
    }
}