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

        public static Principal Instacia
        {
            get
            {
                return Instacia_Principal;
            }
        }

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
        private void LeerCLaves()
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
            using (StreamWriter writer = new StreamWriter(@"C:\Datos\Clave.txt", false))
            {
                string jsonClaves = JsonConvert.SerializeObject(listaclaves);
                writer.Write(jsonClaves);
            }

        }

        public UsuarioLogueado ObtenerUsuario(string email, string clave)//funciona :'D
        {
            CrearArchivos();
            LeerDirectoras();
            LeerCLaves();
            List<Usuario> ListaUsuarios = new List<Usuario>();
            ListaUsuarios.AddRange(ListaDirectoras);
            //ListaUsuarios.AddRange(ListaDocentes);
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
            GuardarClaves(ListaClaves);

            return usuariologueado;
        }

        //verfica que el rol usado sea el correcto
        private Resultado VerificarUsuarioLogeado(Roles rol, UsuarioLogueado usuariologeado)
        {
            var Resultado = new Resultado();
            if (usuariologeado.RolSeleccionado != rol)
            {
                Resultado.Errores.Add("el rol seleccionado no es el correcto");
            }

            return Resultado;
        }

        // amb's de directoras
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

            if (VerificarUsuarioLogeado(Roles.Directora, usuariologueado).EsValido)
            {
                directora.Id = ListaDirectoras.Count() + 1;
                Clave pass = new Clave() { Email = directora.Email, Password = "", Rol = Roles.Directora };
                ListaClaves.Add(pass);
                ListaDirectoras.Add(directora);
                GuardarDirectora(ListaDirectoras);
            }

            return VerificarUsuarioLogeado(Roles.Directora, usuariologueado);
        }

        public Resultado EditarDirectora(int id, Directora directoraeditada, UsuarioLogueado usuariologueado)
        {
            CrearArchivos();
            LeerDirectoras();

            if (VerificarUsuarioLogeado(Roles.Directora, usuariologueado).EsValido)
            {
                //var directora = ListaDirectoras.Where(x => x.Id == id).FirstOrDefault();
                ListaDirectoras.Remove(ObtenerDirectoraPorId(usuariologueado, id));
                ListaDirectoras.Add(directoraeditada);

                GuardarDirectora(ListaDirectoras);
            }

            return VerificarUsuarioLogeado(Roles.Directora, usuariologueado);
        }

        public Resultado EliminarDirectora(int id, Directora directoraeliminada, UsuarioLogueado usuarioLogueado)
        {
            CrearArchivos();
            LeerDirectoras();
            if (VerificarUsuarioLogeado(Roles.Directora, usuarioLogueado).EsValido)
            {
                //var directora = ListaDirectoras.Where(x => x.Id == id).FirstOrDefault();
                ListaDirectoras.Remove(ObtenerDirectoraPorId(usuarioLogueado, id));

                GuardarDirectora(ListaDirectoras);
            }
            return VerificarUsuarioLogeado(Roles.Directora, usuarioLogueado);
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
    }     
}
