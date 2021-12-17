using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class CheckPasswordChange
    {
        public static string CheckPassword(string Username)
        {
            CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();
            var k = orm.CheckPasswordStatus.Where(z => z.UserName == Username && z.PasswordStatus == true).FirstOrDefault();
            if (k != null)
            {
                return "OK";
            }
            else
            {
                return "False";
            }
        }
    }
}