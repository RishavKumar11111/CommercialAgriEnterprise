using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    [MetadataType(typeof(ProfitabilityProjectionMetaData))]
    public partial class ProfitabilityProjection
    {
        internal sealed class ProfitabilityProjectionMetaData
        {
            [Required]
            [RegularExpression(@"^[A-Z]{3}\/[A-Z]{3}\/2\d{3}\-(?!00)[0-9]{2}\/[1-9][0-9]{0,7}$")]
            public string ReferenceNo { get; set; }
            [Required]
            [RegularExpression(@"^2\d{3}$")]
            public string Year { get; set; }
            [Required]
            [RegularExpression(@"^-?(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
            public decimal GrossRevenue { get; set; }
            [Required]
            [RegularExpression(@"^-?(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
            public decimal TotalExpenditure { get; set; }
            [Required]
            [RegularExpression(@"^-?(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
            public decimal GrossProfit { get; set; }
            [Required]
            [RegularExpression(@"^-?(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
            public decimal NetProfit { get; set; }
        }
    }
}