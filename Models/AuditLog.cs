using System;
using System.Collections.Generic;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public static class AuditLog
    {
        public static void Log(string _ip, string _UserID, string _Url, string _Devicetype, string _Model, string _Manufacturer, string _OS, string _Browser, string _ScreenSize, DateTime _DateTime, string _Action, string _Attack, string _Param, string _Mode)
        {
            var orm = new CommercialAgriEnterpriseEntities();
            orm.ActivityLogs.Add(new ActivityLog { IPAddress = _ip, UserID = _UserID, Url = _Url, DeviceType = _Devicetype, Model = _Model, Manufacture = _Manufacturer, OS = _OS, Browser = _Browser, ScreenSize = _ScreenSize, DateTime = _DateTime, Action = _Action, Attack = _Attack, Param = _Param, Mode = _Mode });
            try
            {
                orm.SaveChanges();
            }
            catch (Exception e)
            {
                var ex = e;
            }
        }

        public static List<ActivityLog> activitylog()
        {
            return new CommercialAgriEnterpriseEntities().ActivityLogs.OrderByDescending(g => g.DateTime).ToList();
        }
    }
}