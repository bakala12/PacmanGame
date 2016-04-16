using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
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
    [ContentProperty("Children")]
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
            Children = this.ElementsPanel.Children;
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
        /// Initializes a new instance of GameBoard with the specified number of rows and columns 
        /// and with the colleciton of elements.
        /// </summary>
        /// <param name="rows">The number of rows of GameBoard.</param>
        /// <param name="columns">The number of columns of GameBoard.</param>
        /// <param name="elements"></param>
        public GameBoard(uint rows, uint columns, IEnumerable<GameElement> elements) : this(rows, columns)
        {
            foreach (var gameElement in elements)
            {
                Children.Add(gameElement);
            }
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
        /// Gets the elements on the board.
        /// </summary>
        public IEnumerable<GameElement> Elements => Children.OfType<GameElement>(); 

        /// <summary>
        /// Gets the children of the GameBoard.
        /// </summary>
        public UIElementCollection Children
        {
            get { return (UIElementCollection) GetValue(ChildrenProperty.DependencyProperty); }
            private set { SetValue(ChildrenProperty, value);}
        }

        /// <summary>
        /// Readonly DependencyProperty for storing Children property.
        /// </summary>
        public static readonly DependencyPropertyKey ChildrenProperty =
            DependencyProperty.RegisterReadOnly("Children", typeof (UIElementCollection), typeof (GameBoard),
                new FrameworkPropertyMetadata());

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
