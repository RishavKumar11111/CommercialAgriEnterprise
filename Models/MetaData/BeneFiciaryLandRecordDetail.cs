using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    [MetadataType(typeof(BeneficiaryLandRecordDetailMetadata))]
    public partial class BeneFiciaryLandRecordDetail
    {
        internal sealed class BeneficiaryLandRecordDetailMetadata
        {
            [RegularExpression(@"^[A-Z]{3}\/[A-Z]{3}\/2\d{3}\-(?!00)[0-9]{2}\/[1-9][0-9]{0,7}$")]
            public string ReferenceNo { get; set; }
            [Required]
            [RegularExpression(@"^\d{3}$")]
            public int DistrictCode { get; set; }
            [Required]
            [RegularExpression(@"^\d{4}$")]
            public int BlockCode { get; set; }
            [RegularExpression(@"^(null|\d{1,2})$")]
            public int TehsilCode { get; set; }
            [RegularExpression(@"^(null|\d{6})$")]
            public int GPCode { get; set; }
            [RegularExpression(@"^(null|\d{1,2})$")]
            public int RICircleCode { get; set; }
            [Required]
            [RegularExpression(@"^\d{1,6}$")]
            public int VillageCode { get; set; }
            [Required]
            [RegularExpression(@"^(\d{1,5}|\d{1,5}\/\d{1,5}|\d{1,5}\/\d{1,5}\/\d{1,5})$")]
            public string KhataNo { get; set; }
            [Required]
            [RegularExpression(@"^(\d{1,5}|\d{1,5}\/\d{1,5}|\d{1,5}\/\d{1,5}\/\d{1,5})$")]
            public string PlotNo { get; set; }
            [Required]
            [RegularExpression(@"^[1-9]\d{0,1}$")]
            public string RelationshipCode { get; set; }
            [Required]
            public string TenantName { get; set; }
            public string Kisam { get; set; }
            [Required]
            [RegularExpression(@"^(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,5}|0\.\d{1,5}|\.\d{1,5})$")]
            public decimal? AreaInHectare { get; set; }
            [Required]
            [RegularExpression(@"^(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,5}|0\.\d{1,5}|\.\d{1,5})$")]
            public decimal? AreaInAcre { get; set; }
            [Required]
            [RegularExpression(@"^(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,5}|0\.\d{1,5}|\.\d{1,5})$")]
            public decimal ExecutionArea { get; set; }
            [Required]
            [RegularExpression("^(Area in Acre|Area in Hectare)$")]
            public string UnitExecution { get; set; }
            [RegularExpression("^(Rural|Urban)$")]
            public string Locality { get; set; }
        }
    }
}