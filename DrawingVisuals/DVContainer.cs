using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawingVisuals
{
    // Create a host visual derived from the FrameworkElement class.
// This class provides layout, event handling, and container support for
// the child visual objects.
    public class MyVisualHost : Canvas
    {
        // Create a collection of child visual objects.
        private readonly VisualCollection _children;

        public MyVisualHost()
        {
            _children = new VisualCollection(this);
            //_children.Add(CreateDrawingVisualRectangle());            

            // Add the event handler for MouseLeftButtonUp.
            this.MouseLeftButtonUp += MyVisualHost_MouseLeftButtonUp;
        }
        
        public void CreateUIElements(int uiElements, bool drawLines)
        {
            this._children.Clear();
            this._children.Add(this.CreateDrawingVisualRectangle(uiElements, drawLines));
        }

        public void ClearAll()
        {
            this._children.Clear();
        }

        // Create a DrawingVisual that contains a rectangle.
        private DrawingVisual CreateDrawingVisualRectangle(int uiElements,bool drawLines)
        {
            DrawingVisual drawingVisual = new DrawingVisual();

            // Retrieve the DrawingContext in order to create new drawing content.
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            
            
            var row = 0;
            var col = 0;
            Pen blackPen = new Pen(Brushes.Black, 1.0);
            Pen redPen = new Pen(Brushes.Red, 1.0);


            for (int i = 0; i < uiElements; i++)
            {
                if (i%30 == 0)
                {
                    col = 0;
                    row++;
                }
                               

                if (drawLines)
                {
                    var height = 1;
                    var width = 20;
                    blackPen.Freeze();

                    drawingContext.DrawLine(blackPen, new Point(col * width + 10, row * height + 10 + row), new Point(col * width + width, row * height + 10 + row));

                    drawingContext.DrawLine(redPen, new Point(col * width + 12, row * height + 12 + row), new Point(col * width + width, row * height + 10 + row));

                }
                else
                {
                    var height = 20;
                    var width = 20;
                    // Create a rectangle and draw it in the DrawingContext.

                    

                    Rect rect = new Rect(new Point(col * height + 10 , row * width + 10), new Size(height, width));                                        
                    //RectangleGeometry rectangleGeometry = new RectangleGeometry();
                    //rectangleGeometry.Freeze();                    

                    //PolyLineSegment polyLineSegment = new PolyLineSegment();
                    //polyLineSegment.Freeze();

                    //drawingContextBrushes.Red,)

                    LineSegment line = new LineSegment();                    

                    drawingContext.DrawRectangle(Brushes.LightBlue, null, rect);                
                }

                col++;
            }

            drawingContext.DrawText(new FormattedText("Hallo Test", CultureInfo.CurrentCulture,FlowDirection.LeftToRight,new Typeface("Arial"),10,Brushes.Black), new Point(50,50));

            // Persist the drawing content.
            drawingContext.Close();

            return drawingVisual;
        }

        private Path CreateLine()
        {

            


            LineGeometry myLineGeometry = new LineGeometry();
            myLineGeometry.StartPoint = new Point(10, 20);
            myLineGeometry.EndPoint = new Point(100, 130);

            var myPath = new Path();
            myPath.Stroke = Brushes.Black;
            myPath.StrokeThickness = 1;
            myPath.Data = myLineGeometry;

            return myPath;
        }

        // Provide a required override for the VisualChildrenCount property.
        protected override int VisualChildrenCount
        {
            get { return _children.Count; }
        }

        // Provide a required override for the GetVisualChild method.
        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= _children.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _children[index];
        }

        // Capture the mouse event and hit test the coordinate point value against
        // the child visual objects.
        void MyVisualHost_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Retreive the coordinates of the mouse button event.
            var pt = e.GetPosition((UIElement)sender);

            // Initiate the hit test by setting up a hit test result callback method.
            VisualTreeHelper.HitTest(this, null, myCallback, new PointHitTestParameters(pt));
        }

        // If a child visual object is hit, toggle its opacity to visually indicate a hit.
        public HitTestResultBehavior myCallback(HitTestResult result)
        {
            if (result.VisualHit.GetType() == typeof(DrawingVisual))
            {
                if (((DrawingVisual)result.VisualHit).Opacity == 1.0)
                {
                    ((DrawingVisual)result.VisualHit).Opacity = 0.4;
                }
                else
                {
                    ((DrawingVisual)result.VisualHit).Opacity = 1.0;
                }
            }

            // Stop the hit test enumeration of objects in the visual tree.
            return HitTestResultBehavior.Stop;
        }



    }
}
