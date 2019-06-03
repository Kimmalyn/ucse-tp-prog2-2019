using Contratos;
using Mocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AccountController : BaseController
    {
        private static IServicioWeb servicio = new MockService();

        public ActionResult Logoff()
        {
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                var c = new HttpCookie(FormsAuthentication.FormsCookieName);
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }

            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model)
        {
            var resultado = servicio.ObtenerUsuario(model.Email, model.Clave);

            if (resultado != null)
            {
                var userData = JsonConvert.SerializeObject(resultado);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, resultado.Email, DateTime.Now, DateTime.Now.AddHours(8), true, userData, FormsAuthentication.FormsCookiePath);

                string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}