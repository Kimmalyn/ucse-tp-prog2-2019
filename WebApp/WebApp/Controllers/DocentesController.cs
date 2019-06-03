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
    public class DocentesController : BaseController
    {
        private static IServicioWeb servicio = new MockService();
        // GET: /Docentes/
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(servicio.ObtenerDocentes(null, 0,0,"").Lista);
        }

        // GET: /Docentes/Create
        [HttpGet]
        public ActionResult Form(int id = 0, bool readOnly = false, bool delete = false)
        {
            var usuario = new Docente();
            ViewBag.Title = "Nueva Docente";

            if (id != 0)
            {
                usuario = servicio.ObtenerDocentePorId(usuarioLogueado, id);
                if (delete)
                    ViewBag.Title = "Eliminar Docente";
                else
                    ViewBag.Title = "Editar Docente";
            }

            if (usuario == null)
                return RedirectToAction("Index");

            ViewBag.ReadOnly = readOnly;
            ViewBag.Delete = delete;

            return View(usuario);
        }

        // POST: /Docentes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Form(Docente usuario, bool readOnly = false, bool delete = false)
        {
            if (delete)
            {
                var resultado = servicio.EliminarDocente(usuario.Id, usuario, usuarioLogueado);
                if (resultado.EsValido)
                    return RedirectToAction("Index");

                TempData["Error"] = resultado;
            }

            if (ModelState.IsValid)
            {
                Resultado resultado = new Resultado();
                if (usuario.Id == 0)
                    resultado = servicio.AltaDocente(usuario, usuarioLogueado);
                else
                    resultado = servicio.EditarDocente(usuario.Id, usuario, usuarioLogueado);

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
