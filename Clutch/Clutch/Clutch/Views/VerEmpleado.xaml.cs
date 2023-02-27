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
            this.negocio = negocio;
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
            ComboBox combo = (ComboBox)sender;
            rol = (string)((ComboBoxItem)combo.SelectedItem).Tag;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidacionCampos())
            {
                empleado.nombre = txtBxNombre.Text;
                empleado.apellidos = txtBxApellidos.Text;
                empleado.dni = txtBxDNI.Text;
                string pass = empleado.dni.Substring(4, 4);
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
                    case "Cocinero":
                        Cocina cocinero = new Cocina();
                        negocio.CrearCocinero(empleado, cocinero);
                        break;
                }

                this.DialogResult = true;
            }
          
        }

        private bool ValidacionCampos()
        {
            if (String.IsNullOrEmpty(txtBxNombre.Text))
            {
                MessageBox.Show("Rellene el campo nombre", "Error en un campo", MessageBoxButton.OK, MessageBoxImage.Error);
                txtBxNombre.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtBxApellidos.Text))
            {
                MessageBox.Show("Rellene el campo apellidos", "Error en un campo", MessageBoxButton.OK, MessageBoxImage.Error);
                txtBxApellidos.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtBxDNI.Text))
            {
                MessageBox.Show("Rellene el campo dni", "Error en un campo", MessageBoxButton.OK, MessageBoxImage.Error);
                txtBxDNI.Focus();
                return false;
            }
            if (cmBxTipo.SelectedItem == null)
            {               
               MessageBox.Show("Rellene el campo tipo", "Error en un campo", MessageBoxButton.OK, MessageBoxImage.Error);
               cmBxTipo.Focus();
               return false;                
            }
            return true;
        }
    }
}
