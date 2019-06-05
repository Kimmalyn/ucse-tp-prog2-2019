using Contratos;
using Mocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApp.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public static IServicioWeb CreateService()
        {
            return new MockService();
        }

        protected UsuarioLogueado usuarioLogueado;
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (User.Identity.IsAuthenticated)
            {                 
                var cookie = filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (cookie != null)
                {                    
                    var data = FormsAuthentication.Decrypt(cookie.Value).UserData;

                    ViewBag.User = usuarioLogueado = JsonConvert.DeserializeObject<UsuarioLogueado>(data); 
                }
            }
            base.OnActionExecuting(filterContext);
        }        
    }
}