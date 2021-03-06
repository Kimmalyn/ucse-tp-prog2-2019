﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contratos;
using Newtonsoft.Json;

//agregar.count a las altas
//verificar rol antes de leer
//usar un delegado para verificar los roles
//tipo de dato generico + switch tipo ¨generic¨
//falta el total en la grilla
//iqueryable<alumnos> query = total.where(x=>x.nombre == "a").asquerible();

namespace Logica
{
    public class Principal
    {
        ///
        /// <summary>
        /// SINGLETON
        /// </summary>
        /// 

        private Principal() { }

        private static readonly Principal Instacia_Principal = new Principal();

        public static Principal Instancia
        {
            get
            {
                return Instacia_Principal;
            }
        }

        /// <summary>
        /// PROPIEDADES
        /// </summary>
        
        //NO ES RECOMENDABLE ESTO, SOBRE TODO QUE SEAN PUBLICAS
        //ACOTAR EL USO AL AMBITO DE CADA METODO
        public List<Directora> ListaDirectoras { get; set; }
        public List<Docente> ListaDocentes { get; set; }
        public List<Padre> ListaPadres { get; set; }
        public List<Hijo> ListaHijos { get; set; }
        public List<Clave> ListaClaves { get; set; }
        public List<Nota> ListaNotas { get; set; }
        public List<Sala> ListaSalas { get; set; }

