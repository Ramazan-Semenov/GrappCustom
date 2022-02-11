using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfApp3
{
   public class TreeViewLineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TreeViewItem item = (TreeViewItem)value;
            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);
            return ic.ItemContainerGenerator.IndexFromContainer(item) == ic.Items.Count - 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
   public class TreeViewLineConverter1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TreeViewItem item = (System.Windows.Controls.TreeViewItem)value;
            TreeView ParentTreeView = ParentOfType<TreeView>(item);
            TreeViewItem tvi = (TreeViewItem)ParentTreeView.ItemContainerGenerator.ContainerFromIndex(0);
            return item == tvi;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public T ParentOfType<T>(DependencyObject element) where T : DependencyObject
        {
            if (element == null)
                return default(T);
            else
                return Enumerable.FirstOrDefault<T>(Enumerable.OfType<T>((System.Collections.IEnumerable)GetParents(element)));
        }

        public IEnumerable<DependencyObject> GetParents(DependencyObject element)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            while ((element = GetParent(element)) != null)
                yield return element;
        }

        private DependencyObject GetParent(DependencyObject element)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(element);
            if (parent == null)
            {
                FrameworkElement frameworkElement = element as FrameworkElement;
                if (frameworkElement != null)
                    parent = frameworkElement.Parent;
            }
            return parent;
        }
    }
}
