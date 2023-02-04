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
    /// Lógica de interacción para MantenimientoPedidos.xaml
    /// </summary>
    public partial class MantenimientoPedidos : Window
    {
        private Negocio negocio;
        public MantenimientoPedidos(Negocio negocio)
        {
            InitializeComponent();
            this.negocio = negocio;
            ActualizarLista();
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

        private void mnPedidosSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void mnPedidosCrear_Click(object sender, RoutedEventArgs e)
        {
            Pedido nuevo = new Pedido();
            VerPedido ventana = new VerPedido(nuevo);
            ventana.Owner = this;
            if (ventana.ShowDialog() == true)
            {
                negocio.CrearPedido(nuevo);
                ActualizarLista();
            }
        }

        private void ActualizarLista()
        {
            lvPedidos.Items.Clear();
            List<Pedido> pedidos = negocio.ObtenerPedidos();
            foreach (Pedido x in pedidos)
            {
                ListViewItem item = new ListViewItem();
                item.Content = x;
                item.Tag = x;
                lvPedidos.Items.Add(item);
            }
        }

        private void mnPedidosEditar_Click(object sender, RoutedEventArgs e)
        {
            if (lvPedidos.SelectedItems.Count == 1)
            {
                Pedido ver = (Pedido)((ListViewItem)lvPedidos.SelectedItems[0]).Tag;
                VerPedido ventana = new VerPedido(ver);
                ventana.Owner = this;
                if (ventana.ShowDialog() == true)
                {
                    negocio.EditarPedido(ver);
                    ActualizarLista();
                }
            }
            else
            {
                MessageBox.Show("Necesitas seleccionar un elemento", "No hay seleccion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void mnPedidosBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (lvPedidos.SelectedItems.Count == 1)
            {
                //Proceso de identificacion
                if (Identificacion())
                {
                    MessageBoxResult result = MessageBox.Show("¿Esta seguro que quiere borrar este pedido?", "¡Atencion!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        Pedido borrar = (Pedido)((ListViewItem)lvPedidos.SelectedItems[0]).Tag;

                        if (negocio.BorrarPedido(borrar.id))
                        {
                            MessageBox.Show("Se ha borrado el pedido correctamente", "Pedido Borrado", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar el pedido", "Algo salio mal", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void lvPedidos_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            ctxBorrar.IsEnabled = false;
            ctxEditar.IsEnabled = false;
            if (lvPedidos.SelectedItems.Count == 1)
            {
                ctxEditar.IsEnabled = true;
                ctxBorrar.IsEnabled = true;
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

            this.lvPedidos.Items.SortDescriptions.Clear();

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
                lvPedidos.Items.SortDescriptions.Add(new SortDescription(ordenadoPor, sentidoOrden));
            }
            else
            {
                columnaOrdenada = null;
            }
        }
    }
}
