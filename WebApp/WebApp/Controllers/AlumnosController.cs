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
    
    public class AlumnosController : BaseController
    {
        private static IServicioWeb servicio = new MockService();
        // GET: /Alumnos/
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(servicio.ObtenerAlumnos(null, 0,0,"").Lista);
        }

        // GET: /Alumnos/Create
        [HttpGet]
        public ActionResult Form(int id = 0, bool readOnly = false, bool delete = false)
        {
            var usuario = new Hijo();
            ViewBag.Title = "Nuevo alumno";

            if (id != 0)
            {
                usuario = servicio.ObtenerAlumnoPorId(usuarioLogueado, id);
                if (delete)
                    ViewBag.Title = "Eliminar alumno";
                else
                    ViewBag.Title = "Editar alumno";
            }

            if (usuario == null)
                return RedirectToAction("Index");

            ViewBag.ReadOnly = readOnly;
            ViewBag.Delete = delete;

            return View(usuario);
        }

        // POST: /Alumnos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Form(Hijo usuario, bool readOnly = false, bool delete = false)
        {
            if (delete)
            {
                var resultado = servicio.EliminarAlumno(usuario.Id, usuario, usuarioLogueado);
                if (resultado.EsValido)
                    return RedirectToAction("Index");

                TempData["Error"] = resultado;
            }

            if (ModelState.IsValid)
            {
                Resultado resultado = new Resultado();
                if (usuario.Id == 0)
                    resultado = servicio.AltaAlumno(usuario, usuarioLogueado);
                else
                    resultado = servicio.EditarAlumno(usuario.Id, usuario, usuarioLogueado);

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
