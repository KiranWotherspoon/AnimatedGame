using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace AnimatedGame
{
    public partial class GameScreen : UserControl
    {
        #region Globals
        int rowWidth, rowHeight, playerSpeed = 3, ballSpeed = 5;

        List<Area> outAreas = new List<Area>();
        List<Area> gameAreas = new List<Area>();
        List<Ball> obstacles = new List<Ball>();

        Ball player;

        SolidBrush drawBrush = new SolidBrush(Color.Thistle);


        bool rightArrowDown, leftArrowDown, upArrowDown, downArrowDown;
        #endregion

        public GameScreen()
        {
            InitializeComponent();

            //create all the stuff on the screen
            rowWidth = this.Width / 20;
            rowHeight = this.Height / 13;
            CreateRectangles();
        }

        private void CreateRectangles ()
        {
            //create all out areas, game areas, balls, and the player
            Area rec1 = new Area(0, 0, rowWidth * 2, this.Height); outAreas.Add(rec1); 
            Area rec2 = new Area(rowWidth * 2, 0, rowWidth * 2, rowHeight * 4); outAreas.Add(rec2);
            Area rec3 = new Area(rowWidth * 2, rowHeight * 7, rowWidth * 2, rowHeight * 4); outAreas.Add(rec3);
            Area rec4 = new Area(rowWidth * 4, 0, rowWidth, rowHeight * 5); outAreas.Add(rec4);
            Area rec5 = new Area(rowWidth * 4, rowHeight * 6, rowWidth, rowHeight * 5); outAreas.Add(rec5);
            Area rec6 = new Area(rowWidth * 5, 0, rowWidth * 10, rowHeight * 2); outAreas.Add(rec6);
            Area rec7 = new Area(rowWidth * 5, rowHeight * 9, rowWidth * 10, rowHeight * 2); outAreas.Add(rec7);
            Area rec8 = new Area(rowWidth * 15, 0, rowWidth, rowHeight * 5); outAreas.Add(rec8);
            Area rec9 = new Area(rowWidth * 15, rowHeight * 6, rowWidth, rowHeight * 5); outAreas.Add(rec9);
            Area rec10 = new Area(rowWidth * 16, 0, rowWidth * 2, rowHeight * 4); outAreas.Add(rec10);
            Area rec11 = new Area(rowWidth * 16, rowHeight * 7, rowWidth * 2, rowHeight * 4); outAreas.Add(rec11);
            Area rec12 = new Area(rowWidth * 18, 0, rowWidth * 2, this.Height); outAreas.Add(rec12);

            Area rec13 = new Area(rowWidth * 2, rowHeight * 4, rowWidth * 2, rowHeight * 3); gameAreas.Add(rec13);
            Area rec14 = new Area(rowWidth * 4, rowHeight * 5, rowWidth, rowHeight); gameAreas.Add(rec14);
            Area rec15 = new Area(rowWidth * 5, rowHeight * 2, rowWidth * 10, rowHeight * 7); gameAreas.Add(rec15);
            Area rec16 = new Area(rowWidth * 15, rowHeight * 5, rowWidth, rowHeight); gameAreas.Add(rec16);
            Area rec17 = new Area(rowWidth * 16, rowHeight * 4, rowWidth * 2, rowHeight * 3); gameAreas.Add(rec17);

            Ball ball1 = new Ball(rowWidth * 5, rowHeight * 2, rowWidth/2, ballSpeed); obstacles.Add(ball1);
            Ball ball2 = new Ball(rowWidth * 7, rowHeight * 2, rowWidth/2, ballSpeed); obstacles.Add(ball2);
            Ball ball3 = new Ball(rowWidth * 9, rowHeight * 2, rowWidth/2, ballSpeed); obstacles.Add(ball3);
            Ball ball4 = new Ball(rowWidth * 11, rowHeight * 2, rowWidth/2, ballSpeed); obstacles.Add(ball4);
            Ball ball5 = new Ball(rowWidth * 13, rowHeight * 2, rowWidth/2, ballSpeed); obstacles.Add(ball5);
            Ball ball6 = new Ball(rowWidth * 6, rowHeight * 9 - rowWidth / 2, rowWidth/2, -ballSpeed); obstacles.Add(ball6);
            Ball ball7 = new Ball(rowWidth * 8, rowHeight * 9 - rowWidth / 2, rowWidth/2, -ballSpeed); obstacles.Add(ball7);
            Ball ball8 = new Ball(rowWidth * 10, rowHeight * 9 - rowWidth / 2, rowWidth/2, -ballSpeed); obstacles.Add(ball8);
            Ball ball9 = new Ball(rowWidth * 12, rowHeight * 9- rowWidth / 2, rowWidth/2, -ballSpeed); obstacles.Add(ball9);
            Ball ball10 = new Ball(rowWidth * 14, rowHeight * 9- rowWidth / 2, rowWidth/2, -ballSpeed); obstacles.Add(ball10);

            player = new Ball(rowWidth * 3, rowHeight * 5, rowWidth / 2, playerSpeed);
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //pause screen function
            if (e.KeyCode == Keys.Escape && gameTimer.Enabled)
            {
                gameTimer.Enabled = false;

                rightArrowDown = leftArrowDown = upArrowDown = downArrowDown = false;

                DialogResult result = PauseForm.Show();

                if (result == DialogResult.Cancel)
                {
                    gameTimer.Enabled = true;
                }
                else if (result == DialogResult.Abort)
                {
                    Form1.ChangeScreen(this);
                }

            }

            //change the direction of the player
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //create a temporary location where the player currently is
            int tempX = player.x, tempY = player.y;

            //move the player in the direction(s) they are supposed to be moving
            if (rightArrowDown) { player.Move("right"); }
            if (leftArrowDown) { player.Move("left"); }
            if (upArrowDown) { player.Move("up"); }
            if (downArrowDown) { player.Move("down"); }

            //check to see if the player is colliding with any of the balls and if they are play game over method
            foreach (Ball b in obstacles)
            {
                if (player.Collision(b)) { GameOver(); }
            }

            //move all the balls and if they are hitting a wall reverse their direction
            foreach (Ball b in obstacles)
            {
                b.Move("down");
                if (b.Collision(outAreas[5]) || b.Collision(outAreas[6]))
                {
                    b.speed = -b.speed;
                }
            }

            //check to see if the player is running into the out areas, if they are reset them to the temporary location
            foreach (Area a in outAreas)
            {
                if (a.Collision(player))
                {
                    player.y = tempY;
                    player.x = tempX;
                }
            }

            //if the player has completed the level move them to the next level
            if (gameAreas[4].Collision(player) && gameAreas[3].Collision(player) == false) { NextLevel(); }

            //check to see if the player has run out of lives
            if (Form1.lives <= 0) { RealGameOver(); }

            //call the paint method
            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //draw the out of bounds, the start and end zones, the balls, and the player
            foreach (Area r in outAreas)
            {
                drawBrush.Color = Color.Thistle;
                e.Graphics.FillRectangle(drawBrush, r.x, r.y, r.width, r.height);
            }

            drawBrush.Color = Color.LimeGreen;
            e.Graphics.FillRectangle(drawBrush, gameAreas[0].x, gameAreas[0].y, gameAreas[0].width, gameAreas[0].height);
            e.Graphics.FillRectangle(drawBrush, gameAreas[4].x, gameAreas[4].y, gameAreas[4].width, gameAreas[4].height);

            foreach (Ball b in obstacles)
            {
                drawBrush.Color = Color.Blue;
                e.Graphics.FillEllipse(drawBrush, b.x, b.y, b.size, b.size);
            }

            drawBrush.Color = Color.Crimson;
            e.Graphics.FillRectangle(drawBrush, player.x, player.y, player.size, player.size);

            drawBrush.Color = Color.Red;
            for (int i = 0; i < Form1.lives; i++)
            {
                e.Graphics.FillEllipse(drawBrush, rowWidth / 4 + (i * rowHeight / 2), rowHeight / 4, rowWidth / 2, rowHeight / 2);
            }
        }

        private void GameOver()
        {
            //return player to starting position and subtract a life
            gameTimer.Enabled = false;
            Form1.lives--;
            player.x = rowWidth * 3;
            player.y = rowHeight * 5;
            Thread.Sleep(500);
            gameTimer.Enabled = true;
        }

        private void NextLevel()
        {
            //move the player to the next level
            gameTimer.Enabled = false;
            Form f = this.FindForm();
            f.Controls.Remove(this);
            this.Dispose();
            
            GameScreen2 gs2 = new GameScreen2();
            f.Controls.Add(gs2);
            gs2.Focus();
        }

        private void RealGameOver ()
        {
            Form1.lives = 5;

            gameTimer.Enabled = false;
            Form f = this.FindForm();
            f.Controls.Remove(this);
            this.Dispose();

            MainMenu mm = new MainMenu();
            f.Controls.Add(mm);

        }
        
    }
}
