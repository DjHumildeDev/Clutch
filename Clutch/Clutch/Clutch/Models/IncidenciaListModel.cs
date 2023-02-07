using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clutch.Models
{
    public class IncidenciaListModel
    {
        public int id { get; set; }
        public int idEmpleado { get; set; }
        public string incidencia1 { get; set; }
        public string tipo { get; set; }
        public System.DateTime fecha { get; set; }
        public bool resuelta { get; set; }

        public string Empleado { get; set; }
    }
}
