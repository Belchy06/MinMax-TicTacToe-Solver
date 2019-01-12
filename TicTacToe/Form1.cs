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



        public Form1()
        {
            InitializeComponent();
            setupBoard();
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
