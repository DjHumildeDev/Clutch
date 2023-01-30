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
    /// Lógica de interacción para VerIncidencia.xaml
    /// </summary>
    public partial class VerIncidencia : Window
    {
        private incidencia incidencia;
        private Empleado empleado;
        private string tipo;

        public VerIncidencia(incidencia nueva,Empleado empleado)
        {          
            InitializeComponent();
            this.incidencia = nueva;
            this.empleado = empleado;
           
            RellenarCampos();
        }

        private void RellenarCampos()
        {
            txtBxDescripcionIncidencia.Text = incidencia.incidencia1;
            chBxResuelta.IsChecked = incidencia.resuelta;
            dtPckFecha.SelectedDate = incidencia.fecha;
            txtBxEmpleado.Text = empleado.nombre;

            if (incidencia.tipo == "Moto")
            {
                cmBxTipo.SelectedIndex = 0;
            }
            if (incidencia.tipo == "Cocina")
            {
                cmBxTipo.SelectedIndex = 1;
            }
            if (incidencia.tipo == "Otros")
            {
                cmBxTipo.SelectedIndex = 2;
            }
          
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            incidencia.incidencia1 = txtBxDescripcionIncidencia.Text;
            incidencia.fecha = dtPckFecha.SelectedDate.Value;

            incidencia.tipo = tipo;
            this.DialogResult = true;
        }
  
        private void cmBxTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            tipo = (string)((ComboBoxItem)combo.SelectedItem).Tag;
        }

        private void chBxResuelta_Click(object sender, RoutedEventArgs e)
        {
            if (chBxResuelta.IsChecked == true)
            {
                incidencia.resuelta = true;
            }
            else
            {
                incidencia.resuelta = false;
            }
        }
    }
}
