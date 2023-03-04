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
        public Empleado Empleado {get;set;}
        public Pedido Pedido { get; set; }

        PedidosRepartiendoUC pedidoUC;
        public InfoReparto(PedidosRepartiendoUC pedido)
        {
            InitializeComponent();
            this.pedidoUC = pedido;
            this.Pedido = pedido.Pedido;
            this.Empleado = pedido.Empleado;
            CompletarCampos();
        }

        private void CompletarCampos()
        {
            TimeSpan diferencia = DateTime.UtcNow - pedidoUC.HoraSalida;
            int Tiempo = Convert.ToInt32(diferencia.TotalMinutes);

            txtBxPedido.Text = Pedido.id.ToString();
            txtBxHora.Text = pedidoUC.HoraSalida.TimeOfDay.ToString();
            txtBxRepartidor.Text = Empleado.nombre + " " + Empleado.apellidos;
            txtBxTiempo.Text = Tiempo.ToString() +" Mins";
            txtBxDireccion.Text = Pedido.direccion;
        }
    }
}
