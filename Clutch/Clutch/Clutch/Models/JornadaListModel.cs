using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clutch.Models
{
    class JornadaListModel
    {
        public string rol { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime Entrada { get; set; }
        public Nullable<System.DateTime> Salida { get; set; }
        public Nullable<int> pedidos { get; set; }
        public double sueldo { get; set; }
        public double horas { get; set; }
        public double sueldoHoy { get; set; }
    }
}
