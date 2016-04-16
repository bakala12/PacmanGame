using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GameControls.Elements;
using GameControls.Interfaces;

namespace GameControls.Board
{
    /// <summary>
    /// Represents a panel for storing game elements from GameBoard.
    /// </summary>
    public class GameBoardPanel : Canvas
    {
        /// <summary>
        /// Gets or sets the number of rows on the panel.
        /// </summary>
        public uint Rows
        {
            get { return (uint)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the number of columns on the panel.
        /// </summary>
        public uint Columns
        {
            get { return (uint)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        /// <summary>
        /// Gets a size (width and height) of a single cell in the panel.
        /// </summary>
        public double SquareSize
        {
            get { return (double)GetValue(SquareSizeProperty); }
            private set { SetValue(SquareSizeProperty, value); }
        }

        /// <summary>
        /// A DependencyProperty for storing Rows property.
        /// </summary>
        public static readonly DependencyProperty RowsProperty =
           DependencyProperty.Register("Rows", typeof(uint), typeof(GameBoardPanel),
               new FrameworkPropertyMetadata((uint)1, FrameworkPropertyMetadataOptions.AffectsParentMeasure |
                   FrameworkPropertyMetadataOptions.AffectsParentArrange |
                   FrameworkPropertyMetadataOptions.AffectsArrange |
                   FrameworkPropertyMetadataOptions.AffectsMeasure |
                   FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// A DependencyProperty for storing Columns property.
        /// </summary>
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(uint), typeof(GameBoardPanel),
                new FrameworkPropertyMetadata((uint)1, FrameworkPropertyMetadataOptions.AffectsParentMeasure |
                    FrameworkPropertyMetadataOptions.AffectsParentArrange |
                    FrameworkPropertyMetadataOptions.AffectsArrange |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// A DependencyProperty for storing SquareSize property.
        /// </summary>
        public static readonly DependencyProperty SquareSizeProperty =
            DependencyProperty.Register("SquareSize", typeof(double), typeof(GameBoardPanel),
                new FrameworkPropertyMetadata((double)0, FrameworkPropertyMetadataOptions.AffectsParentMeasure |
                    FrameworkPropertyMetadataOptions.AffectsParentArrange |
                    FrameworkPropertyMetadataOptions.AffectsArrange |
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Overrides a default MeasureOverride method which is a part of WPF control's measure system.
        /// This method tries to set the size of GameBoardPanel to be the largest possible depending on the given size.
        /// </summary>
        /// <param name="availableSize">The available size on the screen.</param>
        /// <returns>The requested size of the panel.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            SquareSize = Math.Min(availableSize.Width / Columns, availableSize.Height / Rows);
            return new Size(SquareSize * Columns, SquareSize * Rows);
        }

        /// <summary>
        /// Overrides a default ArrrangeOverride method from Panel class.
        /// This method arrange the elements layout on panel and place them on proper positions.
        /// </summary>
        /// <param name="finalSize">The final size of the panel.</param>
        /// <returns>The final size of the panel.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            SquareSize = Math.Min(finalSize.Width / Columns, finalSize.Height / Rows);
            Size destinationSize = new Size(SquareSize * Columns, SquareSize * Rows);
            foreach (GameElement element in base.InternalChildren.OfType<GameElement>())
            {
                double row = element.X;
                double column = element.Y;
                element.Arrange(new Rect(column * SquareSize, row * SquareSize, SquareSize, SquareSize));
            }
            return destinationSize;
        }
    }
}
