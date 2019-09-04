using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ArtWPFHelpers.Converters
{
    [ValueConversion(typeof(bool), typeof(string))]
    public sealed class BooleanToStringConverter : IValueConverter    
    {
        public string TrueStateString
        {
            get;
            set;
        }

        public string FalseStateString
        {
            get;
            set;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? TrueStateString : FalseStateString;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            
            return null;
        }
    }
}
