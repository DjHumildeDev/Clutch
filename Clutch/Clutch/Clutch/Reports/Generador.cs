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
        public void GenerarInformeIncidencias()
        {
            Views.ReportViewer visor = new Views.ReportViewer();
            visor.rptViewer.LocalReport.ReportEmbeddedResource = "Clutch.Reports.rptIncidencia.rdlc";

            string consulta = "select i.incidencia,i.fecha,i.resuelta,i.tipo, e.nombre as NombreEmpleado from incidencias as i inner join Empleados as e on i.idEmpleado = e.id";

            ClutchDb ctx = new ClutchDb();
            List<IncidenciaWr> listaProductos = ctx.Database.SqlQuery<IncidenciaWr>(consulta, new object[0]).ToList();

            ReportDataSource fuenteDatos = new ReportDataSource("DataSetIncidencias", listaProductos);

            visor.rptViewer.LocalReport.DataSources.Add(fuenteDatos);

            visor.rptViewer.SetDisplayMode(DisplayMode.PrintLayout);
            visor.rptViewer.RefreshReport();


            visor.Show();
        }
    }
}
