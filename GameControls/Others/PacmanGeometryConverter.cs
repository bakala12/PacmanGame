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
    /// <summary>
    /// A converter that converts Player's dimentions, direction and angle to a geomerty of the Pacman.
    /// This allows appropriate Player styling and animating the mouse move of the Pacman. 
    /// </summary>
    public class PacmanGeometryConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts the specified values to the specified object of the requested type.
        /// </summary>
        /// <param name="values">The values which should be converted.</param>
        /// <param name="targetType">The requested result type of the conversion.</param>
        /// <param name="parameter">The parameter of the converter.</param>
        /// <param name="culture">The information of the culture.</param>
        /// <returns>The result of the conversion.</returns>
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
            ArcSegment arc = new ArcSegment(B, new Size(radius,radius), 0, true, SweepDirection.Clockwise, true);
            PathFigure f = new PathFigure(center, new PathSegment[] {l1, arc}, true);
            geometry.Figures = new PathFigureCollection();
            geometry.Figures.Add(f);
            return geometry;
        }

        /// <summary>
        /// Defines back conversion.
        /// </summary>
        /// <param name="value">The value which should be converted back.</param>
        /// <param name="targetTypes">The requested result types of the backward conversion.</param>
        /// <param name="parameter">The parameter of the converter.</param>
        /// <param name="culture">The information of the culture.</param>
        /// <returns>The result of the backward conversion.</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
