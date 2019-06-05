using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Contratos;

namespace WebApp.Models
{
    public class HijoSelectedViewModel
    {
        public bool Selected { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }

    public class HijoViewModel
    {
        public Padre Usuario { get; set; }
        public HijoSelectedViewModel[] Hijos { get; set; }
    }
}