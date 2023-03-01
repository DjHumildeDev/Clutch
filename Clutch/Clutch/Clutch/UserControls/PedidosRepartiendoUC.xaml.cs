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
    /// Lógica de interacción para PedidosRepartiendoUC.xaml
    /// </summary>
    public partial class PedidosRepartiendoUC : UserControl
    {
        public Pedido Pedido {get;set;}
        public Empleado Empleado { get; set; }
        public PedidosRepartiendoUC()
        {
            InitializeComponent();
            RellenarCampos();
        }

        private void RellenarCampos()
        {
            
        }
    }
}
