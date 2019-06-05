using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Contratos;

namespace WebApp.Models
{
    public class SalaSelectedViewModel
    {
        public bool Selected { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class SalaViewModel
    {
        public Docente Usuario { get; set; }
        public SalaSelectedViewModel[] Salas { get; set; }
    }
}