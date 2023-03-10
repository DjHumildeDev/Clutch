using Clutch.Reports;
using Clutch.UserControls;
using Clutch.Views;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Clutch
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private static Timer aTimer;
       
        public List<PedidosRepartiendoUC> pedidosEnReparto;
        List<Pedido> pedidosSeleccionados;
        Empleado empleadoSeleccionado;

        List<Empleado> repartidoresActivos;
        Negocio negocio;
        
        List<Pedido> pedidos;
        public MainWindow()
        {
            InitializeComponent();
            pedidosSeleccionados = new List<Pedido>();
            pedidosEnReparto = new List<PedidosRepartiendoUC>();
            negocio = new Negocio();
            repartidoresActivos = new List<Empleado>();
            pedidos = new List<Pedido>();
            ComprobarJornadasAbiertas();
            

            ActualizarListaPedidosINI();

            aTimer = new Timer(60000);

            aTimer.Elapsed += RepetirPedidos();
            aTimer.Enabled = true;       
        }
              
        /// <summary>
        /// Comprueba las jornadas que no estan cerradas al abrir el programa
        /// </summary>
        private void ComprobarJornadasAbiertas()
        {            
            List<Jornada> jornadas = negocio.ObtenerJornadasAbiertas();
            if (jornadas != null)
            {
                Empleado repartidor = new Empleado();
                foreach (Jornada item in jornadas)
                {
                    repartidor = negocio.ObtenerEmpleado(item.idEmpleado);
                    repartidoresActivos.Add(repartidor);
                }
                addRepartidorUC();
            }          
        }
        /// <summary>
        /// Repite la funcion actualizar lista de pedidos cada 5 misn
        /// </summary>
        /// <returns>Objeto de clase ElipsedEventHandler</returns>
        private ElapsedEventHandler RepetirPedidos()
        {
            return (sender, e) => { ActualizarListaPedidos(); };
        }
        /// <summary>
        /// Actualiza la lista de pedidos en la pantalla principal
        /// </summary>
        public void ActualizarListaPedidos()
        {
            pedidos = negocio.ObtenerPedidos();
            Dispatcher.Invoke(() => {
                PedidosPanel.Children.Clear();

                foreach (Pedido ped in pedidos)
                {   
                    if (ped.idRepartidor == null)
                    {
                        PedidoUC ucPedido = new PedidoUC();
                        ucPedido.Pedido = ped;

                        ucPedido.CompletarCampos();
                        ucPedido.btnPedidoUC.Tag = ucPedido;
                        ucPedido.btnPedidoUC.Click += PedidoUC_MyEvent;
                        PedidosPanel.Children.Add(ucPedido);                                          
                    }             
                }
            });
        }
        /// <summary>
        /// Actualiza la lista de pedidos en la pantalla principal al iniciar la aplicacion
        /// </summary>
        public void ActualizarListaPedidosINI()
        {
            pedidos = negocio.ObtenerPedidos();
            PedidosPanel.Children.Clear();

            foreach (Pedido ped in pedidos)
            {              
                if (ped.idRepartidor == null)
                {
                    PedidoUC ucPedido = new PedidoUC();
                    ucPedido.Pedido = ped;
                    ucPedido.CompletarCampos();
                    ucPedido.btnPedidoUC.Tag = ucPedido;
                    ucPedido.btnPedidoUC.Click += PedidoUC_MyEvent;
                    PedidosPanel.Children.Add(ucPedido);
                }                
            }
            
        }
        /// <summary>
        /// Actualiza la lista de pedidos en reparto de la pantalla principal
        /// </summary>
        private void addPedidoEnRepartoUC()
        {
            EnRepartoPanel.Children.Clear();
            foreach (Pedido ped in pedidosSeleccionados)
            {
                if (ped.Entregado==false)
                {
                    PedidosRepartiendoUC enRepartoUC = new PedidosRepartiendoUC();
                    enRepartoUC.Empleado = empleadoSeleccionado;
                    enRepartoUC.Pedido = ped;
                    enRepartoUC.HoraSalida = DateTime.Now;
                    EnRepartoPanel.Children.Add(enRepartoUC);
                    enRepartoUC.btnEnReparto.Tag = enRepartoUC;
                    enRepartoUC.btnEnReparto.Click += PedidoEnReparto_Event;
                    enRepartoUC.RellenarCampos();
                    pedidosEnReparto.Add(enRepartoUC);
                }
              
            }
            empleadoSeleccionado = null;
            pedidosSeleccionados.Clear();
            ActualizarListaPedidosINI();
        }
        
        private void mnMenuSalir_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
        /// <summary>
        /// Abre la ventana de mantenimiento de motos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnMantMotos_Click(object sender, RoutedEventArgs e)
        {
            MantenimientoMotos ventana = new MantenimientoMotos(negocio);
            ventana.Owner = this;
            ventana.ShowDialog();
        }
        /// <summary>
        /// Abre la ventana de mantenimiento de empleados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnMantEmpleados_Click(object sender, RoutedEventArgs e)
        {
            MantenimientoEmpleados ventana = new MantenimientoEmpleados(negocio);
            ventana.Owner = this;
            ventana.ShowDialog();
        }
        /// <summary>
        /// Abre la ventana de mantenimiento de incidencias
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnMantIncidencias_Click(object sender, RoutedEventArgs e)
        {
            MantenimientoIncidencias ventana = new MantenimientoIncidencias(negocio);
            ventana.Owner = this;
            ventana.ShowDialog();
        }
        /// <summary>
        /// Abre la ventana de mantenimiento de jornadas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnMantJornadas_Click(object sender, RoutedEventArgs e)
        {
            MantenimientoJornadas ventana = new MantenimientoJornadas(negocio);
            ventana.Owner = this;
            ventana.ShowDialog();
        }
        /// <summary>
        /// Abre la ventana de mantenimiento de pedidos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnMenuPedidos_Click(object sender, RoutedEventArgs e)
        {
            MantenimientoPedidos ventana = new MantenimientoPedidos(negocio);
            ventana.Owner = this;
            ventana.ShowDialog();
        }
        /// <summary>
        /// Abre Un dialogo para el fichaje de los empleados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnFichar_Click(object sender, RoutedEventArgs e)
        {
            Identificacion identificacion = new Identificacion(negocio,true);
            identificacion.Owner = this;
            identificacion.ShowDialog();
           

            if (identificacion.Cerrar == true)
            {
                Empleado borrar = identificacion.empleado;
                EliminarRepartidor(borrar);
            }

            if (negocio.ObtenerRepartidor(identificacion.empleado.id) != null)
            {
                this.repartidoresActivos.Add(identificacion.empleado);
                
                if (identificacion.Cerrar == false)
                {
                    addRepartidorUC();
                }                          
            }
        }
        /// <summary>
        /// Elimina la tarjeta del repartidor activo cuando acaba su turno
        /// </summary>
        /// <param name="borrar"></param>
        private void EliminarRepartidor(Empleado borrar)
        {
            this.repartidoresActivos.Remove(borrar);
            addRepartidorUC(); 
        }
        /// <summary>
        /// Añade la tarjeta del repartidor activo al panel de la pantalla principal
        /// </summary>
        private void addRepartidorUC()
        {
            RepartidoresPanel.Children.Clear();
            foreach (Empleado em in repartidoresActivos)
            {
                RepartidorActivoUC repar = new RepartidorActivoUC();
                repar.Empleado = em;
                repar.Negocio = this.negocio;
                repar.Nombre = em.nombre;
                repar.Apellidos = em.apellidos;
                repar.AsignarValores();
                repar.btnEmpleadoUC.Tag = repar;
                repar.btnEmpleadoUC.Click += RepartidorUC_MyEvent;

                RepartidoresPanel.Children.Add(repar);
            }
        }

    
        /// <summary>
        /// Generar incidecnia desde la pantalla principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnGenIncidencia_Click(object sender, RoutedEventArgs e)
        {
            incidencia nueva = new incidencia();
            Empleado empleado = new Empleado();
            Identificacion identificacion = new Identificacion(negocio, false, empleado,false);
            if (identificacion.ShowDialog() == true)
            {
                empleado = identificacion.EmpleadoSeleccionado;
                VerIncidencia ventana = new VerIncidencia(nueva, empleado);
                ventana.Owner = this;
                if (ventana.ShowDialog() == true)
                {
                    negocio.CrearIncidencia(empleado, nueva);
                }
            }       
        }
        /// <summary>
        /// Genera un informe de incidencias
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnRptInci_Click(object sender, RoutedEventArgs e)
        {
            Generador generador = new Generador();
            generador.GenerarInformeIncidencias();
        }
        /// <summary>
        /// Aparece una ventana con la documentacion de ayuda de la aplicacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnDoc_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"Documentation.chm");
        }
        /// <summary>
        /// Genera un informe d elos pedidos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnRptPedidos_Click(object sender, RoutedEventArgs e)
        {
            Generador generador = new Generador();
            generador.GenerarInformePedidos();
        }
        /// <summary>
        /// Genera un informe de Empleados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mbRptJornadasNormal_Click(object sender, RoutedEventArgs e)
        {
            Generador generador = new Generador();
            generador.GenerarInformeTrabajadores();
        }
        /// <summary>
        /// Genera un informe de Empleados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mbRptJornadasGrafica_Click(object sender, RoutedEventArgs e)
        {
            Generador generador = new Generador();
            generador.GenerarInformeTrabajadoresGrafico();
        }
        /// <summary>
        /// Evento click de las tarjetas de los pedidos de la pantalla principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PedidoUC_MyEvent(object sender, EventArgs e)
        {

            Button Btnpedido = (Button)sender;
            PedidoUC pedido = (PedidoUC)Btnpedido.Tag;
            Pedido borrar = null;

            if (pedido.Seleccionado)
            {

                foreach (Pedido ped in pedidosSeleccionados)
                {
                    if (pedido.Pedido.Equals(ped))
                    {
                        borrar = ped;
                    }
                }
                if (borrar != null)
                {
                    pedidosSeleccionados.Remove(borrar);
                }           
                pedido.Seleccionado = false;
                pedido.btnPedidoUC.Background = Brushes.AliceBlue;
            }
            else
            {
                pedido.btnPedidoUC.Background = Brushes.DarkBlue;
                pedidosSeleccionados.Add(pedido.Pedido);
                pedido.Seleccionado = true;
            }

        }
        /// <summary>
        /// Evewnto click de las tarjetas de los repartidores activos de la pantalla principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepartidorUC_MyEvent(object sender,EventArgs e)
        {
            Button btnRepartidor = (Button)sender;
            RepartidorActivoUC repartidor = (RepartidorActivoUC)btnRepartidor.Tag;

            if (repartidor.Ocupado)
            {
                repartidor.Ocupado = false;
                CerrarPedido(repartidor);


                repartidor.Background = Brushes.AliceBlue;
            }
            else
            {
                if (pedidosSeleccionados.Count != 0)
                {
                    empleadoSeleccionado = repartidor.Empleado;
                    AsignarPedido();
                }
                else
                {
                    MessageBox.Show("No hay pedidos seleccionados", "Sin pedidos Seleccionados", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                repartidor.Ocupado = true;
                repartidor.Background = Brushes.DarkBlue;
            }
        }

        /// <summary>
        /// Funcion que asigna un pedido a un repartidor desde la pantalla principal
        /// </summary>
        private void AsignarPedido()
        {
            Repartidor repar = negocio.ObtenerRepartidor(empleadoSeleccionado.id);
            foreach(Pedido ped in pedidosSeleccionados)
            {
                negocio.RecogerPedido(ped, repar);
            }
            addPedidoEnRepartoUC();
        }

        /// <summary>
        /// Cierra el pedido cuando pulsas en el UC del repartidor con reparto activo
        /// </summary>
        /// <param name="repartidor">RepartidorActivoUC</param>
        private void CerrarPedido(RepartidorActivoUC repartidor)
        {
            Empleado empleado = repartidor.Empleado;            

            foreach (PedidosRepartiendoUC item in pedidosEnReparto)
            {
                if (item.Empleado.Equals(empleado))
                {
                    Jornada jornada =negocio.ObtenerJornadaAbierta(empleado);
                    if (jornada != null)
                    {
                        negocio.EntregarPedido(item.Pedido,jornada);
                        TimeSpan diferencia =  DateTime.UtcNow - item.HoraSalida ;
                        int Tiempo = Convert.ToInt32(diferencia.TotalMinutes);
                        MessageBox.Show("Tiempo Tardado: " + Tiempo + " minutos", "Pedido entregado", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            addPedidoEnRepartoUC();


            //Tengo que pasar los pedidos en reparto que hay puestos en lños UC para cogerlos de manera comoda

            //SacarVentana del tiempo tardado
        }

        /// <summary>
        /// Evento Click del UCPedidoEnReparto
        /// </summary>
        /// <param name="sender">btnEnReparto</param>
        /// <param name="e">EventArgs</param>
        private void PedidoEnReparto_Event(object sender,EventArgs e)
        {
            Button btnPedido = (Button)sender;
            PedidosRepartiendoUC pedido = (PedidosRepartiendoUC)btnPedido.Tag;

            InfoReparto info = new InfoReparto(pedido);
            if (info.ShowDialog() == true)
            {

            }

        }
    }
}
