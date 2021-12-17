using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    public class ProjectInformations
    {
        [Required]
        [RegularExpression(@"^[A-Z]{3}\/[A-Z]{3}\/2\d{3}\-(?!00)[0-9]{2}\/[1-9][0-9]{0,7}$")]
        public string ReferenceNo { get; set; }
        [Required]
        public string ProductToBeProducedOrMarketed { get; set; }
        [Required]
        [RegularExpression("^(Self|Bank)$")]
        public string FinanceType { get; set; }
        [Required]
        public string ProjectName { get; set; }
        [RegularExpression(@"^([1-9]\d{0,7}|[1-9]\d{0,7}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
        public decimal? Capacity { get; set; }
        [RegularExpression(@"^[0-9]\d{0,2}$")]
        public string BankCode { get; set; }
        [RegularExpression(@"^[0-9]\d{0,4}$")]
        public string BranchCode { get; set; }
        [Required]
        [RegularExpression("^(Below 1 Crore|1 Crore or Above)$")]
        public string ApproximateCost { get; set; }
        [Required]
        [RegularExpression(@"^([1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
        public decimal OwnContribution { get; set; }
        [RegularExpression(@"^(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
        public decimal? BankLoan { get; set; }
    }
}