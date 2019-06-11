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
    public class CuadernoController : BaseController
    {
        private static IServicioWeb servicio = new MockService();

        [HttpGet]
        public async Task<ActionResult> Index(int? classroomId = null, int? studenId = null)
        {
            if (usuarioLogueado.RolSeleccionado != Roles.Padre)
            {
                ViewBag.Salas = new SelectList(servicio.ObtenerSalasPorInstitucion(usuarioLogueado), "Id", "Nombre", classroomId);
            }

            var alumnos = servicio.ObtenerPersonas(usuarioLogueado);

            if (classroomId != null)
            {
                alumnos = alumnos.Where(x => x.Sala.Id == classroomId).ToArray();
            }

            ViewBag.Hijos = alumnos;

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Book(int id)
        {
            var alumno = servicio.ObtenerPersonas(usuarioLogueado).SingleOrDefault(x=>x.Id == id);

            if (alumno == null) { 
                return RedirectToAction("Index");
            }

            alumno.Notas = servicio.ObtenerCuadernoComunicaciones(alumno.Id, usuarioLogueado);

            return View(alumno);
        }

        [HttpPost]
        public JsonResult MarcarLeida(int id)
        {
            var rta = servicio.MarcarNotaComoLeida(new Nota() { Id = id }, usuarioLogueado);

            return Json(rta);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult ResponderNota(int id, string mensaje)
        {
            Comentario comentario = new Comentario()
            {
                Fecha = DateTime.Now,
                Mensaje = mensaje,
                Usuario = new Usuario() { Nombre = usuarioLogueado.Nombre, Apellido = usuarioLogueado.Apellido, Email = usuarioLogueado.Email  }
            };

            var rta = servicio.ResponderNota(new Nota() { Id = id }, comentario, usuarioLogueado);

            return Json(rta);
        }
    }
}
