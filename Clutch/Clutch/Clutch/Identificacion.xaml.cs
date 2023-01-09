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

namespace Clutch
{
    /// <summary>
    /// Lógica de interacción para Identificacion.xaml
    /// </summary>
    public partial class Identificacion : Window
    {
        private Negocio negocio;
        private Empleado empleadoAbre;
        private Jornada jornadaHoy;


        public Identificacion(Negocio negocio)
        {
            InitializeComponent();
            this.negocio = negocio;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ComprobarIdentificacion(txtBxUser.Text.ToString(), txtBxPass.Password.ToString()))
            {
                if (YaFichado(empleadoAbre))
                {
                    if (jornadaHoy != null)
                    {
                        CerrarJornada(jornadaHoy);
                       
                        MessageBox.Show("Hasta pronto! " + empleadoAbre.nombre, "Has acabado tu Jornada", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                else
                {
                    Jornada jornada = new Jornada();
                    jornada.Entrada = DateTime.Now;
                    negocio.CrearJornada(empleadoAbre, jornada);
                    MessageBox.Show("Buenos dias " + empleadoAbre.nombre, "Has iniciado sesion", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }         
            }
            else
            {
                MessageBox.Show("Alguno de los campos no es correcto", "Error en el inicio de sesion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void CerrarJornada(Jornada jornadaHoy)
        {
            //calcular sueldo y horas
            DateTime fecha1 = jornadaHoy.Entrada;
            DateTime fecha2 = DateTime.Now;

            TimeSpan result = fecha2.Subtract(fecha1);
            float horas = float.Parse(Convert.ToString(result.TotalHours));

            jornadaHoy.horas = horas;
            jornadaHoy.sueldoHoy = jornadaHoy.sueldo * horas;
            jornadaHoy.Salida = DateTime.Now;

            negocio.EditarJornada(jornadaHoy);
        }

        private bool ComprobarIdentificacion(string usuario, string pass)
        {

            List<Empleado> empleados = negocio.ObtenerEmpleados();
            foreach (Empleado empleado in empleados)
            {
                if (empleado.dni.Equals(usuario))
                {
                    if (empleado.contraseña.Equals(pass))
                    {
                        int id = empleado.id;
                        empleadoAbre = negocio.ObtenerEmpleado(id);
                        
                        return true;

                    }
                }
            }
            return false;
        }

        private bool YaFichado(Empleado empleado)
        {
            List<Jornada> jornadas = negocio.ObtenerJornadas();
            List<Jornada> jornadasEmpleado = new List<Jornada>();
            foreach(Jornada jornada in jornadas)
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
                    if(jornada.Salida == null){
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
    }
}
