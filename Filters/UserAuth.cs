using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace EngineersMatrimony.Filters
{
    public class UserAuth : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (Convert.ToString(filterContext.HttpContext.Session["MID"]) == "")
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }



        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "_ErrorLayout"
                };



            }
        }
    }
}