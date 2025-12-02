using System;
using System.Web;
using System.Web.Mvc;

namespace Omlajue_Ecommerce.Helpers
{
    /// <summary>
    /// Custom authorization attribute to check if user is logged in
    /// </summary>
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Session["UserId"] != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Account/Login");
        }
    }

    /// <summary>
    /// Custom authorization attribute for admin-only access
    /// </summary>
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAdmin = httpContext.Session["IsAdmin"];
            return isAdmin != null && (bool)isAdmin;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Account/Login");
        }
    }
}
