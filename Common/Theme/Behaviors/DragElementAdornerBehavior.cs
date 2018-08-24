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
                    _rootAdornerLayer = AdornerLayer.GetAdornerLayer(AdornerLayerProvider);

                if (_rootAdornerLayer == null)
                    throw new Exception("There is no AdornerLayer in RootElement.");

                return _rootAdornerLayer;
            }
        }

        private UIElement _adornerLayerProvider = null;
        private UIElement AdornerLayerProvider
        {
            get
            {
                if (_adornerLayerProvider == null)
                {
                    DependencyObject topObjectWithAdornerLayer = null;
                    DependencyObject associatedObject = AssociatedObject;

                    for (DependencyObject i = associatedObject; i != null; i = VisualTreeHelper.GetParent(associatedObject))
                    {
                        if (AdornerLayer.GetAdornerLayer((Visual)i) != null)
                            topObjectWithAdornerLayer = i;

                        associatedObject = i;
                    }

                    _adornerLayerProvider = topObjectWithAdornerLayer as UIElement;
                }

                return _adornerLayerProvider;
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

        private void OnQueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            _elementAdorner.Update();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            StartDrag();
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AssociatedObject.PreviewMouseMove += OnMouseMove;
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
            if (_elementAdorner != null)
                return;

            RootAdornerLayer.Add(ElementAdorner);

            AssociatedObject.Opacity = _hiddenWhileDragging ? 0 : 1;

            DragDrop.AddQueryContinueDragHandler(AssociatedObject, OnQueryContinueDrag);
            DragDrop.DoDragDrop(AssociatedObject, AssociatedObject, DragDropEffects.Move);
            DragDrop.RemoveQueryContinueDragHandler(AssociatedObject, OnQueryContinueDrag);

            EndDrag();
        }

        private void EndDrag()
        {
            AssociatedObject.PreviewMouseMove -= OnMouseMove;

            RootAdornerLayer.Remove(ElementAdorner);
            _elementAdorner = null;

            AssociatedObject.Opacity = 1;
        }

        #endregion
    }
}
