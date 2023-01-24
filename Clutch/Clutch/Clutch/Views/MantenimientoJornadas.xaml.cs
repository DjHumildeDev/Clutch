﻿using Clutch.Enums;
using Clutch.Models;
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
    /// Lógica de interacción para MantenimientoJornadas.xaml
    /// </summary>
    public partial class MantenimientoJornadas : Window
    {
        private Negocio negocio;
        
        public MantenimientoJornadas(Negocio negocio)
        {
            InitializeComponent();
            this.negocio = negocio;
            ActualizarLista();
        }

        private void ActualizarLista()
        {
            lvJornadas.Items.Clear();
            List<Jornada> jornadas = negocio.ObtenerJornadas();
            foreach(Jornada jornada in jornadas)
            {
                int idEmpleado =jornada.idEmpleado;
                Empleado empleado = negocio.ObtenerEmpleado(idEmpleado);
                string nombre = empleado.nombre;
                string apellidos = empleado.apellidos;

                string nombreCompleto = nombre + " " + apellidos;
                string rol = "sin rol";

                Encargado esEncargado = negocio.ObtenerEncargado(idEmpleado);
                Cocina esCocinero = negocio.ObtenerCocinero(idEmpleado);
                Repartidor esRepartidor = negocio.ObtenerRepartidor(idEmpleado);

                if(esEncargado != null)
                {
                    rol = RolEnum.Encargado.ToString();
                }
                if (esCocinero != null)
                {
                    rol = RolEnum.Cocina.ToString();
                }
                if (esRepartidor != null)
                {
                    rol = RolEnum.Repartidor.ToString();
                }

                
                
                lvJornadas.Items.Add(new JornadaListModel {Rol=rol,NombreCompleto=nombreCompleto,Entrada=jornada.Entrada,Salida=jornada.Salida,pedidos=jornada.pedidos,sueldo=jornada.sueldo,horas=jornada.horas,sueldoHoy=jornada.sueldoHoy});
            }
        }

        private void lvJornadas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void mnJornadasCrear_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void mnJornadasEditar_Click(object sender, RoutedEventArgs e)
        {
            Jornada jornada = new Jornada();
            VerJornada ver = new VerJornada(jornada);
            ver.Owner = this;
            if (ver.ShowDialog() == true)
            {
                negocio.EditarJornada(jornada);
                ActualizarLista();
            }
        }

        private void mnJornadasBorrar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnJornadasSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dtPckSalida_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmBxEmpleado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dtPckEntrada_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}
