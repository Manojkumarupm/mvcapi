using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tweeter.Filters
{
    public class CustomExceptionHelperFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error"
            };
            filterContext.ExceptionHandled = true;
        }
    }

}