using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public static class ProjectDetailsforEntry
    {
        public static string SearchProjectType(string pCode)
        {
            if (pCode == "01P3" || pCode == "01P4" || pCode == "02P13" || pCode == "02P16" || pCode == "02P18" || pCode == "02P2" || pCode == "02P25" || pCode == "02P3" || pCode == "02P5" || pCode == "03P10" || pCode == "03P11" || pCode == "03P12" || pCode == "03P13" || pCode == "03P6" || pCode == "04P1" || pCode == "04P10" || pCode == "04P11" || pCode == "04P12" || pCode == "04P16" || pCode == "04P17" || pCode == "04P18" || pCode == "04P19" || pCode == "04P20" || pCode == "04P3" || pCode == "04P8" || pCode == "04P9")
            {
                return "success";
            }
            else
            {
                return "error";
            }
        }

        public static string SearchProjectCapacity(string pCode, decimal capacity, string Cunit)
        {
            var k = new CommercialAgriEnterpriseEntities().ProjectTypes.Where(a => a.Code == pCode).FirstOrDefault();
            if (capacity >= k.MinCapacity)
            {
                if (Cunit == k.CapacityUnit)
                {
                    return "success";
                }
                else
                {
                    return "Capacity unit must be " + k.CapacityUnit;
                }
            }
            else
            {
                return "Capacity must be " + k.MinCapacity + " or more.";
            }
        }
    }
}