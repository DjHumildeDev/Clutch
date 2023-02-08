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
        public ReportViewer()
        {
            InitializeComponent();
            rptViewer.Load += ReportViewer_Load;
        }

        private bool _isReportVieweLoaded;

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportVieweLoaded)
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                ClutchDbDataSet dataset = new ClutchDbDataSet();

                dataset.BeginInit();
                
                reportDataSource1.Name = "DataSetIncidencias"; //Name of the report dataset in our .RDLC file
                reportDataSource1.Value = dataset.incidencias;
                this.rptViewer.LocalReport.DataSources.Add(reportDataSource1);
                this.rptViewer.LocalReport.ReportEmbeddedResource = "Clutch.Reports.rptIncidencia.rdlc";

                dataset.EndInit();

                
                rptViewer.RefreshReport();
                _isReportVieweLoaded = true;
            }
        }
    }
}
