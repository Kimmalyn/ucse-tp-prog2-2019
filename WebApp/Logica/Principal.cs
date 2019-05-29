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
        
        public Resultado AltaDirectora(Directora directora, UsuarioLogueado usuarioLogueado)
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

            throw new NotImplementedException();
        }

    }
}
