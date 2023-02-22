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

namespace Clutch.UserControls
{
    /// <summary>
    /// Lógica de interacción para RepartidorActivoUC.xaml
    /// </summary>
    public partial class RepartidorActivoUC : UserControl
    {
        public Empleado empleado { get; set; }
        public Negocio negocio { get; set; }

        public string nombre { get; set; }
        public string apellidos {get ;set; }
        
        public RepartidorActivoUC()
        {
            InitializeComponent();                     
        }
        public void AsignarValores()
        {
            this.lblNombre.Content = nombre + " " + apellidos;
        }
        private void btnEmpleadoUC_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
