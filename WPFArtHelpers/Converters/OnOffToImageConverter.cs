using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace ArtWPFHelpers.Converters
{
    [ValueConversion(typeof(bool), typeof(string))]

    public class OnOffToImageConverter : IValueConverter
    {
        public string OffImage
        {
            get;
            set;
        }
        public string OnImage        {
            get;
            set;
        } 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (null == value)
            {
                return null;
            }
            // For a more sophisticated converter, check also the targetType and react accordingly..
            bool state = (bool)value;
            if ((bool)value == true)
                return OnImage;
            else
                return OffImage;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
