using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clutch.Reports
{
    /// <summary>
    /// Clase generador de informes
    /// </summary>
    public class Generador
    {
        public Negocio negocio;

        /// <summary>
        /// Funcion para generar el informe de incidencias
        /// </summary>
        public void GenerarInformeIncidencias()
        {
            this.negocio = new Negocio();
            Views.ReportViewer visor = new Views.ReportViewer();
            visor.rptViewer.LocalReport.ReportEmbeddedResource = "Clutch.Reports.rptIncidencia.rdlc";

            string consulta = "select i.incidencia,i.fecha,i.resuelta,i.tipo, e.nombre as NombreEmpleado,i.idEmpleado from incidencias as i inner join Empleados as e on i.idEmpleado = e.id Order by e.nombre, i.fecha";

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
        /// <summary>
        /// Funcion para generar un informe con un grafico sobre los Empleados
        /// </summary>
        public void GenerarInformeTrabajadoresGrafico()
        {
            CultureInfo culture = new CultureInfo("es-ES");
            this.negocio = new Negocio();
            Views.ReportViewer visor = new Views.ReportViewer();
            visor.rptViewer.LocalReport.ReportEmbeddedResource = "Clutch.Reports.rptTrabajadoresGrafico.rdlc";

            string consulta = "SELECT e.id, e.nombre,e.apellidos,MONTH(j.Entrada) AS mes,SUM(j.horas) AS total_horas_trabajadas," +
                " COUNT(p.id) AS total_pedidos,(SUM(j.horas)*j.sueldo) AS sueldo FROM empleados e LEFT JOIN Jornada j ON e.id = " +
                "j.idEmpleado LEFT JOIN pedido p ON e.id = p.idRepartidor AND MONTH(j.Entrada) = MONTH(j.Entrada) GROUP BY e.id, e.nombre," +
                " MONTH(j.Entrada), j.sueldo,e.apellidos ORDER BY e.id, MONTH(j.Entrada),e.apellidos";

            ClutchDb ctx = new ClutchDb();
            List<TrabajadoresWr> listaTrabajadores = ctx.Database.SqlQuery<TrabajadoresWr>(consulta, new object[0]).ToList();
            foreach (TrabajadoresWr trabajador in listaTrabajadores)
            {
                if (trabajador.mes != null)
                {
                    trabajador.mesString = culture.DateTimeFormat.GetMonthName(trabajador.mes.Value).ToUpper();
                }
            }

            ReportDataSource fuenteDatos = new ReportDataSource("DataSetTrabajadores", listaTrabajadores);

            visor.rptViewer.LocalReport.DataSources.Add(fuenteDatos);

            visor.rptViewer.SetDisplayMode(DisplayMode.PrintLayout);
            visor.rptViewer.RefreshReport();


            visor.Show();

        }

        /// <summary>
        /// Funcion para generar un informe con una tabla sobre los Empleados
        /// </summary>
        public void GenerarInformeTrabajadores()
        {
            CultureInfo culture = new CultureInfo("es-ES");
            this.negocio = new Negocio();
            Views.ReportViewer visor = new Views.ReportViewer();
            visor.rptViewer.LocalReport.ReportEmbeddedResource = "Clutch.Reports.rptTrabajadores.rdlc";

            string consulta = "SELECT e.id, e.nombre,e.apellidos,MONTH(j.Entrada) AS mes,SUM(j.horas) AS total_horas_trabajadas," +
                " COUNT(p.id) AS total_pedidos,(SUM(j.horas)*j.sueldo) AS sueldo FROM empleados e LEFT JOIN Jornada j ON e.id = " +
                "j.idEmpleado LEFT JOIN pedido p ON e.id = p.idRepartidor AND MONTH(j.Entrada) = MONTH(j.Entrada) GROUP BY e.id, e.nombre," +
                " MONTH(j.Entrada), j.sueldo,e.apellidos ORDER BY e.id, MONTH(j.Entrada),e.apellidos";

            ClutchDb ctx = new ClutchDb();
            List<TrabajadoresWr> listaTrabajadores = ctx.Database.SqlQuery<TrabajadoresWr>(consulta, new object[0]).ToList();
            foreach (TrabajadoresWr trabajador in listaTrabajadores)
            {
                if (trabajador.mes != null)
                {
                    trabajador.mesString = culture.DateTimeFormat.GetMonthName(trabajador.mes.Value).ToUpper();
                }
            }

            ReportDataSource fuenteDatos = new ReportDataSource("DataSetTrabajadores", listaTrabajadores);

            visor.rptViewer.LocalReport.DataSources.Add(fuenteDatos);

            visor.rptViewer.SetDisplayMode(DisplayMode.PrintLayout);
            visor.rptViewer.RefreshReport();


            visor.Show();

        }
        /// <summary>
        /// Funcion para generar un informe sobre los pedidos+
        /// </summary>
        public void GenerarInformePedidos()
        {
            CultureInfo culture = new CultureInfo("es-ES");
            this.negocio = new Negocio();
            Views.ReportViewer visor = new Views.ReportViewer();
            visor.rptViewer.LocalReport.ReportEmbeddedResource = "Clutch.Reports.rptPedidos.rdlc";

            string consulta = "SELECT e.id, e.nombre, e.apellidos, e.dni, MONTH(p.Fecha) AS mes,p.id AS pedido,p.precio,p.pedido,p.direccion" +
                " FROM empleados e JOIN repartidor r ON e.id = r.idEmpleado JOIN pedido p ON r.id = p.idRepartidor " +
                "ORDER BY e.id, mes";

            ClutchDb ctx = new ClutchDb();
            List<PedidosWr> listaPedidos = ctx.Database.SqlQuery<PedidosWr>(consulta, new object[0]).ToList();
            foreach (PedidosWr pedido in listaPedidos)
            {
                if (pedido.mes != 0)
                {
                    pedido.mesString = culture.DateTimeFormat.GetMonthName(pedido.mes.Value).ToUpper();
                }
            }

            ReportDataSource fuenteDatos = new ReportDataSource("DataSetPedidos", listaPedidos);

            visor.rptViewer.LocalReport.DataSources.Add(fuenteDatos);

            visor.rptViewer.SetDisplayMode(DisplayMode.PrintLayout);
            visor.rptViewer.RefreshReport();


            visor.Show();

        }



    }
}
