﻿using System;
using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class ValidateAntiForgeryTokenOnAllPosts : AuthorizeAttribute
{
    public override void OnAuthorization(AuthorizationContext filterContext)
    {
        var request = filterContext.HttpContext.Request;

        //Only validate POSTs
        if (request.HttpMethod == WebRequestMethods.Http.Post)
        {
            //Ajax POSTs and normal form posts have to be treated differently when it comes to validating the AntiForgeryToken
            //if (request.IsAjaxRequest())
            //{

            var antiForgeryCookie = request.Cookies[AntiForgeryConfig.CookieName];
            var cookieValue = antiForgeryCookie != null ? antiForgeryCookie.Value : null;
            AntiForgery.Validate(cookieValue, request.Headers["__RequestVerificationToken"]);
            //}
            //else
            //{
            //new ValidateAntiForgeryTokenAttribute().OnAuthorization(filterContext);
            //}
        }
    }
}

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class ValidateJsonAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationContext filterContext)
    {
        if (filterContext == null)
        {
            throw new ArgumentNullException("filterContext");
        }
        var httpContext = filterContext.HttpContext;
        var cookie = httpContext.Request.Cookies[AntiForgeryConfig.CookieName];
        AntiForgery.Validate(cookie != null ? cookie.Value : null, httpContext.Request.Headers["__RequestVerificationToken"]);
    }
}