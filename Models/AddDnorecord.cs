using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    public class AddDNORecord
    {
        [Required]
        public string DnoFullName { get; set; }
        [Required]
        public byte[] Signature { get; set; }
    }
}