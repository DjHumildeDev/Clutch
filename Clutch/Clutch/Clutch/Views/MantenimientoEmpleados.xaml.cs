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
    /// Lógica de interacción para MantenimientoEmpleados.xaml
    /// </summary>
    public partial class MantenimientoEmpleados : Window
    {
        private Negocio negocio;
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

        private void mnEmpleadosCrear_Click(object sender, RoutedEventArgs e)
        {
            Empleado nuevo = new Empleado();
            VerEmpleado ventana = new VerEmpleado(nuevo,negocio);
            ventana.Owner = this;
            if (ventana.ShowDialog() == true)
            {        
                ActualizarLista();
            }
        }

        private void ActualizarLista()
        {
            lvEmpleados.Items.Clear();
            List<Empleado> empleados = negocio.ObtenerEmpleados();
            foreach (Empleado x in empleados)
            {
                lvEmpleados.Items.Add(x);
            }
        }

        private void mnEmpleadosEditar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnEmpleadosBorrar_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
