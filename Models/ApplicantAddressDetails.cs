using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    public class ApplicantAddressDetails
    {
        [Required]
        [RegularExpression(@"^[A-Z]{3}\/[A-Z]{3}\/2\d{3}\-(?!00)[0-9]{2}\/[1-9][0-9]{0,7}$")]
        public string ReferenceNo { get; set; }
        [Required]
        [RegularExpression(@"^[-\/&,.\w\s]+$")]
        public string CorrespondenceAddress { get; set; }
        [Required]
        [RegularExpression(@"^[-\/&,.\w\s]+$")]
        public string PermanentAddress { get; set; }
        [Required]
        [RegularExpression(@"^(\d{6})$")]
        public string Pin { get; set; }
        [DataType(DataType.EmailAddress)]
        public string EmailID { get; set; }
    }
}