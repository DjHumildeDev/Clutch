using Clutch.Reports;
using Clutch.UserControls;
using Clutch.Views;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clutch
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Empleado> repartidoresActivos;
        Negocio negocio;
        List<Jornada> jornadas;
        List<Pedido> pedidos;
        public MainWindow()
        {
            InitializeComponent();
            negocio = new Negocio();
            repartidoresActivos = new List<Empleado>();
            pedidos = new List<Pedido>();
            ActualizarListaPedidos();
        }

        private void ActualizarListaPedidos()
        {
            pedidos = negocio.ObtenerPedidos();
            PedidosPanel.Children.Clear();

            foreach(Pedido ped in pedidos)
            {
                PedidoUC ucPedido = new PedidoUC();
                ucPedido.pedido = ped;
                ucPedido.ComnpleatarCampos();

                PedidosPanel.Children.Add(ucPedido);
            }
        }

        private void mnMenuSalir_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void mnMantMotos_Click(object sender, RoutedEventArgs e)
        {
            MantenimientoMotos ventana = new MantenimientoMotos(negocio);
            ventana.Owner = this;
            ventana.ShowDialog();
        }

        private void mnMantEmpleados_Click(object sender, RoutedEventArgs e)
        {
            MantenimientoEmpleados ventana = new MantenimientoEmpleados(negocio);
            ventana.Owner = this;
            ventana.ShowDialog();
        }

        private void mnMantIncidencias_Click(object sender, RoutedEventArgs e)
        {
            MantenimientoIncidencias ventana = new MantenimientoIncidencias(negocio);
            ventana.Owner = this;
            ventana.ShowDialog();
        }

        private void mnMantJornadas_Click(object sender, RoutedEventArgs e)
        {
            MantenimientoJornadas ventana = new MantenimientoJornadas(negocio);
            ventana.Owner = this;
            ventana.ShowDialog();
        }

        private void mnMenuPedidos_Click(object sender, RoutedEventArgs e)
        {
            MantenimientoPedidos ventana = new MantenimientoPedidos(negocio);
            ventana.Owner = this;
            ventana.ShowDialog();
        }

        private void mnFichar_Click(object sender, RoutedEventArgs e)
        {
            Identificacion identificacion = new Identificacion(negocio,true);
            identificacion.Owner = this;
            identificacion.ShowDialog();
           

            if (identificacion.Cerrar == true)
            {
                Empleado borrar = identificacion.empleado;
                EliminarRepartidor(borrar);
            }

            if (negocio.ObtenerRepartidor(identificacion.empleado.id) != null)
            {
                this.repartidoresActivos.Add(identificacion.empleado);
                
                if (identificacion.Cerrar == false)
                {
                    addRepartidorUC();
                }                          

            }
        }

        private void EliminarRepartidor(Empleado borrar)
        {
            this.repartidoresActivos.Remove(borrar);
            addRepartidorUC(); 
        }

        private void addRepartidorUC()
        {
            RepartidoresPanel.Children.Clear();
            foreach (Empleado em in repartidoresActivos)
            {
                RepartidorActivoUC repar = new RepartidorActivoUC();
                repar.empleado = em;
                repar.negocio = this.negocio;
                repar.nombre = em.nombre;
                repar.apellidos = em.apellidos;
                repar.AsignarValores();

                RepartidoresPanel.Children.Add(repar);
            }
        }

        private void mnGenIncidencia_Click(object sender, RoutedEventArgs e)
        {
            incidencia nueva = new incidencia();
            Empleado empleado = new Empleado();
            Identificacion identificacion = new Identificacion(negocio, false, empleado,false);
            if (identificacion.ShowDialog() == true)
            {
                empleado = identificacion.EmpleadoSeleccionado;
                VerIncidencia ventana = new VerIncidencia(nueva, empleado);
                ventana.Owner = this;
                if (ventana.ShowDialog() == true)
                {
                    negocio.CrearIncidencia(empleado, nueva);
                }
            }
          
        }

        private void mnRptInci_Click(object sender, RoutedEventArgs e)
        {
            Generador generador = new Generador();
            generador.GenerarInformeIncidencias();
        }
    }
}
