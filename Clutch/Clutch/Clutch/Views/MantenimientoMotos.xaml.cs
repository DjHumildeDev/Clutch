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
    /// Lógica de interacción para MantenimientoMotos.xaml
    /// </summary>
    public partial class MantenimientoMotos : Window
    {
        private Negocio negocio;
        public MantenimientoMotos(Negocio negocio)
        {
            InitializeComponent();
            this.negocio = negocio;
            ActualizarLista();
        }

        private void mnMotosSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void mnMotosCrear_Click(object sender, RoutedEventArgs e)
        {
            Moto nueva = new Moto();
            VerMoto ventana = new VerMoto(nueva);
            ventana.Owner = this;
            if (ventana.ShowDialog() == true)
            {
                negocio.CrearMoto(nueva);
                ActualizarLista();
            }
        }

        private void ActualizarLista()
        {
            lvMotos.Items.Clear();
            List<Moto> motos = negocio.ObtenerMotos();
            foreach (Moto moto in motos)
            {
                lvMotos.Items.Add(moto);
            }
        }

        private void mnMotosEditar_Click(object sender, RoutedEventArgs e)
        {
            if (lvMotos.SelectedItems.Count == 1)
            {
                Moto ver = (Moto)((ListViewItem)lvMotos.SelectedItems[0]).Tag;
                VerMoto ventana = new VerMoto(ver);
                ventana.Owner = this;
                if (ventana.ShowDialog() == true)
                {
                    negocio.EditarMoto(ver);
                    ActualizarLista();
                }
                else
                {
                    MessageBox.Show("Necesitas seleccionar un elemento", "No hay seleccion", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } 

        }

        private void mnMotosBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (lvMotos.SelectedItems.Count == 1)
            {
                //Proceso de identificacion

                MessageBoxResult result = MessageBox.Show("¿Esta seguro que quiere borrar esta  Jornada?", "¡Atencion!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    Moto borrar = (Moto)((ListViewItem)lvMotos.SelectedItems[0]).Tag;

                    if (negocio.BorrarMoto(borrar))
                    {
                        MessageBox.Show("Se ha borrado la jornada correctamente", "Jornada Borrada", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar la jornada", "Algo salio mal", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    ActualizarLista();

                }
            }
            else
            {
                MessageBox.Show("Necesitas seleccionar un elemento", "No hay seleccion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void mnMotoAsignar_Click(object sender, RoutedEventArgs e)
        {
            Repartidor repar = new Repartidor();
            Moto moto = new Moto();
            AsignarMoto ventana = new AsignarMoto(repar,moto,negocio);
            ventana.Owner = this;
            if (ventana.ShowDialog() == true)
            {
                negocio.AsignarMoto(repar, moto);
            }
        }
    }
}
