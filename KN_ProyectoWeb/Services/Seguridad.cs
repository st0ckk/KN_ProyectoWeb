using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KN_ProyectoWeb.Services
{
    public class Seguridad : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sesion = filterContext.HttpContext.Session;
            
            if (sesion["ConsecutivoUsuario"] == null)
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}