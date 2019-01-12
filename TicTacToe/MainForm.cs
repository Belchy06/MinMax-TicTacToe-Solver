using System.Drawing;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class MainForm : Form
    {
        public int Rows = 3;    // Number of rows in the grid
        public int Columns = 3; // Number f columns in the grid
        public float LineThickness = 10F;   // Line thickness in pixels
        public int BordMargin = 30; // Top, right, bottom and left margin of the grid
        public int FieldWidth;  // Width of a single field in the grid
        public int FieldHeight; // Height of a single field in the grid

        public MainForm()
        {
            InitializeComponent();
            MainPanel.Paint += OnDraw;  // Add a Paint Event Handler

            FieldWidth = (MainPanel.Width - BordMargin * 2) / Columns;    // Set width of the grid fields
            FieldHeight = (MainPanel.Height - BordMargin * 2) / Rows;  // Set height of the grid fields
        }

        private void OnDraw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawGrid(g, Color.Black);

            DrawX(g, 0, 0);
            DrawX(g, 1, 1);
            DrawX(g, 2, 2);

            DrawO(g, 2, 0);
            DrawO(g, 2, 1);
            DrawO(g, 1, 2);
        }

        private void DrawGrid(Graphics g, Color color)
        {
            using (Pen pen = new Pen(color))
            {
                pen.Width = LineThickness;

                for (int row = 1; row < Rows; row++)    // Draw all horizontal lines
                {
                    g.DrawLine(pen, new Point(BordMargin, BordMargin + FieldHeight * row),
                        new Point(this.MainPanel.Width - BordMargin, BordMargin + FieldHeight * row));
                }

                for (int column = 1; column < Columns; column++)    // Draw all vertical lines
                {
                    g.DrawLine(pen, new Point(BordMargin + FieldWidth * column, BordMargin),
                        new Point(BordMargin + FieldWidth * column, this.MainPanel.Height - BordMargin));
                }

                pen.Dispose();
            }
        }

        public void DrawX(Graphics g, int column, int row)
        {
            DrawX(g, Color.DarkBlue, new Point(BordMargin + (column + 1) * FieldWidth - FieldWidth / 2, BordMargin + (row + 1) * FieldHeight - FieldWidth / 2), 70);
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

        public void DrawO(Graphics g, int column, int row)
        {
            DrawO(g, Color.DarkRed, new Point(BordMargin + (column + 1) * FieldWidth - FieldWidth / 2, BordMargin + (row + 1) * FieldHeight - FieldWidth / 2), 70);
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
}
