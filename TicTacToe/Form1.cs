using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        private static Bitmap b;
        private static Graphics g;

        private int size = 900;
        private int rows = 3;
        private int cols = 3;

        public static int LineTickness = 50;

        public Form1()
        {
            InitializeComponent();
            setupBoard();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            DrawLine(g, Color.Black, 10, 10, 250, 250);
        }

        public void DrawLine(Graphics g, Color color, int x1, int y1, int x2, int y2)
        {
            using (Pen pen = new Pen(color))
            {
                pen.Width = LineThickness;
                g.DrawLine(pen, new Point(x1, y1), new Point(x2, y2));
                pen.Dispose();
            }
        }


        private void setupBoard()
        {
            if (pbBoard != null)
            {
                this.Controls.Remove(pbBoard);
                this.Refresh();
                b = null;
                pbBoard = null;
            }

            b = new Bitmap(size, size);
            g = Graphics.FromImage(b);
            g.SmoothingMode = SmoothingMode.HighQuality;

            g.Clear(this.BackColor);
            for (int i = 0; i <= rows; i++)
            {
                for (int j = 0; j <= cols; j++)
                {
                    g.DrawLine(Pens.Black, 0, i * (size / cols), cols * (size / cols), i * (size / cols));
                    g.DrawLine(Pens.Black, j * (size / rows), 0, j * (size / rows), rows * (size / rows));
                }
            }
            //Forces execution of all pending graphics operations and returns immediately without waiting for the operations to finish
            g.Flush();
            this.Refresh();

            this.Width = size + 40;
            this.Height = size + 60;

            pbBoard = new PictureBox();
            pbBoard.Name = "Main Board";
            pbBoard.Image = b;
            pbBoard.SetBounds(5, 29, size + 4, size);
            pbBoard.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(pbBoard);
            pbBoard.Location = new Point(10, 10);
        }
    }
}
