using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe_Refactored_Ver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        stGameStatus GameStatus;
        enPlayer PlayerTurn = enPlayer.Player1;

        enum enPlayer
        {
            Player1,
            Player2
        }

        enum enWinner
        {
            Player1,
            Player2,
            Draw,
            GameInProgress
        }

        struct stGameStatus
        {
            public enWinner Winner;
            public bool GameOver;
            public short PlayCount;

        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color white = Color.FromArgb(255, 255, 255, 255);
            Pen whitePen = new Pen(white);
            whitePen.Width = 15;
            //whitePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            whitePen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            whitePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            //draw Horizental lines
            e.Graphics.DrawLine(whitePen, 400, 300, 1050, 300);
            e.Graphics.DrawLine(whitePen, 400, 460, 1050, 460);

            //draw Vertical lines
            e.Graphics.DrawLine(whitePen, 610, 140, 610, 620);
            e.Graphics.DrawLine(whitePen, 840, 140, 840, 620);
        }


        bool CheckWinnerOrNot(Button btn1 , Button btn2 , Button btn3)
        {
            // By default the tag is '?'
            if(btn1.Tag.ToString()=="?" && btn2.Tag.ToString() == "?" && btn3.Tag.ToString() == "?")
            {
                return false; 
            }
            else if(btn1.Tag.ToString() == btn2.Tag.ToString()&& btn2.Tag.ToString() == btn3.Tag.ToString())
            {
                return true; 
            }
            return false;
        }


        bool DetermineTheWinnerFromSelectedBtns() // check the winner after each player select a button
        {
            if (CheckWinnerOrNot(button1, button4, button7)) //1
                return true;

            if (CheckWinnerOrNot(button2, button5, button8))//2
                return true;

            if (CheckWinnerOrNot(button3, button6, button9))//3
                return true;

            if (CheckWinnerOrNot(button1, button2, button3))//4
                return true;

            if (CheckWinnerOrNot(button4, button5, button6))//5
                return true;

            if (CheckWinnerOrNot(button7, button8, button9))//6
                return true;

            if (CheckWinnerOrNot(button1, button5, button9))//7
                return true;

            if (CheckWinnerOrNot(button3, button5, button7))
                return true ;

            return false;
        }

        void DisableAllBtns()
        {
            Button[] btns = {button1,button2,button3,button4,button5,button6,button7,button8,button9 };

            foreach (var item in btns)
            {
                item.Tag = "?";
                item.Enabled= false;    
            }
        }

        void EndGame(enWinner Winner)
        {
            switch (Winner)
            {
                case enWinner.Player1:
                    {
                        MessageBox.Show("Player 1 is the winner","The Winner!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        DisableAllBtns();
                        lblTurn.Text = "Game Over";
                        lblTurn.ForeColor = Color.Red;
                        lblWinner.Text = "Player 1";
                        break;
                    }

                case enWinner.Player2:
                    {
                        MessageBox.Show("Player 2 is the winner", "The Winner!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        DisableAllBtns();
                        lblTurn.Text = "Game Over";
                        lblTurn.ForeColor = Color.Red;
                        lblWinner.Text = "Player 2";
                        break;
                    }

                case enWinner.Draw:
                    {
                        MessageBox.Show("Game is Draw","RES",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        DisableAllBtns();
                        lblTurn.Text = "Game Over";
                        lblTurn.ForeColor = Color.Yellow;
                        lblWinner.Text = "Draw";
                        break;
                    }
            }

        }

        void ChangeBackColors(Button btn)
        {
            btn.BackColor = Color.Yellow;
        }

      
           
          void ChangeImage(Button btn)
            {

            ChangeBackColors(btn);

            if (btn.Tag.ToString() == "?")
            {
                GameStatus.PlayCount++;
                switch (PlayerTurn)
                {
                    // Game will start with player 1  --> x
                    case enPlayer.Player1:
                        {
                            lblTurn.Text = enPlayer.Player1.ToString(); // change the current player label --> player1
                            btn.Tag = "X";
                            btn.Image = Properties.Resources.X;
                            PlayerTurn = enPlayer.Player2; // change the player turn to player 2 -- > for the next turn

                            bool res = DetermineTheWinnerFromSelectedBtns();

                            if (res)
                            {
                                GameStatus.Winner = enWinner.Player1;
                                GameStatus.PlayCount = 0;
                                EndGame(GameStatus.Winner);
                                
                            }
                           
                            // Draw(); // check if the game is draw or not after each player select a button
                            break;
                        }

                    case enPlayer.Player2:
                        {
                            lblTurn.Text = enPlayer.Player2.ToString(); // change the current player label --> player2
                            btn.Tag = "O";
                            btn.Image = Properties.Resources.O;
                            PlayerTurn = enPlayer.Player1; // change the player turn to player 1 -- > for the next turn
                            bool res = DetermineTheWinnerFromSelectedBtns();
                            
                            if (res)
                            {
                                GameStatus.Winner = enWinner.Player2;
                                GameStatus.PlayCount = 0; 
                                EndGame(GameStatus.Winner);
                                
                            }

                           // GameStatus.PlayCount++;
                            //Draw(); // check if the game is draw or not after each player select a button
                            break;


                        }

                }
            }

            else
            {
               
                MessageBox.Show("You Cannot Choose The Same Button Again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (GameStatus.PlayCount == 9 )
            {
                GameStatus.Winner = enWinner.Draw;
                EndGame(enWinner.Draw);
                return;
            }

        }
        

   
 

        private void button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            ChangeImage(btn);
            
        }


        void RestToDefault()
        {
            Button[] btns = { button1, button2, button3, button4, button5, button6, button7, button8, button9 };

            foreach (var item in btns)
            {
                item.Tag = "?";
                item.Enabled = true;
                item.BackColor = Color.Transparent;
                item.Image= Properties.Resources.question_mark_96;
            }
            lblTurn.Text = "Player 1";
            lblWinner.Text = "In Progress";
            lblWinner.ForeColor = Color.Green;
        }
        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestToDefault();
            PlayerTurn = enPlayer.Player1;
            GameStatus.PlayCount = 0;

        }



    }
}
