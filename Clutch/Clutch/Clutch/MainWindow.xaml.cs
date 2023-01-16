using Clutch.Views;
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
        Negocio negocio;
        List<Jornada> jornadas;
        public MainWindow()
        {
            InitializeComponent();
            negocio = new Negocio();
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
            Identificacion identificacion = new Identificacion(negocio);
            identificacion.Owner = this;
            identificacion.ShowDialog();
        }

        private void mnGenIncidencia_Click(object sender, RoutedEventArgs e)
        {
            incidencia nueva = new incidencia();
            VerIncidencia ventana = new VerIncidencia(nueva);
            ventana.Owner = this;
            if (ventana.ShowDialog() == true)
            {
                Empleado empleado = negocio.ObtenerEmpleado(nueva.idEmpleado);
                negocio.CrearIncidencia(empleado,nueva);
            }
        }
    }
}
