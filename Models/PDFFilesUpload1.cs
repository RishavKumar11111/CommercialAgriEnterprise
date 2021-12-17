using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    public class PDFFilesUpload1
    {
        [Required]
        public byte[] LandPdf1 { get; set; }

    }
}