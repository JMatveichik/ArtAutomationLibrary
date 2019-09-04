using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ArtWPFHelpers.Converters
{
    
    [ValueConversion(typeof(long), typeof(string))]
    public class BytesCountToConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (null == value)
            {
                return null;
            }

            
            string[]  suffix = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

            int ind = 0;
            double b = (long)value; 
            do
            {
                b /= 1024;
                ind++;

            } while (b > 1024);


            return string.Format("{0:0.00} {1}", b, suffix[ind]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
