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
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            moto.matricula = txtBxMatricula.Text;
            moto.numero = Convert.ToInt32(txtBxNumero.Text);
            moto.cc = cc;
            moto.estado = estado;

            this.DialogResult = true;
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
