using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class GetBeneficiaryDetailVM
    {
        public string ReferenceNo { private get; set; }
        public Data Data { get { return new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Where(g => g.ReferenceNo == ReferenceNo).Select(g => new Data { Development = g.CapitalInvestmentLand.Development, Building = g.CapitalInvestmentCivilConstruction.Building, Shed = g.CapitalInvestmentCivilConstruction.Shed, OfficeCumStore = g.CapitalInvestmentCivilConstruction.OfficeCumStore, Godown = g.CapitalInvestmentCivilConstruction.Godown, PlantArea = g.CapitalInvestmentCivilConstruction.PlantArea, OtherNecessaryConstruction = g.CapitalInvestmentCivilConstruction.OtherNecessaryConstruction, Fencing = g.CapitalInvestmentLand.Fencing, CivilConstructionTotal = g.CapitalInvestmentCivilConstruction.Total, LandTotal = g.CapitalInvestmentLand.Total, WellOrRiver = g.CapitalInvestmentWaterSupply.WellOrRiver, PumpPipeLine = g.CapitalInvestmentWaterSupply.PumpPipeLine, Tank = g.CapitalInvestmentWaterSupply.Tank, SprinklerFogger = g.CapitalInvestmentWaterSupply.SprinklerFogger, WaterSupplyTotal = g.CapitalInvestmentWaterSupply.Total, InstallationFitting = g.CapitalInvestmentElectrification.InstallationFitting, Transformer = g.CapitalInvestmentElectrification.Transformer, DGSet = g.CapitalInvestmentElectrification.DGSet, ElectrificationTotal = g.CapitalInvestmentElectrification.Total, Equipment = g.CapitalInvestmentPlantMachinery.Equipment, ManualOrMotor = g.CapitalInvestmentPlantMachinery.ManualOrMotor, PlantMachineryTotal = g.CapitalInvestmentPlantMachinery.Total, Livestock = g.CapitalInvestmentAnimalPlant.LiveStock, LivestockType = g.CapitalInvestmentAnimalPlant.LiveStockType, PoultryBreeder = g.CapitalInvestmentAnimalPlant.PoultryBreeder, BroodFish = g.CapitalInvestmentAnimalPlant.BroodFish, Plant = g.CapitalInvestmentAnimalPlant.Plant, AnimalPlantTotal = g.CapitalInvestmentAnimalPlant.Total, FixedAsset = g.CapitalInvestmentMiscellaneou.FixedAsset, MLivestock = g.CapitalInvestmentMiscellaneou.Livestock, Bird = g.CapitalInvestmentMiscellaneou.Bird, Cultivation = g.CapitalInvestmentMiscellaneou.Cultivation, MiscellaneousTotal = g.CapitalInvestmentMiscellaneou.Total, TotalCapitalInvestmentCost = g.CapitalInvestment.TotalCapitalInvestmentCost, RawMaterial = g.RecurringExpenditure.RawMaterial, Salary = g.RecurringExpenditure.Salary, Maintenance = g.RecurringExpenditure.Maintenance, RecurringExpenditureTotal = g.RecurringExpenditure.Total, TotalProjectCost = g.CapitalInvestment.TotalProjectCost, OwnInvestment = g.MeansOfFinance.OwnInvestment, OwnInvestmentRemark = g.MeansOfFinance.OwnInvestmentRemark, TermLoan = g.MeansOfFinance.TermLoan, TermLoanRemark = g.MeansOfFinance.TermLoanRemark, WorkingCapitalLoan = g.MeansOfFinance.WorkingCapitalLoan, WorkingCapitalLoanRemark = g.MeansOfFinance.WorkingCapitalLoanRemark, OtherSource = g.MeansOfFinance.OtherSource, OtherSourceRemark = g.MeansOfFinance.OtherSourceRemark, TotalAmount = g.MeansOfFinance.TotalAmount, BreakEvenPoint = g.KeyBusinessMatrix.BreakEvenPoint, DSCR = g.KeyBusinessMatrix.DSCR, IRR = g.KeyBusinessMatrix.IRR }).FirstOrDefault(); } }
    }

    public class Data
    {
        public decimal? Development { get; set; }
        public decimal? Fencing { get; set; }
        public decimal? LandTotal { get; set; }
        public decimal? Building { get; set; }
        public decimal? Shed { get; set; }
        public decimal? OfficeCumStore { get; set; }
        public decimal? Godown { get; set; }
        public decimal? PlantArea { get; set; }
        public decimal? OtherNecessaryConstruction { get; set; }
        public decimal? CivilConstructionTotal { get; set; }
        public decimal? WellOrRiver { get; set; }
        public decimal? PumpPipeLine { get; set; }
        public decimal? Tank { get; set; }
        public decimal? SprinklerFogger { get; set; }
        public decimal? WaterSupplyTotal { get; set; }
        public decimal? InstallationFitting { get; set; }
        public decimal? Transformer { get; set; }
        public decimal? DGSet { get; set; }
        public decimal? ElectrificationTotal { get; set; }
        public decimal? Equipment { get; set; }
        public decimal? ManualOrMotor { get; set; }
        public decimal? PlantMachineryTotal { get; set; }
        public decimal? Livestock { get; set; }
        public string LivestockType { get; set; }
        public decimal? PoultryBreeder { get; set; }
        public decimal? BroodFish { get; set; }
        public decimal? Plant { get; set; }
        public decimal? AnimalPlantTotal { get; set; }
        public decimal? FixedAsset { get; set; }
        public decimal? MLivestock { get; set; }
        public decimal? Bird { get; set; }
        public decimal? Cultivation { get; set; }
        public decimal? MiscellaneousTotal { get; set; }
        public decimal? RawMaterial { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Maintenance { get; set; }
        public decimal? RecurringExpenditureTotal { get; set; }
        public decimal? TotalCapitalInvestmentCost { get; set; }
        public decimal? TotalProjectCost { get; set; }
        public decimal? OwnInvestment { get; set; }
        public string OwnInvestmentRemark { get; set; }
        public decimal? TermLoan { get; set; }
        public string TermLoanRemark { get; set; }
        public decimal? WorkingCapitalLoan { get; set; }
        public string WorkingCapitalLoanRemark { get; set; }
        public decimal? OtherSource { get; set; }
        public string OtherSourceRemark { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? BreakEvenPoint { get; set; }
        public decimal? DSCR { get; set; }
        public decimal? IRR { get; set; }
    }
}