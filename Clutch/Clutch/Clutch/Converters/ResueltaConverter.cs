using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Clutch.Converters
{
    /// <summary>
    /// Funcion que convierte el booleano del campo Incidencia.Resuelta en un string a partir de la herencia IValueConverter
    /// </summary>
    public class ResueltaConverter : IValueConverter
    {
        private string Resuelta = "Resuelta";
        private string NoResuelta = "No resuelta";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && (bool)value)
            {
                return Resuelta;
            }
            return NoResuelta;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Equals(Resuelta);
        }
    }
}
