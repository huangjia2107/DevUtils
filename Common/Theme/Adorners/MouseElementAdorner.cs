using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;
using System.Windows.Markup;
using Utils.Native;

namespace Theme.Adorners
{
    public class MouseElementAdorner : Adorner
    {
        Point _posRelative;
        FrameworkElement _draggedElement = null;
        VisualBrush _visualBrush = null;

        private bool _snapShotEnabled = false;
        public bool SnapShotEnabled
        {
            get { return _snapShotEnabled; }
            set
            {
                _snapShotEnabled = value;
                _visualBrush = ConstructVisualBrush();
            }
        }

        public MouseElementAdorner(UIElement element, Point posRelative)
            : base(element)
        {
            IsHitTestVisible = false;
            SnapShotEnabled = false;

            _posRelative = posRelative;
            _draggedElement = element as FrameworkElement;
        }

        public void Update()
        {
            InvalidateVisual();
        }

        private UIElement CloneUIElement(UIElement element)
        {
            return XamlReader.Parse(XamlWriter.Save(element)) as UIElement;
        }

        private VisualBrush ConstructVisualBrush()
        {
            return new VisualBrush(_snapShotEnabled ? CloneUIElement(_draggedElement) : _draggedElement);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (_draggedElement != null)
            {
                var screenPos = new Win32.POINT();
                if (Win32.GetCursorPos(ref screenPos))
                {
                    var pos = _draggedElement.PointFromScreen(new Point(screenPos.X, screenPos.Y));

                    var rect = new Rect(
                        pos.X - _posRelative.X,
                        pos.Y - _posRelative.Y,
                        _draggedElement.ActualWidth,
                        _draggedElement.ActualHeight);

                    if (_visualBrush == null)
                        _visualBrush = ConstructVisualBrush();

                    drawingContext.DrawRectangle(_visualBrush, new Pen(Brushes.Transparent, 0), rect);
                }
            }
        }
    }
}
