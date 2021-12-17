using System;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class AllLandRecordVM
    {
        public string RefNo { get; set; }
        public int DistCode { get; set; }
        public int? RDistCode { get; set; }
        public string DistName { get { return new CommercialAgriEnterpriseEntities().LGDDistricts.Where(g => g.RevenueDistrictCode == RDistCode).Select(g => g.DistrictName).FirstOrDefault(); } }
        public int BlockCode { get; set; }
        public string BlockName { get { return new CommercialAgriEnterpriseEntities().LGDBlocks.Where(g => g.BlockCode == BlockCode).Select(g => g.BlockName).FirstOrDefault(); } }
        public int? GPCode { get; set; }
        public string GPName { get { return new CommercialAgriEnterpriseEntities().LGDGPs.Where(g => g.GPCode == GPCode).Select(g => g.GPName).FirstOrDefault(); } }
        public int VillageCode { get; set; }
        public string VCode { get { return Convert.ToString(VillageCode); } }
        public string VillageName { get { if (Locality == "Rural") { return new CommercialAgriEnterpriseEntities().LGDVillages.Where(g => g.VillageCode == VillageCode).Select(g => g.VillageName).FirstOrDefault(); } else { return new CommercialAgriEnterpriseEntities().rVillages.Where(g => g.VCode == VCode && g.DCode == DCode && g.TCode == TCode).Select(g => g.voName).FirstOrDefault(); } } }
        public string KhataNo { get; set; }
        public string PlotNo { get; set; }
        public string RelationshipCode { get; set; }
        public string Relationship { get; set; }
        public decimal ExecArea { get; set; }
        public string ExecUnit { get; set; }
        public string TenantName { get; set; }
        public string Kisam { get; set; }
        public decimal? AreaInHectare { get; set; }
        public decimal? AreaInAcre { get; set; }
        public int? TehsilCode { get; set; }
        public string DCode { get { return new CommercialAgriEnterpriseEntities().rDistricts.Where(z => z.LGDDistrictCode == DistCode).Select(x => x.DCode).FirstOrDefault(); } }
        public string TCode { get { return Convert.ToString(TehsilCode); } }
        public string TehsilName { get { return new CommercialAgriEnterpriseEntities().rTehsils.Where(z => z.TCode == TCode && z.DCode == DCode).Select(x => x.TName).FirstOrDefault(); } }
        public int? RICircleCode { get; set; }
        public string RICCode { get { return Convert.ToString(RICircleCode); } }
        public string RICircleName { get { return new CommercialAgriEnterpriseEntities().riCircles.Where(z => z.TCode == TCode && z.DCode == DCode && z.riCode == RICCode).Select(x => x.riName).FirstOrDefault(); } }
        public string Locality { get; set; }
    }
}