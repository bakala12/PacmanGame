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
    public class PacmanGeometryConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Direction? direction = values[0] as Direction?;
            double angle = values[1] as double? ?? 0;
            double width = values[2] as double? ?? 0;
            double height = values[3] as double? ?? 0;
            if (!direction.HasValue) return null;
            Point center = new Point(width/2, height/2);
            double radius = (width < height) ? width/2 : height/2;
            angle = (angle*Math.PI/180);
            double x = radius*Math.Cos(angle/2);
            double y = radius*Math.Sin(angle/2);
            Point A = new Point();
            Point B = new Point();
            switch (direction.Value)
            {
                case Direction.Right:
                    A = new Point(center.X + x, center.Y + y);
                    B = new Point(center.X + x, center.Y - y);
                    break;
                case Direction.Left:
                    A = new Point(center.X - x, center.Y - y);
                    B = new Point(center.X - x, center.Y + y);
                    break;
                case Direction.Up:
                    B = new Point(center.X - y, center.Y - x);
                    A = new Point(center.X + y, center.Y - x);
                    break;
                case Direction.Down:
                    B = new Point(center.X + y, center.Y + x);
                    A = new Point(center.X - y, center.Y + x);
                    break;
                default:
                    throw new InvalidOperationException();
            }
            PathGeometry geometry = new PathGeometry();
            LineSegment l1 = new LineSegment(A, true);
            ArcSegment arc = new ArcSegment(B, new Size(radius, radius), 0, true, SweepDirection.Clockwise, true);
            PathFigure f = new PathFigure(center, new PathSegment[] {l1, arc}, true);
            geometry.Figures = new PathFigureCollection();
            geometry.Figures.Add(f);
            return geometry;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
