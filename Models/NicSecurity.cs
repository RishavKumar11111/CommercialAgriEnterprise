using System;
using System.Web.Mvc;

namespace CommercialAgriEnterprise.Models
{
    public class NicSecurity : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Convert.ToString(System.Web.HttpContext.Current.Session["NICSec_AuthToken"]) == "" || Convert.ToString(System.Web.HttpContext.Current.Session["NICSec_AuthToken"]) == null || System.Web.HttpContext.Current.Request.Cookies["NICSec_AuthToken"].Value == null || System.Web.HttpContext.Current.Request.Cookies["NICSec_AuthToken"].Value == "" || System.Web.HttpContext.Current.Session["uid"] == null || Convert.ToString(System.Web.HttpContext.Current.Session["NICSec_AuthToken"]) != System.Web.HttpContext.Current.Request.Cookies["NICSec_AuthToken"].Value)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Account", Action = "Login" }));
            }
        }
    }
}