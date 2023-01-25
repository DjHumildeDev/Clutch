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
    /// Lógica de interacción para VerJornada.xaml
    /// </summary>
    public partial class VerJornada : Window
    {
        private List<Empleado> empleados;
        private Jornada jornada;
        private Empleado empleado;

        public VerJornada(Jornada jornada, Empleado empleado, List<Empleado> empleados)
        {
            InitializeComponent();
            InicializarCombo();
            this.empleados = empleados;
            this.jornada = jornada;
            this.empleado = empleado;
        }

        private void InicializarCombo()
        {
            foreach(Empleado emp in empleados)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = emp.nombre + " " + emp.apellidos;
                item.Tag = emp;
                cmBxEmpleado.Items.Add(item);
            }
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            jornada.Entrada = (DateTime)TPickerEntrada.Value;
            jornada.horas = Convert.ToDouble(txtBxHoras.Text);
            jornada.sueldoHoy = Convert.ToDouble(txtBxSueldoHoy.Text);
            jornada.idEmpleado = empleado.id;
            jornada.pedidos = Convert.ToInt32(txtBxPedidos.Text);
            if (TPickerSalida.IsEnabled == true)
            {
                jornada.Salida = (DateTime)TPickerSalida.Value;
            }

            this.DialogResult = true;
        }

        private void cmBxEmpleado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            empleado  = (Empleado)((ComboBoxItem)sender).Tag;

        }

        private void chBxSalida_Click(object sender, RoutedEventArgs e)
        {
            if(chBxSalida.IsChecked == true)
            {
                TPickerSalida.IsEnabled = true;
            }
            else
            {
                TPickerSalida.IsEnabled = false;
            }
        }
    }
}
