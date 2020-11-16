using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreWeb.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //if (ViewBag.UserID == null || ViewBag.UserID != 1)
            if (HttpContext.Session.GetInt32("UserID") != 1 || HttpContext.Session.GetInt32("UserID") == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Accounts",
                    action = "Login"
                }));
            }
            base.OnActionExecuted(filterContext);
            //base.OnActionExecuted(filterContext);
        }

    }
}