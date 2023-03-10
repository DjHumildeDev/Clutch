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
    /// Lógica de interacción para MantenimientoMotos.xaml
    /// </summary>
    public partial class MantenimientoMotos : Window
    {
        private Negocio negocio;
        public MantenimientoMotos(Negocio negocio)
        {
            InitializeComponent();
            this.negocio = negocio;
            ActualizarLista();
        }

        private void mnMotosSalir_Click(object sender, RoutedEventArgs e)
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

        private void mnMotosCrear_Click(object sender, RoutedEventArgs e)
        {
            if (Identificacion())
            {
                Moto nueva = new Moto();
                VerMoto ventana = new VerMoto(nueva);
                ventana.Owner = this;
                if (ventana.ShowDialog() == true)
                {
                    negocio.CrearMoto(nueva);
                    ActualizarLista();
                }
            }         
        }

        private void ActualizarLista()
        {
            //Necesito Corregir la base de datos ya que al crearse el repartidor los cc y la moto asignada tienen que ser null

            Empleado empleado = new Empleado();
            lvMotos.Items.Clear();
            List<Moto> motos = negocio.ObtenerMotos();
            foreach (Moto moto in motos)
            {
                MotoListModel motoItem = new MotoListModel();
                if (moto.estado == "Ocupada")
                {
                    Repartidor repar = negocio.ObtenerRepartidor_Moto(moto.id);
                    if (repar != null)
                    {
                        empleado = negocio.ObtenerEmpleado(repar.idEmpleado);
                        motoItem.asignada = empleado.nombre + " " + empleado.apellidos;
                    }
                }


                motoItem.cc = moto.cc;
                motoItem.estado = moto.estado;
                motoItem.matricula = moto.matricula;
                motoItem.numero = moto.numero;

                ListViewItem item = new ListViewItem();
                item.Content = motoItem;
                item.Tag = moto;

                lvMotos.Items.Add(item);
            }
        }

        private void mnMotosEditar_Click(object sender, RoutedEventArgs e)
        {

            if (lvMotos.SelectedItems.Count == 1)
            {
                if (Identificacion())
                {
                    Moto ver = (Moto)((ListViewItem)lvMotos.SelectedItems[0]).Tag;
                    VerMoto ventana = new VerMoto(ver);
                    ventana.Owner = this;
                    if (ventana.ShowDialog() == true)
                    {
                        negocio.EditarMoto(ver);
                        ActualizarLista();
                    }
                }
            }
            else
            {
                MessageBox.Show("Necesitas seleccionar un elemento", "No hay seleccion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        private void mnMotosBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (lvMotos.SelectedItems.Count == 1)
            {
                //Proceso de identificacion
                if (Identificacion())
                {
                    MessageBoxResult result = MessageBox.Show("¿Esta seguro que quiere borrar esta moto?", "¡Atencion!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        Moto borrar = (Moto)((ListViewItem)lvMotos.SelectedItems[0]).Tag;

                        if (negocio.BorrarMoto(borrar))
                        {
                            MessageBox.Show("Se ha borrado la moto correctamente", "Moto Borrada", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar la moto", "Algo salio mal", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void mnMotoAsignar_Click(object sender, RoutedEventArgs e)
        {
            Repartidor repar = new Repartidor();
            Moto moto = new Moto();
            AsignarMoto ventana = new AsignarMoto(repar,moto,negocio);
            ventana.Owner = this;
            if (ventana.ShowDialog() == true)
            {
                negocio.AsignarMoto(repar, moto);
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

            this.lvMotos.Items.SortDescriptions.Clear();

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
                lvMotos.Items.SortDescriptions.Add(new SortDescription(ordenadoPor, sentidoOrden));
            }
            else
            {
                columnaOrdenada = null;
            }
        }

        private void lvMotos_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            ctxBorrar.IsEnabled = false;
            ctxEditar.IsEnabled = false;
            ctxAsignar.IsEnabled = false;
            if (lvMotos.SelectedItems.Count == 1)
            {
                ctxEditar.IsEnabled = true;
                ctxBorrar.IsEnabled = true;
                ctxAsignar.IsEnabled = true;
            }
        }

        private void ctxAsignar_Click(object sender, RoutedEventArgs e)
        {
            Repartidor repar = new Repartidor();
            Moto moto = (Moto)((ListViewItem)lvMotos.SelectedItems[0]).Tag;
            AsignarMoto ventana = new AsignarMoto(repar, moto, negocio);
            ventana.Owner = this;
            if (ventana.ShowDialog() == true)
            {
                repar = ventana.repartidor;
                negocio.AsignarMoto(repar, moto);
                ActualizarLista();
            }
        }

        private void txtBxMatricula_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionView vista = (CollectionView)CollectionViewSource.GetDefaultView(lvMotos.Items);
            vista.Filter = MaticulaFilter;
            vista.Refresh();
        }

        private bool MaticulaFilter(object item)
        {
            ListViewItem moto = (ListViewItem)item;
            if (String.IsNullOrEmpty(txtBxMatricula.Text))
            {
                return true;
            }
            else
            {
                return ((moto.Tag as Moto).matricula.IndexOf(txtBxMatricula.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private void cmBxcc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionView vista = (CollectionView)CollectionViewSource.GetDefaultView(lvMotos.Items);
            vista.Filter = CCFilter;
            vista.Refresh();
        }

        private bool CCFilter(object item)
        {
            ListViewItem moto = (ListViewItem)item;
            ComboBoxItem cmbItem = (ComboBoxItem)cmBxCC.SelectedItem;
            if (String.IsNullOrEmpty(cmbItem.Tag.ToString()))
            {
                return true;
            }
            else
            {
                return (moto.Tag as Moto).cc.Equals(cmbItem.Tag);
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtBxMatricula.Text = String.Empty;
            ActualizarLista();
        }

        private void cmBxEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionView vista = (CollectionView)CollectionViewSource.GetDefaultView(lvMotos.Items);
            vista.Filter = EstadoFilter;
            vista.Refresh();
        }

        private bool EstadoFilter(object item)
        {
            ListViewItem moto = (ListViewItem)item;
            ComboBoxItem cmbItem = (ComboBoxItem)cmBxEstado.SelectedItem;
            if (String.IsNullOrEmpty(cmbItem.Tag.ToString()))
            {
                return true;
            }
            else
            {
                return ((moto.Tag as Moto).estado.IndexOf(cmbItem.Tag.ToString(), StringComparison.OrdinalIgnoreCase) >= 0);
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
