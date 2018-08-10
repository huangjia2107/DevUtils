using System;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Documents;
using Theme.Adorners;
using Utils.Extensions;
using Utils.Native;

namespace Theme.Behaviors
{
    public class DragItemsPositionBehavior : Behavior<Panel>
    {
        private Point _cacheMouseDownPos;
        private UIElement _dragedChild = null;

        private MousePanelAdorner _panelAdorner = null;
        private MousePanelAdorner GetPanelAdorner(UIElement panel, UIElement draggedChild)
        {
            if (_panelAdorner == null)
                _panelAdorner = ConstructMousePanelAdorner(panel, draggedChild);

            return _panelAdorner;
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

        private bool? _isFromItemsPanelTemplate = null;
        private bool IsFromItemsPanelTemplate
        {
            get
            {
                if (!_isFromItemsPanelTemplate.HasValue)
                    _isFromItemsPanelTemplate = VisualTreeHelper.GetParent(AssociatedObject) is ItemsPresenter;

                return _isFromItemsPanelTemplate.Value;
            }
        }

        private ItemsControl _itemsContainer = null;
        private ItemsControl ItemsContainer
        {
            get
            {
                if (_itemsContainer == null)
                {
                    DependencyObject associatedObject = AssociatedObject;
                    for (DependencyObject i = associatedObject; i != null; i = VisualTreeHelper.GetParent(associatedObject))
                    {
                        if (i is ItemsControl)
                        {
                            _itemsContainer = i as ItemsControl;
                            break;
                        }

                        associatedObject = i;
                    }
                }

                return _itemsContainer;
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
            AssociatedObject.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnMouseLeftButtonDown), true);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.RemoveHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnMouseLeftButtonDown));
        }

        #endregion

        #region Event

        private void OnQueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            _panelAdorner.Update();
            MoveChild(_dragedChild);
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (AssociatedObject.Children == null || AssociatedObject.Children.Count == 0)
                return;

            foreach (UIElement child in AssociatedObject.Children)
            {
                _cacheMouseDownPos = e.GetPosition(child);

                var hitResult = VisualTreeHelper.HitTest(child, _cacheMouseDownPos);
                if (hitResult != null)
                {
                    StartDrag(child);
                    break;
                }
            }
        }

        #endregion

        #region Func

        private MousePanelAdorner ConstructMousePanelAdorner(UIElement panel, UIElement draggedChild)
        {
            if (panel == null || draggedChild == null)
                return null;

            return new MousePanelAdorner(panel, draggedChild as FrameworkElement, Mouse.GetPosition(draggedChild));
        }

        private void StartDrag(UIElement dragedChild)
        {
            _dragedChild = dragedChild;

            RootAdornerLayer.Add(GetPanelAdorner(AssociatedObject, _dragedChild));
            _dragedChild.Opacity = 0;

            DragDrop.AddQueryContinueDragHandler(AssociatedObject, OnQueryContinueDrag);
            DragDrop.DoDragDrop(AssociatedObject, _dragedChild, DragDropEffects.Move);

            EndDrag();
        }

        private void EndDrag()
        { 
            DragDrop.RemoveQueryContinueDragHandler(AssociatedObject, OnQueryContinueDrag);

            RootAdornerLayer.Remove(_panelAdorner);
            _panelAdorner = null;

            _dragedChild.Opacity = 1;
            _dragedChild = null;
        }

        private void MoveChild(UIElement dragedChild)
        {
            var screenPos = new Win32.POINT();
            if (!Win32.GetCursorPos(ref screenPos))
                return;

            var posToPanel = AssociatedObject.PointFromScreen(new Point(screenPos.X, screenPos.Y));
            var dragedElement = dragedChild as FrameworkElement;

            var childRect = new Rect(posToPanel.X - _cacheMouseDownPos.X, posToPanel.Y - _cacheMouseDownPos.Y, dragedElement.ActualWidth, dragedElement.ActualHeight);

            //find the child which has max overlapping area with dragedChild
            Size? maxOverlapSize = null;
            FrameworkElement maxOverlapChild = null;
            foreach (FrameworkElement fe in AssociatedObject.Children)
            {
                if (fe == dragedElement)
                    continue;

                var sp = fe.TranslatePoint(new Point(), AssociatedObject);
                var overlapSize = GetOverlapSize(new Rect(sp, new Point(sp.X + fe.ActualWidth, sp.Y + fe.ActualHeight)), childRect);

                if (overlapSize.IsEmpty)
                    continue;

                if (maxOverlapSize == null || (overlapSize.Width * overlapSize.Height).GreaterThan(maxOverlapSize.Value.Width * maxOverlapSize.Value.Height))
                {
                    maxOverlapSize = overlapSize;
                    maxOverlapChild = fe;
                }
            }

            //check the overlapping area whether match the exchanging child condition
            if (!maxOverlapSize.HasValue || maxOverlapSize.Value.IsEmpty)
                return;

            if (maxOverlapSize.Value.Width.GreaterThanOrClose(maxOverlapChild.ActualWidth / 2) && maxOverlapSize.Value.Height.GreaterThanOrClose(maxOverlapChild.ActualHeight / 2))
            {
                var targetIndex = AssociatedObject.Children.IndexOf(maxOverlapChild);

                if (IsFromItemsPanelTemplate)
                {
                    var sourceIndex = AssociatedObject.Children.IndexOf(dragedChild);
                    var sourceItem = ItemsContainer.Items[sourceIndex];

                    ItemsContainer.Items.RemoveAt(sourceIndex);
                    ItemsContainer.Items.Insert(targetIndex, sourceItem);

                    //Update
                    _dragedChild = AssociatedObject.Children[targetIndex];
                    _dragedChild.Opacity = 0;
                }
                else
                {
                    AssociatedObject.Children.Remove(dragedChild);
                    AssociatedObject.Children.Insert(targetIndex, dragedChild);
                }
            }
        }

        private Size GetOverlapSize(Rect rect1, Rect rect2)
        {
            return Rect.Intersect(rect1, rect2).Size;
        }

        #endregion
    }
}
