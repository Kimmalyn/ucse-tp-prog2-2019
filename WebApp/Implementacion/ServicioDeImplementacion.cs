using Contratos;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementacion
{
    //EN ESTA CLASE PODRIAN RESOLVER EL ACCESO POR ROLES PARA NO NECESITARLO EN PRINCIPAL
    public class ServicioDeImplementacion : IServicioWeb
    {
        public Resultado AltaAlumno(Hijo hijo, UsuarioLogueado usuarioLogueado)//Funciona
        {
            return Principal.Instancia.AltaAlumno(hijo, usuarioLogueado);
        }

        public Resultado AltaDirectora(Directora directora, UsuarioLogueado usuarioLogueado) //Funciona
        {            
            return Principal.Instancia.AltaDirectora(directora, usuarioLogueado);
        }

        public Resultado AltaDocente(Docente docente, UsuarioLogueado usuarioLogueado) //Funciona
        {
            return Principal.Instancia.AltaDocente(docente, usuarioLogueado);
        }

        public Resultado AltaNota(Nota nota, Sala[] salas, Hijo[] hijos, UsuarioLogueado usuarioLogueado)//Funciona
        {
            return Principal.Instancia.AltaNota(nota, salas, hijos, usuarioLogueado);
        }

        public Resultado AltaPadreMadre(Padre padre, UsuarioLogueado usuarioLogueado)//Funciona
        {
            return Principal.Instancia.AltaPadre(padre, usuarioLogueado);
        }

        public Resultado AsignarDocenteSala(Docente docente, Sala sala, UsuarioLogueado usuarioLogueado)//Funciona
        {
            return Principal.Instancia.AsignarDocenteSala(docente, sala, usuarioLogueado);
        }

        public Resultado AsignarHijoPadre(Hijo hijo, Padre padre, UsuarioLogueado usuarioLogueado)//Funciona
        {
            return Principal.Instancia.AsignarHijoPadre(hijo, padre, usuarioLogueado);
        }

        public Resultado DesasignarDocenteSala(Docente docente, Sala sala, UsuarioLogueado usuarioLogueado)//Funciona
        {
            return Principal.Instancia.DesasignarDocenteSala(docente, sala, usuarioLogueado);
        }

        public Resultado DesasignarHijoPadre(Hijo hijo, Padre padre, UsuarioLogueado usuarioLogueado)//Funciona
        {
            return Principal.Instancia.DesasignarHijoPadre(hijo, padre, usuarioLogueado);
        }

        public Resultado EditarAlumno(int id, Hijo hijo, UsuarioLogueado usuarioLogueado)//Funciona
        {
            return Principal.Instancia.EditarAlumno(id, hijo, usuarioLogueado);
        }

        public Resultado EditarDirectora(int id, Directora directora, UsuarioLogueado usuarioLogueado) //Funciona
        {
            return Principal.Instancia.EditarDirectora(id, directora, usuarioLogueado);
        }

        public Resultado EditarDocente(int id, Docente docente, UsuarioLogueado usuarioLogueado) //Funciona
        {
            return Principal.Instancia.EditarDocente(id, docente, usuarioLogueado);
        }

        public Resultado EditarPadreMadre(int id, Padre padre, UsuarioLogueado usuarioLogueado)//funciona
        {
            return Principal.Instancia.EditarPadre(id, padre, usuarioLogueado);
        }

        public Resultado EliminarAlumno(int id, Hijo hijo, UsuarioLogueado usuarioLogueado)//Funciona
        {
            return Principal.Instancia.EliminarAlumno(id, hijo, usuarioLogueado);
        }

        public Resultado EliminarDirectora(int id, Directora directora, UsuarioLogueado usuarioLogueado) //Funciona
        {
            return Principal.Instancia.EliminarDirectora(id, directora, usuarioLogueado);
        }

        public Resultado EliminarDocente(int id, Docente docente, UsuarioLogueado usuarioLogueado) //Funciona
        {
            return Principal.Instancia.EliminarDocente(id, docente, usuarioLogueado);
        }

        public Resultado EliminarPadreMadre(int id, Padre padre, UsuarioLogueado usuarioLogueado)//Funciona
        {
            return Principal.Instancia.EliminarPadre(id, padre, usuarioLogueado);
        }

        public Resultado MarcarNotaComoLeida(Nota nota, UsuarioLogueado usuarioLogueado)//Funciona
        {
            return Principal.Instancia.MarcarNotaComoLeida(nota, usuarioLogueado);
        }

        public Hijo ObtenerAlumnoPorId(UsuarioLogueado usuarioLogueado, int id)//Funciona
        {
            return Principal.Instancia.ObtenerAlumnoPorId(usuarioLogueado, id);
        }

        public Grilla<Hijo> ObtenerAlumnos(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)//Funciona
        {
            return Principal.Instancia.ObtenerAlumnos(usuarioLogueado, paginaActual, totalPorPagina, busquedaGlobal);
        }

        public Nota[] ObtenerCuadernoComunicaciones(int idPersona, UsuarioLogueado usuarioLogueado)
        {
            return Principal.Instancia.ObtenerCuadernoComunicaciones(idPersona, usuarioLogueado);
        }

        public Directora ObtenerDirectoraPorId(UsuarioLogueado usuarioLogueado, int id) //Funciona
        {
            return Principal.Instancia.ObtenerDirectoraPorId(usuarioLogueado,id);
        }

        public Grilla<Directora> ObtenerDirectoras(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal) //Funciona
        {
            return Principal.Instancia.ObtenerDirectoras(usuarioLogueado, paginaActual, totalPorPagina, busquedaGlobal);
        }

        public Docente ObtenerDocentePorId(UsuarioLogueado usuarioLogueado, int id) //Funciona
        {
            return Principal.Instancia.ObtenerDocentePorId(usuarioLogueado, id);
        }

        public Grilla<Docente> ObtenerDocentes(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal) //Funciona
        {
            return Principal.Instancia.ObtenerDocentes(usuarioLogueado, paginaActual, totalPorPagina, busquedaGlobal);
        }

        public Institucion[] ObtenerInstituciones()
        {
            throw new NotImplementedException();
        }

        public string ObtenerNombreGrupo()//funciona
        {
            return $"StarFleet - Busso,Copes";
        }

        public Padre ObtenerPadrePorId(UsuarioLogueado usuarioLogueado, int id)//funciona
        {
            return Principal.Instancia.ObtenerPadrePorId(usuarioLogueado, id);
        }

        public Grilla<Padre> ObtenerPadres(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)//funciona
        {
            return Principal.Instancia.ObtenerPadres(usuarioLogueado, paginaActual, totalPorPagina, busquedaGlobal);
        }

        public Hijo[] ObtenerPersonas(UsuarioLogueado usuarioLogueado)//funciona
        {
            return Principal.Instancia.ObtenerPersonas(usuarioLogueado);
        }

        public Sala[] ObtenerSalasPorInstitucion(UsuarioLogueado usuarioLogueado)//funciona
        {
            return Principal.Instancia.ObtenerSalasPorInstitucion(usuarioLogueado);
        }

        public UsuarioLogueado ObtenerUsuario(string email, string clave)//Funciona
        {
            return Principal.Instancia.ObtenerUsuario(email, clave);
        }

        public Resultado ResponderNota(Nota nota, Comentario nuevoComentario, UsuarioLogueado usuarioLogueado)
        {
            return Principal.Instancia.ResponderNota(nota, nuevoComentario, usuarioLogueado);
        }
    }
}
