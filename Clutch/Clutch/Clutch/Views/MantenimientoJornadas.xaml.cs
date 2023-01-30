using Clutch.Enums;
using Clutch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Clutch.Views
{
    /// <summary>
    /// Lógica de interacción para MantenimientoJornadas.xaml
    /// </summary>
    public partial class MantenimientoJornadas : Window
    {
        private Negocio negocio;
        private GridViewColumnHeader columnaOrdenada;
        private ListSortDirection sentidoOrden;

        private List<Empleado> empleados = new List<Empleado>();

        public MantenimientoJornadas(Negocio negocio)
        {
            InitializeComponent();
            this.negocio = negocio;
            empleados = negocio.ObtenerEmpleados();
            ActualizarLista();
            RellenarCombo();
        }

        private void RellenarCombo()
        {
            cmBxEmpleado.Items.Add("Seleccionar Empleado");
            foreach (Empleado emp in empleados)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = emp.nombre + " " + emp.apellidos;
                item.Tag = emp;
                cmBxEmpleado.Items.Add(item);
            }
        }

        private void ActualizarLista()
        {
            lvJornadas.Items.Clear();
            List<Jornada> jornadas = negocio.ObtenerJornadas();
            foreach(Jornada jornada in jornadas)
            {
                int idEmpleado =jornada.idEmpleado;
                Empleado empleado = negocio.ObtenerEmpleado(idEmpleado);
                string nombre = empleado.nombre;
                string apellidos = empleado.apellidos;

                string nombreCompleto = nombre + " " + apellidos;
                string rol = "sin rol";

                Encargado esEncargado = negocio.ObtenerEncargado(idEmpleado);
                Cocina esCocinero = negocio.ObtenerCocinero(idEmpleado);
                Repartidor esRepartidor = negocio.ObtenerRepartidor(idEmpleado);

                if(esEncargado != null)
                {
                    rol = RolEnum.Encargado.ToString();
                }
                if (esCocinero != null)
                {
                    rol = RolEnum.Cocina.ToString();
                }
                if (esRepartidor != null)
                {
                    rol = RolEnum.Repartidor.ToString();
                }
                JornadaListModel jornadaItem = new JornadaListModel { Rol = rol, NombreCompleto = nombreCompleto, Entrada = jornada.Entrada, Salida = jornada.Salida, pedidos = jornada.pedidos, sueldo = jornada.sueldo, horas = jornada.horas, sueldoHoy = jornada.sueldoHoy };
                ListViewItem item = new ListViewItem();

                item.Content = jornadaItem;
                item.Tag = jornada;
                lvJornadas.Items.Add(item);
            }
        }


        private void mnJornadasCrear_Click(object sender, RoutedEventArgs e)
        {
            //Proceso de identificacion
            Empleado empleado = new Empleado();
            Jornada jornada = new Jornada();
            VerJornada ver = new VerJornada(jornada,empleado,empleados);
            ver.Owner = this;
            if (ver.ShowDialog() == true)
            {
                negocio.CrearJornada(empleado, jornada);
                ActualizarLista();
            }
        }

        private void mnJornadasEditar_Click(object sender, RoutedEventArgs e)
        {
            if (lvJornadas.SelectedItems.Count == 1)
            {
                //Proceso de identificacion
                Jornada jornada = (Jornada)((ListViewItem)lvJornadas.SelectedItems[0]).Tag;
                Empleado empleado = negocio.ObtenerEmpleado(jornada.idEmpleado);
                VerJornada ver = new VerJornada(jornada, empleado, empleados);
                ver.Owner = this;
                if (ver.ShowDialog() == true)
                {
                    negocio.EditarEmpleado(empleado);
                    ActualizarLista();
                }
            }
            else
            {
                MessageBox.Show("Necesitas seleccionar un elemento", "No hay seleccion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void mnJornadasBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (lvJornadas.SelectedItems.Count == 1)
            {
                //Proceso de identificacion

                MessageBoxResult result = MessageBox.Show("¿Esta seguro que quiere borrar esta  Jornada?", "¡Atencion!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    Jornada borrar = (Jornada)((ListViewItem)lvJornadas.SelectedItems[0]).Tag;

                    if (negocio.BorrarJornada(borrar.id))
                    {
                        MessageBox.Show("Se ha borrado la jornada correctamente", "Jornada Borrada", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar la jornada", "Algo salio mal", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    ActualizarLista();
                    
                }
            }
            else
            {
                MessageBox.Show("Necesitas seleccionar un elemento", "No hay seleccion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void mnJornadasSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            cmBxEmpleado.SelectedIndex = 0;
            ActualizarLista();
        }
        private void lvJornadas_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            ctxBorrar.IsEnabled = false;
            ctxEditar.IsEnabled = false;
            if (lvJornadas.SelectedItems.Count == 1)
            {
                ctxEditar.IsEnabled = true;
                ctxBorrar.IsEnabled = true;
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader columnaClick = (sender as GridViewColumnHeader);
            string ordenadoPor = columnaClick.Tag.ToString();

            if (columnaOrdenada == null)
            {
                columnaOrdenada = columnaClick;
                sentidoOrden = ListSortDirection.Descending;
            }

            this.lvJornadas.Items.SortDescriptions.Clear();

            if (columnaOrdenada == columnaClick)
            {
                if (sentidoOrden == ListSortDirection.Ascending)
                {
                    sentidoOrden = ListSortDirection.Descending;
                }
                else
                {
                    sentidoOrden = ListSortDirection.Ascending;
                }
                lvJornadas.Items.SortDescriptions.Add(new SortDescription(ordenadoPor, sentidoOrden));
            }
            else
            {
                columnaOrdenada = null;
            }
        }
    }
}
