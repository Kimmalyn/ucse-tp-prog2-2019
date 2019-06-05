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
    
    public class DocentesController : BaseController
    {
        private static IServicioWeb servicio = new MockService();
        // GET: /Docentes/
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View();
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

        [HttpGet]
        public ActionResult Assign(int id)
        {
            ViewBag.Title = "Asignar docente a salas";

            var usuario = servicio.ObtenerDocentePorId(usuarioLogueado, id);
            var salas = servicio.ObtenerSalasPorInstitucion(usuarioLogueado);

            SalaViewModel model = new SalaViewModel();
            model.Usuario = usuario;
            
            model.Salas = salas.Select(x => new SalaSelectedViewModel()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Selected = usuario.Salas?.Any(s=>s.Id == x.Id) ?? false
            }).ToArray();

            return View(model);
        }

        [HttpPost]
        public ActionResult Assign(int id, SalaViewModel model)
        {
            var salas = servicio.ObtenerSalasPorInstitucion(usuarioLogueado);
            var usuario = servicio.ObtenerDocentePorId(usuarioLogueado, id);

            foreach (var sala in usuario.Salas ?? new Sala[] { })
            {
                if (model.Salas.Any(x=>x.Selected && x.Id == sala.Id) == false)
                {
                    servicio.DesasignarDocenteSala(usuario, sala, usuarioLogueado);
                }
            }

            foreach (var item in model.Salas.Where(x=>x.Selected))
            {
                var sala = salas.Single(x => x.Id == item.Id);

                servicio.AsignarDocenteSala(usuario, sala, usuarioLogueado);
            }


            return RedirectToAction("Index");
        }
    }
}
