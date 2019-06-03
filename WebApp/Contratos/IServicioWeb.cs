using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contratos
{
    public interface IServicioWeb
    {
        string ObtenerNombreGrupo();

        UsuarioLogueado ObtenerUsuario(string email, string clave);

        Institucion[] ObtenerInstituciones();
        /// <summary>
        /// El usuario logueado debe ser una directora del mismo institucion
        /// </summary>
        /// <param name="directora"></param>
        /// <param name="usuarioLogueado"></param>
        /// <returns></returns>
        Resultado AltaDirectora(Directora directora, UsuarioLogueado usuarioLogueado);

        /// <summary>
        /// El usuario logueado debe ser una directora
        /// </summary>
        /// <param name="hijo"></param>
        /// <param name="usuarioLogueado"></param>
        /// <returns></returns>
        Resultado AltaAlumno(Hijo hijo, UsuarioLogueado usuarioLogueado);

        /// <summary>
        /// El usuario logueado debe ser una directora
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hijo"></param>
        /// <param name="usuarioLogueado"></param>
        /// <returns></returns>
        Resultado EditarAlumno(int id, Hijo hijo, UsuarioLogueado usuarioLogueado);

        /// <summary>
        /// El usuario logueado debe ser una directora
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hijo"></param>
        /// <param name="usuarioLogueado"></param>
        /// <returns></returns>
        Resultado EliminarAlumno(int id, Hijo hijo, UsuarioLogueado usuarioLogueado);

        /// <summary>
        /// El usuario logueado debe ser una directora del mismo institucion
        /// </summary>
        /// <param name="directora"></param>
        /// <param name="usuarioLogueado"></param>
        /// <returns></returns>
        Resultado EditarDirectora(int id, Directora directora, UsuarioLogueado usuarioLogueado);
        /// <summary>
        /// El usuario logueado debe ser una directora del mismo institucion
        /// </summary>
        /// <param name="directora"></param>
        /// <param name="usuarioLogueado"></param>
        /// <returns></returns>
        Resultado EliminarDirectora(int id, Directora directora, UsuarioLogueado usuarioLogueado);
        /// <summary>
        /// Las salas son de la institucion del usuario logueado
        /// </summary>
        /// <param name="institucion"></param>
        /// <param name="usuarioLogueado"></param>
        /// <returns></returns>
        Sala[] ObtenerSalasPorInstitucion(UsuarioLogueado usuarioLogueado);
        /// <summary>
        /// El usuario logueado debe ser una directora del mismo institucion
        /// </summary>
        /// <param name="docente"></param>
        /// <param name="usuarioLogueado"></param>
        /// <returns></returns>
        Resultado AltaDocente(Docente docente, UsuarioLogueado usuarioLogueado);
        /// <summary>
        /// El usuario logueado debe ser una directora del mismo institucion
        /// </summary>
        /// <param name="id"></param>
        /// <param name="docente"></param>
        /// <param name="usuarioLogueado"></param>
        /// <returns></returns>
        Resultado EditarDocente(int id, Docente docente, UsuarioLogueado usuarioLogueado);
        /// <summary>
        /// El usuario logueado debe ser una directora del mismo institucion
        /// </summary>
        /// <param name="id"></param>
        /// <param name="docente"></param>
        /// <param name="usuarioLogueado"></param>
        /// <returns></returns>
        Resultado EliminarDocente(int id, Docente docente, UsuarioLogueado usuarioLogueado);
        /// <summary>
        /// El usuario debe ser directora del mismo institucion
        /// </summary>
        /// <param name="padre"></param>
        /// <param name="usuarioLogueado"></param>
        /// <returns></returns>
        Resultado AltaPadreMadre(Padre padre, UsuarioLogueado usuarioLogueado);
        /// <summary>
        /// El usuario debe ser directora del mismo institucion
        /// </summary>
        /// <param name="padre"></param>
        /// <param name="usuarioLogueado"></param>
        /// <returns></returns>
        Resultado EditarPadreMadre(int id, Padre padre, UsuarioLogueado usuarioLogueado);
        /// <summary>
        /// El usuario debe ser directora del mismo institucion
        /// </summary>
        /// <param name="padre"></param>
        /// <param name="usuarioLogueado"></param>
        /// <returns></returns>
        Resultado EliminarPadreMadre(int id, Padre padre, UsuarioLogueado usuarioLogueado);
        /// <summary>
        /// El usuario debe ser directora, y tanto la sala como el docente deben pertenecer a su institucion.
        /// </summary>
        /// <param name="docente"></param>
        /// <param name="sala"></param>
        /// <param name="usuarioLogueado"></param>
        /// <returns></returns>
        Resultado AsignarDocenteSala(Docente docente, Sala sala, UsuarioLogueado usuarioLogueado);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="docente"></param>
        /// <param name="sala"></param>
        /// <param name="usuarioLogueado"></param>
        /// <returns></returns>
        Resultado DesasignarDocenteSala(Docente docente, Sala sala, UsuarioLogueado usuarioLogueado);
        /// <summary>
        /// El usuario debe ser directora, y el hijo debe estar asociado a una sala de su institucion
        /// </summary>
        /// <param name="hijo"></param>
        /// <param name="padre"></param>
        /// <param name="usuarioLogueado"></param>
        /// <returns></returns>
        Resultado AsignarHijoPadre(Hijo hijo, Padre padre, UsuarioLogueado usuarioLogueado);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hijo"></param>
        /// <param name="padre"></param>
        /// <param name="usuarioLogueado"></param>
        /// <returns></returns>
        Resultado DesasignarHijoPadre(Hijo hijo, Padre padre, UsuarioLogueado usuarioLogueado);
        /// <summary>
        /// Si el usuario es directora, retornar alumnos de la institucion, si es docente los de sus salas, y si es padre solo sus hijos.
        /// </summary>        
        /// <returns></returns>
        Hijo[] ObtenerPersonas(UsuarioLogueado usuarioLogueado);
        /// <summary>
        /// Obtiene las notas de un cuaderno, si el usuario es padre solo puede obtener cuadernos de sus hijos, si es docente de alumnos de sus salas
        /// y si es directora de cualquier alumno de la institucion
        /// </summary>
        /// <param name="usuarioLogueado"></param>
        /// <returns></returns>
        Nota[] ObtenerCuadernoComunicaciones(int idPersona, UsuarioLogueado usuarioLogueado);
        /// <summary>
        /// Alta de una nota, la nota puede estar dirigida a 1 o varias salas, o 1 o varios alumnos. Si el usuario es padre solamente podra enviar a sus hijos.
        /// </summary>
        /// <param name="nota"></param>
        /// <param name="salas"></param>
        /// <param name="hijos"></param>
        /// <param name="usuarioLogueado"></param>
        /// <returns></returns>
        Resultado AltaNota(Nota nota, Sala[] salas, Hijo[] hijos, UsuarioLogueado usuarioLogueado);
        /// <summary>
        /// Respuesta a una nota. Si es docente la nota debe ser de un alumno de la sala
        /// </summary>
        /// <param name="nota"></param>
        /// <param name="nuevoComentario"></param>
        /// <param name="usuarioLogueado"></param>
        /// <returns></returns>
        Resultado ResponderNota(Nota nota, Comentario nuevoComentario, UsuarioLogueado usuarioLogueado);
        /// <summary>
        /// Marca la nota como leida si le corresponde.
        /// </summary>
        /// <param name="nota"></param>
        /// <param name="usuarioLogueado"></param>
        /// <returns></returns>
        Resultado MarcarNotaComoLeida(Nota nota, UsuarioLogueado usuarioLogueado);

        /// <summary>
        /// Grilla de directoras
        /// </summary>
        /// <param name="usuarioLogueado"></param>
        /// <param name="paginaActual"></param>
        /// <param name="totalPorPagina"></param>
        /// <param name="busquedaGlobal"></param>
        /// <returns></returns>
        Grilla<Directora> ObtenerDirectoras(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal);

        /// <summary>
        /// Grilla de docentes
        /// </summary>
        /// <param name="usuarioLogueado"></param>
        /// <param name="paginaActual"></param>
        /// <param name="totalPorPagina"></param>
        /// <param name="busquedaGlobal"></param>
        /// <returns></returns>
        Grilla<Docente> ObtenerDocentes(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal);

        /// <summary>
        /// Grilla de padres
        /// </summary>
        /// <param name="usuarioLogueado"></param>
        /// <param name="paginaActual"></param>
        /// <param name="totalPorPagina"></param>
        /// <param name="busquedaGlobal"></param>
        /// <returns></returns>
        Grilla<Padre> ObtenerPadres(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal);

        /// <summary>
        /// Grilla de alumnos
        /// </summary>
        /// <param name="usuarioLogueado"></param>
        /// <param name="paginaActual"></param>
        /// <param name="totalPorPagina"></param>
        /// <param name="busquedaGlobal"></param>
        /// <returns></returns>
        Grilla<Hijo> ObtenerAlumnos(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal);

        /// <summary>
        /// Obtener directora por ID
        /// </summary>
        /// <param name="usuarioLogueado"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Directora ObtenerDirectoraPorId(UsuarioLogueado usuarioLogueado, int id);

        /// <summary>
        /// Obtener docente por ID
        /// </summary>
        /// <param name="usuarioLogueado"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Docente ObtenerDocentePorId(UsuarioLogueado usuarioLogueado, int id);

        /// <summary>
        /// Obtener padre por ID
        /// </summary>
        /// <param name="usuarioLogueado"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Padre ObtenerPadrePorId(UsuarioLogueado usuarioLogueado, int id);

        /// <summary>
        /// Obtener hijo por ID
        /// </summary>
        /// <param name="usuarioLogueado"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Hijo ObtenerAlumnoPorId(UsuarioLogueado usuarioLogueado, int id);
    }
}
