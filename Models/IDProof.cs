using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    public class IDProof
    {
        [Required]
        public byte[] IdentityProof { get; set; }
    }
}