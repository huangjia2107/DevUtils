using System;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Documents;
using Theme.Adorners;

namespace Theme.Behaviors
{
    public class DragItemsPositionBehavior : Behavior<Panel>
    {
        private MouseElementAdorner _elementAdorner = null;
        private MouseElementAdorner GetElementAdorner(UIElement child)
        {
            if (_elementAdorner == null)
                _elementAdorner = ConstructMouseElementAdorner(child);

            return _elementAdorner;
        }


        private AdornerLayer _rootAdornerLayer = null;
        private AdornerLayer RootAdornerLayer
        {
            get
            {
                if (_rootAdornerLayer == null)
                    _rootAdornerLayer = AdornerLayer.GetAdornerLayer(RootElement);

                if (_rootAdornerLayer == null)
                    throw new Exception("There is no AdornerLayer in RootElement.");

                return _rootAdornerLayer;
            }
        }

        private UIElement _rootElement = null;
        private UIElement RootElement
        {
            get
            {
                if (_rootElement == null)
                {
                    DependencyObject associatedObject = AssociatedObject;
                    for (DependencyObject i = associatedObject; i != null && AdornerLayer.GetAdornerLayer((Visual)i) != null; i = VisualTreeHelper.GetParent(associatedObject))
                    {
                        associatedObject = i;
                    }

                    _rootElement = associatedObject as UIElement;
                }

                return _rootElement;
            }
        }

        #region Override

        protected override void OnAttached()
        {
            AssociatedObject.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.OnMouseLeftButtonDown), false);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.RemoveHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.OnMouseLeftButtonDown));
        }

        #endregion

        #region Event

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (AssociatedObject.Children == null || AssociatedObject.Children.Count == 0)
                return;

            foreach (UIElement child in AssociatedObject.Children)
            {
                var hitResult = VisualTreeHelper.HitTest(child, e.GetPosition(child));
                if (hitResult != null)
                {
                    StartDrag(child);
                    break;
                }
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            HandleDrag(sender as UIElement);
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            (sender as UIElement).ReleaseMouseCapture();
        }

        private void OnLostMouseCapture(object sender, MouseEventArgs e)
        {
            EndDrag(sender as UIElement);
        }

        #endregion

        #region Func

        private MouseElementAdorner ConstructMouseElementAdorner(UIElement child)
        {
            if (child == null)
                return null;

            return new MouseElementAdorner(child, Mouse.GetPosition(child));
        }

        private void StartDrag(UIElement child)
        {
            RootAdornerLayer.Add(GetElementAdorner(child));

            child.Opacity = 0;
            child.CaptureMouse();
            child.MouseMove += OnMouseMove;
            child.LostMouseCapture += this.OnLostMouseCapture;
            child.AddHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(OnMouseLeftButtonUp), false);
        }

        private void HandleDrag(UIElement child)
        {
            GetElementAdorner(child).Update();
            UpdateChildPosition(child);
        }

        private void EndDrag(UIElement child)
        {
            RootAdornerLayer.Remove(GetElementAdorner(child));
            _elementAdorner = null;

            child.Opacity = 1;

            child.MouseMove -= OnMouseMove;
            child.LostMouseCapture -= this.OnLostMouseCapture;
            child.RemoveHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(OnMouseLeftButtonUp));
        }

        private void UpdateChildPosition(UIElement dragedChild)
        {
            var posToChild = Mouse.GetPosition(dragedChild);
            var posToPanel = Mouse.GetPosition(AssociatedObject);
            var frameworkElement = dragedChild as FrameworkElement;

            var childRect = new Rect(posToPanel.X - posToChild.X, posToPanel.Y - posToChild.Y, frameworkElement.ActualWidth, frameworkElement.ActualHeight);

            //find the child which has max overlapping area with dragedChild


            //check the overlapping area whether match the exchanging child condition
        }

        #endregion
    }
}