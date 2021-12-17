using System;
using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    public class BLORecordValidation
    {
        [Required]
        [RegularExpression(@"^[A-Z]{3}\/[A-Z]{3}\/2\d{3}\-(?!00)[0-9]{2}\/[1-9][0-9]{0,7}$")]
        public string ReferenceNo { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? BLODate { get; set; }
        [RegularExpression("^(0|1|2)$")]
        public int BLOStatus { get; set; }
        [RegularExpression(@"^[\<\>'\-\/&,.\w\s]+$")]
        public string RejectionReason { get; set; }
        [RegularExpression(@"^[a-zA-Z\s]+$")]
        public string BLOUserName { get; set; }
        public string IPAddress { get; set; }
        [RegularExpression(@"^2\d{3}\-(?!00)[0-9]{2}$")]
        public string FinancialYear { get; set; }
    }
}