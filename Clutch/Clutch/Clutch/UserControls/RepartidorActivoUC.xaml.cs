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
        public Empleado Empleado { get; set; }
        public Negocio Negocio { get; set; }

        public bool Ocupado { get; set; }

        public string Nombre { get; set; }
        public string Apellidos {get ;set; }
        
        public RepartidorActivoUC()
        {
            InitializeComponent();                     
        }
        public void AsignarValores()
        {
            this.lblNombre.Content = Nombre + " " + Apellidos;
        }

    }
}
