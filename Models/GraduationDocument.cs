using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    public class GraduationDocument
    {
        [Required]
        public byte[] CastGraduationCertificate { get; set; }
    }
}