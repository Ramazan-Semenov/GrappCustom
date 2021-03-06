using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApp3
{
    public class HorzLineConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            TreeViewItem item = (TreeViewItem)value;
            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);
            int index = ic.ItemContainerGenerator.IndexFromContainer(item);

            if ((string)parameter == "left")
            {
                // Either left most or single item
                if (index == 0)
                    return (int)0;
                else
                    return (int)1;
            }
            else // assume "right"
            {
                // Either right most or single item
                if (index == ic.Items.Count - 1)
                    return (int)0;
                else
                    return (int)1;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
