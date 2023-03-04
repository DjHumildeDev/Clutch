using Clutch.UserControls;
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
    /// Lógica de interacción para InfoReparto.xaml
    /// </summary>
    public partial class InfoReparto : Window
    {
        public Empleado Empleado { get; set; }
        public Pedido Pedido { get; set; }


        public PedidosRepartiendoUC PedidoUc { get; set; }
        public InfoReparto(PedidosRepartiendoUC pedido)
        {
            InitializeComponent();
            this.PedidoUc = pedido;
            this.Pedido = pedido.Pedido;
            this.Empleado = pedido.Empleado;

            CompletarCampos();
        }

        /// <summary>
        /// Completa los cascos de la ventana de info de reparto
        /// </summary>
        private void CompletarCampos()
        {
            try
            {
                TimeSpan diferencia = DateTime.Now - PedidoUc.HoraSalida;
                int Tiempo = Convert.ToInt32(diferencia.TotalMinutes);

                txtBxPedido.Text = Pedido.id.ToString();

                txtBxHora.Text = string.Format("{0:HH\\:mm}", PedidoUc.HoraSalida);
                txtBxRepartidor.Text = Empleado.nombre + " " + Empleado.apellidos;
                txtBxTiempo.Text = Tiempo.ToString() + " Mins";
                txtBxDireccion.Text = Pedido.direccion;

            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocurrio algun error inesperado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.DialogResult = false;
            }
           
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
