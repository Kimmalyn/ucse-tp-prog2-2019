using System;

namespace Contratos
{
    public class Hijo : Usuario
    {
        public Institucion Institucion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int ResultadoUltimaEvaluacionAnual { get; set; }
        public Sala Sala { get; set; }
    }
}