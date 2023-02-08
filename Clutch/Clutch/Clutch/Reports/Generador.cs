using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clutch.Reports
{
    public class Generador
    {
        public Negocio negocio;
        public void GenerarInformeIncidencias()
        {
            this.negocio = new Negocio();
            Views.ReportViewer visor = new Views.ReportViewer();
            visor.rptViewer.LocalReport.ReportEmbeddedResource = "Clutch.Reports.rptIncidencia.rdlc";

            string consulta = "select i.incidencia,i.fecha,i.resuelta,i.tipo, e.nombre as NombreEmpleado,i.idEmpleado from incidencias as i inner join Empleados as e on i.idEmpleado = e.id";

            ClutchDb ctx = new ClutchDb();
            List<IncidenciaWr> listaIncidencias = ctx.Database.SqlQuery<IncidenciaWr>(consulta, new object[0]).ToList();
            foreach(IncidenciaWr incidencia in listaIncidencias)
            {               
                Empleado emp = negocio.ObtenerEmpleado(incidencia.idEmpleado);
                incidencia.Empleado = emp.nombre + " " + emp.apellidos;
                incidencia.fechaString = incidencia.Fecha.ToShortDateString();
            }

            ReportDataSource fuenteDatos = new ReportDataSource("DataSetIncidencias", listaIncidencias);

            visor.rptViewer.LocalReport.DataSources.Add(fuenteDatos);

            visor.rptViewer.SetDisplayMode(DisplayMode.PrintLayout);
            visor.rptViewer.RefreshReport();


            visor.Show();
        }
    }
}
