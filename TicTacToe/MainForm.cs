using System.Drawing;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class MainForm : Form
    {
        public int Rows = 3;
        public int Columns = 3;
        public float LineThickness = 10F;
        public int BordMargin = 30;
        public int FieldWidth;
        public int FieldHeight;

        public MainForm()
        {
            InitializeComponent();
            MainPanel.Paint += OnDraw;  // Add a Paint Event Handler

            FieldWidth = (MainPanel.Width - BordMargin * 2) / Columns;    // Set width of the grid fields
            FieldHeight = (MainPanel.Height - BordMargin * 2) / Rows;  // Set height of the grid fields
        }

        protected void OnDraw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawGrid(g, Color.Black);
            DrawX(g, Color.Black, 100, 100, 50);
            DrawO(g, Color.Black, 100, 100, 50);
        }

        public void DrawLine(Graphics g, Pen pen, int x1, int y1, int x2, int y2)
        {
            g.DrawLine(pen, new Point(x1, y1), new Point(x2, y2));
        }

        public void DrawGrid(Graphics g, Color color)
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

        public void DrawX(Graphics g, Color color, int x, int y, int size)
        {
            using (Pen pen = new Pen(color))
            {
                pen.Width = LineThickness;

                g.DrawLine(pen, new Point(x - size / 2, y - size / 2),
                    new Point(x + size / 2, y + size / 2)); // Top-left to Bottom-right
                g.DrawLine(pen, new Point(x - size / 2, y + size / 2),
                    new Point(x + size / 2, y - size / 2)); // Bottom-left to Top-right

                pen.Dispose();
            }
        }

        public void DrawO(Graphics g, Color color, int x, int y, int size)
        {
            using (Pen pen = new Pen(color))
            {
                pen.Width = LineThickness;

                g.DrawEllipse(pen, new Rectangle(x - size / 2, y - size / 2, size, size));  // Draw circle

                pen.Dispose();
            }
        }

    }
}
