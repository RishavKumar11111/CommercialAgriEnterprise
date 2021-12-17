using System.ComponentModel.DataAnnotations;

namespace CommercialAgriEnterprise.Models
{
    [MetadataType(typeof(BeneficiaryDocumentMetadata))]
    public partial class BeneficiaryDocument
    {
        internal sealed class BeneficiaryDocumentMetadata
        {
            [Required]
            [RegularExpression(@"^[A-Z]{3}\/[A-Z]{3}\/2\d{3}\-(?!00)[0-9]{2}\/[1-9][0-9]{0,7}$")]
            public string ReferenceNo { get; set; }
            [Required]
            public string Photograph { get; set; }
            [Required]
            public byte[] IdentityProof { get; set; }
            [Required]
            public byte[] Signature { get; set; }
        }
    }
}