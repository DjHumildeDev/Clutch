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
        public VerJornada(Jornada jornada)
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmBxEmpleado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
