﻿using System;
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
        private Repartidor repartidor;
        private string cc;

        public AsignarMoto(Repartidor repartidor,Moto moto,Negocio negocio) 
        {
            InitializeComponent();
            this.negocio = negocio;
            this.moto = moto;
            this.repartidor = repartidor;
            repartidores = negocio.ObtenerRepartidores();
            motos = negocio.ObtenerMotos();
            RellenarCombos();    
        }

        private void RellenarCombos()
        {
            foreach (Repartidor rep in repartidores)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = 
                item.Tag = rep;
                cmBxEmpleado.Items.Add(item);
            }
            foreach (Moto moto in motos)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = moto.numero.ToString();
                item.Tag = moto;
                cmBxMoto.Items.Add(item);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
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
            this.DialogResult = false;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {       
           
            this.DialogResult = true;
        }

       
    }
}
