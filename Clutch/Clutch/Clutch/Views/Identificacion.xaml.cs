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


        public Identificacion(Negocio negocio)
        {
            InitializeComponent();
            this.negocio = negocio;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ComprobarIdentificacion(txtBxUser.Text.ToString(), txtBxPass.Password.ToString()))
            {
                Jornada jornada = new Jornada();
                jornada.Entrada = DateTime.Now;
                negocio.CrearJornada(empleadoAbre, jornada);
                MessageBox.Show("Buenos dias " + empleadoAbre.nombre, "Has iniciado sesion", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Alguno de los campos no es correcto", "Error en el inicio de sesion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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
    }
}
