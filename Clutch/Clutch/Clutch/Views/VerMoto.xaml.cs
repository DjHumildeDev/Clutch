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
    /// Lógica de interacción para VerMoto.xaml
    /// </summary>
    public partial class VerMoto : Window
    {
        private Moto moto;
        private string cc;
        private string estado;
        public VerMoto(Moto moto)
        {
            InitializeComponent();
            
            this.moto = moto;
            RellenarCampos();

        }

        private void RellenarCampos()
        {
            txtBxMatricula.Text = moto.matricula;
            txtBxNumero.Text = moto.numero.ToString();
            if (moto.cc != null)
            {
                if (moto.cc.Equals("49"))
                {
                    cmbBxCC.SelectedIndex = 0;
                }
                if (moto.cc.Equals("125"))
                {
                    cmbBxCC.SelectedIndex = 1;
                }

                switch (moto.estado)
                {
                    case "Disponible":
                        cmbBxEstado.SelectedIndex = 0;
                        break;
                    case "Ocupada":
                        cmbBxEstado.SelectedIndex = 1;
                        break;
                    case "En taller":
                        cmbBxEstado.SelectedIndex = 2;
                        break;
                    case "Averiada":
                        cmbBxEstado.SelectedIndex = 3;
                        break;
                }
            }
           
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (ComprobarCampos())
            {
                moto.matricula = txtBxMatricula.Text;
                moto.numero = Convert.ToInt32(txtBxNumero.Text);
                moto.cc = cc;
                moto.estado = estado;

                this.DialogResult = true;
            }         
        }

        private bool ComprobarCampos()
        {
            int numero;
            if(int.TryParse(txtBxNumero.Text,out numero))
            {
                return true;
            }
            else
            {
                MessageBox.Show("El numero de moto no puede contener letras vuelva a introducirlo","Error en un campo",MessageBoxButton.OK,MessageBoxImage.Error);
                txtBxNumero.Text = String.Empty;
                txtBxNumero.Focus();
                return false;
            }
        }

        private void cmbBxEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            estado = (string)((ComboBoxItem)combo.SelectedItem).Tag;
        }

        private void cmbBxCC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            cc = (string)((ComboBoxItem)combo.SelectedItem).Tag;
        }
    }
}
