﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clutch.Reports
{
    public class IncidenciaWr
    {
        public string Empleado { get; set; }
        public string Tipo { get; set; }
        public string incidencia { get; set; }
        public DateTime Fecha { get; set; }

        public bool resuelta { get; set; }
    }
}
