using System;
using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    public class DDARecordValidation
    {
        [Required]
        [RegularExpression(@"^[A-Z]{3}\/[A-Z]{3}\/2\d{3}\-(?!00)[0-9]{2}\/[1-9][0-9]{0,7}$")]
        public string ReferenceNo { get; set; }
        [RegularExpression("^(0|1|2)$")]
        public int DDAStatus { get; set; }
        [RegularExpression("^(0|1|null)$")]
        public int BackToBLOStatus { get; set; }
        [RegularExpression("^(0|1|2)$")]
        public int IntegratedFarmingStatus { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? IntegratedFarmingDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DDAStatusDate { get; set; }
        [RegularExpression(@"^[\<\>'\-\/&,.\w\s]+$")]
        public string DDARejectionReason { get; set; }
        [RegularExpression(@"^[a-zA-Z\s]+$")]
        public string DDAUserName { get; set; }
        public string IPAddress { get; set; }
        [RegularExpression(@"^2\d{3}\-(?!00)[0-9]{2}$")]
        public string FinancialYear { get; set; }
    }
}