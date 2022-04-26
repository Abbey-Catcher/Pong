using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong
{
    public partial class Pong : Form
    {

        Rectangle player1 = new Rectangle(10, 130, 10, 60);
        Rectangle player2 = new Rectangle(10, 200, 10, 60);
        Rectangle ball = new Rectangle(295, 195, 10, 10);

        int player1Score = 0;
        int player2Score = 0;
        int playerTurn = 1;

        int playerSpeed = 4;
        int ballXSpeed = 8;
        int ballYSpeed = 8;

        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool rightArrowDown = false;
        bool leftArrowDown = false;

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        Pen whitePen = new Pen(Color.White, 3);

        public Pong()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move ball 
            ball.X += ballXSpeed;
            ball.Y += ballYSpeed;

            //move player 1 
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
            }

            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
            }

            if (aDown == true && player1.X > 0)
            {
                player1.X -= playerSpeed;
            }

            if (dDown == true && player1.X < this.Width - player1.Width)
            {
                player1.X += playerSpeed;
            }

            //move player 2 
            if (upArrowDown == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
            }

            if (downArrowDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += playerSpeed;
            }

            if (leftArrowDown == true && player2.X > 0)
            {
                player2.X -= playerSpeed;
            }

            if (rightArrowDown == true && player2.X < this.Width - player2.Width)
            {
                player2.X += playerSpeed;
            }

            //check if ball hit top or bottom wall and change direction if it does 
            if (ball.Y < 0 || ball.Y > this.Height - ball.Height)
            {
                ballYSpeed *= -1;  // or: ballYSpeed  =  -ballYSpeed; 
            }

            //checks if ball hit right wall
            if (ball.X > this.Width - ball.Width)
            {
                ballXSpeed *= -1;
            }

            //Check if ball hits either player
            //If it does change the direction and place the ball in front of the player hit 

            if (ball.X <= 0)
            {
                //if ball hits left wall, gives point to other player
                if (playerTurn == 1)
                {
                    player2Score++;
                    p2ScoreLabel.Text = $"{player2Score}";
                }
                else
                {
                    player1Score++;
                    p1ScoreLabel.Text = $"{player1Score}";
                }

                //resets positions
                ball.X = 295;
                ball.Y = 195;

                player1.Y = 130;
                player2.Y = 200;
                player1.X = 10;
                player2.X = 10;
            }

            //ball intersects with player, switches player turn and changes ball direction
            //playerTurn == 1 - makes player 2 not be able to hit ball
            if (player1.IntersectsWith(ball) && playerTurn == 1)
            {
                ballXSpeed *= -1;
                ball.X = player1.X + ball.Width;
                playerTurn = 2;
            }
            //playerTurn == 2 - makes player 1 not be able to hit ball
            else if (player2.IntersectsWith(ball) && playerTurn == 2)
            {
                ballXSpeed *= -1;
                ball.X = player2.X + ball.Width;
                playerTurn = 1;
            }

            // check score and stop game if either player is at 3 
            if (player1Score == 3)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 1 Wins!!";
            }
            else if (player2Score == 3)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 2 Wins!!";
            }

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(redBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, ball);

            if (playerTurn == 1)
            {
                e.Graphics.DrawRectangle(whitePen, player1);
            }

            if (playerTurn == 2)
            {
                e.Graphics.DrawRectangle(whitePen, player2);
            }
        }
    }
}
