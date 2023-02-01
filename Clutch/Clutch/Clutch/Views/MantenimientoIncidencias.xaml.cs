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
    /// Lógica de interacción para MantenimientoIncidencias.xaml
    /// </summary>
    public partial class MantenimientoIncidencias : Window
    {
        private Negocio negocio;
        public MantenimientoIncidencias(Negocio negocio)
        {
            InitializeComponent();
            this.negocio = negocio;
            ActualizarLista();
        }

        private void ActualizarLista()
        {
            lvIncidencias.Items.Clear();
            List<incidencia> incidencias = negocio.ObtenerIncidencias();
            foreach(incidencia inci in incidencias)
            {
                ListViewItem item = new ListViewItem();
                item.Content = inci;
                item.Tag = inci;
                lvIncidencias.Items.Add(item);              
            }
        }

        private void dtPckFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void chBxResuelta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmBxTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void cmBxEmpleado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void mnIncidenciasBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (lvIncidencias.SelectedItems.Count == 1)
            {
                if (Identificacion())
                {

                }
            }
            else
            {
                MessageBox.Show("Necesitas seleccionar un elemento", "No hay seleccion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void mnIncidenciassEditar_Click(object sender, RoutedEventArgs e)
        {
            if (lvIncidencias.SelectedItems.Count == 1)
            {
                if (Identificacion())
                {

                }
            }
            else
            {
                MessageBox.Show("Necesitas seleccionar un elemento", "No hay seleccion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void mnIncidenciasCrear_Click(object sender, RoutedEventArgs e)
        {
            //Accer como un login para sacar el empleado que crea la incidencia
            if (Identificacion())
            {
                incidencia nueva = new incidencia();
                Empleado empleado = new Empleado();
                Identificacion identificacion = new Identificacion(negocio, false, empleado, false);
                if (identificacion.ShowDialog() == true)
                {
                    empleado = identificacion.EmpleadoSeleccionado;
                    VerIncidencia ventana = new VerIncidencia(nueva, empleado);
                    ventana.Owner = this;
                    if (ventana.ShowDialog() == true)
                    {
                        negocio.CrearIncidencia(empleado, nueva);
                        ActualizarLista();
                    }
                }
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


        private void mnIncidenciasSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            ActualizarLista();
        }
        private void lvIncidencias_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            ctxBorrar.IsEnabled = false;
            ctxEditar.IsEnabled = false;
            if (lvIncidencias.SelectedItems.Count == 1)
            {
                ctxEditar.IsEnabled = true;
                ctxBorrar.IsEnabled = false;
            }
        }

        private GridViewColumnHeader columnaOrdenada;
        private ListSortDirection sentidoOrden;
        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader columnaClick = (sender as GridViewColumnHeader);
            string ordenadoPor = columnaClick.Tag.ToString();

            if (columnaOrdenada == null)
            {
                columnaOrdenada = columnaClick;
                sentidoOrden = ListSortDirection.Descending;
            }

            this.lvIncidencias.Items.SortDescriptions.Clear();

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
                lvIncidencias.Items.SortDescriptions.Add(new SortDescription(ordenadoPor, sentidoOrden));
            }
            else
            {
                columnaOrdenada = null;
            }
        }
    }
}
