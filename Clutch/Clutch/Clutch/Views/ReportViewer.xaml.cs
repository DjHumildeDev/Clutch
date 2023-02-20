﻿using Clutch.Reports;
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
    /// Lógica de interacción para ReportViewer.xaml
    /// </summary>
    public partial class ReportViewer : Window
    {
        private Generador generador;
        public ReportViewer()
        {
            InitializeComponent();                       
            rptViewer.Load += ReportViewer_Load;
        }



        private void ReportViewer_Load(object sender, EventArgs e)
        {          
            this.rptViewer.RefreshReport();
        }
    }
}