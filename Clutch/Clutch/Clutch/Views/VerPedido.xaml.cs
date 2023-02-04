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
    /// Lógica de interacción para VerPedido.xaml
    /// </summary>
    public partial class VerPedido : Window
    {
        private Pedido pedido;

        public VerPedido(Pedido pedido)
        {
            InitializeComponent();
            this.pedido = pedido;
            txtBxDireccion.Text = pedido.direccion;
            txtBxPedido.Text = pedido.pedido1;
            txtBxPrecio.Text = pedido.precio.ToString();
            txtBxTelefono.Text = pedido.tlfCliente;
            
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
