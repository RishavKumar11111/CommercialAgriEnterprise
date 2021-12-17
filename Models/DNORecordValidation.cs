using System;
using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    public class DNORecordValidation
    {
        [Required]
        [RegularExpression(@"^[A-Z]{3}\/[A-Z]{3}\/2\d{3}\-(?!00)[0-9]{2}\/[1-9][0-9]{0,7}$")]
        public string ReferenceNo { get; set; }
        [RegularExpression("^(0|1|2)$")]
        public int DNOStatus { get; set; }
        [RegularExpression("^(0|1|null)$")]
        public int BackToBLOStatus { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DNODate { get; set; }
        [RegularExpression(@"^[\<\>'\-\/&,.\w\s]+$")]
        public string RejectionReason { get; set; }
        [RegularExpression(@"^[a-zA-Z\s]+$")]
        public string DNOUserName { get; set; }
        public string IPAddress { get; set; }
        [RegularExpression(@"^2\d{3}\-(?!00)[0-9]{2}$")]
        public string FinancialYear { get; set; }
    }
}