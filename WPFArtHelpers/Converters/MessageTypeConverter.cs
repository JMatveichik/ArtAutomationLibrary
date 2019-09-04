using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using ArtWPFHelpers.Models;

namespace ArtWPFHelpers.Converters
{

    [ValueConversion(typeof(BanchMessageType), typeof(string))]
    public class MessageTypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (null == value)
            {
                return null;
            }
            // For a more sophisticated converter, check also the targetType and react accordingly..
            BanchMessageType state = (BanchMessageType)value;

            switch (state)
            {
                case BanchMessageType.Information:
                    return "/Demineralizer;component/Resources/info.ico";

                case BanchMessageType.Warning:
                    return "/Demineralizer;component/Resources/warning.ico";

                case BanchMessageType.Alarm:
                    return "/Demineralizer;component/Resources/error.ico";

                case BanchMessageType.Started:
                    return "/Demineralizer;component/Resources/bullet_triangle_green.ico";

                case BanchMessageType.Successfully:
                    return "/Demineralizer;component/Resources/check.ico";

                default:
                    return null;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

    }
}
