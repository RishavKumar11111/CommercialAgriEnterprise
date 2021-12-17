using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    public class BankDocument
    {
        [Required]
        public byte[] CISBankClearanceCertificate { get; set; }
    }
}