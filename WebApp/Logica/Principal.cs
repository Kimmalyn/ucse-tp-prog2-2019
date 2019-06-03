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
    class Principal
    {
        public List<Directora> ListaDirectoras { get; set; }
        
        public void CargarDirectora(Directora directora, UsuarioLogueado usuarioLogueado)
        {
            //id nombre apellido email institucion cargo fecha ingreso
            var listadirectora = new List<Directora>();

            if (!File.Exists(@"C:\Datos\Directoras.txt"))
            {
                File.Create(@"C:\Datos\Directoras.txt").Close();
            }

            using (StreamWriter writer = new StreamWriter(@"C:\Datos\Directoras.txt", false))
            {
                string jsonDirectoras = JsonConvert.SerializeObject(listadirectora);
                writer.Write(jsonDirectoras);
            }
        }

        public void LeerDiectora()
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

        public void 
    }
}
