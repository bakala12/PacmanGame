using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameControls.Elements;
using Block = GameControls.Elements.Block;

namespace GameControls.Board
{
    /// <summary>
    /// Represents a control for the whole game board. 
    /// It contains all graphical GameElements.
    /// </summary>
    public partial class GameBoard : UserControl
    {
        /// <summary>
        /// Initializes a new instance of GameBoard control.
        /// </summary>
        public GameBoard()
        {
            InitializeComponent();
            Rows = 1;
            Columns = 1;
            Background = Brushes.Orange;
        }

        /// <summary>
        /// Initializes a new instance of GameBoard with the specified number of rows and columns.
        /// </summary>
        /// <param name="rows">The number of rows of GameBoard.</param>
        /// <param name="columns">The number of columns of GameBoard.</param>
        public GameBoard(uint rows, uint columns) : this()
        {
            Rows = rows;
            Columns = columns;
        }

        /// <summary>
        /// Gets the number of rows of the board.
        /// </summary>
        public uint Rows
        {
            get { return (uint)GetValue(RowsProperty); }
            private set { SetValue(RowsProperty, value); }
        }

        /// <summary>
        /// Gets the number of columns of the board.
        /// </summary>
        public uint Columns
        {
            get { return (uint)GetValue(ColumnsProperty); }
            private set { SetValue(ColumnsProperty, value); }
        }

        /// <summary>
        /// A DependencyProperty for storing Rows property.
        /// </summary>
        public static readonly DependencyProperty RowsProperty = 
            DependencyProperty.Register("Rows", typeof(uint), typeof(GameBoard), new FrameworkPropertyMetadata());

        /// <summary>
        /// A DependencyProperty for storing Columns property.
        /// </summary>
        public static readonly DependencyProperty ColumnsProperty = 
            DependencyProperty.Register("Columns", typeof(uint), typeof(GameBoard), new FrameworkPropertyMetadata());
    }
}
