using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevUtils.Models;
using UtilModelService;
using Utils.Extensions;

namespace DevUtils.Views
{
    /// <summary>
    /// Interaction logic for UtilsControl1.xaml
    /// </summary>
    public partial class UtilControl : UserControl
    {
        public UtilControl()
        {
            InitializeComponent();
        }

        private void ScrollViewer_OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (VisualTreeHelper.HitTest(AllUtilScrollViewer, Mouse.GetPosition(AllUtilScrollViewer)) == null)
                return;

            if (MiniUtilsGrid.TranslatePoint(new Point(), AllUtilScrollViewer).Y.LessThanOrClose(13))
            {
                UtilTypeListBox.SelectedValue = null;
                MiniUtilsRadioButton.IsChecked = true;
                return;
            }

            MiniUtilsRadioButton.IsChecked = false;
            for (var i = 0; i < AllUtilsItemsControl.Items.Count; i++)
            {
                var item = AllUtilsItemsControl.ItemContainerGenerator.ContainerFromIndex(i) as FrameworkElement;

                if (item.TranslatePoint(new Point(), AllUtilScrollViewer).Y.LessThanOrClose(0))
                {
                    if (item.DataContext != null && item.DataContext is ClassifiedUtil)
                    {
                        var classifiedUtil = item.DataContext as ClassifiedUtil;
                        UtilTypeListBox.SelectedValue = classifiedUtil.Type;
                    }
                }
            }
        }

        private void UtilTypeListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MiniUtilsRadioButton == null
                || !(UtilTypeListBox.SelectedValue is UtilType)
                || VisualTreeHelper.HitTest(UtilTypeListBox, Mouse.GetPosition(UtilTypeListBox)) == null)
                return;

            var source = AllUtilsItemsControl.ItemsSource as IEnumerable<ClassifiedUtil>;
            var util = source.FirstOrDefault(u => u.Type == (UtilType)UtilTypeListBox.SelectedValue);
            if (source == null || util == null)
                return;

            MiniUtilsRadioButton.IsChecked = false;

            var selectedIndex = util.Index;
            if (selectedIndex == 0)
            {
                AllUtilScrollViewer.ScrollToTop();
            }
            else
            {
                var item = AllUtilsItemsControl.ItemContainerGenerator.ContainerFromIndex(selectedIndex) as FrameworkElement;
                if (item == null)
                    return;

                var yOffset = item.TranslatePoint(new Point(), AllUtilScrollViewer).Y;

                AllUtilScrollViewer.ScrollToVerticalOffset(AllUtilScrollViewer.VerticalOffset + yOffset-2);
            }
        }

        private void MiniUtilsRadioButton_OnClick(object sender, RoutedEventArgs e)
        {
            UtilTypeListBox.SelectedValue = null;

            var yOffset = MiniUtilsGrid.TranslatePoint(new Point(), AllUtilScrollViewer).Y;

            AllUtilScrollViewer.ScrollToVerticalOffset(AllUtilScrollViewer.VerticalOffset + yOffset - 2);

           // AllUtilScrollViewer.ScrollToBottom();
        } 
    }
}
