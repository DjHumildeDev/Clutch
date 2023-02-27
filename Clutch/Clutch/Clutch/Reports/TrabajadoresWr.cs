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
        public Nullable<Int32> mes { get; set; }
        public Nullable<Double> total_horas_trabajadas { get; set; }

        public Nullable<Int32> total_pedidos { get; set; }
        public Nullable<Double> sueldo { get; set; }
        public string mesString { get; set; }
    }
}
