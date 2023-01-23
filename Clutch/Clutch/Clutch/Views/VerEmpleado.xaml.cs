using Clutch.Enums;
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
    /// Lógica de interacción para VerEmpleado.xaml
    /// </summary>
    public partial class VerEmpleado : Window
    {
        private Empleado empleado;
        private Negocio negocio;
        private string rol;
        public VerEmpleado(Empleado empleado,Negocio negocio)
        {
            InitializeComponent();
            this.empleado = empleado;
            RellenarCampos();
            
        }

        private void RellenarCampos()
        {
            rol = String.Empty;
            txtBxNombre.Text = empleado.nombre;
            txtBxApellidos.Text = empleado.apellidos;
            txtBxDNI.Text = empleado.dni;
            txtBxPass.Text = empleado.contraseña;

            List<Cocina> cocineros = negocio.ObtenerCocineros();
            List<Encargado> encargados = negocio.ObtenerEncargados();
            List<Repartidor> repartidores = negocio.ObtenerRepartidores();

            foreach(Cocina cocinero in cocineros)
            {
                if (cocinero.idEmpleado == empleado.id)
                {
                    rol = RolEnum.Cocina.ToString();   
                }
            }
            foreach (Encargado encargado in encargados)
            {
                if (encargado.idEmpleado == empleado.id)
                {
                    rol = RolEnum.Encargado.ToString();
                }
            }
            foreach (Repartidor repa in repartidores)
            {
                if (repa.idEmpleado == empleado.id)
                {
                    rol = RolEnum.Repartidor.ToString();
                }
            }

            switch (rol)
            {
                case "Encargado":
                    cmBxTipo.SelectedIndex = 0;
                    break;
                case "Repartidor":
                    cmBxTipo.SelectedIndex = 1;
                    break;
                case "Cocina":
                    cmBxTipo.SelectedIndex = 2;
                    break;
            }

        }

        private void cmBxTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            empleado.nombre = txtBxNombre.Text;
            empleado.apellidos = txtBxApellidos.Text;
            empleado.dni = txtBxDNI.Text;
            string pass = empleado.dni.Substring(4, 3);
            empleado.contraseña = pass;
            empleado.alta = DateTime.Today;

            switch (rol)
            {
                case "Encargado":
                    Encargado nuevo = new Encargado();
                    negocio.CrearEncargado(empleado, nuevo);
                    break;
                case "Repartidor":
                    Repartidor repar = new Repartidor();
                    negocio.CrearRepartidor(empleado, repar);
                    break;
                case "Cocina":
                    Cocina cocinero = new Cocina();
                    negocio.CrearCocinero(empleado, cocinero);
                    break;
            }

            this.DialogResult = true;
        }
    }
}
