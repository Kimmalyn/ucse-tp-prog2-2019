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
        ///
        /// <summary>
        /// Metodos
        /// </summary>
        /// 
        public UsuarioLogueado ObtenerUsuario(string email, string clave)
        {
            List<Usuario> ListaUsuarios = new List<Usuario>();
            ListaUsuarios.AddRange(ListaDirectoras);
            ListaUsuarios.AddRange(ListaDocentes);
            ListaUsuarios.AddRange(ListaPadres);
            ListaUsuarios.AddRange(ListaHijos);


        }
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
        // alta de directoras
        public void LeerDirectoras()
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
        public void GuardarDirectora(List<Directora> listadirectora)
        {
            CrearArchivos();
            using (StreamWriter writer = new StreamWriter(@"C:\Datos\Directoras.txt", false))
            {
                string jsonDirectoras = JsonConvert.SerializeObject(listadirectora);
                writer.Write(jsonDirectoras);
            }
        }
        public Resultado AltaDirectora(Directora directora, UsuarioLogueado usuariologueado)
        {
            CrearArchivos();
            LeerDirectoras();

            if (VerificarUsuarioLogeado(Roles.Directora, usuariologueado).EsValido)
            {
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
                var directora = ListaDirectoras.Where(x => x.Id == id).FirstOrDefault();
                ListaDirectoras.Remove(directora);
                ListaDirectoras.Add(directoraeditada);

                GuardarDirectora(ListaDirectoras);
            }

            return VerificarUsuarioLogeado(Roles.Directora, usuariologueado);
        }
        public Resultado EliminarDirectora(int id, Directora directoraeliminada, UsuarioLogueado usuarioLogueado)
        {
            CrearArchivos();
            LeerDirectoras();
            if (VerificarUsuarioLogeado(Roles.Directora,usuarioLogueado).EsValido)
            {
                var directora = ListaDirectoras.Where(x => x.Id == id).FirstOrDefault();
                ListaDirectoras.Remove(directora);

                GuardarDirectora(ListaDirectoras);
            }            
            return VerificarUsuarioLogeado(Roles.Directora, usuarioLogueado);
        }
    

    }
}
