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
    /// Lógica de interacción para AsignarMoto.xaml
    /// </summary>
    public partial class AsignarMoto : Window
    {
        private Negocio negocio;
        private List<Repartidor> repartidores;
        private List<Moto> motos;
        private Moto moto;
        public Repartidor repartidor { get; set; }
        private string cc;

        public AsignarMoto(Repartidor repartidor,Moto moto,Negocio negocio) 
        {
            InitializeComponent();
            cmBxMoto.IsEnabled = false;
            this.negocio = negocio;
            this.moto = moto;
            this.repartidor = repartidor;
            repartidores = negocio.ObtenerRepartidores();
            motos = negocio.ObtenerMotos();
            RellenarCombos();
            if (moto.estado != null)
            {
                if (moto.cc.Equals("49"))
                {
                    cmBxCentimetros.SelectedIndex = 0;
                    RellenarMotos("49");
                }
                if (moto.cc.Equals("125"))
                {
                    cmBxCentimetros.SelectedIndex = 1;
                    RellenarMotos("125");
                }

                foreach(ComboBoxItem item in cmBxMoto.Items)
                {
                    if (item.Tag.Equals(moto))
                    {
                        cmBxMoto.SelectedItem = item;
                    }
                }
            }
              
        }

        private void RellenarCombos()
        {
            foreach (Repartidor rep in repartidores)
            {
                ComboBoxItem item = new ComboBoxItem();
                Empleado empleado = negocio.ObtenerEmpleado(rep.idEmpleado);
                item.Content = empleado.nombre + " " + empleado.apellidos;
                item.Tag = rep;
                cmBxEmpleado.Items.Add(item);
            }
          
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
       
            this.DialogResult = false;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos())
            {
                if (cmBxCentimetros.SelectedItem != null)
                {
                    cc = (string)((ComboBoxItem)cmBxCentimetros.SelectedItem).Tag;
                }
                if (cmBxMoto.SelectedItem != null)
                {
                    moto = (Moto)((ComboBoxItem)cmBxMoto.SelectedItem).Tag;
                }

                if (cmBxEmpleado.SelectedItem != null)
                {
                    repartidor = (Repartidor)((ComboBoxItem)cmBxEmpleado.SelectedItem).Tag;
                }
                this.DialogResult = true;
            }
           
        }

        private bool ValidarCampos()
        {
            if (cmBxCentimetros.SelectedItem == null)
            {
                MessageBox.Show("Seleccione la cilindrada", "Error en un campo", MessageBoxButton.OK, MessageBoxImage.Error);

                cmBxCentimetros.Focus();
                return false;
            }
            if (cmBxEmpleado.SelectedItem == null)
            {
                MessageBox.Show("Seleccione el empleado", "Error en un campo", MessageBoxButton.OK, MessageBoxImage.Error);

                cmBxEmpleado.Focus();
                return false;
            }
            if(cmBxMoto.SelectedItem == null)
            {
                MessageBox.Show("Seleccione la moto", "Error en un campo", MessageBoxButton.OK, MessageBoxImage.Error);

                cmBxMoto.Focus();
                return false;
            }
            return true;
        }

        private void cmBxMoto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmBxMoto.SelectedItem != null)
            {
                moto = (Moto)((ComboBoxItem)cmBxMoto.SelectedItem).Tag;
                if (moto.cc.Equals("49"))
                {
                    cmBxCentimetros.SelectedIndex = 0;
                }
                if (moto.cc.Equals("125"))
                {
                    cmBxCentimetros.SelectedIndex = 1;
                }
            }
        }

        private void cmBxCentimetros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmBxCentimetros.SelectedItem != null)
            {
                if (cmBxCentimetros.SelectedIndex == 0)
                {
                    RellenarMotos("49");
                }
                if (cmBxCentimetros.SelectedIndex == 1)
                {
                    RellenarMotos("125");
                }
            }
        }

        private void RellenarMotos(string cc)
        {
            cmBxMoto.Items.Clear();

            if (cc.Equals("49"))
            {
                foreach(Moto moto in motos)
                {
                    if (moto.cc.Equals("49"))
                    {
                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = moto.numero.ToString();
                        item.Tag = moto;
                        cmBxMoto.Items.Add(item);
                    }
                                                   
                }
            }
            if (cc.Equals("125"))
            {
                foreach (Moto moto in motos)
                {
                    if (moto.cc.Equals("125"))
                    {
                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = moto.numero.ToString();
                        item.Tag = moto;
                        cmBxMoto.Items.Add(item);
                    }

                }
            }

            cmBxMoto.IsEnabled = true;
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
