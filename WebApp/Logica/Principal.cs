using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contratos;
using Newtonsoft.Json;

namespace Logica
{
    public class Principal
    {
        ///
        /// <summary>
        /// Singleton
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
        ///
        ///
        /// <summary>
        /// Propiedades
        /// </summary>
        /// 
        public List<Directora> ListaDirectoras { get; set; }
        public List<Docente> ListaDocentes { get; set; }
        public List<Padre> ListaPadres { get; set; }
        public List<Hijo> ListaHijos { get; set; }
        public List<Clave> ListaClaves { get; set; }
        //public List<Usuario> ListaUsuarios { get; set; }

        ///
        /// <summary>
        /// Metodos
        /// </summary>
        ///

        //CreaTodosArchivos
        private void CrearArchivos()
        {
            if (!File.Exists(@"C:\Datos\Directoras.txt"))
            {
                File.Create(@"C:\Datos\Directoras.txt").Close();
            }
            if (ListaDirectoras == null)
            {
                List<Directora> ListaDirectoras = new List<Directora>();
            }

            if (!File.Exists(@"C:\Datos\Docentes.txt"))
            {
                File.Create(@"C:\Datos\Docentes.txt").Close();
            }
            if (ListaDocentes == null)
            {
                List<Docente> ListaDocentes = new List<Docente>();
            }

            if (!File.Exists(@"C:\Datos\Padres.txt"))
            {
                File.Create(@"C:\Datos\Padres.txt").Close();
            }
            if (ListaPadres == null)
            {
                List<Padre> ListaPadres = new List<Padre>();
            }

            if (!File.Exists(@"C:\Datos\Alumnos.txt"))
            {
                File.Create(@"C:\Datos\Alumnos.txt").Close();
            }
            if (ListaHijos == null)
            {
                List<Hijo> ListaHijos = new List<Hijo>();
            }

            if (!File.Exists(@"C:\Datos\Claves.txt"))
            {
                File.Create(@"C:\Datos\Claves.txt").Close();
            }
            if (ListaClaves == null)
            {
                List<Clave> ListaClaves = new List<Clave>();
            }

        }

        //Login
        private void LeerClaves()
        {
            using (StreamReader reader = new StreamReader(@"C:\Datos\Claves.txt"))
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
            using (StreamWriter writer = new StreamWriter(@"C:\Datos\Claves.txt", false))
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
            //ListaUsuarios.AddRange(ListaPadres);

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
            using (StreamReader reader = new StreamReader(@"C:\Datos\Directoras.txt"))
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
            using (StreamWriter writer = new StreamWriter(@"C:\Datos\Directoras.txt", false))
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
                Lista = listagrilla
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
            using (StreamReader reader = new StreamReader(@"C:\Datos\Docentes.txt"))
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
            using (StreamWriter writer = new StreamWriter(@"C:\Datos\Docentes.txt", false))
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
                Lista = listagrilla
            };
        }

        /// <summary>
        /// FIN DOCENTE
        /// </summary>
         
        /// <summary>
        /// INICIO SALAS
        /// </summary>

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
            }
            return resultado;
        }

        public Sala[] ObtenerSalasPorInstitucion(UsuarioLogueado usuarioLogueado)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// FIN SALAS
        /// </summary>

        /// <summary>
        /// INICIO PADRES
        /// </summary>

        private void LeerPadres()
        {
            using (StreamReader reader = new StreamReader(@"C:\Datos\Padres.txt"))
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
            using (StreamWriter writer = new StreamWriter(@"C:\Datos\Padres.txt", false))
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
                Lista = listagrilla
            };
        }

        /// <summary>
        /// FIN PADRES
        /// </summary>
    }
}
