﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Lógica de interacción para VerJornada.xaml
    /// </summary>
    public partial class VerJornada : Window
    {
        private List<Empleado> empleados;
        private Jornada jornada;
        private Empleado empleado;

        private bool salida,entrada,pedidos;

        public VerJornada(Jornada jornada, Empleado empleado, List<Empleado> empleados)
        {
            InitializeComponent();
            entrada = true;
            this.empleados = empleados;
            this.jornada = jornada;
            this.empleado = empleado;


            InicializarCombo();
        }

        private void InicializarCombo()
        {
            foreach(Empleado emp in empleados)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = emp.nombre + " " + emp.apellidos;
                item.Tag = emp;
                cmBxEmpleado.Items.Add(item);
            }
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            jornada.Entrada = (DateTime)TPickerEntrada.Value;
            jornada.horas = Convert.ToDouble(txtBxHoras.Text);
            jornada.sueldoHoy = Convert.ToDouble(txtBxSueldoHoy.Text);
            jornada.idEmpleado = empleado.id;
            jornada.pedidos = Convert.ToInt32(txtBxPedidos.Text);
            if (TPickerSalida.IsEnabled == true)
            {
                jornada.Salida = (DateTime)TPickerSalida.Value;
            }

            this.DialogResult = true;
        }

        private void cmBxEmpleado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmBxEmpleado.SelectedItem != null)
            {
                empleado = (Empleado)((ComboBoxItem)cmBxEmpleado.SelectedItem).Tag;
            }        
        }

        private void chBxSalida_Click(object sender, RoutedEventArgs e)
        {
            if(chBxSalida.IsChecked == true)
            {
                TPickerSalida.IsEnabled = true;
            }
            else
            {
                TPickerSalida.IsEnabled = false;
                salida = false;
                txtBxSueldoHoy.Text = "0";
                txtBxHoras.Text = "0";
            }
        }

        private void TPickerEntrada_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ComprobarCamposSueldo();
        }

        private void TPickerSalida_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            salida = true;
            ComprobarCamposSueldo();
        }

        private void txtBxPedidos_TextChanged(object sender, TextChangedEventArgs e)
        {
            pedidos = true;
            ComprobarCamposSueldo();
        }

        private void ComprobarCamposSueldo()
        {
            if (pedidos == true && entrada == true && salida == true)
            {
                RellenarHorasSueldo();
            }
        }

        private void RellenarHorasSueldo()
        {
            double horas = 0, sueldo = 0;
            DateTime salida = new DateTime();
            DateTime entrada = new DateTime();
            int pedidos = 0;

            if (!(String.IsNullOrEmpty(txtBxPedidos.Text)))
            {
                pedidos = Convert.ToInt32(txtBxPedidos.Text);
                salida = (DateTime)TPickerSalida.Value;
                entrada = (DateTime)TPickerEntrada.Value;
                this.jornada.sueldo = 12.5;
                TimeSpan result = salida.Subtract(entrada);
                horas = double.Parse(Convert.ToString(result.TotalHours));

                pedidos = Convert.ToInt32(txtBxPedidos.Text);


                sueldo = this.jornada.sueldo * horas + pedidos;

                txtBxSueldoHoy.Text = sueldo.ToString();
                txtBxHoras.Text = horas.ToString();

            }



        }

        private void txtBxPedidos_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
