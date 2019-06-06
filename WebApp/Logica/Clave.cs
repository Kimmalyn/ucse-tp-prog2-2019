using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contratos;

namespace Logica
{
    public  class Clave
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Roles Rol { get; set; }
    }
}
