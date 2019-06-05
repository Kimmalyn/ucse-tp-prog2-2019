using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Collections.Generic;
using System;
using System.Net.Mail;
using WebApp.Controllers;
using Contratos;
using Mocks;

namespace AnBem.WebApplication.Controllers
{
    
    public class DirectoresController : BaseController
    {
        private static IServicioWeb servicio = new MockService();
        // GET: /Directoras/
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View();
        }

        // GET: /Directoras/Create
        [HttpGet]
        public ActionResult Form(int id = 0, bool readOnly = false, bool delete = false)
        {
            var usuario = new Directora();
            ViewBag.Title = "Nueva directora";

            if (id != 0)
            {
                usuario = servicio.ObtenerDirectoraPorId(usuarioLogueado, id);
                if (delete)
                    ViewBag.Title = "Eliminar directora";
                else
                    ViewBag.Title = "Editar directora";
            }

            if (usuario == null)
                return RedirectToAction("Index");

            ViewBag.ReadOnly = readOnly;
            ViewBag.Delete = delete;

            return View(usuario);
        }

        // POST: /Directoras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Form(Directora usuario, bool readOnly = false, bool delete = false)
        {
            if (delete)
            {
                var resultado = servicio.EliminarDirectora(usuario.Id, usuario, usuarioLogueado);
                if (resultado.EsValido)
                    return RedirectToAction("Index");

                TempData["Error"] = resultado;
            }

            if (ModelState.IsValid)
            {
                Resultado resultado = new Resultado();
                if (usuario.Id == 0)
                    resultado = servicio.AltaDirectora(usuario, usuarioLogueado);
                else
                    resultado = servicio.EditarDirectora(usuario.Id, usuario, usuarioLogueado);

                if (resultado.EsValido)
                    return RedirectToAction("Index");

                TempData["Error"] = resultado;
            }

            ViewBag.ReadOnly = readOnly;
            ViewBag.Delete = delete;

            return View(usuario);
        }

    }
}
