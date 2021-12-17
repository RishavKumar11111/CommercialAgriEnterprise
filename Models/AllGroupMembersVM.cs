using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class AllGroupMembersVM
    {
        public string GroupMemberFarmerID { get; set; }
        public string EducationalQualificationCode { get; set; }
        public string EducationalQualificationName { get { return new CommercialAgriEnterpriseEntities().EducationalQualifications.Where(z => z.Code == EducationalQualificationCode).Select(z => z.Name).FirstOrDefault(); } }
    }
}