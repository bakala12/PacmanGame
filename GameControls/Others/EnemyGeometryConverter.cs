using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace GameControls.Others
{
    public class EnemyGeometryConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double width = values[0] as double? ?? 0;
            double height = values[1] as double? ?? 0;
            PathFigure pf = new PathFigure();
            pf.StartPoint = new Point(width, height);
            PathSegmentCollection psc = new PathSegmentCollection();
            psc.Add(new LineSegment() { Point = new Point(18 * width / 20, 5 * height / 20) });
            psc.Add(new ArcSegment() { Point = new Point(2 * width / 20, 5 * height / 20), SweepDirection = SweepDirection.Counterclockwise, RotationAngle = 90, Size = new Size(3 * width / 20, 5 * height / 20) });
            psc.Add(new LineSegment() { Point = new Point(0, height) });
            psc.Add(new LineSegment() { Point = new Point(5 * width / 20, 17 * height / 20) });
            psc.Add(new LineSegment() { Point = new Point(10 * width / 20, height) });
            psc.Add(new LineSegment() { Point = new Point(15 * width / 20, 17 * height/20) });
            pf.Segments = psc;
            pf.IsClosed = true;
            PathFigureCollection pfc = new PathFigureCollection();
            pfc.Add(pf);
            PathGeometry geometry = new PathGeometry();
            geometry.Figures = pfc;
            return geometry;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class EnemyGeometryConverter2 : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double width = values[0] as double? ?? 0;
            double height = values[1] as double? ?? 0;
            GeometryGroup gg = new GeometryGroup();
            GeometryCollection gc = new GeometryCollection();
            gc.Add((PathGeometry)new EnemyGeometryConverter().Convert(values, targetType, parameter, culture));
            gc.Add(new EllipseGeometry() { Center = new Point(6 * width / 20, 6 * height / 20), RadiusX = 3 * width / 20, RadiusY = 3 * height / 20 });
            gc.Add(new EllipseGeometry() { Center = new Point(14 * width / 20, 6 * height / 20), RadiusX = 3 * width / 20, RadiusY = 3 * height / 20 });
            gc.Add(new EllipseGeometry() { Center = new Point(6 * width / 20, 6 * height / 20), RadiusX = 1.5 * width / 20, RadiusY = 1.5 * height / 20 });
            gc.Add(new EllipseGeometry() { Center = new Point(14 * width / 20, 6 * height / 20), RadiusX = 1.5 * width / 20, RadiusY = 1.5 * height / 20 });
            gc.Add(new RectangleGeometry() { Rect = new Rect(new Point(5 * width / 20, 10 * height / 20), new Size(10 * width / 20, 5 * height / 20)), RadiusX = 0, RadiusY = 0 });
            gc.Add(new RectangleGeometry() { Rect = new Rect(new Point(6 * width / 20, 10 * height / 20), new Size(2.5 * width / 20, 2 * height / 20)), RadiusX = 0, RadiusY = 0 });
            gc.Add(new RectangleGeometry() { Rect = new Rect(new Point(9 * width / 20, 13 * height / 20), new Size(2.5 * width / 20, 2 * height / 20)), RadiusX = 0, RadiusY = 0 });
            gc.Add(new RectangleGeometry() { Rect = new Rect(new Point(12 * width / 20, 10 * height / 20), new Size(2.5 * width / 20, 2 * height / 20)), RadiusX = 0, RadiusY = 0 });
            gg.Children = gc;
            gg.FillRule = FillRule.EvenOdd;
            return gg;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
