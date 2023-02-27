using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clutch.Reports
{
    public class TrabajadoresWr
    {
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public int mes { get; set; }
        public double total_horas_trabajadas { get; set; }

        public int total_pedidos { get; set; }
        public double sueldo { get; set; }
    }
}
