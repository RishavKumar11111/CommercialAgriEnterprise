using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    public class PDFFilesUpload2
    {
        [Required]
        public byte[] LandPdf2 { get; set; }
    }
}