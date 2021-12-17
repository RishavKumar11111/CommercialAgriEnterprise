using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    public class PdfFilesforBeneficiaryDetail
    {
        [Required]
        public byte[] LandPdf1 { get; set; }
        public byte[] LandPdf2 { get; set; }
    }
}