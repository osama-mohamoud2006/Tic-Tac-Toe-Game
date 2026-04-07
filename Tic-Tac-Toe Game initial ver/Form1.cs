using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tic_Tac_Toe2.Properties;

namespace Tic_Tac_Toe2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DefaultPictureBoxSettings();
        }

        void DefaultPictureBoxSettings()
        {
            // Set default properties for all picture boxes
            PictureBox[] pictureBoxes =  { pictureBox1, pictureBox2, pictureBox3, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10 };
            foreach (var pb in pictureBoxes)
            {

                pb.Image = Resources.Question;

                pb.BackColor = Color.Red; 
            }
        }

        // Call the position update whenever the form loads
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            PositionPictureBoxes();
        }

        // Keep the grid responsive on form resize
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            PositionPictureBoxes();
            this.Invalidate(); // Ask the form to redraw the lines
        }

        private void PositionPictureBoxes()
        {
            // Calculate the board layout using the logic from your Paint event
            int boardSize = Math.Min(this.ClientSize.Width / 2, this.ClientSize.Height - 40);
            int startX = this.ClientSize.Width - boardSize - 20;
            int startY = (this.ClientSize.Height - boardSize) / 2;
            int thirdSize = boardSize / 3;

            // Define a margin so the picture box fits neatly within the drawn lines
            int margin = 10;
            int pbSize = thirdSize - (margin * 2);

            // Create a 2D array representation of all 9 picture boxes based on the designer components
            PictureBox[,] pbs = new PictureBox[,] {
                { pictureBox2, pictureBox9, pictureBox10 },
                { pictureBox1, pictureBox8, pictureBox7 },
                { pictureBox3, pictureBox5, pictureBox6 }
            };

            // Programmatically assign locations and sizes
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (pbs[row, col] != null)
                    {
                        pbs[row, col].Size = new Size(pbSize, pbSize);
                        pbs[row, col].Location = new Point(
                            startX + (col * thirdSize) + margin, 
                            startY + (row * thirdSize) + margin);
                    }
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.White, 10))
            {
                // Define a suitable square size for the board (half the width or the full height, minus padding)
                int boardSize = Math.Min(this.ClientSize.Width / 2, this.ClientSize.Height - 40);
                
                // Position it on the right side with a 20 pixel margin, centered vertically
                int startX = this.ClientSize.Width - boardSize - 20;
                int startY = (this.ClientSize.Height - boardSize) / 2;
                
                int thirdSize = boardSize / 3;

                // Vertical lines
                e.Graphics.DrawLine(pen, startX + thirdSize, startY, startX + thirdSize, startY + boardSize);
                e.Graphics.DrawLine(pen, startX + thirdSize * 2, startY, startX + thirdSize * 2, startY + boardSize);

                // Horizontal lines
                e.Graphics.DrawLine(pen, startX, startY + thirdSize, startX + boardSize, startY + thirdSize);
                e.Graphics.DrawLine(pen, startX, startY + thirdSize * 2, startX + boardSize, startY + thirdSize * 2);
            }
        }

        private void pictureBoxes_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            pb.BackColor = Color.Yellow;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            grbMainGame.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DefaultPictureBoxSettings();
        }
    }
}
