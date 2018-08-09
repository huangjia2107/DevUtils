using System;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Documents;
using Theme.Adorners;

namespace Theme.Behaviors
{
    public class DragElementAdornerBehavior : Behavior<FrameworkElement>
    {
        private bool _hiddenWhileDragging = false;
        public bool HiddenWhileDragging
        {
            get { return _hiddenWhileDragging; }
            set
            {
                _hiddenWhileDragging = value;

                _elementAdorner = ConstructMouseElementAdorner();
            }
        }

        private MouseElementAdorner _elementAdorner = null;
        private MouseElementAdorner ElementAdorner
        {
            get
            {
                if (_elementAdorner == null)
                    _elementAdorner = ConstructMouseElementAdorner();

                return _elementAdorner;
            }
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
            AssociatedObject.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnMouseLeftButtonDown), false);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.RemoveHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnMouseLeftButtonDown));
        }

        #endregion

        #region Event

        public event MouseEventHandler DragBegun;
        public event MouseEventHandler DragFinished;
        public event MouseEventHandler Dragging;

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StartDrag();

            if (this.DragBegun != null)
                this.DragBegun(this, e);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            HandleDrag();

            if (this.Dragging != null)
                this.Dragging(this, e);
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AssociatedObject.ReleaseMouseCapture();
        }

        private void OnLostMouseCapture(object sender, MouseEventArgs e)
        {
            EndDrag();

            if (this.DragFinished != null)
                this.DragFinished(this, e);
        }

        #endregion

        #region Func

        private MouseElementAdorner ConstructMouseElementAdorner()
        {
            if (AssociatedObject == null)
                return null;

            return new MouseElementAdorner(AssociatedObject, Mouse.GetPosition(AssociatedObject));
        }

        private void StartDrag()
        {
            RootAdornerLayer.Add(ElementAdorner);

            AssociatedObject.Opacity = _hiddenWhileDragging ? 0 : 1;

            AssociatedObject.CaptureMouse();
            AssociatedObject.MouseMove += OnMouseMove;
            AssociatedObject.LostMouseCapture += this.OnLostMouseCapture;
            AssociatedObject.AddHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(OnMouseLeftButtonUp), false);
        }

        private void HandleDrag()
        {
            ElementAdorner.Update();
            //RootAdornerLayer.Update();
        }

        private void EndDrag()
        {
            RootAdornerLayer.Remove(ElementAdorner);
            _elementAdorner = null;

            AssociatedObject.Opacity = 1;

            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.LostMouseCapture -= this.OnLostMouseCapture;
            AssociatedObject.RemoveHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(OnMouseLeftButtonUp));
        }

        #endregion
    }
}
