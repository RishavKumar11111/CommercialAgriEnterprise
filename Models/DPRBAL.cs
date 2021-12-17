using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommercialAgriEnterprise.Models
{
    public class DPRBAL
    {
        CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();
        FARMERDBEntities orm1 = new FARMERDBEntities();

        public DPRVM GetBeneficiaryDetail(string referenceNo, string farmerID, string aadharNo)
        {
            var data = orm1.M_FARMER_REGISTRATION.Where(a => a.NICFARMERID == farmerID).FirstOrDefault();
            if (data != null)
            {
                if (ConvertToSHA256(data.VCHAADHARNO) != aadharNo)
                {
                    return null;
                }
                else
                {
                    var details = orm.BeneficiaryDetails.Where(z => z.ReferenceNo == referenceNo && z.FarmerID == data.NICFARMERID).FirstOrDefault();
                    if (details != null && data != null)
                    {
                        string beneficiaryName = string.Empty;
                        if (details.RelationApplicantName != null)
                        {
                            beneficiaryName = details.RelationApplicantName;
                        }
                        else
                        {
                            beneficiaryName = data.VCHFARMERNAME;
                        }
                        var projectDetails = orm.BeneficiaryProjectDetails.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
                        string departmentName = orm.Departments.Where(z => z.Code == projectDetails.DepartmentCode).Select(a => a.Name).FirstOrDefault();
                        string projectName = orm.ProjectTypes.Where(z => z.Code == projectDetails.ProjectTypeCode).Select(a => a.Name).FirstOrDefault();
                        string aadhaarNo = string.Empty;
                        if (data.VCHAADHARNO.Length > 4)
                        {
                            aadhaarNo = "********" + data.VCHAADHARNO.Substring(data.VCHAADHARNO.Length - 4, 4);
                        }
                        return orm.BeneficiaryDetails.Where(x => x.ReferenceNo == referenceNo).Select(x => new DPRVM { BeneficiaryName = beneficiaryName, DepartmentName = departmentName, ProjectTypeName = projectName, Capacity = projectDetails.Capacity, AadhaarNo = aadhaarNo }).FirstOrDefault();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public DPRVM GetBeneficiaryDetailMasked(string referenceNo, string farmerID, string aadharNo)
        {
            var data = orm1.M_FARMER_REGISTRATION.Where(a => a.NICFARMERID == farmerID).FirstOrDefault();
            if (data != null)
            {
                string aadhaarNo = string.Empty;
                if (data.VCHAADHARNO.Length > 4)
                {
                    aadhaarNo = "********" + data.VCHAADHARNO.Substring(data.VCHAADHARNO.Length - 4, 4);
                }
                if (ConvertToSHA256(aadhaarNo) != aadharNo)
                {
                    return null;
                }
                else
                {
                    var details = orm.BeneficiaryDetails.Where(z => z.ReferenceNo == referenceNo && z.FarmerID == data.NICFARMERID).FirstOrDefault();
                    if (details != null && data != null)
                    {
                        string beneficiaryName = string.Empty;
                        if (details.RelationApplicantName != null)
                        {
                            beneficiaryName = details.RelationApplicantName;
                        }
                        else
                        {
                            beneficiaryName = data.VCHFARMERNAME;
                        }
                        var projectDetails = orm.BeneficiaryProjectDetails.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
                        string departmentName = orm.Departments.Where(z => z.Code == projectDetails.DepartmentCode).Select(a => a.Name).FirstOrDefault();
                        string projectName = orm.ProjectTypes.Where(z => z.Code == projectDetails.ProjectTypeCode).Select(a => a.Name).FirstOrDefault();
                        return orm.BeneficiaryDetails.Where(x => x.ReferenceNo == referenceNo).Select(x => new DPRVM { BeneficiaryName = beneficiaryName, DepartmentName = departmentName, ProjectTypeName = projectName, Capacity = projectDetails.Capacity, AadhaarNo = aadhaarNo }).FirstOrDefault();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public string ConvertToSHA256(string pass)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(pass);
            System.Security.Cryptography.SHA256Managed sha256hasstring = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hasstring.ComputeHash(bytes);
            return ByteArrayToHexString(hash);
        }

        public static string ByteArrayToHexString(byte[] ba)
        {
            System.Text.StringBuilder hex = new System.Text.StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        public string CheckReferenceNo(string referenceNo, string farmerID, string aadhaarNo)
        {
            if (GetBeneficiaryDetailMasked(referenceNo, farmerID, aadhaarNo) != null)
            {
                var refNo = orm.DPRStatus.Where(z => z.ReferenceNo == referenceNo).Select(x => x.Status).FirstOrDefault();
                var regdStatus = orm.BeneficiaryDetails.Where(z => z.ReferenceNo == referenceNo && z.RegistrationStatus == "completed").Select(x => x.RegistrationStatus).FirstOrDefault();
                if (refNo == 0 && regdStatus == "completed")
                {
                    return "Successful";
                }
                else if (regdStatus != "completed")
                {
                    return "You have not completed the Registration.";
                }
                else if (refNo == 1 && regdStatus == "completed")
                {
                    return "You have completed the DPR.";
                }
                else
                {
                    return "Something is wrong.";
                }
            }
            else
            {
                return "Please enter the correct details.";
            }
        }

        public List<ProfitabilityProjection> GetProfitabilityProjectionDetails(string referenceNo)
        {
            return orm.ProfitabilityProjections.Where(z => z.ReferenceNo == referenceNo).ToList();
        }

        public ProfitabilityProjection GetProfitabilityProjectionData(string referenceNo, string year)
        {
            return orm.ProfitabilityProjections.Where(z => z.ReferenceNo == referenceNo && z.Year == year).FirstOrDefault();
        }

        public List<CashFlowStatement> GetCashFlowStatementDetails(string referenceNo)
        {
            return orm.CashFlowStatements.Where(z => z.ReferenceNo == referenceNo).ToList();
        }

        public CashFlowStatement GetCashFlowStatementData(string referenceNo, string year)
        {
            return orm.CashFlowStatements.Where(z => z.ReferenceNo == referenceNo && z.Year == year).FirstOrDefault();
        }

        public bool AddInvestmentLand(CapitalInvestmentLand cil, CapitalInvestment ci)
        {
            orm.CapitalInvestmentLands.Add(cil);
            orm.CapitalInvestments.Add(ci);
            DPRStatu ds = new DPRStatu();
            ds.ReferenceNo = cil.ReferenceNo;
            ds.Status = 0;
            orm.DPRStatus.Add(ds);
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = cil.ReferenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool UpdateInvestmentLand(string referenceNo, CapitalInvestmentLand cil, CapitalInvestment ci, string userID)
        {
            var k = orm.CapitalInvestmentLands.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (userID != null && k != null)
            {
                BLODPRLog bdl = new BLODPRLog();
                bdl.ReferenceNo = k.ReferenceNo;
                bdl.Development = k.Development;
                bdl.Fencing = k.Fencing;
                bdl.LandTotal = k.Total;
                bdl.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                bdl.TotalProjectCost = ci.TotalProjectCost;
                bdl.BLOUserName = userID;
                bdl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                bdl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                bdl.UserDateTime = DateTime.Now;
                orm.BLODPRLogs.Add(bdl);
            }
            if (k != null)
            {
                k.Development = cil.Development;
                k.Fencing = cil.Fencing;
                k.Total = cil.Total;
            }
            var m = orm.CapitalInvestments.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (m != null)
            {
                m.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                m.TotalProjectCost = ci.TotalProjectCost;
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = referenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool AddInvestmentCivilConstuction(CapitalInvestmentCivilConstruction cicc, CapitalInvestment ci)
        {
            var t = orm.CapitalInvestmentLands.Where(z => z.ReferenceNo == cicc.ReferenceNo).FirstOrDefault();
            if (t != null)
            {
                orm.CapitalInvestmentCivilConstructions.Add(cicc);
                var k = orm.CapitalInvestments.Where(z => z.ReferenceNo == ci.ReferenceNo).FirstOrDefault();
                if (k != null)
                {
                    k.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                    k.TotalProjectCost = ci.TotalProjectCost;
                }
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = cicc.ReferenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool UpdateInvestmentCivilConstruction(string referenceNo, CapitalInvestmentCivilConstruction cicc, CapitalInvestment ci, string userID)
        {
            var k = orm.CapitalInvestmentCivilConstructions.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (userID != null && k != null)
            {
                BLODPRLog bdl = new BLODPRLog();
                bdl.ReferenceNo = k.ReferenceNo;
                bdl.Building = k.Building;
                bdl.Shed = k.Shed;
                bdl.OfficeCumStore = k.OfficeCumStore;
                bdl.Godown = k.Godown;
                bdl.PlantArea = k.PlantArea;
                bdl.OtherNecessaryConstruction = k.OtherNecessaryConstruction;
                bdl.CivilConstructionTotal = k.Total;
                bdl.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                bdl.TotalProjectCost = ci.TotalProjectCost;
                bdl.BLOUserName = userID;
                bdl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                bdl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                bdl.UserDateTime = DateTime.Now;
                orm.BLODPRLogs.Add(bdl);
            }
            if (k != null)
            {
                k.Building = cicc.Building;
                k.Shed = cicc.Shed;
                k.OfficeCumStore = cicc.OfficeCumStore;
                k.Godown = cicc.Godown;
                k.PlantArea = cicc.PlantArea;
                k.OtherNecessaryConstruction = cicc.OtherNecessaryConstruction;
                k.Total = cicc.Total;
            }
            var m = orm.CapitalInvestments.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (m != null)
            {
                m.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                m.TotalProjectCost = ci.TotalProjectCost;
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = referenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool AddInvestmentWaterSupply(CapitalInvestmentWaterSupply ciws, CapitalInvestment ci)
        {
            var t = orm.CapitalInvestmentCivilConstructions.Where(z => z.ReferenceNo == ciws.ReferenceNo).FirstOrDefault();
            if (t != null)
            {
                orm.CapitalInvestmentWaterSupplies.Add(ciws);
                var k = orm.CapitalInvestments.Where(z => z.ReferenceNo == ci.ReferenceNo).FirstOrDefault();
                if (k != null)
                {
                    k.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                    k.TotalProjectCost = ci.TotalProjectCost;
                }
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = ciws.ReferenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool UpdateInvestmentWaterSupply(string referenceNo, CapitalInvestmentWaterSupply ciws, CapitalInvestment ci, string userID)
        {
            var k = orm.CapitalInvestmentWaterSupplies.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (userID != null && k != null)
            {
                BLODPRLog bdl = new BLODPRLog();
                bdl.ReferenceNo = k.ReferenceNo;
                bdl.WellOrRiver = k.WellOrRiver;
                bdl.PumpPipeLine = k.PumpPipeLine;
                bdl.Tank = k.Tank;
                bdl.SprinklerFogger = k.SprinklerFogger;
                bdl.WaterSupplyTotal = k.Total;
                bdl.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                bdl.TotalProjectCost = ci.TotalProjectCost;
                bdl.BLOUserName = userID;
                bdl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                bdl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                bdl.UserDateTime = DateTime.Now;
                orm.BLODPRLogs.Add(bdl);
            }
            if (k != null)
            {
                k.WellOrRiver = ciws.WellOrRiver;
                k.PumpPipeLine = ciws.PumpPipeLine;
                k.Tank = ciws.Tank;
                k.SprinklerFogger = ciws.SprinklerFogger;
                k.Total = ciws.Total;
            }
            var m = orm.CapitalInvestments.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (m != null)
            {
                m.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                m.TotalProjectCost = ci.TotalProjectCost;
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = referenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool AddInvestmentElectrification(CapitalInvestmentElectrification cie, CapitalInvestment ci)
        {
            var t = orm.CapitalInvestmentWaterSupplies.Where(z => z.ReferenceNo == cie.ReferenceNo).FirstOrDefault();
            if (t != null)
            {
                orm.CapitalInvestmentElectrifications.Add(cie);
                var k = orm.CapitalInvestments.Where(z => z.ReferenceNo == ci.ReferenceNo).FirstOrDefault();
                if (k != null)
                {
                    k.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                    k.TotalProjectCost = ci.TotalProjectCost;
                }
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = cie.ReferenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool UpdateInvestmentElectrification(string referenceNo, CapitalInvestmentElectrification cie, CapitalInvestment ci, string userID)
        {
            var k = orm.CapitalInvestmentElectrifications.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (userID != null && k != null)
            {
                BLODPRLog bdl = new BLODPRLog();
                bdl.ReferenceNo = k.ReferenceNo;
                bdl.InstallationFitting = k.InstallationFitting;
                bdl.Transformer = k.Transformer;
                bdl.DGSet = k.DGSet;
                bdl.ElectrificationTotal = k.Total;
                bdl.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                bdl.TotalProjectCost = ci.TotalProjectCost;
                bdl.BLOUserName = userID;
                bdl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                bdl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                bdl.UserDateTime = DateTime.Now;
                orm.BLODPRLogs.Add(bdl);
            }
            if (k != null)
            {
                k.InstallationFitting = cie.InstallationFitting;
                k.Transformer = cie.Transformer;
                k.DGSet = cie.DGSet;
                k.Total = cie.Total;
            }
            var m = orm.CapitalInvestments.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (m != null)
            {
                m.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                m.TotalProjectCost = ci.TotalProjectCost;
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = referenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool AddInvestmentPlantMachinery(CapitalInvestmentPlantMachinery cipm, CapitalInvestment ci)
        {
            var t = orm.CapitalInvestmentElectrifications.Where(z => z.ReferenceNo == cipm.ReferenceNo).FirstOrDefault();
            if (t != null)
            {
                orm.CapitalInvestmentPlantMachineries.Add(cipm);
                var k = orm.CapitalInvestments.Where(z => z.ReferenceNo == ci.ReferenceNo).FirstOrDefault();
                if (k != null)
                {
                    k.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                    k.TotalProjectCost = ci.TotalProjectCost;
                }
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = cipm.ReferenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool UpdateInvestmentPlantMachinery(string referenceNo, CapitalInvestmentPlantMachinery cipm, CapitalInvestment ci, string userID)
        {
            var k = orm.CapitalInvestmentPlantMachineries.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (userID != null && k != null)
            {
                BLODPRLog bdl = new BLODPRLog();
                bdl.ReferenceNo = k.ReferenceNo;
                bdl.Equipment = k.Equipment;
                bdl.ManualOrMotor = k.ManualOrMotor;
                bdl.PlantMachineryTotal = k.Total;
                bdl.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                bdl.TotalProjectCost = ci.TotalProjectCost;
                bdl.BLOUserName = userID;
                bdl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                bdl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                bdl.UserDateTime = DateTime.Now;
                orm.BLODPRLogs.Add(bdl);
            }
            if (k != null)
            {
                k.Equipment = cipm.Equipment;
                k.ManualOrMotor = cipm.ManualOrMotor;
                k.Total = cipm.Total;
            }
            var m = orm.CapitalInvestments.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (m != null)
            {
                m.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                m.TotalProjectCost = ci.TotalProjectCost;
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = referenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool AddInvestmentAnimalPlant(CapitalInvestmentAnimalPlant ciap, CapitalInvestment ci)
        {
            var t = orm.CapitalInvestmentPlantMachineries.Where(z => z.ReferenceNo == ciap.ReferenceNo).FirstOrDefault();
            if (t != null)
            {
                orm.CapitalInvestmentAnimalPlants.Add(ciap);
                var k = orm.CapitalInvestments.Where(z => z.ReferenceNo == ci.ReferenceNo).FirstOrDefault();
                if (k != null)
                {
                    k.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                    k.TotalProjectCost = ci.TotalProjectCost;
                }
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = ciap.ReferenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool UpdateInvestmentAnimalPlant(string referenceNo, CapitalInvestmentAnimalPlant ciap, CapitalInvestment ci, string userID)
        {
            var k = orm.CapitalInvestmentAnimalPlants.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (userID != null && k != null)
            {
                BLODPRLog bdl = new BLODPRLog();
                bdl.ReferenceNo = k.ReferenceNo;
                bdl.AnimalPlantLiveStock = k.LiveStock;
                bdl.LiveStockType = k.LiveStockType;
                bdl.PoultryBreeder = k.PoultryBreeder;
                bdl.BroodFish = k.BroodFish;
                bdl.Plant = k.Plant;
                bdl.AnimalPlantTotal = k.Total;
                bdl.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                bdl.TotalProjectCost = ci.TotalProjectCost;
                bdl.BLOUserName = userID;
                bdl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                bdl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                bdl.UserDateTime = DateTime.Now;
                orm.BLODPRLogs.Add(bdl);
            }
            if (k != null)
            {
                k.LiveStock = ciap.LiveStock;
                k.LiveStockType = ciap.LiveStockType;
                k.PoultryBreeder = ciap.PoultryBreeder;
                k.BroodFish = ciap.BroodFish;
                k.Plant = ciap.Plant;
                k.Total = ciap.Total;
            }
            var m = orm.CapitalInvestments.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (m != null)
            {
                m.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                m.TotalProjectCost = ci.TotalProjectCost;
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = referenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool AddInvestmentMiscellaneous(CapitalInvestmentMiscellaneou cim, CapitalInvestment ci)
        {
            var t = orm.CapitalInvestmentAnimalPlants.Where(z => z.ReferenceNo == cim.ReferenceNo).FirstOrDefault();
            if (t != null)
            {
                orm.CapitalInvestmentMiscellaneous.Add(cim);
                var k = orm.CapitalInvestments.Where(z => z.ReferenceNo == ci.ReferenceNo).FirstOrDefault();
                if (k != null)
                {
                    k.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                    k.TotalProjectCost = ci.TotalProjectCost;
                }
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = cim.ReferenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool UpdateInvestmentMiscellaneous(string referenceNo, CapitalInvestmentMiscellaneou cim, CapitalInvestment ci, string userID)
        {
            var k = orm.CapitalInvestmentMiscellaneous.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (userID != null && k != null)
            {
                BLODPRLog bdl = new BLODPRLog();
                bdl.ReferenceNo = k.ReferenceNo;
                bdl.FixedAsset = k.FixedAsset;
                bdl.MiscellaneousLivestock = k.Livestock;
                bdl.Bird = k.Bird;
                bdl.Cultivation = k.Cultivation;
                bdl.MiscellaneousTotal = k.Total;
                bdl.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                bdl.TotalProjectCost = ci.TotalProjectCost;
                bdl.BLOUserName = userID;
                bdl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                bdl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                bdl.UserDateTime = DateTime.Now;
                orm.BLODPRLogs.Add(bdl);
            }
            if (k != null)
            {
                k.FixedAsset = cim.FixedAsset;
                k.Livestock = cim.Livestock;
                k.Bird = cim.Bird;
                k.Cultivation = cim.Cultivation;
                k.Total = cim.Total;
            }
            var m = orm.CapitalInvestments.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (m != null)
            {
                m.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                m.TotalProjectCost = ci.TotalProjectCost;
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = referenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool AddRecurringExpenditure(RecurringExpenditure re, CapitalInvestment ci)
        {
            var t = orm.CapitalInvestmentMiscellaneous.Where(z => z.ReferenceNo == re.ReferenceNo).FirstOrDefault();
            if (t != null)
            {
                orm.RecurringExpenditures.Add(re);
                var k = orm.CapitalInvestments.Where(z => z.ReferenceNo == ci.ReferenceNo).FirstOrDefault();
                if (k != null)
                {
                    k.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                    k.TotalProjectCost = ci.TotalProjectCost;
                }
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = re.ReferenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool UpdateRecurringExpenditure(string referenceNo, RecurringExpenditure re, CapitalInvestment ci, string userID)
        {
            var k = orm.RecurringExpenditures.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (userID != null && k != null)
            {
                BLODPRLog bdl = new BLODPRLog();
                bdl.ReferenceNo = k.ReferenceNo;
                bdl.RawMaterial = k.RawMaterial;
                bdl.Salary = k.Salary;
                bdl.Maintenance = k.Maintenance;
                bdl.RecurringExpenditureTotal = k.Total;
                bdl.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                bdl.TotalProjectCost = ci.TotalProjectCost;
                bdl.BLOUserName = userID;
                bdl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                bdl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                bdl.UserDateTime = DateTime.Now;
                orm.BLODPRLogs.Add(bdl);
            }
            if (k != null)
            {
                k.RawMaterial = re.RawMaterial;
                k.Salary = re.Salary;
                k.Maintenance = re.Maintenance;
                k.Total = re.Total;
            }
            var m = orm.CapitalInvestments.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (m != null)
            {
                m.TotalCapitalInvestmentCost = ci.TotalCapitalInvestmentCost;
                m.TotalProjectCost = ci.TotalProjectCost;
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = referenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool AddMeansOfFinance(MeansOfFinance mof)
        {
            var t = orm.RecurringExpenditures.Where(z => z.ReferenceNo == mof.ReferenceNo).FirstOrDefault();
            if (t != null)
            {
                orm.MeansOfFinances.Add(mof);
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = mof.ReferenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool UpdateMeansOfFinance(string referenceNo, MeansOfFinance mof, string userID)
        {
            var k = orm.MeansOfFinances.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (userID != null && k != null)
            {
                BLODPRLog bdl = new BLODPRLog();
                bdl.ReferenceNo = k.ReferenceNo;
                bdl.OwnInvestment = k.OwnInvestment;
                bdl.OwnInvestmentRemark = k.OwnInvestmentRemark;
                bdl.TermLoan = k.TermLoan;
                bdl.TermLoanRemark = k.TermLoanRemark;
                bdl.WorkingCapitalLoan = k.WorkingCapitalLoan;
                bdl.WorkingCapitalLoanRemark = k.WorkingCapitalLoanRemark;
                bdl.OtherSource = k.OtherSource;
                bdl.OtherSourceRemark = k.OtherSourceRemark;
                bdl.MeansOfFinanceTotal = k.TotalAmount;
                bdl.BLOUserName = userID;
                bdl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                bdl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                bdl.UserDateTime = DateTime.Now;
                orm.BLODPRLogs.Add(bdl);
            }
            if (k != null)
            {
                k.OwnInvestment = mof.OwnInvestment;
                k.OwnInvestmentRemark = mof.OwnInvestmentRemark;
                k.TermLoan = mof.TermLoan;
                k.TermLoanRemark = mof.TermLoanRemark;
                k.WorkingCapitalLoan = mof.WorkingCapitalLoan;
                k.WorkingCapitalLoanRemark = mof.WorkingCapitalLoanRemark;
                k.OtherSource = mof.OtherSource;
                k.OtherSourceRemark = mof.OtherSourceRemark;
                k.TotalAmount = mof.TotalAmount;
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = referenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool AddProfitabilityProjection(string referenceNo, List<ProfitabilityProjection> lpp)
        {
            var t = orm.MeansOfFinances.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (t != null)
            {
                orm.ProfitabilityProjections.AddRange(lpp);
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = referenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool UpdateProfitabilityProjection(string referenceNo, string year, ProfitabilityProjection lpp, string userID)
        {
            var k = orm.ProfitabilityProjections.Where(z => z.ReferenceNo == referenceNo && z.Year == year).FirstOrDefault();
            if (userID != null && k != null)
            {
                BLODPRLog bdl = new BLODPRLog();
                bdl.ReferenceNo = k.ReferenceNo;
                bdl.GrossRevenue = k.GrossRevenue;
                bdl.TotalExpenditure = k.TotalExpenditure;
                bdl.GrossProfit = k.GrossProfit;
                bdl.NetProfit = k.NetProfit;
                bdl.BLOUserName = userID;
                bdl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                bdl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                bdl.UserDateTime = DateTime.Now;
                orm.BLODPRLogs.Add(bdl);
            }
            if (k != null)
            {
                k.GrossRevenue = lpp.GrossRevenue;
                k.TotalExpenditure = lpp.TotalExpenditure;
                k.GrossProfit = lpp.GrossProfit;
                k.NetProfit = lpp.NetProfit;
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = referenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool AddCashFlowStatement(string referenceNo, List<CashFlowStatement> lcfs)
        {
            var t = orm.ProfitabilityProjections.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (t != null)
            {
                orm.CashFlowStatements.AddRange(lcfs);
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = referenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool UpdateCashFlowStatement(string referenceNo, string year, CashFlowStatement lcfs, string userID)
        {
            var k = orm.CashFlowStatements.Where(z => z.ReferenceNo == referenceNo && z.Year == year).FirstOrDefault();
            if (userID != null && k != null)
            {
                BLODPRLog bdl = new BLODPRLog();
                bdl.ReferenceNo = k.ReferenceNo;
                bdl.TotalCashInflow = k.TotalCashInflow;
                bdl.TotalCashOutflow = k.TotalCashOutflow;
                bdl.NetCashInflow = k.NetCashInflow;
                bdl.BLOUserName = userID;
                bdl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                bdl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                bdl.UserDateTime = DateTime.Now;
                orm.BLODPRLogs.Add(bdl);
            }
            if (k != null)
            {
                k.TotalCashInflow = lcfs.TotalCashInflow;
                k.TotalCashOutflow = lcfs.TotalCashOutflow;
                k.NetCashInflow = lcfs.NetCashInflow;
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = referenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool AddKeyBusinessMatrix(KeyBusinessMatrix kbm)
        {
            var t = orm.CashFlowStatements.Where(z => z.ReferenceNo == kbm.ReferenceNo).FirstOrDefault();
            if (t != null)
            {
                orm.KeyBusinessMatrices.Add(kbm);
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = kbm.ReferenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool UpdateKeyBusinessMatrix(string referenceNo, KeyBusinessMatrix kbm, string userID)
        {
            var k = orm.KeyBusinessMatrices.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (userID != null && k != null)
            {
                BLODPRLog bdl = new BLODPRLog();
                bdl.ReferenceNo = k.ReferenceNo;
                bdl.BreakEvenPoint = k.BreakEvenPoint;
                bdl.DSCR = k.DSCR;
                bdl.IRR = k.IRR;
                bdl.BLOUserName = userID;
                bdl.IPAddress = HttpContext.Current.Request.UserHostAddress;
                bdl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
                bdl.UserDateTime = DateTime.Now;
                orm.BLODPRLogs.Add(bdl);
            }
            if (k != null)
            {
                k.BreakEvenPoint = kbm.BreakEvenPoint;
                k.DSCR = kbm.DSCR;
                k.IRR = kbm.IRR;
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = referenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public bool UpdateDPRStatus(string referenceNo, DPRStatu ds, BLORecord br)
        {
            var t = orm.KeyBusinessMatrices.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
            if (t != null)
            {
                var m = orm.DPRStatus.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
                if (m != null)
                {
                    m.Status = ds.Status;
                }
                var k = orm.BLORecords.Where(z => z.ReferenceNo == referenceNo).FirstOrDefault();
                if (k != null)
                {
                    k.DPRStatus = br.DPRStatus;
                    k.MobileUploadStatus = 0;
                }
            }
            DPRLog dl = new DPRLog();
            dl.ReferenceNo = referenceNo;
            dl.URL = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            dl.UserDateTime = DateTime.Now;
            dl.FinancialYear = GenerateGoaheadnumber.GetCurrentFinancialYear();
            dl.IPAddress = HttpContext.Current.Request.UserHostAddress;
            orm.DPRLogs.Add(dl);
            try
            {
                orm.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }
    }
}