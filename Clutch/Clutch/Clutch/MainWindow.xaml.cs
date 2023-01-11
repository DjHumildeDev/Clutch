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

        }

        private void mnMantMotos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnMantEmpleados_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnMantIncidencias_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnMantJornadas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnMenuPedidos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnFichar_Click(object sender, RoutedEventArgs e)
        {
            Identificacion identificacion = new Identificacion(negocio);
            identificacion.Owner = this;
            identificacion.ShowDialog();
        }

        private void mnGenIncidencia_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
