using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ArtWPFHelpers.Converters
{
    [ValueConversion(typeof(ListBoxItem), typeof(int))]
    public class ListItemToPositionConverter : IValueConverter
    {
        #region Implementation of IValueConverter


        public static T FindAncestor<T>(DependencyObject from) where T : class
        {
            if (from == null)
                return null;

            var candidate = from as T;
            return candidate ?? FindAncestor<T>(VisualTreeHelper.GetParent(from));
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as ListBoxItem;
            if (item != null)
            {
                var lb = FindAncestor<ListBox>(item);
                if (lb != null)
                {
                    var index = lb.Items.IndexOf(item.Content);
                    return index;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
