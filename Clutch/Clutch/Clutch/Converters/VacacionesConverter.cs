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
    /// Funcion que convierte el booleano del campo Empleado.vacaciones en un string a partir de la herencia IValueConverter
    /// </summary>
    public class VacacionesConverter : IValueConverter
    {
        private string DeVacaciones = "De vacaciones";
        private string NoVacaciones = "No";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && (bool)value)
            {
                return DeVacaciones;
            }
            return NoVacaciones;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Equals(DeVacaciones);
        }
    }
}
