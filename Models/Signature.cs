using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    public class Signature
    {
        [Required]
        public byte[] USign { get; set; }
    }
}