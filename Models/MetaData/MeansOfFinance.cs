using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    [MetadataType(typeof(MeansOfFinanceMetaData))]
    public partial class MeansOfFinance
    {
        internal sealed class MeansOfFinanceMetaData
        {
            [Required]
            [RegularExpression(@"^[A-Z]{3}\/[A-Z]{3}\/2\d{3}\-(?!00)[0-9]{2}\/[1-9][0-9]{0,7}$")]
            public string ReferenceNo { get; set; }
            [Required]
            [RegularExpression(@"^(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
            public decimal OwnInvestment { get; set; }
            //[RegularExpression(@"^[-\/&,.\w\s]{0,300}+$")]
            public string OwnInvestmentRemark { get; set; }
            [Required]
            [RegularExpression(@"^(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
            public decimal TermLoan { get; set; }
            //[RegularExpression(@"^[-\/&,.\w\s]{0,300}+$")]
            public string TermLoanRemark { get; set; }
            [Required]
            [RegularExpression(@"^(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
            public decimal WorkingCapitalLoan { get; set; }
            //[RegularExpression(@"^[-\/&,.\w\s]{0,300}+$")]
            public string WorkingCapitalLoanRemark { get; set; }
            [Required]
            [RegularExpression(@"^(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
            public decimal OtherSource { get; set; }
            //[RegularExpression(@"^[-\/&,.\w\s]{0,300}+$")]
            public string OtherSourceRemark { get; set; }
            [Required]
            [RegularExpression(@"^(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
            public decimal TotalAmount { get; set; }
        }
    }
}