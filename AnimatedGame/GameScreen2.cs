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
    public partial class GameScreen2 : UserControl
    {
        #region Globals
        int rowHeight, rowWidth, ballSpeed = 5;
        SolidBrush drawBrush = new SolidBrush(Color.Thistle);

        List<Area> outAreas = new List<Area>();
        List<Area> gameAreas = new List<Area>();
        List<Ball> obstacles = new List<Ball>();

        Ball player;

        bool rightArrowDown, leftArrowDown, upArrowDown, downArrowDown;
        #endregion

        public GameScreen2()
        {
            InitializeComponent();

            rowWidth = this.Width / 20;
            rowHeight = this.Height / 13;
            CreateRectangles();
        }

        private void CreateRectangles ()
        {
            //create all out of bounds and game areas, balls, and the player 
            Area a1 = new Area(0, 0, rowWidth * 2, this.Height); outAreas.Add(a1);
            Area a2 = new Area(rowWidth * 2, 0, rowWidth * 3, rowHeight * 2); outAreas.Add(a2);
            Area a3 = new Area(rowWidth * 2, rowHeight * 9, rowWidth * 5, rowHeight * 2); outAreas.Add(a3);
            Area a4 = new Area(rowWidth * 5, 0, rowWidth, rowHeight * 8); outAreas.Add(a4);
            Area a5 = new Area(rowWidth * 6, 0, rowWidth * 7, rowHeight * 3); outAreas.Add(a5);
            Area a6 = new Area(rowWidth * 7, rowHeight * 8, rowWidth * 7, rowHeight * 3); outAreas.Add(a6);
            Area a7 = new Area(rowWidth * 13, 0, rowWidth * 5, rowHeight * 2); outAreas.Add(a7);
            Area a8 = new Area(rowWidth * 14, rowHeight * 3, rowWidth, rowHeight * 8); outAreas.Add(a8);
            Area a9 = new Area(rowWidth * 15, rowHeight * 9, rowWidth * 3, rowHeight * 2); outAreas.Add(a9);
            Area a10 = new Area(rowWidth * 18, 0, rowWidth * 2, this.Height); outAreas.Add(a10);

            Area a11 = new Area(rowWidth * 2, rowHeight * 2, rowWidth * 3, rowHeight * 7); gameAreas.Add(a11);
            Area a12 = new Area(rowWidth * 5, rowHeight * 8, rowWidth * 2, rowHeight * 1); gameAreas.Add(a12);
            Area a13 = new Area(rowWidth * 6, rowHeight * 3, rowWidth * 8, rowHeight * 5); gameAreas.Add(a13);
            Area a14 = new Area(rowWidth * 13, rowHeight * 2, rowWidth * 2, rowHeight * 1); gameAreas.Add(a14);
            Area a15 = new Area(rowWidth * 15, rowHeight * 2, rowWidth * 3, rowHeight * 7); gameAreas.Add(a15);

            Ball b1 = new Ball(rowWidth * 6, rowHeight * 3, rowWidth / 2, ballSpeed); obstacles.Add(b1);
            Ball b2 = new Ball(rowWidth * 6, rowHeight * 5, rowWidth / 2, ballSpeed); obstacles.Add(b2);
            Ball b3 = new Ball(rowWidth * 6, rowHeight * 7, rowWidth / 2, ballSpeed); obstacles.Add(b3);
            Ball b4 = new Ball(rowWidth * 14 - rowWidth / 2, rowHeight * 4, rowWidth / 2, -ballSpeed); obstacles.Add(b4);
            Ball b5 = new Ball(rowWidth * 14 - rowWidth / 2, rowHeight * 6 , rowWidth / 2, -ballSpeed); obstacles.Add(b5);

            player = new Ball(rowWidth * 3, rowHeight * 5, rowWidth / 2, 3);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //create a temorary location at the players location
            int tempX = player.x, tempY = player.y;
            
            //move the player in the direction(s) they are moving
            if (rightArrowDown) { player.Move("right"); }
            if (leftArrowDown) { player.Move("left"); }
            if (upArrowDown) { player.Move("up"); }
            if (downArrowDown) { player.Move("down"); }

            //check to see if the player is colliding with any balls
            foreach (Ball b in obstacles)
            {
                if (player.Collision(b)) { GameOver(); }
            }

            //move each ball, and if they are colliding with an out of bounds area reverse their direction
            foreach (Ball b in obstacles)
            {
                b.Move("right");
                if (b.Collision(outAreas[3]) || b.Collision(outAreas[7]))
                {
                    b.speed = -b.speed;
                }
            }

            //check if the player is colliding with an out of bounds area, if they are retun them to their temporary position
            foreach (Area a in outAreas)
            {
                if (a.Collision(player))
                {
                    player.y = tempY;
                    player.x = tempX;
                }
            }

            //check to see if the player has beaten the level, if they have move them to the next level
            if (player.Collision(gameAreas[4]) && player.Collision(gameAreas[3]) == false) { NextLevel(); }

            //check to see if the player has run out of lives
            if (Form1.lives <= 0) { RealGameOver(); }

            //call paint method
            Refresh();
        }

        private void GameScreen2_Paint(object sender, PaintEventArgs e)
        {
            //draw everything
            foreach (Area a in outAreas)
            {
                drawBrush.Color = Color.Thistle;
                e.Graphics.FillRectangle(drawBrush, a.x, a.y, a.width, a.height);
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

        private void GameScreen2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //pause function
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

            //change the players direction
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

        private void GameScreen2_KeyUp(object sender, KeyEventArgs e)
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

        private void GameOver()
        {
            //reset the player to the starting position
            gameTimer.Enabled = false;
            player.x = rowWidth * 3;
            player.y = rowHeight * 5;
            Form1.lives--;
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

            MainMenu mm = new MainMenu();
            f.Controls.Add(mm);
            mm.Focus();
        }

        private void RealGameOver()
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
