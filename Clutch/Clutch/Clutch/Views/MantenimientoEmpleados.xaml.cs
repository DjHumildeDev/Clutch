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
    /// Lógica de interacción para MantenimientoEmpleados.xaml
    /// </summary>
    public partial class MantenimientoEmpleados : Window
    {
        private Negocio negocio;
        private GridViewColumnHeader columnaOrdenada = null;
        private ListSortDirection sentidoOrden;
        public MantenimientoEmpleados(Negocio negocio)
        {
            InitializeComponent();
            this.negocio = negocio;
            ActualizarLista();
        }

        private void mnEmpleadosSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void mnEmpleadosCrear_Click(object sender, RoutedEventArgs e)
        {
            if (Identificacion())
            {
                Empleado nuevo = new Empleado();
                VerEmpleado ventana = new VerEmpleado(nuevo, negocio);
                ventana.Owner = this;
                if (ventana.ShowDialog() == true)
                {
                    ActualizarLista();
                }
            } 
        }

        private void ActualizarLista()
        {
            lvEmpleados.Items.Clear();
            List<Empleado> empleados = negocio.ObtenerEmpleados();
            foreach (Empleado x in empleados)
            {
                ListViewItem item = new ListViewItem();
                item.Content = x;
                item.Tag = x;
                lvEmpleados.Items.Add(item);
            }
        }

        private void mnEmpleadosEditar_Click(object sender, RoutedEventArgs e)
        {
            if (lvEmpleados.SelectedItems.Count == 1){
                if (Identificacion())
                {
                    Empleado ver = (Empleado)((ListViewItem)lvEmpleados.SelectedItems[0]).Tag;
                    VerEmpleado ventana = new VerEmpleado(ver, negocio);
                    ventana.Owner = this;
                    if (ventana.ShowDialog() == true)
                    {
                        negocio.EditarEmpleado(ver);
                        ActualizarLista();
                    }
                }
            }
            else
            {
                MessageBox.Show("Necesitas seleccionar un elemento", "No hay seleccion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void mnEmpleadosBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (lvEmpleados.SelectedItems.Count == 1)
            {
                //Proceso de identificacion
                if (Identificacion())
                {
                    MessageBoxResult result = MessageBox.Show("¿Esta seguro que quiere borrar este empleado?", "¡Atencion!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        Empleado borrar = (Empleado)((ListViewItem)lvEmpleados.SelectedItems[0]).Tag;

                        if (negocio.BorrarEmpleado(borrar.id))
                        {
                            MessageBox.Show("Se ha borrado el empleado correctamente", "Empleado Borrado", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar el empleado", "Algo salio mal", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void cmBxNombre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmBxApellidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lvJornadas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dtPckAlta_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            ActualizarLista();
        }

        private void lvEmpleados_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            ctxBorrar.IsEnabled = false;
            ctxEditar.IsEnabled = false;
            if (lvEmpleados.SelectedItems.Count == 1)
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
          
            this.lvEmpleados.Items.SortDescriptions.Clear();
           
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
                lvEmpleados.Items.SortDescriptions.Add(new SortDescription(ordenadoPor, sentidoOrden));
            }
            else
            {
                columnaOrdenada = null;
            }
            lvEmpleados.Items.Refresh();
        }
    }
}
