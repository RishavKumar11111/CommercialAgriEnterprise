using System;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class BeneficiaryDocVM
    {
        public BeneficiaryDocVM(string _referenceno)
        {
            referenceno = _referenceno;
        }
        private string referenceno { get; set; }
        public DOCVM GetDocumentDetails { get { return new CommercialAgriEnterpriseEntities().BeneficiaryDocuments.Where(g => g.ReferenceNo == referenceno).Select(g => new DOCVM { Photo = g.Photograph, Identity = g.IdentityProof, Signature = g.Signature }).FirstOrDefault(); } }
    }

    public class DOCVM
    {
        public byte[] Photo { get; set; }
        public string PhotoSIgn { get { return Convert.ToBase64String(Photo); } }
        public byte[] Identity { get; set; }
        public string IdentityProof { get { return Convert.ToBase64String(Identity); } }
        public byte[] Signature { get; set; }
        public string Sign { get { return Convert.ToBase64String(Signature); } }
    }
}