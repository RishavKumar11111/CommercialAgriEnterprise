using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    public class Photo
    {
        [Required]
        public byte[] UPhoto { get; set; }
    }
}