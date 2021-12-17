using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    public class AdminCollectorDetailModify
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[7-9]\d{9}$")]
        public string MobileNo { get; set; }
    }
}