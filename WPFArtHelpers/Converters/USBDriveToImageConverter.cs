using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ArtWPFHelpers.Converters
{

    [ValueConversion(typeof(DriveInfo), typeof(string))]
    public class USBDriveToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (null == value)
            {
                return null;
            }
            // For a more sophisticated converter, check also the targetType and react accordingly..
            DriveInfo di = (DriveInfo)value;

            switch (di.DriveType)
            {
                case DriveType.Fixed:
                    return "/Demineralizer;component/Resources/externalhdd.ico";

                case DriveType.Removable:
                    return "/Demineralizer;component/Resources/memorystick.ico";


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