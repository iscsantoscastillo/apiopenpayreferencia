using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiOpenpayReferencia.Models
{
    public class DatoEntrada
    {
        public decimal Importe { get; set; }
        public string ClaveSolicitud { get; set; }
        public string Cliente { get; set; }
        public string ModeloEquipo { get; set; }
    }
}
