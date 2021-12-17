using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    public class DNOCheck
    {
        [Required]
        [RegularExpression(@"^[A-Z]{3}\/[A-Z]{3}\/2\d{3}\-(?!00)[0-9]{2}\/[1-9][0-9]{0,7}$")]
        public string ReferenceNo { get; set; }
        [Required]
        [RegularExpression(@"^0$")]
        public int DNOStatus { get; set; }
    }
}