using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MassFacultyUpdateNew.Services
{
    public class SessionTimeOutAttribute : IActionFilter
    {
        public SessionTimeOutAttribute(IHttpContextAccessor httpaccessor)
        {
            Httpaccessor = httpaccessor;
        }

        public IHttpContextAccessor Httpaccessor { get; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var cnt = context.ActionDescriptor.RouteValues.Count();
            string Requested_action = string.Empty;           
            if (cnt > 0)
            {
                Requested_action = context.ActionDescriptor.RouteValues.FirstOrDefault().Value;                
            }
            if (Requested_action != "Login")
            {
                
                var sess = Httpaccessor.HttpContext.Session.Get("Username");
                if (sess == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Account",
                        action = "Login"
                    }));
                }               

            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
