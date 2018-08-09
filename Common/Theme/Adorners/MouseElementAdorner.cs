using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;

namespace Theme.Adorners
{
    public class MouseElementAdorner : Adorner
    {
        Point _posRelative;
        FrameworkElement _draggedElement = null;
        ImageBrush _imageBrush = null;

        public MouseElementAdorner(UIElement element, Point posRelative)
            : base(element)
        {
            IsHitTestVisible = false;

            _posRelative = posRelative;
            _draggedElement = element as FrameworkElement;
            _imageBrush = ConstructImageBrush();
        }

        public void Update()
        {
            InvalidateVisual();
        }

        public RenderTargetBitmap RenderVisualToBitmap(Visual vsual, int width, int height)
        {
            var drawingVisual = new DrawingVisual();
            using (var context = drawingVisual.RenderOpen())
            {
                context.DrawRectangle(new VisualBrush(vsual), null, new Rect(0, 0, width, height));
                context.Close();
            }

            RenderTargetBitmap rtb = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Default);
            rtb.Render(drawingVisual);

            return rtb;
        }

        private ImageBrush ConstructImageBrush()
        {
            return new ImageBrush(RenderVisualToBitmap(_draggedElement, (int)_draggedElement.ActualWidth, (int)_draggedElement.ActualHeight));
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (_draggedElement != null)
            {
                var pos = Mouse.GetPosition(_draggedElement);

                var rect = new Rect(
                    pos.X - _posRelative.X,
                    pos.Y - _posRelative.Y,
                    _draggedElement.ActualWidth,
                    _draggedElement.ActualHeight);

                drawingContext.DrawRectangle(_imageBrush, null, rect);
            }
        }
    }
}
