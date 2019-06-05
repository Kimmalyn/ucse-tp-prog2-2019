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
using WebApp.Models;

namespace AnBem.WebApplication.Controllers
{
    public class PadresController : BaseController
    {
        private static IServicioWeb servicio = new MockService();

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View();
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

        [HttpGet]
        public ActionResult Assign(int id)
        {
            ViewBag.Title = "Asignar hijo a padre";

            var usuario = servicio.ObtenerPadrePorId(usuarioLogueado, id);
            var alumnos = servicio.ObtenerAlumnos(usuarioLogueado, 0, int.MaxValue, null).Lista;

            HijoViewModel model = new HijoViewModel();
            model.Usuario = usuario;

            model.Hijos = alumnos.Select(x => new HijoSelectedViewModel()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                Selected = usuario.Hijos?.Any(s => s.Id == x.Id) ?? false
            }).ToArray();

            return View(model);
        }

        [HttpPost]
        public ActionResult Assign(int id, HijoViewModel model)
        {
            var usuario = servicio.ObtenerPadrePorId(usuarioLogueado, id);
            var alumnos = servicio.ObtenerAlumnos(usuarioLogueado, 0, int.MaxValue, null).Lista;

            foreach (var hijo in usuario.Hijos ?? new Hijo[] { })
            {
                if (model.Hijos.Any(x => x.Selected && x.Id == hijo.Id) == false)
                {
                    servicio.DesasignarHijoPadre(hijo, usuario, usuarioLogueado);
                }
            }

            foreach (var item in model.Hijos.Where(x => x.Selected))
            {
                var hijo = alumnos.Single(x => x.Id == item.Id);

                servicio.AsignarHijoPadre(hijo, usuario, usuarioLogueado);
            }


            return RedirectToAction("Index");
        }
    }
}
