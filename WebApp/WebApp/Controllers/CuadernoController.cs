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
using Implementacion;

namespace AnBem.WebApplication.Controllers
{
    public class CuadernoController : BaseController
    {
        private static IServicioWeb servicio = new ServicioDeImplementacion();

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

        [HttpGet]
        public async Task<ActionResult> CrearNotas(int[] salas)
        {
            if (usuarioLogueado.RolSeleccionado != Roles.Padre)
            {
                ViewBag.Salas = servicio.ObtenerSalasPorInstitucion(usuarioLogueado);
                ViewBag.SalasSeleccionadas = salas;
                if (salas != null)
                {
                    var alumnos = servicio.ObtenerPersonas(usuarioLogueado);

                    ViewBag.Hijos = alumnos.Where(x => salas.Contains(x.Sala.Id)).ToArray();
                }
            }
            else
            {
                ViewBag.Hijos = servicio.ObtenerPersonas(usuarioLogueado);
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CrearNotas(Nota nota, int[] salas, int[] alumnos)
        {
            var infoSalas = salas != null ? salas.Select(x => new Sala() { Id = x }).ToArray() : new Sala[] { };
            var infoHijos = alumnos != null ? alumnos.Select(x => new Hijo() { Id = x }).ToArray() : new Hijo[] { };
            
            nota.Comentarios = new Comentario[] { };

            servicio.AltaNota(nota, infoSalas, infoHijos, usuarioLogueado);


            TempData["Ok"] = "Las notas se crearon correctamente";

            return RedirectToAction("Index");
        }
    }
}
