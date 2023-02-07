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
                item.Tag = jornadaItem;
                lvJornadas.Items.Add(item);
            }
        }
        private bool Identificacion()
        {
            Empleado empleado = new Empleado();
            Identificacion identificacion = new Identificacion(negocio, false, empleado, true);
            if (identificacion.ShowDialog() == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void mnJornadasCrear_Click(object sender, RoutedEventArgs e)
        {
            //Proceso de identificacion
            if (Identificacion())
            {
                Empleado empleado = new Empleado();
                Jornada jornada = new Jornada();
                VerJornada ver = new VerJornada(jornada, empleado, empleados);
                ver.Owner = this;
                if (ver.ShowDialog() == true)
                {
                    empleado = ver.empleadoSeleccionado;
                    jornada.idEmpleado = ver.empleadoSeleccionado.id;
                    negocio.CrearJornada(empleado, jornada);
                    ActualizarLista();
                }
            }
          
        }

        private void mnJornadasEditar_Click(object sender, RoutedEventArgs e)
        {
            if (lvJornadas.SelectedItems.Count == 1)
            {
                if (Identificacion())
                {
                    //Proceso de identificacion
                    Jornada jornada = (Jornada)((ListViewItem)lvJornadas.SelectedItems[0]).Tag;
                    Empleado empleado = negocio.ObtenerEmpleado(jornada.idEmpleado);
                    VerJornada ver = new VerJornada(jornada, empleado, empleados);
                    ver.Owner = this;
                    if (ver.ShowDialog() == true)
                    {
                        empleado = ver.empleadoSeleccionado;
                        jornada.idEmpleado = ver.empleadoSeleccionado.id;
                        negocio.EditarJornada(jornada);
                        ActualizarLista();
                    }
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
                if (Identificacion())
                {
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
            txtBxEmpleado.Text = String.Empty;
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

        private void txtBxEmpleado_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionView vista = (CollectionView)CollectionViewSource.GetDefaultView(lvJornadas.Items);
            vista.Filter = NombreFilter;
            vista.Refresh();
        }

        private bool NombreFilter(object item)
        {
            ListViewItem jornada = (ListViewItem)item;
            if (String.IsNullOrEmpty(txtBxEmpleado.Text))
            {
                return true;
            }
            else
            {
                return ((jornada.Tag as JornadaListModel).NombreCompleto.IndexOf(txtBxEmpleado.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private void dtPckSalida_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionView vista = (CollectionView)CollectionViewSource.GetDefaultView(lvJornadas.Items);
            vista.Filter = SalidaFilter;
            vista.Refresh();
        }

        private bool SalidaFilter(object item)
        {
            ListViewItem jornada = (ListViewItem)item;
            if (dtPckSalida.SelectedDate == null)
            {
                return true;
            }
            else
            {
                return ((jornada.Tag as JornadaListModel).Salida.ToString().IndexOf(dtPckSalida.SelectedDate.Value.ToShortDateString(),StringComparison.OrdinalIgnoreCase)>=0);
            }
        }

        private void dtPckEntrada_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionView vista = (CollectionView)CollectionViewSource.GetDefaultView(lvJornadas.Items);
            vista.Filter = EntradaFilter;
            vista.Refresh();
        }

        private bool EntradaFilter(object item)
        {

            ListViewItem jornada = (ListViewItem)item;
            if (dtPckEntrada.SelectedDate == null)
            {
                return true;
            }
            else
            {
                return ((jornada.Tag as JornadaListModel).Entrada.ToShortDateString()).Equals(dtPckSalida.SelectedDate.Value.ToShortDateString());
            }
        }
    }
}
