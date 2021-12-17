using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    public class BankConsent
    {
        [Required]
        public byte[] BankConsentLetter { get; set; }
    }
}