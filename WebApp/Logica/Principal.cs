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
        /// <summary>
        /// Singleton
        /// </summary>
        private Principal() { }

        private static readonly Principal Instacia_Principal = new Principal();

        public static Principal Instacia
        {
            get
            {
               return Instacia_Principal;
            }
        }
        /// <summary>
        /// Propiedades
        /// </summary>
        public List<Directora> ListaDirectoras { get; set; }
        public List<Docente> ListaDocentes { get; set; }
        public List<Padre> ListaPadres { get; set; }
        public List<Hijo> ListaHijos { get; set; }
        /// <summary>
        /// Metodos
        /// </summary>
        public void CrearArchivos()
        {
            if (!File.Exists(@"C:\Datos\Directoras.txt"))
            {
                File.Create(@"C:\Datos\Directoras.txt").Close();
            }

            if (!File.Exists(@"C:\Datos\Maestras.txt"))
            {
                File.Create(@"C:\Datos\Maestras.txt").Close();
            }

            if (!File.Exists(@"C:\Datos\Padres.txt"))
            {
                File.Create(@"C:\Datos\Padres.txt").Close();
            }

            if (!File.Exists(@"C:\Datos\Alumnos.txt"))
            {
                File.Create(@"C:\Datos\Alumnos.txt").Close();
            }
        }

        public void ObtenerUsuarioLogeado(Usuario usuario, UsuarioLogueado usuariologeado)
        {
            usuariologeado.Nombre = usuario.Nombre;
            usuariologeado.Apellido = usuario.Apellido;
            usuariologeado.Email = usuario.Email;
            //ver que onda el tema del rol
        }
 
        public void GuardarDirectora(Directora directora, UsuarioLogueado usuariologueado)
        {
            var listadirectora = new List<Directora>();
            listadirectora.Add(directora);

            CrearArchivos();

            ObtenerUsuarioLogeado(directora,usuariologueado);

            using (StreamWriter writer = new StreamWriter(@"C:\Datos\Directoras.txt", false))
            {
                string jsonDirectoras = JsonConvert.SerializeObject(listadirectora);
                writer.Write(jsonDirectoras);
            }
        }

        public void LeerDirectora()
        {
            using (StreamReader reader = new StreamReader(@"C:\Datos\Directoras.txt"))
            {
                string contenido = reader.ReadToEnd();
                ListaDirectoras = JsonConvert.DeserializeObject<List<Directora>>(contenido);

                if (ListaDirectoras == null)
                {
                    ListaDirectoras = new List<Directora>();
                }
            }
        }

    }
}
