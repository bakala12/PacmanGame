using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
            get { return (uint) GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value);}
        }

        /// <summary>
        /// A DependencyProperty for storing Rows property.
        /// </summary>
        public static readonly DependencyProperty RowsProperty =
           DependencyProperty.Register("Rows", typeof(uint), typeof(GameBoardPanel),
               new FrameworkPropertyMetadata((uint)1,FrameworkPropertyMetadataOptions.AffectsParentMeasure |
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
    }
}
