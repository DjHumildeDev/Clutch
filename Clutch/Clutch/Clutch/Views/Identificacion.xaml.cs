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
    /// Lógica de interacción para Identificacion.xaml
    /// </summary>
    public partial class Identificacion : Window
    {
        private Negocio negocio;
        public Empleado empleado { get; set; }
        private Jornada jornadaHoy;
        private bool fichar;
        public bool Cerrar { get; set; }
        private bool Encargado;
        public Empleado EmpleadoSeleccionado { get; set; }
        public Identificacion()
        {
            InitializeComponent();
        }

        public Identificacion(Negocio negocio,bool fichar):this()
        {
            this.negocio = negocio;
            this.fichar = fichar;

        }
        public Identificacion(Negocio negocio, bool fichar, Empleado empleado,bool encargado):this()
        {
            this.negocio = negocio;
            this.fichar = fichar;
            this.empleado = empleado;
            this.Encargado = encargado;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos())
            {
                if (ComprobarIdentificacion(txtBxUser.Text.ToString(), txtBxPass.Password.ToString()))
                {
                    if (fichar)
                    {
                        if (YaFichado(empleado))
                        {
                            if (jornadaHoy != null)
                            {
                                CerrarJornada(jornadaHoy);
                                Cerrar = true;
                                MessageBox.Show("Hasta pronto! " + empleado.nombre, "Has acabado tu Jornada", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Close();
                            }
                        }
                        else
                        {
                            Cerrar = false;
                            Jornada jornada = new Jornada();
                            jornada.Entrada = DateTime.Now;
                            jornada.pedidos = 0;
                            negocio.CrearJornada(empleado, jornada);
                            MessageBox.Show("Buenos dias " + empleado.nombre, "Has iniciado sesion", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.DialogResult = true;
                        }
                    }
                    else
                    {
                        if (Encargado == true)
                        {
                            EmpleadoSeleccionado = empleado;
                            Encargado encargado = negocio.ObtenerEncargado(EmpleadoSeleccionado.id);
                            if (encargado != null)
                            {
                                MessageBox.Show("Identificacion correcta " + empleado.nombre, "Identificado", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.DialogResult = true;
                            }
                            else
                            {
                                MessageBox.Show("Este usuario no es encargado", "Identificado", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            EmpleadoSeleccionado = empleado;


                            MessageBox.Show("Identificacion correcta" + empleado.nombre, "Identificado", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.DialogResult = true;
                        }


                    }
                }
                else
                {
                    MessageBox.Show("Alguno de los campos no es correcto", "Error en la identificacion", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Hay algun campo incompleto", "Error en la identificacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }          
        }

        /// <summary>
        /// Comprueba que los datos introducidos en los textbox sean validos
        /// </summary>
        /// <returns></returns>
        private bool ValidarCampos()
        {
            if (String.IsNullOrEmpty(txtBxUser.Text))
            {
                return false;
            }
            if (String.IsNullOrEmpty(txtBxPass.Password))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Funcion encargada de cerrar la jornada al hacer el fichaje de cierre
        /// </summary>
        /// <param name="jornadaHoy"></param>
        private void CerrarJornada(Jornada jornadaHoy)
        {
            //calcular sueldo y horas
            DateTime fecha1 = jornadaHoy.Entrada;
            DateTime fecha2 = DateTime.Now;

            TimeSpan result = fecha2.Subtract(fecha1);
            float horas = float.Parse(Convert.ToString(result.TotalHours));

            jornadaHoy.horas = horas;
            if (jornadaHoy.pedidos != null)
            {
                jornadaHoy.sueldoHoy = (double)(jornadaHoy.sueldo * horas + jornadaHoy.pedidos);
            }
            else
            {
                jornadaHoy.sueldoHoy = (double)(jornadaHoy.sueldo * horas);
            }
           
            jornadaHoy.Salida = DateTime.Now;

            negocio.EditarJornada(jornadaHoy);
        }
        /// <summary>
        /// Funcion que comprueba que el campo usuario y la contraseña sean correctos de cada trabajador
        /// </summary>
        /// <param name="usuario">usuario</param>
        /// <param name="pass">Contraseña</param>
        /// <returns>Booleano en funcion de si es correcto el inicio de sesion o no</returns>
        private bool ComprobarIdentificacion(string usuario, string pass)
        {
            List<Empleado> empleados = negocio.ObtenerEmpleados();
            foreach (Empleado empleado in empleados)
            {
                if (empleado.dni.Equals(usuario,StringComparison.OrdinalIgnoreCase))
                {
                    if (empleado.contraseña.Equals(pass))
                    {
                        int id = empleado.id;
                        this.empleado = negocio.ObtenerEmpleado(id);

                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Funcion que se encarga de comprobar si el fichaje es de salida o de entrada
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        private bool YaFichado(Empleado empleado)
        {
            List<Jornada> jornadas = negocio.ObtenerJornadas();
            List<Jornada> jornadasEmpleado = new List<Jornada>();
            foreach (Jornada jornada in jornadas)
            {
                if (jornada.idEmpleado.Equals(empleado.id))
                {
                    jornadasEmpleado.Add(jornada);
                }
            }

            foreach (Jornada jornada in jornadasEmpleado)
            {
                if (jornada.Entrada.Date.Equals(DateTime.Today))
                {
                    if (jornada.Salida == null)
                    {
                        jornadaHoy = jornada;
                        return true;
                    }
                    else
                    {
                        jornadaHoy = null;
                    }
                }
            }
            return false;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                System.Diagnostics.Process.Start(@"Documentation.chm");
            }
        }
    }
}