        public static string PathDirectoras { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Directoras.txt").ToString(); } }
        public static string PathDocentes { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Docentes.txt").ToString(); } }
        public static string PathPadres { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Padres.txt").ToString(); } }
        public static string PathHijos { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Hijos.txt").ToString(); } }
        public static string PathClaves { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Claves.txt").ToString(); } }
        public static string PathSalas { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Salas.txt").ToString(); } }
        public static string PathNotas { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Notas.txt").ToString(); } }

        /// <summary>
        /// METODOS
        /// </summary>


        //CreaTodosArchivos
        //ESTO PODRIA HACERSE EN UNA CLASE EN UN ARCHIVO DISTINTO, Y SOBRE TODO, HACERSE UNA SOLA VEZ EN EL CONSTRUCTOR PRIVADO DEL SINGLETON, PARA
        //EVITAR TENER QUE PONERLO EN TODOS LADOS
        private void CrearArchivos()
        {
            if (!File.Exists(PathDirectoras))
            {
                File.Create(PathDirectoras).Close();
            }
            if (ListaDirectoras == null)
            {
                List<Directora> ListaDirectoras = new List<Directora>();
            }

            if (!File.Exists(PathDocentes))
            {
                File.Create(PathDocentes).Close();
            }
            if (ListaDocentes == null)
            {
                List<Docente> ListaDocentes = new List<Docente>();
            }

            if (!File.Exists(PathPadres))
            {
                File.Create(PathPadres).Close();
            }
            if (ListaPadres == null)
            {
                List<Padre> ListaPadres = new List<Padre>();
            }

            if (!File.Exists(PathHijos))
            {
                File.Create(PathHijos).Close();
            }
            if (ListaHijos == null)
            {
                List<Hijo> ListaHijos = new List<Hijo>();
            }

            if (!File.Exists(PathClaves))
            {
                File.Create(PathClaves).Close();
            }
            if (ListaClaves == null)
            {
                List<Clave> ListaClaves = new List<Clave>();
            }

            if (!File.Exists(PathSalas))
            {
                File.Create(PathSalas).Close();
            }
            if (ListaDirectoras == null)
            {
                List<Sala> ListaSalas = new List<Sala>();
            }

            if (!File.Exists(PathNotas))
            {
                File.Create(PathNotas).Close();
            }
            if (ListaNotas == null)
            {
                List<Nota> ListaNotas = new List<Nota>();
            }

        }

        //Login
        private void LeerClaves()
        {
            using (StreamReader reader = new StreamReader(PathClaves))
            {
                CrearArchivos();
                string contenido = reader.ReadToEnd();
                ListaClaves = JsonConvert.DeserializeObject<List<Clave>>(contenido);

                if (ListaClaves == null)
                {
                    ListaClaves = new List<Clave>();
                }
            }
        }
        
        private void GuardarClaves(List<Clave> listaclaves)
        {
            CrearArchivos();
            using (StreamWriter writer = new StreamWriter(PathClaves, false))
            {
                string jsonClaves = JsonConvert.SerializeObject(listaclaves);
                writer.Write(jsonClaves);
            }
        }

        public UsuarioLogueado ObtenerUsuario(string email, string clave)//funciona :'D
        {
            CrearArchivos();
            LeerDirectoras();
            LeerDocentes();
            LeerPadres();
            LeerClaves();

            List<Usuario> ListaUsuarios = new List<Usuario>();
            ListaUsuarios.AddRange(ListaDirectoras);
            ListaUsuarios.AddRange(ListaDocentes);
            ListaUsuarios.AddRange(ListaPadres);

            var pass = ListaClaves.Where(x => x.Email == email && x.Password == clave).FirstOrDefault();
            var usuario = ListaUsuarios.Where(x => x.Email == email).FirstOrDefault();
            var usuariologueado = new UsuarioLogueado();

            if (pass != null || usuario != null)
            {
                usuariologueado.Nombre = usuario.Nombre;
                usuariologueado.Apellido = usuario.Apellido;
                usuariologueado.Email = email;
                usuariologueado.RolSeleccionado = pass.Rol;
            }
            else
            {
                Resultado resultado = new Resultado();
                resultado.Errores.Add("Error de autenticacion");
                usuariologueado = null;
            }

            GuardarDirectora(ListaDirectoras);
            GuardarDocente(ListaDocentes);
            GuardarPadre(ListaPadres);
            GuardarClaves(ListaClaves);

            return usuariologueado;
        }

        //verfica que el rol usado sea el correcto
        private Resultado VerificarUsuarioLogeado(Roles rol, UsuarioLogueado usuariologeado)
        {
            var Resultado = new Resultado();
            if (usuariologeado.RolSeleccionado != rol)
            {
                Resultado.Errores.Add("No se tienen los permisos necesarios para realizar esta acción.");
            }

            return Resultado;
        }

        /// <summary>
        /// INICIO DIRECTORA
        /// </summary>

        private void LeerDirectoras()
        {
            using (StreamReader reader = new StreamReader(PathDirectoras))
            {
                CrearArchivos();
                string contenido = reader.ReadToEnd();
                ListaDirectoras = JsonConvert.DeserializeObject<List<Directora>>(contenido);

                if (ListaDirectoras == null)
                {
                    ListaDirectoras = new List<Directora>();
                }
            }
        }

        private void GuardarDirectora(List<Directora> listadirectora)
        {
            CrearArchivos();
            using (StreamWriter writer = new StreamWriter(PathDirectoras, false))
            {
                string jsonDirectoras = JsonConvert.SerializeObject(listadirectora);
                writer.Write(jsonDirectoras);
            }
        }

        public Directora ObtenerDirectoraPorId(UsuarioLogueado usuarioLogueado, int id)
        {
            CrearArchivos();
            LeerDirectoras();
            var directora = new Directora();
            if (VerificarUsuarioLogeado(Roles.Directora, usuarioLogueado).EsValido)
            {
                directora = ListaDirectoras.Where(x => x.Id == id).FirstOrDefault();
            }
            else
            {
                directora = null;
            }
            GuardarDirectora(ListaDirectoras);
            return directora;
        }
        
        public Resultado AltaDirectora(Directora directora, UsuarioLogueado usuariologueado)
        {
            CrearArchivos();
            LeerDirectoras();
            LeerClaves();

            Resultado verificacion = VerificarUsuarioLogeado(Roles.Directora, usuariologueado);
            if (verificacion.EsValido)
            {
                directora.Id = ListaDirectoras.Count() + 1;
                Random rnd = new Random();
                var pswrd = rnd.Next(100000, 999999).ToString();
                ListaDirectoras.Add(directora);
                Clave pass = new Clave() { Email = directora.Email, Password = pswrd, Rol = Roles.Directora };
                ListaClaves.Add(pass);
                GuardarDirectora(ListaDirectoras);
                GuardarClaves(ListaClaves);
            }

            return verificacion;
        }

        public Resultado EditarDirectora(int id, Directora directoraeditada, UsuarioLogueado usuariologueado)
        {
            CrearArchivos();
            LeerDirectoras();

            Resultado verificacion = VerificarUsuarioLogeado(Roles.Directora, usuariologueado);
            if (verificacion.EsValido)
            {
                var directora = ObtenerDirectoraPorId(usuariologueado, id);
                ListaDirectoras.Remove(directora);
                ListaDirectoras.Add(directoraeditada);
                GuardarDirectora(ListaDirectoras);
            }


            return verificacion;
        }

        public Resultado EliminarDirectora(int id, Directora directoraeliminada, UsuarioLogueado usuariologueado)
        {
            CrearArchivos();
            LeerDirectoras();
            Resultado verificacion = VerificarUsuarioLogeado(Roles.Directora, usuariologueado);
            if (verificacion.EsValido)
            {
                var directora = ObtenerDirectoraPorId(usuariologueado, id);
                ListaDirectoras.Remove(directora);
                GuardarDirectora(ListaDirectoras);
            }
            return verificacion;
        }

        public Grilla<Directora> ObtenerDirectoras(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            CrearArchivos();
            LeerDirectoras();

            var listagrilla = ListaDirectoras
               .Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal))
               .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray();
            
            GuardarDirectora(ListaDirectoras);
            return new Grilla<Directora>
            {
                Lista = listagrilla,
                CantidadRegistros = ListaDirectoras.Count(),
            };
        }

        /// <summary>
        /// FIN DIRECTORA
        /// </summary>

        /// <summary>
        /// INICIO DOCENTE
        /// </summary>

        private void LeerDocentes()
        {
            using (StreamReader reader = new StreamReader(PathDocentes))
            {
                CrearArchivos();
                string contenido = reader.ReadToEnd();
                ListaDocentes = JsonConvert.DeserializeObject<List<Docente>>(contenido);

                if (ListaDocentes == null)
                {
                    ListaDocentes = new List<Docente>();
                }
            }
        }

        private void GuardarDocente(List<Docente> listadocente)
        {
            CrearArchivos();
            using (StreamWriter writer = new StreamWriter(PathDocentes, false))
            {
                string jsonDocentes = JsonConvert.SerializeObject(listadocente);
                writer.Write(jsonDocentes);
            }
        }

        public Docente ObtenerDocentePorId(UsuarioLogueado usuarioLogueado, int id)
        {
            CrearArchivos();
            LeerDocentes();
            var docente = new Docente();
            if (VerificarUsuarioLogeado(Roles.Directora, usuarioLogueado).EsValido)
            {
                docente = ListaDocentes.Where(x => x.Id == id).FirstOrDefault();
            }
            else
            {
                docente = null;
            }
            GuardarDocente(ListaDocentes);
            return docente;
        }

        public Resultado AltaDocente(Docente docente, UsuarioLogueado usuariologueado)
        {
            CrearArchivos();
            LeerDocentes();
            LeerClaves();

            Resultado verificacion = VerificarUsuarioLogeado(Roles.Directora, usuariologueado);
            if (verificacion.EsValido)
            {
                docente.Id = ListaDocentes.Count() + 1;
                Random rnd = new Random();
                var pswrd = rnd.Next(100000, 999999).ToString();
                ListaDocentes.Add(docente);
                Clave pass = new Clave() { Email = docente.Email, Password = pswrd, Rol = Roles.Docente };
                ListaClaves.Add(pass);
                GuardarDocente(ListaDocentes);
                GuardarClaves(ListaClaves);

            }
            
            return verificacion;
        }

        public Resultado EditarDocente(int id, Docente docenteeditada, UsuarioLogueado usuariologueado)
        {
            CrearArchivos();
            LeerDocentes();

            Resultado verificacion = VerificarUsuarioLogeado(Roles.Directora, usuariologueado);
            if (verificacion.EsValido)
            {
                var docente = ObtenerDocentePorId(usuariologueado, id);
                ListaDocentes.Remove(docente);
                ListaDocentes.Add(docenteeditada);
                GuardarDocente(ListaDocentes);
            }

            return verificacion;
        }

        public Resultado EliminarDocente(int id, Docente docenteeliminada, UsuarioLogueado usuariologueado)
        {
            CrearArchivos();
            LeerDocentes();
            Resultado verificacion = VerificarUsuarioLogeado(Roles.Directora, usuariologueado);
            if (verificacion.EsValido)
            {
                var docente = ObtenerDocentePorId(usuariologueado, id);
                ListaDocentes.Remove(docente);
                GuardarDocente(ListaDocentes);
            }
            return verificacion;
        }

        public Grilla<Docente> ObtenerDocentes(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            CrearArchivos();
            LeerDocentes();

            var listagrilla = ListaDocentes
               .Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal))
               .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray();

            GuardarDocente(ListaDocentes);
            return new Grilla<Docente>
            {
                Lista = listagrilla,
                CantidadRegistros = ListaDocentes.Count(),
            };
        }

        /// <summary>
        /// FIN DOCENTE
        /// </summary>
        
        /// <summary>
        /// INICIO SALAS
        /// </summary>
        
        private void LeerSalas()
        {
            using (StreamReader reader = new StreamReader(PathSalas))
            {
                CrearArchivos();
                string contenido = reader.ReadToEnd();
                ListaSalas = JsonConvert.DeserializeObject<List<Sala>>(contenido);

                if (ListaSalas == null)
                {
                    ListaSalas = new List<Sala>();
                }
            }
        }

        public Resultado AsignarDocenteSala(Docente docente, Sala sala, UsuarioLogueado usuariologueado)
        {
            var resultado = new Resultado();
            if (VerificarUsuarioLogeado(Roles.Directora, usuariologueado).EsValido)
            {
                var salasdocente = docente.Salas != null ? docente.Salas.ToList() : new List<Sala>();

                if (salasdocente.Any(x => x.Id == sala.Id) == false) //Verifica que la sala agregar no este repetida
                    salasdocente.Add(sala);
                else
                    resultado.Errores.Add("La sala ingresada ya está asignada al docente.");

                docente.Salas = salasdocente.ToArray();

                EditarDocente(docente.Id, docente, usuariologueado); //Modifica el docente asignandole las salas
                GuardarDocente(ListaDocentes);
            }
            return resultado;
        }

        public Resultado DesasignarDocenteSala(Docente docente, Sala sala, UsuarioLogueado usuariologueado)
        {
            var resultado = new Resultado();
            if (VerificarUsuarioLogeado(Roles.Directora, usuariologueado).EsValido)
            {
                var salasDocente = docente.Salas != null ? docente.Salas.ToList() : new List<Sala>();

                if (salasDocente.Any(x => x.Id == sala.Id) == true) //Verifica que la sala a desasignar exista
                    salasDocente.Remove(sala);
                else
                    resultado.Errores.Add("La sala ingresada no está asignada al docente.");

                docente.Salas = salasDocente.ToArray();

                //SI SIEMPRE QUE EDITAS TENES QUE GUARDAR, LA INVOCACION AL GUARDAR LA PODRIAS HACER DENTRO DEL METODO EDITAR
                EditarDocente(docente.Id, docente, usuariologueado); //Modifica el docente desasignandole las salas
                GuardarDocente(ListaDocentes);
            }
            return resultado;
        }

        public Sala[] ObtenerSalasPorInstitucion(UsuarioLogueado usuariologueado)
        {
            CrearArchivos();
            LeerDocentes();
            LeerSalas();
            Sala[] lista_salas = null;
            //Resultado verificacion = verificarusuarioLogeado(Roles.Directora, usuariologueado); 
            if (usuariologueado.RolSeleccionado == Roles.Directora)
            {
                lista_salas = ListaSalas.ToArray();
            }
            else if (usuariologueado.RolSeleccionado == Roles.Docente)
            {
                Docente maestra = ListaDocentes.Where(x => x.Email == usuariologueado.Email && x.Apellido == usuariologueado.Apellido).FirstOrDefault();
                Sala nuevasala = new Sala();
                lista_salas = maestra.Salas;
            }

            return lista_salas;
        }

        /// <summary>
        /// FIN SALAS
        /// </summary>

        /// <summary>
        /// INICIO PADRES
        /// </summary>

        private void LeerPadres()
        {
            using (StreamReader reader = new StreamReader(PathPadres))
            {
                CrearArchivos();
                string contenido = reader.ReadToEnd();
                ListaPadres = JsonConvert.DeserializeObject<List<Padre>>(contenido);

                if (ListaPadres == null)
                {
                    ListaPadres = new List<Padre>();
                }
            }
        }

        private void GuardarPadre(List<Padre> listapadre)
        {
            CrearArchivos();
            using (StreamWriter writer = new StreamWriter(PathPadres, false))
            {
                string jsonPadres = JsonConvert.SerializeObject(listapadre);
                writer.Write(jsonPadres);
            }
        }

        public Padre ObtenerPadrePorId(UsuarioLogueado usuarioLogueado, int id)
        {
            CrearArchivos();
            LeerPadres();
            var padre = new Padre();
            if (VerificarUsuarioLogeado(Roles.Directora, usuarioLogueado).EsValido)
            {
                padre = ListaPadres.Where(x => x.Id == id).FirstOrDefault();
            }
            else
            {
                padre = null;
            }
            GuardarPadre(ListaPadres);
            return padre;
        }

        public Resultado AltaPadre(Padre padre, UsuarioLogueado usuariologueado)
        {
            CrearArchivos();
            LeerPadres();
            LeerClaves();

            Resultado verificacion = VerificarUsuarioLogeado(Roles.Directora, usuariologueado);
            if (verificacion.EsValido)
            {
                padre.Id = ListaPadres.Count() + 1;
                Random rnd = new Random();
                var pswrd = rnd.Next(100000, 999999).ToString();
                ListaPadres.Add(padre);
                Clave pass = new Clave() { Email = padre.Email, Password = pswrd, Rol = Roles.Padre };
                ListaClaves.Add(pass);
                GuardarPadre(ListaPadres);
                GuardarClaves(ListaClaves);
            }

            return verificacion;
        }

        public Resultado EditarPadre(int id, Padre padreeditado, UsuarioLogueado usuariologueado)
        {
            CrearArchivos();
            LeerPadres();

            Resultado verificacion = VerificarUsuarioLogeado(Roles.Directora, usuariologueado);
            if (verificacion.EsValido)
            {
                var padre = ObtenerPadrePorId(usuariologueado, id);
                ListaPadres.Remove(padre);
                ListaPadres.Add(padreeditado);
                GuardarPadre(ListaPadres);
            }

            return verificacion;
        }

        public Resultado EliminarPadre(int id, Padre padreeliminado, UsuarioLogueado usuariologueado)
        {
            CrearArchivos();
            LeerPadres();

            Resultado verificacion = VerificarUsuarioLogeado(Roles.Directora, usuariologueado);
            if (verificacion.EsValido)
            {
                var padre = ObtenerPadrePorId(usuariologueado, id);
                ListaPadres.Remove(padre);
                GuardarPadre(ListaPadres);
            }
            return verificacion;
        }

        public Grilla<Padre> ObtenerPadres(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            CrearArchivos();
            LeerPadres();

            var listagrilla = ListaPadres
               .Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal))
               .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray();

            GuardarPadre(ListaPadres);
            return new Grilla<Padre>
            {
                Lista = listagrilla,
                CantidadRegistros = ListaPadres.Count(),
            };
        }

        /// <summary>
        /// FIN PADRES
        /// </summary>

        /// <summary>
        /// INICIO HIJOS
        /// </summary>

        private void LeerHijos()
        {
            using (StreamReader reader = new StreamReader(PathHijos))
            {
                CrearArchivos();
                string contenido = reader.ReadToEnd();
                ListaHijos = JsonConvert.DeserializeObject<List<Hijo>>(contenido);

                if (ListaHijos == null)
                {
                    ListaHijos = new List<Hijo>();
                }
            }
        }

        private void GuardarHijos(List<Hijo> listahijos)
        {
            CrearArchivos();
            using (StreamWriter writer = new StreamWriter(PathHijos))
            {
                string jsonHijos = JsonConvert.SerializeObject(listahijos);
                writer.Write(jsonHijos);
            }
        }

        public Hijo ObtenerAlumnoPorId(UsuarioLogueado usuarioLogueado, int id)
        {
            CrearArchivos();
            LeerHijos();
            var hijo = new Hijo();
            if (VerificarUsuarioLogeado(Roles.Directora, usuarioLogueado).EsValido)
            {
                hijo = ListaHijos.Single(x => x.Id == id);
            }
            else
            {
                hijo = null;
            }
            GuardarHijos(ListaHijos);
            return hijo;
        }

        public Resultado AltaAlumno(Hijo hijo, UsuarioLogueado usuarioLogueado)
        {
            CrearArchivos();
            LeerHijos();
            var resultado = VerificarUsuarioLogeado(Roles.Directora, usuarioLogueado);
            if (resultado.EsValido)
            {
                hijo.Id = ListaHijos.Count() + 1;
                ListaHijos.Add(hijo);
            }
            GuardarHijos(ListaHijos);
            return resultado;

        }

        public Resultado EditarAlumno(int id, Hijo hijoeditado, UsuarioLogueado usuariologueado)
        {
            CrearArchivos();
            LeerHijos();

            var resultado = VerificarUsuarioLogeado(Roles.Directora, usuariologueado);

            if (resultado.EsValido)
            {
                var hijo = ObtenerAlumnoPorId(usuariologueado, id);
                ListaHijos.Remove(hijo);
                ListaHijos.Add(hijoeditado);
                GuardarHijos(ListaHijos);
            }           

            return resultado;
        }

        public Resultado EliminarAlumno(int id, Hijo hijo, UsuarioLogueado usuariologueado)
        {
            LeerHijos();

            var resultado = VerificarUsuarioLogeado(Roles.Directora, usuariologueado);

            if (resultado.EsValido)
            {
                var hijoeliminado = ObtenerAlumnoPorId(usuariologueado, id);
                ListaHijos.Remove(hijoeliminado);
            }
            GuardarHijos(ListaHijos);
            return resultado;
        }

        public Grilla<Hijo> ObtenerAlumnos(UsuarioLogueado usuarioLogueado, int paginaActual, int totalPorPagina, string busquedaGlobal)
        {
            CrearArchivos();
            LeerHijos();

            var listagrilla = ListaHijos
               .Where(x => string.IsNullOrEmpty(busquedaGlobal) || x.Nombre.Contains(busquedaGlobal) || x.Apellido.Contains(busquedaGlobal))
               .Skip(paginaActual * totalPorPagina).Take(totalPorPagina).ToArray();
            
            GuardarHijos(ListaHijos);
            return new Grilla<Hijo>
            {
                Lista = listagrilla,
                CantidadRegistros = ListaHijos.Count(),
            };
        }

        public Resultado AsignarHijoPadre(Hijo hijo, Padre padre, UsuarioLogueado usuariologueado)
        {
            CrearArchivos();
            LeerPadres();

            var resultado = new Resultado();

            if (VerificarUsuarioLogeado(Roles.Directora,usuariologueado).EsValido)
            {               

                var listahijos = padre.Hijos != null ? padre.Hijos.ToList() : new List<Hijo>();

                if (listahijos.Any(x => x.Id == hijo.Id) == false) //Verifica que el hijo agregar no este repetida
                    listahijos.Add(hijo);
                else
                    resultado.Errores.Add("El hijo ya esta asignado");

                padre.Hijos = listahijos.ToArray();

                EditarPadre(padre.Id, padre, usuariologueado); //Modifica el docente asignandole las salas

                GuardarPadre(ListaPadres);
            }
            return resultado;
        }

        public Resultado DesasignarHijoPadre(Hijo hijo, Padre padre, UsuarioLogueado usuariologueado)
        {
            CrearArchivos();
            LeerPadres();

            var resultado = new Resultado();

            if (VerificarUsuarioLogeado(Roles.Directora, usuariologueado).EsValido)
            {              

                var listahijos = padre.Hijos != null ? padre.Hijos.ToList() : new List<Hijo>();

                if (listahijos.Any(x => x.Id == hijo.Id) == false) //Verifica que el hijo a agregar no este repetido
                    listahijos.Add(hijo);
                else
                    resultado.Errores.Add("El hijo ya esta asignado");

                padre.Hijos = listahijos.ToArray();

                EditarPadre(padre.Id, padre, usuariologueado); //Modifica el padre asignandole los hijos

                GuardarPadre(ListaPadres);
            }
            return resultado;
        }

        public Hijo[] ObtenerPersonas(UsuarioLogueado usuarioLogueado)
        {
            CrearArchivos();
            LeerPadres();
            LeerHijos();
            var resultado = new Resultado();

            Hijo[] hijos = null;
            if (VerificarUsuarioLogeado(Roles.Directora, usuarioLogueado).EsValido)
            {
                hijos = ListaHijos.ToArray();
            }
            if (VerificarUsuarioLogeado(Roles.Padre, usuarioLogueado).EsValido)
            {
               var  padre = ListaPadres.Single(x => x.Email == usuarioLogueado.Email && x.Apellido == usuarioLogueado.Apellido);

                if (padre.Hijos == null)
                    resultado.Errores.Add("no hay hijos asignados");
                hijos = padre.Hijos;
            }
            if (VerificarUsuarioLogeado(Roles.Docente, usuarioLogueado).EsValido)
            {
                var docente = ListaDocentes.Single(x => x.Email == usuarioLogueado.Email & x.Apellido == usuarioLogueado.Apellido);

                foreach (var sala in docente.Salas)
                {
                    var hijo = ListaHijos.Where(x => x.Sala.Id == sala.Id).ToList();

                    var listaconagregado = hijos == null ? new List<Hijo>() : hijos.ToList();

                    if (hijo !=null)
                        listaconagregado.AddRange(hijo);

                    hijos = listaconagregado.ToArray();
                }
            }

            return hijos;
        }

        /// <summary>
        /// FIN HIJOS
        /// </summary>

        /// <summary>
        /// INICIO NOTAS
        /// </summary>

        private void LeerNotas()
        {
            using (StreamReader reader = new StreamReader(PathNotas))
            {
                CrearArchivos();
                string contenido = reader.ReadToEnd();
                ListaNotas = JsonConvert.DeserializeObject<List<Nota>>(contenido);

                if (ListaNotas == null)
                {
                    ListaNotas = new List<Nota>();
                }
            }
        }

        private void GuardarNotas(List<Nota> listaNotas)
        {
            CrearArchivos();
            using (StreamWriter writer = new StreamWriter(PathNotas))
            {
                string jsonNotas = JsonConvert.SerializeObject(listaNotas);
                writer.Write(jsonNotas);
            }

        }

        //HACIENDO PRIMERO LO DE SALAS SE EVITA DUPLICIDAD DE CODIGO
        public Resultado AltaNota(Nota nota, Sala[] salas, Hijo[] hijos, UsuarioLogueado usuariologueado)
        {
            CrearArchivos();

            var resultado = new Resultado();

            if (hijos == null)
            {
                foreach (var sala in salas)
                {
                    LeerHijos();
                    foreach (var buscador in ListaHijos)
                    {

                        if (buscador.Sala.Id == sala.Id)
                        {
                            LeerNotas();
                            var notasxhijo = buscador.Notas == null ? new List<Nota>() : buscador.Notas.ToList();

                            if (notasxhijo.Any(x => x.Id == nota.Id))
                                resultado.Errores.Add("la nota esta agregada");
                            else
                                nota.Id = ListaNotas.Count() + 1;
                                ListaNotas.Add(nota);
                                notasxhijo.Add(nota);

                            buscador.Notas = notasxhijo.ToArray();

                            GuardarNotas(ListaNotas);
                        }
                        
                    }
                    GuardarHijos(ListaHijos);
                }
            }//si selecciona salas
            else
                resultado.Errores.Add("no se seleccionaron salas");

            if (salas != null)
            {
                
                foreach (var hijo in hijos)
                {
                    LeerHijos();
                    foreach (var buscador in ListaHijos)
                    {
                        
                        if (hijo.Id == buscador.Id)
                        {
                            LeerNotas();
                            var notasxhijo = buscador.Notas == null ? new List<Nota>() : buscador.Notas.ToList();

                            if (notasxhijo.Any(x => x.Id == nota.Id))
                                resultado.Errores.Add("la nota esta agregada");
                            else
                                nota.Id = ListaNotas.Count() + 1;
                                ListaNotas.Add(nota);
                                notasxhijo.Add(nota);

                            buscador.Notas = notasxhijo.ToArray();
                            GuardarNotas(ListaNotas);
                        }
                    }
                    GuardarHijos(ListaHijos);
                }
               
            }//si selecciona hijos
            else
                resultado.Errores.Add("no se seleccionaron ningun hijo");

            return resultado;
        }

        public Resultado MarcarNotaComoLeida(Nota nota, UsuarioLogueado usuarioLogueado)
        {
            CrearArchivos();
            LeerNotas();
            //FALTA VALIDAR QUE EL USUARIO PUEDA LLEGAR A LA NOTA
            Resultado resultado = VerificarUsuarioLogeado(Roles.Padre, usuarioLogueado);

            if (resultado.EsValido)
            {
               ListaNotas.Find(x => x.Id == nota.Id).Leida = true;

            }

            GuardarNotas(ListaNotas);

            return resultado;
        }

        public Nota[] ObtenerCuadernoComunicaciones(int idPersona, UsuarioLogueado usuariologueado)
        {
            CrearArchivos();
            LeerNotas();
            LeerHijos();
            LeerPadres();

            List<Nota> notas=new List<Nota>();


            if (VerificarUsuarioLogeado(Roles.Directora,usuariologueado).EsValido | VerificarUsuarioLogeado(Roles.Padre, usuariologueado).EsValido | VerificarUsuarioLogeado(Roles.Docente, usuariologueado).EsValido)
            {
                //FALTA VALIDAR QUE SEA HIJO DEL PADRE, O QUE ESTE EN ALGUNA SALA DEL DOCENTE
                var alumno = ListaHijos.Single(x => x.Id == idPersona);

                if (alumno.Notas!=null)
                {
                    foreach (var nota in alumno.Notas)
                    {
                        var notaenlista = ListaNotas.SingleOrDefault(x => x.Id == nota.Id);
                        notas.Add(notaenlista);
                    }
                    
                }
                
            }

            GuardarHijos(ListaHijos);
            GuardarPadre(ListaPadres);
            GuardarNotas(ListaNotas);
            return notas.ToArray();
        }

        public Resultado ResponderNota(Nota nota, Comentario nuevoComentario, UsuarioLogueado usuarioLogueado)
        {
            CrearArchivos();
            LeerNotas();
            var resultado = new Resultado();
            //FALTA VALIDAR QUE EL USUARIO LOGUEADO PUEDA ACCEDER A LA NOTA
            if (resultado.EsValido)
            {
                var notacomentada = ListaNotas.Single(x => x.Id == nota.Id);
                ListaNotas.Remove(notacomentada);

                var comentarios = notacomentada.Comentarios == null ? new List<Comentario>() : notacomentada.Comentarios.ToList();

                if (nuevoComentario.Mensaje != "")
                    comentarios.Add(nuevoComentario);
                else
                    resultado.Errores.Add("no se escrbribio ningun comentario");

                notacomentada.Comentarios = comentarios.ToArray();

                ListaNotas.Add(notacomentada);
            }

            GuardarNotas(ListaNotas);

            return resultado;                       
        }

        /// <summary>
        /// FIN NOTAS
        /// </summary>

    }
}
