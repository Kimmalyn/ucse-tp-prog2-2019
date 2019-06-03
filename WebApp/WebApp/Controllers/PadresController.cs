using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Collections.Generic;
using System;
using System.Net.Mail;
using WebApp;
using Contratos;
using Mocks;

namespace AnBem.WebApplication.Controllers
{
    //[Authorize]
    public class PadresController : BaseController
    {
        private static IServicioWeb servicio = new MockService();
        // GET: /Padres/
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(servicio.ObtenerPadres(null, 0,0,"").Lista);
        }

        // GET: /Padres/Create
        [HttpGet]
        public ActionResult Form(int id = 0, bool readOnly = false, bool delete = false)
        {
            var usuario = new Padre();
            ViewBag.Title = "Nueva Padre";

            if (id != 0)
            {
                usuario = servicio.ObtenerPadrePorId(usuarioLogueado, id);
                if (delete)
                    ViewBag.Title = "Eliminar Padre";
                else
                    ViewBag.Title = "Editar Padre";
            }

            if (usuario == null)
                return RedirectToAction("Index");

            ViewBag.ReadOnly = readOnly;
            ViewBag.Delete = delete;

            return View(usuario);
        }

        // POST: /Padres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Form(Padre usuario, bool readOnly = false, bool delete = false)
        {
            if (delete)
            {
                var resultado = servicio.EliminarPadreMadre(usuario.Id, usuario, usuarioLogueado);
                if (resultado.EsValido)
                    return RedirectToAction("Index");

                TempData["Error"] = resultado;
            }

            if (ModelState.IsValid)
            {
                Resultado resultado = new Resultado();
                if (usuario.Id == 0)
                    resultado = servicio.AltaPadreMadre(usuario, usuarioLogueado);
                else
                    resultado = servicio.EditarPadreMadre(usuario.Id, usuario, usuarioLogueado);

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
