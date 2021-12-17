using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    [MetadataType(typeof(CashFlowStatementMetaData))]
    public partial class CashFlowStatement
    {
        internal sealed class CashFlowStatementMetaData
        {
            [Required]
            [RegularExpression(@"^[A-Z]{3}\/[A-Z]{3}\/2\d{3}\-(?!00)[0-9]{2}\/[1-9][0-9]{0,7}$")]
            public string ReferenceNo { get; set; }
            [Required]
            [RegularExpression(@"^2\d{3}$")]
            public string Year { get; set; }
            [Required]
            [RegularExpression(@"^-?(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
            public decimal TotalCashInflow { get; set; }
            [Required]
            [RegularExpression(@"^-?(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
            public decimal TotalCashOutflow { get; set; }
            [Required]
            [RegularExpression(@"^-?(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
            public decimal NetCashInflow { get; set; }
        }
    }
}