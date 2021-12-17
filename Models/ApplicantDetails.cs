using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    public class ApplicantDetails
    {
        [Required]
        [RegularExpression(@"^[A-Z]{3}\/[A-Z]{3}\/2\d{3}\-(?!00)[0-9]{2}\/[1-9][0-9]{0,7}$")]
        public string ReferenceNo { get; set; }
        [Required]
        [RegularExpression(@"^(0|[1-9]\d{0,12}|[1-9]\d{0,12}\.\d{1,2}|0\.\d{1,2}|\.\d{1,2})$")]
        public decimal AnnualIncome { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$")]
        public string PresentOccupation { get; set; }
        [Required]
        [RegularExpression("^(Yes|No)$")]
        public string PreviousExperience { get; set; }
        [RegularExpression("^[1-7]$")]
        public int? GroupTypeCode { get; set; }
        [RegularExpression(@"^[a-zA-Z\s]+$")]
        public string FirmName { get; set; }
    }
}