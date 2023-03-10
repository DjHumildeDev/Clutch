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
           
            foreach (incidencia inci in incidencias)
            {
                Empleado emp = negocio.ObtenerEmpleado(inci.idEmpleado);
                IncidenciaListModel incidenciaItem = new IncidenciaListModel();
                incidenciaItem.Empleado = emp.nombre + " " + emp.apellidos;
                incidenciaItem.id = inci.id;
                incidenciaItem.fecha = inci.fecha;
                incidenciaItem.idEmpleado = inci.idEmpleado;
                incidenciaItem.incidencia1 = inci.incidencia1;
                incidenciaItem.tipo = inci.tipo;
                incidenciaItem.resuelta = inci.resuelta;

                ListViewItem item = new ListViewItem();
                item.Content = incidenciaItem;
                item.Tag = inci;
                lvIncidencias.Items.Add(item);
            }
        }


        private void dtPckFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionView vista = (CollectionView)CollectionViewSource.GetDefaultView(lvIncidencias.Items);
            vista.Filter = FechaFilter;
            vista.Refresh();
        }

        private bool FechaFilter(object item)
        {
            ListViewItem inci = (ListViewItem)item;
            if (dtPckFecha.SelectedDate == null)
            {
                return true;
            }
            else
            {
                return ((inci.Content as IncidenciaListModel).fecha.ToShortDateString().Equals(dtPckFecha.SelectedDate.Value.ToShortDateString()));
            }
        }

        private void chBxResuelta_Click(object sender, RoutedEventArgs e)
        {
            CollectionView vista = (CollectionView)CollectionViewSource.GetDefaultView(lvIncidencias.Items);
            vista.Filter = ResueltaFilter;
            vista.Refresh();
        }

        private bool ResueltaFilter(object item)
        {
            ListViewItem inci = (ListViewItem)item;                            
            return (inci.Content as IncidenciaListModel).resuelta.Equals(chBxResuelta.IsChecked);       
        }

        private void cmBxTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionView vista = (CollectionView)CollectionViewSource.GetDefaultView(lvIncidencias.Items);
            vista.Filter = TipoFilter;
            vista.Refresh();
        }

        private bool TipoFilter(object item)
        {
           ListViewItem inci = (ListViewItem)item;
           ComboBoxItem cmbItem = (ComboBoxItem)cmBxTipo.SelectedItem;
            if (String.IsNullOrEmpty(cmbItem.Tag.ToString()))
            {
                return true;
            }
            else
            {
                return (inci.Content as IncidenciaListModel).tipo.Equals(cmbItem.Tag);
            }
        }

        private void txtBxEmpleado_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionView vista = (CollectionView)CollectionViewSource.GetDefaultView(lvIncidencias.Items);
            vista.Filter = EmpleadoFilter;
            vista.Refresh();
        }

        private bool EmpleadoFilter(object item)
        {
            ListViewItem incidencia = (ListViewItem)item;
            if (String.IsNullOrEmpty(txtBxEmpleado.Text))
            {
                return true;
            }
            else
            {
                 return ((incidencia.Content as IncidenciaListModel).Empleado.IndexOf(txtBxEmpleado.Text, StringComparison.OrdinalIgnoreCase) >= 0);               
            }
        }

        private void mnIncidenciasBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (lvIncidencias.SelectedItems.Count == 1)
            {
                if (Identificacion())
                {
                    MessageBoxResult result = MessageBox.Show("¿Esta seguro que quiere borrar esta incidencia?", "¡Atencion!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        incidencia borrar = (incidencia)((ListViewItem)lvIncidencias.SelectedItems[0]).Tag;

                        if (negocio.BorrarIncidencia(borrar.id))
                        {
                            MessageBox.Show("Se ha borrado la incidencia correctamente", "Incidencia Borrada", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar la incidencia", "Algo salio mal", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void mnIncidenciassEditar_Click(object sender, RoutedEventArgs e)
        {
            if (lvIncidencias.SelectedItems.Count == 1)
            {
                if (Identificacion())
                {
                    incidencia ver = (incidencia)((ListViewItem)lvIncidencias.SelectedItems[0]).Tag;
                    Empleado empleado = negocio.ObtenerEmpleado(ver.idEmpleado);
                    VerIncidencia ventana = new VerIncidencia(ver,empleado);
                    ventana.Owner = this;
                    if (ventana.ShowDialog() == true)
                    {
                        negocio.EditarIncidencia(ver);
                        ActualizarLista();
                    }
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
            txtBxEmpleado.Text = String.Empty;
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

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                System.Diagnostics.Process.Start(@"Documentation.chm");
            }
        }
    }
}
