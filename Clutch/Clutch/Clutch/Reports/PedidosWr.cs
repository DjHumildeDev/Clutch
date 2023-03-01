using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clutch.Reports
{
    public class PedidosWr
    {
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string dni { get; set; }
        public Nullable<Int32> mes { get; set; }
        public Nullable<Double> precio { get; set; }
        public Int32 pedido { get; set; }
        public string direccion { get; set; }

        public string mesString { get; set; }
    }
}

