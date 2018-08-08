using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Input;

namespace Theme.Behaviors
{
    public class DragItemsPositionBehavior : Behavior<Panel>
    {
        protected override void OnAttached()
        {
            AssociatedObject.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnMouseLeftButtonDown), false);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.RemoveHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnMouseLeftButtonDown));
        }

        #region Event

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (AssociatedObject.Children == null || AssociatedObject.Children.Count == 0)
                return; 
        }

        #endregion
    }
}
