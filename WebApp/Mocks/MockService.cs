using Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocks
{
    public class MockService : IServicioWeb
    {
        public static List<Directora> _directoras = new List<Directora>()
        {
            new Directora(){ Id = 1, Nombre = "A 1", Apellido ="B", Email = "C", Cargo = "D"},new Directora(){ Id = 2, Nombre = "A 2", Apellido ="B", Email = "C", Cargo = "D"},
            new Directora(){ Id = 3, Nombre = "A 3", Apellido ="B", Email = "C", Cargo = "D"},new Directora(){ Id = 4, Nombre = "A 4", Apellido ="B", Email = "C", Cargo = "D"},
            new Directora(){ Id = 5, Nombre = "A 5", Apellido ="B", Email = "C", Cargo = "D"},new Directora(){ Id = 6, Nombre = "A 6", Apellido ="B", Email = "C", Cargo = "D"},
            new Directora(){ Id = 7, Nombre = "A 7", Apellido ="B", Email = "C", Cargo = "D"},new Directora(){ Id = 8, Nombre = "A 8", Apellido ="B", Email = "C", Cargo = "D"},
            new Directora(){ Id = 9, Nombre = "A 9", Apellido ="B", Email = "C", Cargo = "D"},new Directora(){ Id = 10, Nombre = "A 10", Apellido ="B", Email = "C", Cargo = "D"},
            new Directora(){ Id = 11, Nombre = "A 11", Apellido ="B", Email = "C", Cargo = "D"},new Directora(){ Id = 12, Nombre = "A 12", Apellido ="B", Email = "C", Cargo = "D"},
        };

        public static List<Docente> _docentes = new List<Docente>()
        {
            new Docente(){ Id = 1, Nombre = "D 1", Apellido ="DA 1", Email = "DE 1"},new Docente(){ Id = 2, Nombre = "D 2", Apellido ="DA 2", Email = "DE 2"},
            new Docente(){ Id = 3, Nombre = "D 3", Apellido ="DA 3", Email = "DE 3"},new Docente(){ Id = 4, Nombre = "D 4", Apellido ="DA 4", Email = "DE 4"},
            new Docente(){ Id = 5, Nombre = "D 5", Apellido ="DA 5", Email = "DE 5"},new Docente(){ Id = 6, Nombre = "D 6", Apellido ="DA 6", Email = "DE 6"},
            new Docente(){ Id = 7, Nombre = "D 7", Apellido ="DA 7", Email = "DE 7"},new Docente(){ Id = 8, Nombre = "D 8", Apellido ="DA 8", Email = "DE 8"},
            new Docente(){ Id = 9, Nombre = "D 9", Apellido ="DA 9", Email = "DE 9"},new Docente(){ Id = 10, Nombre = "D 10", Apellido ="DA 10", Email = "DE 10"},
            new Docente(){ Id = 11, Nombre = "D 11", Apellido ="DA 11", Email = "DE 11"},new Docente(){ Id = 12, Nombre = "D 12", Apellido ="DA 12", Email = "DE 12"},
            new Docente(){ Id = 13, Nombre = "D 13", Apellido ="DA 13", Email = "DE 13"},new Docente(){ Id = 14, Nombre = "D 14", Apellido ="DA 14", Email = "DE 14"},
            new Docente(){ Id = 15, Nombre = "D 15", Apellido ="DA 15", Email = "DE 15"},new Docente(){ Id = 16, Nombre = "D 16", Apellido ="DA 16", Email = "DE 16"},
        };

        public static List<Padre> _padres = new List<Padre>()
        {
            new Padre(){ Id = 1, Nombre = "P 1", Apellido = "PA 1", Email = "PE 1"},new Padre(){ Id = 2, Nombre = "P 2", Apellido = "PA 2", Email = "PE 2"},
            new Padre(){ Id = 3, Nombre = "P 3", Apellido = "PA 3", Email = "PE 3"},new Padre(){ Id = 4, Nombre = "P 4", Apellido = "PA 4", Email = "PE 4"},
            new Padre(){ Id = 5, Nombre = "P 5", Apellido = "PA 5", Email = "PE 5"},new Padre(){ Id = 6, Nombre = "P 6", Apellido = "PA 6", Email = "PE 6"},
            new Padre(){ Id = 7, Nombre = "P 7", Apellido = "PA 7", Email = "PE 7"},new Padre(){ Id = 8, Nombre = "P 8", Apellido = "PA 8", Email = "PE 8"},
            new Padre(){ Id = 9, Nombre = "P 9", Apellido = "PA 9", Email = "PE 9"},new Padre(){ Id = 10, Nombre = "P 10", Apellido = "PA 10", Email = "PE 10"},
            new Padre(){ Id = 11, Nombre = "P 11", Apellido = "PA 11", Email = "PE 11"},new Padre(){ Id = 12, Nombre = "P 12", Apellido = "PA 12", Email = "PE 12"},
        };

        public static List<Nota> _notas1 = new List<Nota>()
        {
            new Nota(){ Id = 1, Leida = false, Titulo= "Nota 1", Descripcion = "Descripcion de la nota 1", Comentarios = new Comentario[]{ } },
            new Nota(){ Id = 2, Leida = false, Titulo= "Nota 2", Descripcion = "Descripcion de la nota 2", Comentarios = new Comentario[]{
                new Comentario() { Fecha = DateTime.Now.AddDays(-2), Mensaje = "Comentario 1" , Usuario = new Usuario(){ Nombre = "Usuario", Apellido="Cualquiera" } },
                new Comentario() { Fecha = DateTime.Now.AddDays(-1), Mensaje = "Comentario 2" , Usuario = new Usuario(){ Nombre = "Usuario", Apellido="Cualquiera 2" } },
            } }
        };

        public static List<Nota> _notas2 = new List<Nota>()
        {
            new Nota(){ Id = 3, Leida = true, Titulo= "Nota 3", Descripcion = "Descripcion de la nota 3", Comentarios = new Comentario[]{ } },            
        };

        public static List<Nota> _notas3 = new List<Nota>()
        {
            new Nota(){ Id = 4, Leida = false, Titulo= "Nota 4", Descripcion = "Descripcion de la nota 4", Comentarios = new Comentario[]{ } },            
        };

        public static List<Nota> _notas4 = new List<Nota>()
        {
            new Nota(){ Id = 5, Leida = true, Titulo= "Nota 5", Descripcion = "Descripcion de la nota 5", Comentarios = new Comentario[]{ } },
        };

        public static List<Hijo> _alumnos = new List<Hijo>()
        {
            new Hijo(){ Id = 1, Nombre = "AL 1", Apellido="AP 1", Email="APE 1", FechaNacimiento = new DateTime(1990,5,4), ResultadoUltimaEvaluacionAnual = 10, Sala = new Sala(){ Id = 1 }, Notas = _notas1.ToArray() },
            new Hijo(){ Id = 2, Nombre = "AL 2", Apellido="AP 2", Email="APE 2", FechaNacimiento = new DateTime(1991,3,20), ResultadoUltimaEvaluacionAnual = 6, Sala = new Sala(){ Id = 2 }, Notas = _notas2.ToArray()},
            new Hijo(){ Id = 3, Nombre = "AL 3", Apellido="AP 3", Email="APE 3", FechaNacimiento = new DateTime(1992,12,14), ResultadoUltimaEvaluacionAnual = 5, Sala = new Sala(){ Id = 2 }, Notas = _notas3.ToArray()},
            new Hijo(){ Id = 4, Nombre = "AL 4", Apellido="AP 4", Email="APE 4", FechaNacimiento = new DateTime(1989,11,29), ResultadoUltimaEvaluacionAnual = 3, Sala = new Sala(){ Id = 3 }, Notas = _notas4.ToArray()},
        };

        public static List<Sala> _salas = new List<Sala>()
        {
            new Sala(){ Id = 1, Nombre = "Sala 1" },
            new Sala(){ Id = 2, Nombre = "Sala 2" },
            new Sala(){ Id = 3, Nombre = "Sala 3" },
        };

        public Resultado AltaDirectora(Directora directora, UsuarioLogueado usuarioLogueado)
        {
            directora.Id = _directoras.Count + 1;
            _directoras.Add(directora);

            return new Resultado();
        }

        public Resultado AltaDocente(Docente docente, UsuarioLogueado usuarioLogueado)
        {
            docente.Id = _docentes.Count + 1;
            _docentes.Add(docente);

            return new Resultado();
        }

        public Resultado AltaAlumno(Hijo hijo, UsuarioLogueado usuarioLogueado)
        {
            hijo.Id = _alumnos.Count + 1;
            _alumnos.Add(hijo);

            return new Resultado();
        }

        public Resultado AltaNota(Nota nota, Sala[] salas, Hijo[] hijos, UsuarioLogueado usuarioLogueado)
        {
            if (hijos != null && hijos.Length > 0)
            {
                foreach (var item in hijos)
                {
                    var hijo = _alumnos.Single(x => x.Id == item.Id);
                    var notasHijo = hijo.Notas == null ? new List<Nota>() : hijo.Notas.ToList();

                    notasHijo.Add(nota);

                    hijo.Notas = notasHijo.ToArray();
                }
            }
            else
            {
                List<Hijo> alumnos = new List<Hijo>();
                foreach (var sala in salas)
                {
                    alumnos.AddRange(_alumnos.Where(x => x.Sala.Id == sala.Id));
                }

                foreach (var item in alumnos)
                {
                    var notasHijo = item.Notas == null ? new List<Nota>() : item.Notas.ToList();

                    notasHijo.Add(nota);

                    item.Notas = notasHijo.ToArray();                    
                }
            }

            return new Resultado();
        }

        public Resultado AltaPadreMadre(Padre padre, UsuarioLogueado usuarioLogueado)
        {
            padre.Id = _padres.Count + 1;
            _padres.Add(padre);

            return new Resultado();
        }

        public Resultado AsignarDocenteSala(Docente docente, Sala sala, UsuarioLogueado usuarioLogueado)
        {
            var salasDocente = docente.Salas != null ? docente.Salas.ToList() : new List<Sala>();

            if (salasDocente.Any(x => x.Id == sala.Id) == false)
                salasDocente.Add(sala);

            docente.Salas = salasDocente.ToArray();

            return new Resultado();
        }

        public Resultado AsignarHijoPadre(Hijo hijo, Padre padre, UsuarioLogueado usuarioLogueado)
        {
            var hijosPadre = padre.Hijos != null ? padre.Hijos.ToList() : new List<Hijo>();

            if (hijosPadre.Any(x => x.Id == hijo.Id) == false)
                hijosPadre.Add(hijo);

            padre.Hijos = hijosPadre.ToArray();

            return new Resultado();
        }

        public Resultado DesasignarDocenteSala(Docente docente, Sala sala, UsuarioLogueado usuarioLogueado)
        {
            var salasDocente = docente.Salas != null ? docente.Salas.ToList() : new List<Sala>();

            if (salasDocente.Any(x => x.Id == sala.Id) == true)
                salasDocente.Remove(sala);

            docente.Salas = salasDocente.ToArray();

            return new Resultado();
        }

        public Resultado DesasignarHijoPadre(Hijo hijo, Padre padre, UsuarioLogueado usuarioLogueado)
        {
            var hijosPadre = padre.Hijos != null ? padre.Hijos.ToList() : new List<Hijo>();

            if (hijosPadre.Any(x => x.Id == hijo.Id) == true)
                hijosPadre.Remove(hijo);

            padre.Hijos = hijosPadre.ToArray();

            return new Resultado();
        }

        public Resultado EditarDirectora(int id, Directora directora, UsuarioLogueado usuarioLogueado)
        {
            _directoras.RemoveAll(x => x.Id == id);
            _directoras.Add(directora);

            return new Resultado();
        }

        public Resultado EditarAlumno(int id, Hijo hijo, UsuarioLogueado usuarioLogueado)
        {
            _alumnos.RemoveAll(x => x.Id == id);
            _alumnos.Add(hijo);

            return new Resultado();
        }

        public Resultado EditarDocente(int id, Docente docente, UsuarioLogueado usuarioLogueado)
        {
            _docentes.RemoveAll(x => x.Id == id);
            _docentes.Add(docente);

            return new Resultado();
        }

        public Resultado EditarPadreMadre(int id, Padre padre, UsuarioLogueado usuarioLogueado)
        {
            _padres.RemoveAll(x => x.Id == id);
            _padres.Add(padre);

            return new Resultado();
        }

        public Resultado EliminarDirectora(int id, Directora directora, UsuarioLogueado usuarioLogueado)
        {
            _directoras.RemoveAll(x => x.Id == id);

            return new Resultado();
        }

        public Resultado EliminarDocente(int id, Docente docente, UsuarioLogueado usuarioLogueado)
        {
            _docentes.RemoveAll(x => x.Id == id);

            return new Resultado();
        }

        public Resultado EliminarPadreMadre(int id, Padre padre, UsuarioLogueado usuarioLogueado)
        {
            _padres.RemoveAll(x => x.Id == id);

            return new Resultado();
        }

        public Resultado EliminarAlumno(int id, Hijo hijo, UsuarioLogueado usuarioLogueado)
        {
            _alumnos.RemoveAll(x => x.Id == id);

            return new Resultado();
        }

        public Resultado MarcarNotaComoLeida(Nota nota, UsuarioLogueado usuarioLogueado)
        {
            var _notas = _notas1.Union(_notas2).Union(_notas3).Union(_notas4);

            var n = _notas.Single(x => x.Id == nota.Id);

            n.Leida = true;

            return new Resultado();
        }

        public Hijo ObtenerAlumnoPorId(UsuarioLogueado usuarioLogueado, int id)
        {
            return _alumnos.First(x => x.Id == id);
        }

        public Grilla<Hijo> ObtenerAlumnos(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            return new Grilla<Hijo>()
            {
                Lista = _alumnos
                .Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal))
                .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray(),
                CantidadRegistros = _alumnos.Count
            };
        }

        public Nota[] ObtenerCuadernoComunicaciones(int idPersona, UsuarioLogueado usuarioLogueado)
        {
            return _alumnos.Single(x => x.Id == idPersona).Notas;
        }

        public Directora ObtenerDirectoraPorId(UsuarioLogueado usuarioLogueado, int id)
        {
            return _directoras.First(x => x.Id == id);
        }

        public Grilla<Directora> ObtenerDirectoras(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            return new Grilla<Directora>()
            {
                Lista = _directoras.Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal))
                                    .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray(),
                CantidadRegistros = _directoras.Count
            };
        }

        public Docente ObtenerDocentePorId(UsuarioLogueado usuarioLogueado, int id)
        {
            return _docentes.First(x => x.Id == id);
        }

        public Grilla<Docente> ObtenerDocentes(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            return new Grilla<Docente>()
            {
                Lista = _docentes
                .Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal))
                .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray(),
                CantidadRegistros = _docentes.Count
            };
        }

        public Institucion[] ObtenerInstituciones()
        {
            throw new NotImplementedException();
        }

        public string ObtenerNombreGrupo()
        {
            return $"Repo original";
        }

        public Padre ObtenerPadrePorId(UsuarioLogueado usuarioLogueado, int id)
        {
            return _padres.First(x => x.Id == id);
        }

        public Grilla<Padre> ObtenerPadres(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            return new Grilla<Padre>()
            {
                Lista = _padres
                .Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal))
                .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray(),
                CantidadRegistros = _padres.Count
            };
        }

        public Hijo[] ObtenerPersonas(UsuarioLogueado usuarioLogueado)
        {
            switch (usuarioLogueado.RolSeleccionado)
            {
                case Roles.Padre:
                    return _alumnos.Where(x => x.Id == 1 || x.Id == 2).ToArray();
                case Roles.Directora:
                    return _alumnos.ToArray();
                case Roles.Docente:
                    return _alumnos.Where(x => x.Sala.Id == 2 || x.Sala.Id == 4).ToArray();
                default:
                    throw new Exception("Rol no implementado");                    
            }
        }

        public Sala[] ObtenerSalasPorInstitucion(UsuarioLogueado usuarioLogueado)
        {
            return usuarioLogueado.RolSeleccionado == Roles.Docente ? _salas.Where(x=>x.Id == 2 || x.Id == 3).ToArray() : _salas.ToArray();
        }

        public UsuarioLogueado ObtenerUsuario(string email, string clave)
        {
            if (email == "" || clave == "")
                return null;

            if (email == "directora@ucse.com" && clave == "123456")
                return new UsuarioLogueado() { Email = email, Nombre = "Usuario", Apellido = "Directora", Roles = new Roles[] { Roles.Directora }, RolSeleccionado = Roles.Directora };

            if (email == "docente@ucse.com" && clave == "123456")
                return new UsuarioLogueado() { Email = email, Nombre = "Usuario", Apellido = "Docente", Roles = new Roles[] { Roles.Docente }, RolSeleccionado = Roles.Docente };

            if (email == "padre@ucse.com" && clave == "123456")
                return new UsuarioLogueado() { Email = email, Nombre = "Usuario", Apellido = "Padre", Roles = new Roles[] { Roles.Padre }, RolSeleccionado = Roles.Padre };

            return null;
        }

        public Resultado ResponderNota(Nota nota, Comentario nuevoComentario, UsuarioLogueado usuarioLogueado)
        {
            var _notas = _notas1.Union(_notas2).Union(_notas3).Union(_notas4);

            var n = _notas.Single(x => x.Id == nota.Id);
            var comentarios = n.Comentarios == null ? new List<Comentario>() : n.Comentarios.ToList();
            comentarios.Add(nuevoComentario);

            n.Comentarios = comentarios.ToArray();

            return new Resultado();
        }
    }
}
