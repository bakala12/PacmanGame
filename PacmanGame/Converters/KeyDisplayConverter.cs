using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace PacmanGame.Converters
{
    /// <summary>
    /// Converter for displaying player's control keys. 
    /// </summary>
    public class KeyDisplayConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool active = values[0] as bool? ?? false;
            Key key = values[1] as Key? ?? Key.None;
            return !active ? key.ToString() : "...";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
