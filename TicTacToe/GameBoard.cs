using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TicTacToe
{
    public partial class GameBoard : Form
    {
        private int WindowSize = 500;    // Size of the window
        private static readonly int BoardSize = 4; // Consolidated Rows and Columns into one variable

        private float LineThickness = 10;   // Line thickness in pixels
        private int BordMargin = 30; // Top, right, bottom and left margin of the grid

        private int FieldWidth;  // Width of a single field in the grid
        private int FieldHeight; // Height of a single field in the grid
        private int XOSize; // Size of Xs and Os

        private BoardClickHandler ClickHandler;  // Current board click handler

        private GameLogic Logic;    // Class to handle the game logic
        public FieldState[,] BoardState;  // The current state of the board

        public GameBoard()
        {
            InitializeComponent();
            InitGame();
        }

        public void InitGame()
        {
            Width = WindowSize; // Set width of form
            Height = WindowSize;    // Set height of form

            MainPanel.Paint += OnDraw;  // Add a Paint Event Handler
            MainPanel.MouseDown += OnMousePress;    // Add a Mouse Event Handler

            FieldWidth = (MainPanel.Width - BordMargin * 2) / BoardSize;    // Set width of the grid fields
            FieldHeight = (MainPanel.Height - BordMargin * 2) / BoardSize;  // Set height of the grid fields

            XOSize = FieldWidth / 2;    // Set size of Xs and Os to half the width of a field

            BoardState = new FieldState[BoardSize, BoardSize];    // Initialize two dimensional grid array
            for (int c = 0; c < BoardSize; c++) for (int r = 0; r < BoardSize; r++)
            {
                BoardState[c, r] = FieldState.EMPTY; // Set default value to empty state
            }

            Logic = new GameLogic(this);  // Initialize game logic
            Logic.CreateNewPlayer(PlayerType.HUMAN); // Assign player 1 to X
            Logic.CreateNewPlayer(PlayerType.ROBOT); // Assign bot to O

            Logic.StartGame();  // Starts the game
        }

        private void OnDraw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawBoardState(g);  // Draw the grid and all the placed Xs and Os 
        }

        private void OnMousePress(object sender, MouseEventArgs e)
        {
            if(e.X > BordMargin && e.X < MainPanel.Width - BordMargin
                && e.Y > BordMargin && e.Y < MainPanel.Height - BordMargin) // Clicked on board
            {
                int column = (e.X - BordMargin) / FieldWidth;   // Calculate column from X coordinate
                int row = (e.Y - BordMargin) / FieldHeight; // Calculate row from Y coordinate

                if(ClickHandler != null) ClickHandler.ClickedOnBoard(column, row);  // Pass click event onto current handler
                
            }
        }

        public void SetField(FieldState state, int column, int row)
        {
            BoardState[column, row] = state; // Check to see if grid is empty before placing
        }

        public FieldState GetField(int column, int row)
        {
            return BoardState[column, row]; // Check to see if grid is empty before placing
        }

        public void SetClickHandler(BoardClickHandler handler)
        {
            this.ClickHandler = handler;
        }

        public int GetBoardSize()
        {
            return BoardSize;
        }

        public int MaxBoardIndex()
        {
            return BoardSize - 1;
        }

        /*
         * Iterates through every field to check if any of them are empty
         */
        public bool IsFull()
        {
            for (int column = 0; column < BoardState.Length / BoardSize; column++) //Divide by 3 as .Length calculates the 3 x 3 of the array as 9
            {
                for (int row = 0; row < BoardState.Length / BoardSize; row++)
                {
                    if (BoardState[column, row] == FieldState.EMPTY) return false;
                }
            }
            return true;
        }

        private void DrawBoardState(Graphics g)
        {
            DrawGrid(g, Color.Black);

            for (int c = 0; c < BoardSize; c++)
            {
                for (int r = 0; r < BoardSize; r++)
                {
                    if(BoardState[c, r] == FieldState.O)    
                    {
                        DrawO(g, c, r);
                    }
                    else if (BoardState[c, r] == FieldState.X)
                    {
                        DrawX(g, c, r);
                    }
                }
            }
        }

        private void DrawGrid(Graphics g, Color color)
        {
            using (Pen pen = new Pen(color))
            {
                pen.Width = LineThickness;

                for (int row = 1; row < BoardSize; row++)    // Draw all horizontal lines
                {
                    g.DrawLine(pen, new Point(BordMargin, BordMargin + FieldHeight * row),
                        new Point(this.MainPanel.Width - BordMargin, BordMargin + FieldHeight * row));
                }

                for (int column = 1; column < BoardSize; column++)    // Draw all vertical lines
                {
                    g.DrawLine(pen, new Point(BordMargin + FieldWidth * column, BordMargin),
                        new Point(BordMargin + FieldWidth * column, this.MainPanel.Height - BordMargin));
                }

                pen.Dispose();
            }
        }

        private void DrawX(Graphics g, int column, int row)
        {
            DrawX(g, Color.DarkBlue, 
                new Point(BordMargin + (column + 1) * FieldWidth - FieldWidth / 2, BordMargin + (row + 1) * FieldHeight - FieldWidth / 2), XOSize);
        }

        private void DrawX(Graphics g, Color color, Point p, int size)
        {
            using (Pen pen = new Pen(color))
            {
                pen.Width = LineThickness;

                g.DrawLine(pen, new Point(p.X - size / 2, p.Y - size / 2),
                    new Point(p.X + size / 2, p.Y + size / 2)); // Top-left to Bottom-right
                g.DrawLine(pen, new Point(p.X - size / 2, p.Y + size / 2),
                    new Point(p.X + size / 2, p.Y - size / 2)); // Bottom-left to Top-right

                pen.Dispose();
            }
        }

        private void DrawO(Graphics g, int column, int row)
        {
            DrawO(g, Color.DarkRed,
                new Point(BordMargin + (column + 1) * FieldWidth - FieldWidth / 2, BordMargin + (row + 1) * FieldHeight - FieldWidth / 2), XOSize);
        }

        private void DrawO(Graphics g, Color color, Point p, int size)
        {
            using (Pen pen = new Pen(color))
            {
                pen.Width = LineThickness;

                g.DrawEllipse(pen, new Rectangle(p.X - size / 2, p.Y - size / 2, size, size));  // Draw circle

                pen.Dispose();
            }
        }
    }

    [Flags]
    public enum FieldState
    {
        EMPTY = -1,
        O = 0,
        X   = 1,
    }

    public enum EndState
    {
        XWin = -1,
        Draw = 0,
        OWin = 1,
        Other = 2,
    }

}
