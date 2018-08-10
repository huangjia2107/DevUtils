﻿using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using Utils.Native;

namespace Theme.Adorners
{
    public class MouseElementAdorner : Adorner
    {
        Point _posRelative;
        Size _adornerElementSize;
        ImageBrush _imageBrush = null;

        public MouseElementAdorner(FrameworkElement adornedElement, Point posRelative)
            : base(adornedElement)
        {
            IsHitTestVisible = false;

            _posRelative = posRelative;
            _adornerElementSize = new Size(adornedElement.ActualWidth, adornedElement.ActualHeight);
            _imageBrush = ConstructImageBrush(adornedElement);
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

        private ImageBrush ConstructImageBrush(FrameworkElement frameworkElement)
        {
            return new ImageBrush(RenderVisualToBitmap(frameworkElement, (int)frameworkElement.ActualWidth, (int)frameworkElement.ActualHeight));
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (AdornedElement != null)
            {
                var screenPos = new Win32.POINT();
                if (Win32.GetCursorPos(ref screenPos))
                {
                    var pos = AdornedElement.PointFromScreen(new Point(screenPos.X, screenPos.Y));
                    var rect = new Rect(new Point(pos.X - _posRelative.X, pos.Y - _posRelative.Y), _adornerElementSize);

                    //System.Diagnostics.Trace.TraceInformation("Adorner Pos = {0},{1},{2},{3}", rect.X, rect.Y, rect.Width, rect.Height);

                    drawingContext.DrawRectangle(_imageBrush, null, rect);
                }
            }
        }
    }
}
