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

        private void cmBxTipo_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmBxEmpleado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void mnIncidenciasBorrar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnIncidenciassEditar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnIncidenciasCrear_Click(object sender, RoutedEventArgs e)
        {
            //Accer como un login para sacar el empleado que crea la incidencia
            incidencia nueva = new incidencia();
            Empleado empleado = new Empleado();
            VerIncidencia ventana = new VerIncidencia(nueva);
            ventana.Owner = this;
            if (ventana.ShowDialog() == true)
            {
                negocio.CrearIncidencia(empleado,nueva);
                ActualizarLista();
            }
        }

        private void mnIncidenciasSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lvJornadas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
