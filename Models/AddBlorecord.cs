using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    public class AddBLORecord
    {
        [Required]
        public string BloFullName { get; set; }
        [Required]
        public byte[] Signature { get; set; }
    }
}