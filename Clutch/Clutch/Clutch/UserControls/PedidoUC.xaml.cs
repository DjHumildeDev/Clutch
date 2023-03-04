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
    /// Lógica de interacción para PedidoUC.xaml
    /// </summary>
    public partial class PedidoUC : UserControl
    {     
        public Pedido Pedido { get; set; }
        public bool Seleccionado { get; set; }

        public PedidoUC()
        {
            InitializeComponent();
            Seleccionado = false;
        }
        public void CompletarCampos()
        {
            lblNumero.Content = Pedido.id.ToString();
            lblDireccion.Content = Pedido.direccion;
            lblTelefono.Content = Pedido.tlfCliente;
        }     
    }
}
